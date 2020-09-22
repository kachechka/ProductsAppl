namespace ProductsAppl.Domain.Core.Models
{
    /// <summary>
    /// Base database model.
    /// </summary>
    public interface IDbModel
    {
        /// <summary>
        /// Gets Id of the DB model.
        /// </summary>
        /// <value>Id of the DB model.</value>
        string Id { get; }
    }
}
