using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace app.advertise.DataAccess
{
    public class AdvertisementDbContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        public AdvertisementDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("AdvertisementDbConnection");
        }
        public IDbConnection CreateConnection()
       => new OracleConnection(_connectionString);
    }
}
