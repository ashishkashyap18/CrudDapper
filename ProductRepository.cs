using CrudDapper.Interface;
using CrudDapper.Models;
using Dapper;
using System.Data;

namespace CrudDapper
{
    public class ProductRepository : IProduct
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            const string sql = @"SELECT [NewId], [Name], [Price], [Qty], [Description], [CreatedOn] FROM [Products]";
            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<Product>(sql);
        }

        public async Task<Product> FindProductById(Guid id)
        {
            const string sql = @"SELECT * FROM [Products] WHERE [NewId] = @NewId";
            using var connection = _context.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<Product>(sql, new { NewId = id });
        }

        public async Task<Product> Add(Product product)
        {
            product.NewId = Guid.NewGuid();
            product.CreatedOn = DateTime.UtcNow;
            const string sql = @"
                INSERT INTO [Products] (NewId, Name, Price, Qty, Description, CreatedOn)
                VALUES (@NewId, @Name, @Price, @Qty, @Description, @CreatedOn)";
            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(sql, product);
            return product;
        }

        public async Task<bool> Delete(Guid id)
        {
            try
            {
                const string sql = @"DELETE FROM [Products] WHERE [NewId] = @NewId";
                using var connection = _context.CreateConnection();
                var rowsAffected = await connection.ExecuteAsync(sql, new { NewId = id });
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error occurred: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> Update(Product product)
        {
            const string sql = @"
                UPDATE [Products]
                SET Name = @Name, Price = @Price, Qty = @Qty, Description = @Description
                WHERE [NewId] = @NewId";
            using var connection = _context.CreateConnection();
            var rowsAffected = await connection.ExecuteAsync(sql, product);
            return rowsAffected > 0;
        }
    }
}
