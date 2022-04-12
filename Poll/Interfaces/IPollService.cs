using Poll.Models;

namespace Poll.Interfaces
{
    public interface IPollService
    {
        Task<Models.Poll> GetAsync(int id);
        Task<List<Models.Poll>> GetAllFromUserAsync(int userId);
        Task<List<Models.Poll>> GetAllAsync();
        Task<int> AddOrUpdate(Models.Poll poll);
    }
}
