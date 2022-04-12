using Poll.Interfaces;
using Poll.Models;

namespace Poll.Services
{
    public class PollResponseService : IPollResponseService
    {
        private Main _dbConn;

        public PollResponseService(Main connection)
        {
            _dbConn = connection;
        }

        public async Task<int> AddOrUpdate(PollRespose pollRespose)
        {
            var exist = await _dbConn.Connection.Table<Models.PollRespose>().Where(r => r.Id == pollRespose.Id).ToListAsync();
            if (exist.Any())
            {
                pollRespose.Id = exist[0].Id;
            }
            return await _dbConn.Connection.InsertOrReplaceAsync(pollRespose);
        }

        public async Task<List<PollRespose>> GetAllFromPollAsync(int pollId) => await _dbConn.Connection.Table<PollRespose>().Where(p => p.PollId == pollId).ToListAsync();

        public async Task<List<PollRespose>> GetAllFromUserAsync(int userId) => await _dbConn.Connection.Table<PollRespose>().Where(u=> u.AnsweredId == userId).ToListAsync();

        public async Task<List<PollRespose>> GetAllWithoutUserAsync() => await _dbConn.Connection.Table<PollRespose>().Where(u => u.AnsweredId == 0).ToListAsync();

    }
}
