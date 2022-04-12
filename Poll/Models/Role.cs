using SQLite;

namespace Poll.Models
{
    public class Role
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [MaxLength(200)]
        public string Name { get; set; }
    }
}
