using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;

namespace WebApp.Models
{
    public class SiteProvider : BaseProvider
    {
        public SiteProvider(IConfiguration configuration) :base(configuration)
        {
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
                    brand = new BrandRepository(Connection);
                }
                return brand;
            }
        }

        //public void Dispose()
        //{
        //    if (Connection!= null && Connection.State == ConnectionState.Open)
        //    {
        //        Connection.Close();
        //        Connection.Dispose();
        //    }
        //}
    }
}
