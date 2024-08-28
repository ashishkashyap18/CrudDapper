using CrudDapper.Models;

namespace CrudDapper.Interface
{
    public interface IProduct
    {
        Task<IEnumerable<Product>> GetProducts();
        Task<Product> FindProductById(Guid uid);
        Task<Product> Add(Product product);
        Task<bool> Update(Product product);
        Task<bool> Delete(Guid id);
    }
}
