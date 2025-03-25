using CourseProvider.Infrastructure.Data.Entities;

namespace CourseProvider.Infrastructure.GraphQL.ObjectTypes;

public class CourseType : ObjectType<CourseEntity>
{

    protected override void Configure(IObjectTypeDescriptor<CourseEntity> descriptor)
    {
        descriptor.Field(c => c.Id).Type<NonNullType<StringType>>();
        descriptor.Field(c => c.ImageUri).Type<StringType>();
        descriptor.Field(c => c.ImageHeaderUri).Type<StringType>();
        descriptor.Field(c => c.IsBestSeller).Type<NonNullType<BooleanType>>();
        descriptor.Field(c => c.IsDigital).Type<NonNullType<BooleanType>>();
        descriptor.Field(c => c.Categories).Type<ListType<StringType>>();
        descriptor.Field(c => c.Title).Type<StringType>();
        descriptor.Field(c => c.Ingress).Type<StringType>();
        descriptor.Field(c => c.StarRating).Type<NonNullType<DecimalType>>();
        descriptor.Field(c => c.Reviews).Type<StringType>();
        descriptor.Field(c => c.LikesInProcent).Type<StringType>();
        descriptor.Field(c => c.Likes).Type<StringType>();
        descriptor.Field(c => c.Hours).Type<StringType>();
        descriptor.Field(c => c.Authors).Type<ListType<AuthorType>>();
        descriptor.Field(c => c.Prices).Type<PricesType>();
        descriptor.Field(c => c.Content).Type<ContentType>();
    }
}
public class AuthorType : ObjectType<AuthorEntity>
{
    protected override void Configure(IObjectTypeDescriptor<AuthorEntity> descriptor)
    {
        descriptor.Field(c => c.FirstName).Type<StringType>();
        descriptor.Field(c => c.LastName).Type<StringType>();
        descriptor.Field(c => c.AuthorImage).Type<StringType>();
    }
}

public class PricesType : ObjectType<PricesEntity>
{
    protected override void Configure(IObjectTypeDescriptor<PricesEntity> descriptor)
    {
        descriptor.Field(c => c.Currency).Type<StringType>();
        descriptor.Field(c => c.Price).Type<DecimalType>();
        descriptor.Field(c => c.Discount).Type<DecimalType>();
    }
}

public class ContentType : ObjectType<ContentEntity>
{
    protected override void Configure(IObjectTypeDescriptor<ContentEntity> descriptor)
    {
        descriptor.Field(c => c.Description).Type<StringType>();
        descriptor.Field(c => c.Includes).Type<StringType>();
        descriptor.Field(c => c.ProgramDetails).Type<ListType<StringType>>();
    }
}

public class ProgramDetailsType : ObjectType<ProgramDetailsEntity>
{
    protected override void Configure(IObjectTypeDescriptor<ProgramDetailsEntity> descriptor)
    {
        descriptor.Field(c => c.Id).Type<IntType>();
        descriptor.Field(c => c.Title).Type<StringType>();
        descriptor.Field(c => c.Description).Type<StringType>();
    }
}



