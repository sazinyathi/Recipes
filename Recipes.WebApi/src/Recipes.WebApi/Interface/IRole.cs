using Recipes.WebApi.Recipes.Model;
using Recipes.WebApi.Recipes.Model.Table;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Recipes.WebApi.Interface
{
    public interface IRole
    {
        Task<bool> InsertAsyn(Role course);
        Task<bool> UpdateAsync(Role course);
        Task<IEnumerable<Role>> GetAllAsync();
        Task<Role> GetByIdAsync(int courseId);
        Task<bool> DeleteAsync(Role course);
        Task<bool> IsProviderNameExists(string courseName);
    }
}
