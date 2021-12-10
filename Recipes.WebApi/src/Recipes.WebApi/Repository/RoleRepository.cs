using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Extensions.Configuration;
using Recipes.WebApi.Interface;
using Recipes.WebApi.Recipes.Model.Table;
using Recipes.WebApi.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Recipes.WebApi.Repository
{
    public class RoleRepository : IRole
    {
        private readonly IConfiguration _configuration;
        public RoleRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<bool> DeleteAsync(Role course)
        {
            try
            {
                await using var connection = DBConnection.GetOpenConnection(_configuration.GetConnectionString(StringHelpers.Database.Recipes));
                _ = await connection.UpdateAsync(course);
                // Return success
                return true;
            }
            catch (Exception exc)
            {
                return false;
            }
        }

        public async Task<IEnumerable<Role>> GetAllAsync()
        {
            await using var connection = DBConnection.GetOpenConnection(_configuration.GetConnectionString(StringHelpers.Database.Recipes));
            return connection.Query<Role>(@"SELECT * FROM Role ORDER BY [RoleID] DESC").ToList();
        }

        public async Task<Role> GetByIdAsync(int roleId)
        {
            const string sql = "SELECT * FROM Role R WHERE R.RoleID = @roleId ";
            await using var connection = DBConnection.GetOpenConnection(_configuration.GetConnectionString(StringHelpers.Database.Recipes));
            return connection.QueryFirstOrDefault<Role>(sql, new { roleId });
        }

        public async Task<bool> InsertAsyn(Role role)
        {
            try
            {
                await using var connection = DBConnection.GetOpenConnection(_configuration.GetConnectionString(StringHelpers.Database.Recipes));

                _ = await connection.InsertAsync(role);
                // Return success
                return true;
            }
            catch (Exception e)
            {
                // Log error
                return false;
            }
        }

       

        public async Task<bool> IsProviderNameExists(string courseName)
        {
            await using var connection = DBConnection.GetOpenConnection(_configuration.GetConnectionString(StringHelpers.Database.Recipes));
            return connection.Query<bool>("proc_Course_IsCourseNameExists", new { CourseName = courseName }, commandType: CommandType.StoredProcedure).First();
        }

        public async Task<bool> UpdateAsync(Role course)
        {
            try
            {
                await using var connection = DBConnection.GetOpenConnection(_configuration.GetConnectionString(StringHelpers.Database.Recipes));
                _ = await connection.UpdateAsync(course);
                // Return success
                return true;
            }
            catch //(Exception exc)
            {
                return false;
            }
        }
    }
}
