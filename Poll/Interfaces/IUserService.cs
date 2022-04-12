using Poll.Models;

namespace Poll.Interfaces
{
    public interface IUserService
    {
        Task<User> GetAsync(int id);
        Task<List<User>> GetAllAsync();

        Task<int> AddOrUpdate(User user);

    }
}
