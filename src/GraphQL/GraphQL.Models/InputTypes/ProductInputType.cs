using GraphQL.Types;
using ProductsAppl.Domain.Core.Models;

namespace GraphQL.Models.InputTypes
{
    public class ProductInputType : InputObjectGraphType<Product>
    {
        public ProductInputType()
        {
            Name = GetType().Name[0..^4];

            Field<IntGraphType>(nameof(Product.Id));
            Field<NonNullGraphType<StringGraphType>>(nameof(Product.Name));
            Field<NonNullGraphType<StringGraphType>>(nameof(Product.Description));
            Field<NonNullGraphType<DecimalGraphType>>(nameof(Product.Price));
            Field<DecimalGraphType>(nameof(Product.SalePrice));
        }
    }
}
