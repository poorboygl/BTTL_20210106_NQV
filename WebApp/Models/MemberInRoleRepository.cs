using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;

namespace WebApp.Models
{
    public class MemberInRoleRepository:BaseRepository
    {
        public MemberInRoleRepository(IConfiguration configuration) : base(configuration) { }
        public int Add(MemberInRole obj)
        {
            Parameter[] parameters =
            {
                new Parameter {Name= "@MemberID", Value= obj.MemberID, DbType= DbType.Guid},
                new Parameter {Name = "@RoleID", Value = obj.RoleID, DbType= DbType.Guid}
            };
            return Save("AddMemberInRole", parameters, CommandType.StoredProcedure);
        }
    }
}
