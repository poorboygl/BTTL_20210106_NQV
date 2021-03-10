using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using Microsoft.Data.SqlClient;

namespace WebApp.Models
{
    public class RoleRepository:BaseRepository
    {
        public RoleRepository(IConfiguration configuration):base(configuration)
        {

        }
     
        public List<Role> GetRolesByMember(Guid id)
        {
            using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("BikeStore")))
            {
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = "GetRolesByMember";
                    command.CommandType = CommandType.StoredProcedure;
                    Parameter parameter = new Parameter
                    {
                        Name = "@MemberId",
                        Value = id,
                        DbType = DbType.Guid
                    };
                    Add(command, parameter);
                    connection.Open();
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        List<Role> list = new List<Role>();
                        while (reader.Read())
                        {
                            list.Add(FetchWithChecked(reader));
                        }
                        return list;
                    }
                }
            }
        }
        static Role FetchWithChecked(IDataReader reader)
        {
            return new Role
            {
                ID = (Guid)reader["RoleID"],
                Name = (string)reader["RoleName"],
                Checked = (bool)reader["Checked"]

            };
        }

        static Role Fetch(IDataReader reader)
        {
            return new Role
            {
                ID = (Guid)reader["RoleID"],
                Name = (string)reader["RoleName"],

            };
        }
        public List<Role> GetRoles()
        {
            using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("BikeStore")))
            {
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = "Select *From Role";
                    connection.Open();
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        List<Role> list = new List<Role>();
                        while (reader.Read())
                        {
                            list.Add(Fetch(reader));
                        }
                        return list;
                    }
                }
            }
        }
        public List<Role> GetRoles(Guid id)
        {
            using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("BikeStore")))
            {
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = "Select Role.* From MemberInRole Join Role on MemberInRole.roleID = Role.RoleID And MemberID = @ID AND isDeleted = 0 ";
                    Parameter parameter = new Parameter
                    {
                        Name = "@ID",
                        Value = id,
                        DbType = DbType.Guid
                    };
                    Add(command, parameter);
                    connection.Open();

                    using (IDataReader reader = command.ExecuteReader())
                    {
                        List<Role> list = new List<Role>();
                        while (reader.Read())
                        {
                            list.Add(Fetch(reader));
                        }
                        return list;
                    }
                }
            }
        }
        public int Add(Role obj)
        {
            Parameter parameter = new Parameter { Name = "@Name", Value = obj.Name };
            return Save("AddRole", parameter, CommandType.StoredProcedure);
        }
    }
}
