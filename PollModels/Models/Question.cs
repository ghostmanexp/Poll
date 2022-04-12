using SQLite;

namespace Poll.Models
{
    public class Question
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [MaxLength(2000)]
        public string Text { get; set; }
        [MaxLength(80)]
        public string QuestionType { get; set; }
        [MaxLength(2000)]
        public string Options { get; set; }
    }
}
