using Poll.Models;

namespace Models.ViewModels
{
    public class PollQuestionViewModel
    {
        public List<PollQuestion> questions { get; set; }
        public Poll.Models.Poll poll { get; set; }
        public Poll.Models.User user { get; set; }
    }
}
