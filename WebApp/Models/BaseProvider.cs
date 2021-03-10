using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public abstract class BaseProvider : IDisposable
    {

        IDbConnection connection;
        IConfiguration configuration;
        //public SiteProvider(IDbConnection connection)
        //{
        //    this.connection = connection;
        //    Console.WriteLine("Goi Ket noi");
        //}
        public BaseProvider(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public IDbConnection Connection
        {
            get
            {
                if (connection is null)
                {
                    connection = new SqlConnection(configuration.GetConnectionString("BikeStore"));
                    connection.Open();
                    Console.WriteLine("Mo Ket Noi");
                }
                return connection;
            }
        }

        public void Dispose()
        {
            if (connection != null && connection.State == ConnectionState.Open)
            {
                connection.Close();
                connection.Dispose();
                Console.WriteLine("Da Dong ket Noi");
            }
        }
    }
}
