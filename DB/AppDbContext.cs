using Microsoft.Data.SqlClient;
using System.Data;

namespace CrudDapper
{
    public class AppDbContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connection;

        public AppDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connection = configuration.GetConnectionString("Default");
        }
        public IDbConnection CreateConnection() => new SqlConnection(_connection);
    }
}