using app.advertise.DataAccess.ConnectionStrings;
using app.advertise.DataAccess.Entities.Vendor;
using app.advertise.libraries.Exceptions;
using Dapper;
using Microsoft.Extensions.Logging;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace app.advertise.DataAccess.Repositories.Vendor
{
    public interface IAppliDocRepository
    {
        Task UpdateDoc(AppliDoc parameters);
        Task<AppliDoc> GetSingleDoc(AppliDoc parameters);
        Task InsertDoc(AppliDoc parameters);
    }
    public class AppliDocRepository : IAppliDocRepository
    {
        private readonly AdvertisementDbContext _context;
        private readonly ILogger<AppliDocRepository> _logger;
        public AppliDocRepository(AdvertisementDbContext context, ILogger<AppliDocRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task UpdateDoc(AppliDoc parameters)
        {
            using (var orc = _context.OracleConnection)
            {
                var cmd = new OracleCommand(Queries.Update_Appli_Doc, orc)
                {
                    BindByName = true
                };

                cmd.Parameters.Add(new OracleParameter()
                {
                    ParameterName = "BLO_APPLIDOC_IMAGE",
                    Value = parameters.BLO_APPLIDOC_IMAGE,
                    OracleDbType = OracleDbType.Blob,
                    Direction = ParameterDirection.Input
                });

                cmd.Parameters.Add(new OracleParameter()
                {
                    ParameterName = "DAT_APPLIDOC_UPDDT",
                    Value = DateTime.Now,
                    OracleDbType = OracleDbType.Date,
                    Direction = ParameterDirection.Input
                });

                cmd.Parameters.Add(new OracleParameter()
                {
                    ParameterName = "VAR_APPLIDOC_UPDBY",
                    Value = parameters.VAR_APPLIDOC_UPDBY,
                    OracleDbType = OracleDbType.Varchar2,
                    Direction = ParameterDirection.Input
                });

                cmd.Parameters.Add(new OracleParameter()
                {
                    ParameterName = "NUM_APPLIDOC_ULBID",
                    Value = parameters.NUM_APPLIDOC_ULBID,
                    OracleDbType = OracleDbType.Int32,
                    Direction = ParameterDirection.Input
                });
                cmd.Parameters.Add(new OracleParameter()
                {
                    ParameterName = "NUM_APPLIDOC_APPID",
                    Value = parameters.NUM_APPLIDOC_APPID,
                    OracleDbType = OracleDbType.Int32,
                    Direction = ParameterDirection.Input
                });
                cmd.Parameters.Add(new OracleParameter()
                {
                    ParameterName = "NUM_APPLIDOC_ID",
                    Value = parameters.NUM_APPLIDOC_ID,
                    OracleDbType = OracleDbType.Int32,
                    Direction = ParameterDirection.Input
                });

                orc.Open();
                var response = await cmd.ExecuteNonQueryAsync();

                cmd.Dispose();
                orc.Close();
                orc.Dispose();

                if (!(response > 0))
                    throw new DBException($"Unable to update document {parameters.VAR_APPLIDOC_APPLINO}.", _logger);
            }
        }

        public async Task<AppliDoc> GetSingleDoc(AppliDoc parameters)
        {
            using var connection = _context.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<AppliDoc>(Queries.Get_Doc_APPId, parameters);
        }

        public async Task InsertDoc(AppliDoc parameters)
        {
            using (var orc = _context.OracleConnection)
            {
                var cmd = new OracleCommand(Queries.Update_Appli_Doc, orc)
                {
                    BindByName = true
                };

                cmd.Parameters.Add(new OracleParameter()
                {
                    ParameterName = "NUM_APPLIDOC_ULBID",
                    Value = parameters.NUM_APPLIDOC_ULBID,
                    OracleDbType = OracleDbType.Int32,
                    Direction = ParameterDirection.Input
                });
                cmd.Parameters.Add(new OracleParameter()
                {
                    ParameterName = "NUM_APPLIDOC_APPID",
                    Value = parameters.NUM_APPLIDOC_APPID,
                    OracleDbType = OracleDbType.Int32,
                    Direction = ParameterDirection.Input
                });
                cmd.Parameters.Add(new OracleParameter()
                {
                    ParameterName = "VAR_APPLIDOC_APPLINO",
                    Value = parameters.VAR_APPLIDOC_APPLINO,
                    OracleDbType = OracleDbType.Varchar2,
                    Direction = ParameterDirection.Input
                });
                cmd.Parameters.Add(new OracleParameter()
                {
                    ParameterName = "BLO_APPLIDOC_IMAGE",
                    Value = parameters.BLO_APPLIDOC_IMAGE,
                    OracleDbType = OracleDbType.Blob,
                    Direction = ParameterDirection.Input
                });
                cmd.Parameters.Add(new OracleParameter()
                {
                    ParameterName = "VAR_APPLIDOC_INSBY",
                    Value = parameters.VAR_APPLIDOC_INSBY,
                    OracleDbType = OracleDbType.Varchar2,
                    Direction = ParameterDirection.Input
                });
                cmd.Parameters.Add(new OracleParameter()
                {
                    ParameterName = "DAT_APPLIDOC_INSDT",
                    Value = DateTime.Now,
                    OracleDbType = OracleDbType.Date,
                    Direction = ParameterDirection.Input
                });


                orc.Open();
                var response = await cmd.ExecuteNonQueryAsync();

                cmd.Dispose();
                orc.Close();
                orc.Dispose();

                if (!(response > 0))
                    throw new DBException($"Unable to insert document {parameters.VAR_APPLIDOC_APPLINO}.", _logger);
            }
        }
        }
    }

