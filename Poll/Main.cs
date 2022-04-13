using Microsoft.AspNetCore.Mvc;
using SQLite;

namespace Poll
{
    /// <summary>
    /// Implement DTO
    /// </summary>
    public class Main : ControllerBase
    {
        public SQLiteAsyncConnection Connection;
        public Main()
        {
            var xxx = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            // Get an absolute path to the database file
            var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "PollData.db");
            Connection = new SQLiteAsyncConnection(databasePath);

            _ = CreateTables(Connection);
        }

        private async Task CreateTables(SQLiteAsyncConnection _dbConn)
        {
            await _dbConn.CreateTableAsync<Models.Poll>();
            await _dbConn.CreateTableAsync<Models.PollQuestion>();
            await _dbConn.CreateTableAsync<Models.PollRespose>();
            await _dbConn.CreateTableAsync<Models.Question>();
            await _dbConn.CreateTableAsync<Models.Role>();
            await _dbConn.CreateTableAsync<Models.User>();
        }
    }
}
