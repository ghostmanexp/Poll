using Poll.Interfaces;
using Poll.Models;

namespace Poll.Services
{
    public class QuestionService : IQuestionService
    {
        private Main _dbConn;

        public QuestionService(Main connection)
        {
            _dbConn = connection;
        }

        public async Task<int> AddOrUpdate(Question question)
        {
            var exist = await _dbConn.Connection.Table<Question>().Where(r => r.Id == question.Id).ToListAsync();
            if (exist.Any())
            {
                question.Id = exist[0].Id;
            }
            return await _dbConn.Connection.InsertOrReplaceAsync(question);
        }

        public async Task<List<Question>> GetAllAsync() => await _dbConn.Connection.Table<Question>().ToListAsync();
    }
}
