using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class ProductRepository:BaseRepository
    {
        //SearchProducts
        public List<Product> SearchProducts(string q, int index, int size, out int total)
        {
            using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("BikeStore")))
            {
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SearchProducts";
                    command.CommandType = CommandType.StoredProcedure;
                    Parameter[] parameters =
                    {
                        new Parameter{Name="@Q", Value= $"%{q}%"},
                        new Parameter{Name="@Index", Value=index, DbType=DbType.Int32},
                        new Parameter{Name="@Size",Value=size, DbType=DbType.Int32},
                        new Parameter{Name="@Total", Direction= ParameterDirection.Output, DbType=DbType.Int32}
                    };
                    Add(command, parameters);

                    connection.Open();
                    //List<Product> list = new List<Product>();
                    //using (IDataReader reader = command.ExecuteReader())
                    //{

                    //    while (reader.Read())
                    //    {

                    //        list.Add(Fetch2(reader));
                    //    }
                    //}
                    //total = (int)parameters[3].DataParameter.Value;
                    //return list;
                    List<Product> list = FetcAll(command);
                    total = (int)parameters[3].DataParameter.Value;
                    return list;
                }

            }
        }
        public ProductRepository(IConfiguration configuration):base(configuration)
        {

        }
        public int Edit(Product obj)
        {
            Parameter[] parameters =
           {
                new Parameter{Name = "@ID", Value = obj.ID},
                new Parameter{ Name = "@Name",Value = obj.Name},
                new Parameter{Name= "@CategoryId", Value = obj.CategoryID},
                new Parameter{Name="@BrandId",Value = obj.BrandID },
                new Parameter{Name="@ModelYear", Value=obj.ModelYear},
                new Parameter{Name="@Price", Value = obj.Price}
            };
            return Save("EditProduct", parameters, CommandType.StoredProcedure);
        }
        public Product GetProductByID(int id)
        {
            using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("BikeStore")))
            {
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = "Select * from production.Product where ProductID = @Id";
                    Add(command, new Parameter { Name = "@Id", Value = id });

                    connection.Open();
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        if  (reader.Read())
                        {
                            return Fetch(reader);
                        }
                        return null;
                    }
                }

            }
        }
        public int Add(Product obj)
        {
            Parameter[] parameters =
            {
                new Parameter{ Name = "@Name",Value = obj.Name},
                new Parameter{Name= "@CategoryId", Value = obj.CategoryID},
                new Parameter{Name="@BrandId",Value = obj.BrandID },
                new Parameter{Name="@ModelYear", Value=obj.ModelYear},
                new Parameter{Name="@Price", Value = obj.Price}
            };
            return Save("AddProduct", parameters, CommandType.StoredProcedure);
        }
        static Product Fetch(IDataReader reader)
        {
            return new Product
            {
                ID = (int)reader["ProductId"],
                Name = (string)reader["ProductName"],
                BrandID = (short)reader["Brandid"],
                CategoryID = (short)reader["CategoryID"],
                ModelYear = (short)reader["ModelYear"],
                Price = (decimal)reader["Price"]
            };
        }
        static Product Fetch2(IDataReader reader)
        {
            //return new Product
            //{
            //    ID = (int)reader["ProductId"],
            //    Name = (string)reader["ProductName"],
            //    BrandID = (short)reader["Brandid"],
            //    BrandName = (string)reader["BrandName"],
            //    CategoryID = (short)reader["CategoryID"],
            //    CategoryName = (string)reader["CategoryName"],
            //    ModelYear = (short)reader["ModelYear"],
            //    Price = (decimal)reader["Price"],
            //};
            Product obj = Fetch(reader);
            obj.BrandName = (string)reader["BrandName"];
            obj.CategoryName = (string)reader["CategoryName"];
            return obj;
        }
        static List<Product> FetcAll(IDbCommand command)
        {
            List<Product> list = new List<Product>();
            using (IDataReader reader = command.ExecuteReader())
            {
                while(reader.Read())
                {
                    list.Add(Fetch2(reader));
                }
                return list;
            }
        }
        public List<Product> GetProducts(int index, int size, out int total)
        {
            using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("BikeStore")))
            {
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = "GetProducts";
                    command.CommandType = CommandType.StoredProcedure;
                    Parameter[] parameters =
                    {
                        new Parameter{Name="@Index", Value= index, DbType=DbType.Int32},
                        new Parameter{Name="@Size", Value=size, DbType=DbType.Int32},
                        new Parameter{Name="@Total", Direction=ParameterDirection.Output,DbType= DbType.Int32}
                    };
                    Add(command, parameters);

                    connection.Open();
                    //List<Product> list = new List<Product>();
                    //using (IDataReader reader = command.ExecuteReader())
                    //{

                    //    while (reader.Read())
                    //    {
                    //        //list.Add(Fetch(reader));
                    //        list.Add(Fetch2(reader));
                    //    }                     
                    //}
                    //IDataParameter parameter = (IDataParameter)command.Parameters["@Total"];
                    //total = (int)parameter.Value;
                    //return list;
                    List<Product> list = FetcAll(command);
                    IDataParameter parameter = (IDataParameter)command.Parameters["@Total"];
                    total = (int)parameter.Value;
                    return list;
                }

            }
        }
        public List<Product> GetProducts(int index, int size)
        {
            using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("BikeStore")))
            {
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = "GetProductsNotTotal";
                    command.CommandType = CommandType.StoredProcedure;
                    Parameter[] parameters =
                    {
                        new Parameter{Name="@Index", Value= index, DbType=DbType.Int32},
                        new Parameter{Name="@Size", Value=size, DbType=DbType.Int32},
                    };
                    Add(command, parameters);

                    connection.Open();
                    return FetcAll(command);
                    //List<Product> list = new List<Product>();

                    //using (IDataReader reader = command.ExecuteReader())
                    //{

                    //    while (reader.Read())
                    //    {
                    //        //list.Add(Fetch(reader));
                    //        list.Add(Fetch2(reader));
                    //    }
                    //}
                    //return list;
                }

            }
        }

    }
}
