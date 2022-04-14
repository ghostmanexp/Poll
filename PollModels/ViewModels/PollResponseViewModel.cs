namespace Models.ViewModels
{
    public class PollResponseViewModel
    {
        public Poll.Models.PollRespose respose { get; set; }
        public Poll.Models.Poll poll { get; set; }
        public Poll.Models.Question question { get; set; }
        public Poll.Models.User user { get; set; } 
    }
}
