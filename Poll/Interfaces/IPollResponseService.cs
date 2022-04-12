using Poll.Models;

namespace Poll.Interfaces
{
    public interface IPollResponseService
    {
        Task<List<Models.PollRespose>> GetAllFromPollAsync(int pollId);
        Task<List<Models.PollRespose>> GetAllFromUserAsync(int pollId);
        Task<List<PollRespose>> GetAllWithoutUserAsync();

        Task<int> AddOrUpdate(Models.PollRespose pollRespose);
    }
}
