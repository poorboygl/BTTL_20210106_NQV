using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public abstract class BaseRepository
    {
        protected IConfiguration configuration;
        public BaseRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public IDbConnection connection;
        public BaseRepository(IDbConnection connection)
        {
            this.connection = connection;
        }

        public void Add(IDbCommand command, Parameter parameter)
        {
            IDataParameter dataParameter = command.CreateParameter();
            dataParameter.ParameterName = parameter.Name;
            dataParameter.Value = parameter.Value;

            dataParameter.DbType = parameter.DbType;
            dataParameter.Direction = parameter.Direction;
            parameter.DataParameter = dataParameter;
            command.Parameters.Add(dataParameter);
        }
        public void Add(IDbCommand command, Parameter[] parameters)
        {
            foreach (Parameter item in parameters)
            {
                Add(command, item);
            }
        }
        public int Save(string sql, Parameter parameter, CommandType commandType = CommandType.Text)
        {
            using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("BikeStore")))
            {
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = sql;
                    command.CommandType = commandType;
                    Add(command, parameter);
                    connection.Open();
                    return command.ExecuteNonQuery();

                }
            }
        }
        public int Save(string sql, Parameter[] parameters, CommandType commandType = CommandType.Text)
        {
            using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("BikeStore")))
            {
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = sql;
                    command.CommandType = commandType;
                    Add(command, parameters);
                    connection.Open();
                    return command.ExecuteNonQuery();

                }
            }
        }
    }
}
