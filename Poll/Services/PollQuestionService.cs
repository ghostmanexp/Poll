using Poll.Interfaces;
using Poll.Models;

namespace Poll.Services
{
    public class PollQuestionService : IPollQuestionService
    {
        private Main _dbConn;

        public PollQuestionService(Main connection)
        {
            _dbConn = connection;
        }

        public async Task<int> AddOrUpdate(PollQuestion pollQuestion)
        {
            var exist = await _dbConn.Connection.Table<PollQuestion>().Where(r => r.Id == pollQuestion.Id).ToListAsync();
            if (exist.Any())
            {
                pollQuestion.Id = exist[0].Id;
            }
            return await _dbConn.Connection.InsertOrReplaceAsync(pollQuestion);
        }

        public async Task<List<PollQuestion>> GetAllFromPollAsync(int pollId) => await _dbConn.Connection.Table<PollQuestion>().Where(p => p.PollId == pollId).ToListAsync();
    }
}
