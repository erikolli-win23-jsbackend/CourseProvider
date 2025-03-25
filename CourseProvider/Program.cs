using CourseProvider.Infrastructure.Data.Contexts;
using CourseProvider.Infrastructure.GraphQL.Mutation;
using CourseProvider.Infrastructure.GraphQL.ObjectTypes;
using CourseProvider.Infrastructure.GraphQL.Querries;
using CourseProvider.Infrastructure.Services;
using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Azure.Cosmos;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices(services =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();

        
        string cosmosUri = Environment.GetEnvironmentVariable("COSMOS_URI")!;
        string cosmosDb = Environment.GetEnvironmentVariable("COSMOS_DBNAME")!;

        if (string.IsNullOrEmpty(cosmosUri) || string.IsNullOrEmpty(cosmosDb))
        {
            throw new InvalidOperationException("COSMOS_URI or COSMOS_DBNAME is not set. Please check your environment variables.");
        }

        
        services.AddPooledDbContextFactory<DataContext>(options =>
        {
            options.UseCosmos(cosmosUri, cosmosDb)
                   .UseLazyLoadingProxies();
        });

        services.AddScoped<ICourseService, CourseService>();

        services.AddGraphQLFunction()
            .AddQueryType<CourseQuery>()
            .AddMutationType<CourseMutation>()
            .AddType<CourseType>();

    })
    .Build();


await InitializeDatabaseAsync();

host.Run();



async Task InitializeDatabaseAsync()
{
    string cosmosUri = Environment.GetEnvironmentVariable("COSMOS_URI")!;
    string cosmosDb = Environment.GetEnvironmentVariable("COSMOS_DBNAME")!;

    using var cosmosClient = new CosmosClient(cosmosUri);
    var databaseResponse = await cosmosClient.CreateDatabaseIfNotExistsAsync(cosmosDb);
    var database = databaseResponse.Database;

    
    await database.CreateContainerIfNotExistsAsync("Courses", "/id");

    using var scope = host.Services.CreateScope();
    var dbContextFactory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<DataContext>>();
    using var context = dbContextFactory.CreateDbContext();

    Console.WriteLine(" Database and container initialized successfully!");
}
