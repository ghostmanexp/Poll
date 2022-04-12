using Poll.Interfaces;
using Poll.Models;

namespace Poll.Services
{
    public class UserService : IUserService
    {
        private Main _dbConn;

        public UserService(Main connection)
        {
            _dbConn = connection;
        }

        public async Task<User> GetAsync(int id) => await _dbConn.Connection.Table<User>().FirstOrDefaultAsync(f => f.Id == id);

        public async Task<List<User>> GetAllAsync() => await _dbConn.Connection.Table<User>().ToListAsync();

        public async Task<int> AddOrUpdate(User user)
        {
            var userData = new User()
            {
                RoleId = user.RoleId,
                UserName = user.UserName,
                Password = user.Password,
            };

            var exist = await _dbConn.Connection.Table<User>().Where(r => r.UserName == user.UserName).ToListAsync();
            if (exist.Any())
            {
                userData.Id = exist[0].Id;
            }

            return await _dbConn.Connection.InsertOrReplaceAsync(userData);
        }

    }
}
