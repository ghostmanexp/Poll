using SQLite;

namespace Poll.Models
{
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [MaxLength(200)]
        public string UserName { get; set; }
        /// <summary>
        /// Todo: HASH PASSWORD
        /// </summary>
        [MaxLength(200)]
        public string Password { get; set; }
        public int RoleId { get; set; }
    }
}
