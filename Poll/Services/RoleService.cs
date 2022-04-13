using Poll.Interfaces;
using Poll.Models;
using SQLite;

namespace Poll.Services
{
    public class RoleService : IRoleService
    {
        private Main _dbConn;

        public RoleService(Main connection)
        {
            _dbConn = connection;
        }

        public async Task<Role> GetAsync(int id) => await _dbConn.Connection.Table<Role>().FirstOrDefaultAsync(f=> f.Id == id);

        public async Task<List<Role>> GetAllAsync() => await _dbConn.Connection.Table<Role>().ToListAsync();

        public async Task<int> AddOrUpdate(Role role)
        {
            var exist = await _dbConn.Connection.Table<Role>().Where(r => r.Name == role.Name).ToListAsync();
            if (exist.Any())
            {
                role.Id = exist[0].Id;
            }

            var inserted = await _dbConn.Connection.InsertOrReplaceAsync(role);
            if (inserted > 0)
            {
                return role.Id;
            }
            return 0;
        }

    }
}
