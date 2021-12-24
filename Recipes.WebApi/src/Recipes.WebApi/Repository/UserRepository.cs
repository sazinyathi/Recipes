using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Extensions.Configuration;
using Recipes.WebApi.Interface;
using Recipes.WebApi.Recipes.Model.Recipes.Custom;
using Recipes.WebApi.Recipes.Model.Recipes.Table;
using Recipes.WebApi.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recipes.WebApi.Repository
{
    public class UserRepository : IUser
    {
        private readonly IConfiguration _configuration;
        public UserRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<bool> DeleteAsync(User user)
        {
            try
            {
                await using var connection = DBConnection.GetOpenConnection(_configuration.GetConnectionString(StringHelpers.Database.Recipes));
                _ = await connection.UpdateAsync(user);
                // Return success
                return true;
            }
            catch (Exception exc)
            {
                return false;
            }
        }

        public async Task<UserInformation> FindByUsernameAsync(string username)
        {
            const string sql = "SELECT U.UserID, U.FirstName, U.LastName, U.UserName, R.RoleName, U.Email FROM User U INNER JOIN Role R ON U.RoleID = R.RoleID WHERE U.UserName = @username ";
            await using var connection = DBConnection.GetOpenConnection(_configuration.GetConnectionString(StringHelpers.Database.Recipes));
            return connection.QueryFirstOrDefault<UserInformation>(sql, new { username });
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            await using var connection = DBConnection.GetOpenConnection(_configuration.GetConnectionString(StringHelpers.Database.Recipes));
            return connection.Query<User>(@"SELECT * FROM User WHERE DeletedOn IS NULL AND DeletedByUserID IS NULL ORDER BY [UserID] DESC").ToList();
        }

        public async Task<User> GetByIdAsync(int userId)
        {
            const string sql = "SELECT * FROM User U WHERE U.UserID = @userId ";
            await using var connection = DBConnection.GetOpenConnection(_configuration.GetConnectionString(StringHelpers.Database.Recipes));
            return connection.QueryFirstOrDefault<User>(sql, new { userId });
        }

        public async Task<bool> InsertAsyn(User user)
        {
            try
            {
                await using var connection = DBConnection.GetOpenConnection(_configuration.GetConnectionString(StringHelpers.Database.Recipes));

                _ = await connection.InsertAsync(user);
                // Return success
                return true;
            }
            catch (Exception e)
            {
                // Log error
                return false;
            }
        }

        public Task<bool> IsUserNameExists(string courseName)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAsync(User user)
        {
            try
            {
                await using var connection = DBConnection.GetOpenConnection(_configuration.GetConnectionString(StringHelpers.Database.Recipes));
                _ = await connection.UpdateAsync(user);
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
