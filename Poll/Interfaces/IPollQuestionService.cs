namespace Poll.Interfaces
{
    public interface IPollQuestionService
    {
        Task<List<Models.PollQuestion>> GetAllFromPollAsync(int pollId);
        Task<int> AddOrUpdate(Models.PollQuestion pollQuestion);
    }
}
