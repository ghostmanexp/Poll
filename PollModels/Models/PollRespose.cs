using SQLite;

namespace Poll.Models
{
    public class PollRespose
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int PollId { get; set; }
        public int QuestionId { get; set; }
        [MaxLength(2000)]

        public string Response { get; set; }
        
        public int AnsweredId { get; set; }
    }
}
