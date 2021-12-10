using Recipes.WebApi.Recipes.Model.Recipes.Custom;
using Recipes.WebApi.Recipes.Model.Recipes.Table;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Recipes.WebApi.Interface
{
    public interface IUser
    {
        Task<bool> InsertAsyn(User user);
        Task<bool> UpdateAsync(User user);
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> GetByIdAsync(int userId);
        Task<bool> DeleteAsync(User user);
        Task<bool> IsUserNameExists(string courseName);
        Task<UserInformation> FindByUsernameAsync(string username);
    }
}
