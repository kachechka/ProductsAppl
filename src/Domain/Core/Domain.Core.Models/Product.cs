namespace ProductsAppl.Domain.Core.Models
{
    // TODO: add comments.
    public class Product : IDbModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal? SalePrice { get; set; }

        public Product()
        { }
    }
}