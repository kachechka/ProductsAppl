using GraphQL.Types;
using ProductsAppl.Domain.Core.Models;

namespace GraphQL.Models.Types
{
    public class ProductType : ObjectGraphType<Product>
    {
        public ProductType()
        {
            InitializeFields();
        }

        private void InitializeFields()
        {
            Field(x => x.Id);
            Field(x => x.Name);
            Field(x => x.Description);
            Field(x => x.Price);
            Field<DecimalGraphType>(nameof(Product.SalePrice));
        }
    }
}