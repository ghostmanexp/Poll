using SQLite;

namespace Poll.Models
{
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [MaxLength(200)]
        public string UserName { get; set; }
        [MaxLength(16)]
        public string Password { get; set; }
        public int RoleId { get; set; }
    }
}
