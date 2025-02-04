using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;
using UC.Core.Abstracts;

namespace UC.LADI.MAG.WEB.Services.DbContext.master
{
    public class DbSession : absBaseSession, IDisposable
    {
        public readonly string dbtype;

        public DbSession(IConfiguration configuration)
        {
            dbtype = configuration.GetSection("ConnectionStrings:master_dbtype").Value;
            if (configuration.GetConnectionString("master") != null && !string.IsNullOrEmpty(configuration.GetConnectionString("master").ToString()))
            {
                if(dbtype == "PostgreSql")
                {
                    Connection = new NpgsqlConnection(configuration.GetConnectionString("master"));
                }
                else if (dbtype == "SqlServer")
                {
                    Connection = new SqlConnection(configuration.GetConnectionString("master"));
                }

                Connection.Open();
            }
        }

        public void Dispose()
        {
            if(Connection != null && Connection.State == ConnectionState.Open)
            {
                Connection.Close();
                Connection.Dispose();
            }    
        }
    }
}
