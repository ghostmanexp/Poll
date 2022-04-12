using Poll.Interfaces;

namespace Poll.Services
{
    public class PollService : IPollService
    {
        private Main _dbConn;

        public PollService(Main connection)
        {
            _dbConn = connection;
        }

        public async Task<int> AddOrUpdate(Models.Poll poll)
        {
            var exist = await _dbConn.Connection.Table<Models.Poll>().Where(r => r.Id == poll.Id).ToListAsync();
            if (exist.Any())
            {
                poll.Id = exist[0].Id;
            }

            return await _dbConn.Connection.InsertOrReplaceAsync(poll);
        }

        public async Task<List<Models.Poll>> GetAllAsync() => await _dbConn.Connection.Table<Models.Poll>().ToListAsync();

        public async Task<List<Models.Poll>> GetAllFromUserAsync(int userId) => await _dbConn.Connection.Table<Models.Poll>().Where(p => p.UserId == userId).ToListAsync();

        public async Task<Models.Poll> GetAsync(int id) => await _dbConn.Connection.Table<Models.Poll>().FirstOrDefaultAsync(p => p.Id == id);
    }
}
