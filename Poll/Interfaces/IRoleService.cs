using Poll.Models;

namespace Poll.Interfaces
{
    public interface IRoleService
    {
        Task<Role> GetAsync(int id);
        Task<List<Role>> GetAllAsync();

        Task<int> AddOrUpdate(Role role);
    }
}
