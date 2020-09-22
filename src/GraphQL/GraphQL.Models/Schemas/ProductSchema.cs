using GraphQL.Models.Mutations;
using GraphQL.Models.Queries;
using GraphQL.Types;

namespace GraphQL.Models.Schemas
{
    public class ProductSchema : Schema
    {
        public ProductSchema(IDependencyResolver resolver)
            : base(resolver)
        {
            Query = resolver.Resolve<ProductQuery>();
            Mutation = resolver.Resolve<ProductMutation>();
        }
    }
}