using SQLite;

namespace Poll.Models
{
    public class PollQuestion
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public int PollId { get; set; }
        public int OrderId { get; set; }
    }
}
