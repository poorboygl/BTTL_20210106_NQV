using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;

namespace WebApp.Models
{
    public class SiteProvider : IDisposable
    {
        IDbConnection connection;
        public SiteProvider(IConfiguration configuration)
        {
            connection = new SqlConnection(configuration.GetConnectionString("BikeStore"));
            connection.Open();
        }
        BrandRepository brand;
        MemberRepository member;
        MemberInRoleRepository memberInroleRepository;
        RoleRepository role;
        public BrandRepository Brand
        {
            get
            {
                if (brand is null)
                {
                    brand = new BrandRepository(connection);
                }
                return brand;
            }
        }

        public void Dispose()
        {
            if (connection!= null && connection.State == ConnectionState.Open)
            {
                connection.Close();
                connection.Dispose();
            }
        }
    }
}
