namespace Poll.Interfaces
{
    public interface IQuestionService
    {
        Task<List<Models.Question>> GetAllAsync();
        Task<int> AddOrUpdate(Models.Question question);
    }
}
