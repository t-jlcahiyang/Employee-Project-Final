using Microsoft.Data.SqlClient;

namespace EmployeeApplication.Data
{
    public class DataConnectionProvider
    {
        private readonly string _connectionString;

        public DataConnectionProvider(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("LocalDB") ?? throw new InvalidOperationException("DB not found");
        }
        public SqlConnection CreateConnection() => new SqlConnection(_connectionString);

    }
}
