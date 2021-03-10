using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class BrandRepository:BaseRepository
    {
        public BrandRepository(IConfiguration configuration):base(configuration)
        {

        }
        public BrandRepository(IDbConnection connection):base(connection)
        {

        }
        static Brand Fetch(IDataReader reader)
        {
            return new Brand
            {
                Id = (short)reader["BrandId"],
                Name = (string)reader["BrandName"]
            };
        }
        public Brand GetBrandById (int id)
        {

                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM production.Brand where BrandID = @ID";
                    Add(command, new Parameter { Name = "@Id", Value = id });
                    //IDataParameter parameter = command.CreateParameter();
                    //parameter.ParameterName = "@id";
                    //parameter.Value = id;
                    //command.Parameters.Add(parameter);
                    //connection.Open();
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return Fetch(reader);
                        }
                        return null;
                        //if (reader.Read())
                        //{
                        //    return (new Brand
                        //    {
                        //        Id = (short)reader["BrandId"],
                        //        Name = (string)reader["BrandName"]
                        //    });
                        //}
                        //return null;

                    }
                }

           
        }
        public int Save(Brand obj)
        {
            Parameter[] parameters =
            {
            new Parameter
            {
                Name = "@Name",
                Value = obj.Name

            },
            new Parameter
            {
                Name = "@id",
                Value = obj.Id

            }

        };

            string sql = "SaveBrand";
            return Save(sql, parameters, CommandType.StoredProcedure);
        }
        public List<Brand> GetBrands()
        {
         
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM production.Brand";
                    //connection.Open();
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        List<Brand> list = new List<Brand>();
                        while (reader.Read())
                        {
                            list.Add(Fetch(reader));
                            //list.Add(new Brand
                            //{
                            //    Id = (short)reader["BrandId"],
                            //    Name = (string)reader["BrandName"]
                            //});
                        }
                        return list;
                    }
                }

            
        }

    }
}
