using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using System.Data;


namespace WebApp.Models
{
    public class CategoryRepository : BaseRepository
    {
       // public IConfiguration configuration;
        public CategoryRepository(IConfiguration configuration) :base(configuration)
        {
            //this.configuration = configuration;
        }
        public int Edit(Category obj)
        {
            string sql = "Update  PRODUCTION.CATEGORY set CategoryName= @Name where CategoryID = @ID";
            Parameter[] parameters =
            {
                new Parameter {Name = "@Id", Value=obj.Id},
                new Parameter {Name="@Name", Value=obj.Name},
            };
            return Save(sql, parameters);
        }
        public int Delete(int id)
        {
            string sql = "DELETE FROM PRODUCTION.CATEGORY WHERE CATEGORYID = @ID";
            return Save(sql, new Parameter { Name = "@ID", Value = id });
        }
        public int Delete(int[] a)
        {
            using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("BikeStore")))
            {
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = $"DELETE FROM PRODUCTION.CATEGORY WHERE CATEGORYID  IN({string.Join(',',a)})";
                    connection.Open();
                    return command.ExecuteNonQuery();
                }

            }
        }
        public int Add(Category obj)
        {
            string sql = "INSERT INTO PRODUCTION.CATEGORY (CATEGORYNAME) VALUES (@NAME)";
            return Save(sql, new Parameter { Name = "@Name", Value = obj.Name });
        }
        public List<Category> GetCategories()
        {
            using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("BikeStore")))
            {
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM production.Category";
                    connection.Open();
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        List<Category> list = new List<Category>();
                        while (reader.Read())
                        {
                            list.Add(new Category
                            {
                                Id = (short)reader["CategoryId"],
                                Name = (string)reader["CategoryName"]
                            });
                        }
                        return list;
                    }
                }

            }
        }


        public Category GetCategorybyID(int id)
        {
            using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("BikeStore")))
            {
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM production.Category where categoryID = @ID";
                    IDataParameter parameter = command.CreateParameter();
                    parameter.ParameterName = "@id";
                    parameter.Value = id;
                    command.Parameters.Add(parameter);
                    connection.Open();
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        
                        if(reader.Read())
                        {
                            return (new Category
                            {
                                Id = (short)reader["CategoryId"],
                                Name = (string)reader["CategoryName"]
                            });
                        }
                        return null;
                    }
                }

            }
        }
        public int Add2(Category obj)
        {
            using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("BikeStore")))
            {
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = "INSERT INTO PRODUCTION.CATEGORY (CATEGORYNAME) VALUES (@NAME)";
                    IDataParameter parameter = command.CreateParameter();
                    parameter.ParameterName = "@Name";
                    parameter.Value = obj.Name;
                    command.Parameters.Add(parameter);
                    connection.Open();
                    return command.ExecuteNonQuery();
                }
            }
        }
        public int Edit2(Category obj)
        {
            using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("BikeStore")))
            {
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = "Update  PRODUCTION.CATEGORY set CategoryName= @Name where CategoryID = @ID";

                    IDataParameter parameter = command.CreateParameter();
                    parameter.ParameterName = "@Name";
                    parameter.Value = obj.Name;
                    command.Parameters.Add(parameter);


                    IDataParameter IDparameter = command.CreateParameter();
                    IDparameter.ParameterName = "@ID";
                    IDparameter.Value = obj.Id;
                    command.Parameters.Add(IDparameter);

                    connection.Open();
                    return command.ExecuteNonQuery();
                }
            }
        }
        public int Delete2(int id)
        {
            using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("BikeStore")))
            {
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = "DELETE FROM PRODUCTION.CATEGORY WHERE CATEGORYID = @ID";
                    IDataParameter parameter = command.CreateParameter();
                    parameter.ParameterName = "@ID";
                    parameter.Value = id;
                    command.Parameters.Add(parameter);
                    connection.Open();
                    return command.ExecuteNonQuery();



                }

            }
        }
    }
}
