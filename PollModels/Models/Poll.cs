using SQLite;

namespace Poll.Models
{
    public class Poll
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [MaxLength(200)]
        public string Title { get; set; }
        [MaxLength(2000)]
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public DateTime Expires { get; set; }
        public int? UserId { get; set; }
    }
}
