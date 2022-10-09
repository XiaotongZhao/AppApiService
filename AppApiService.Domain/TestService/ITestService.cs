namespace AppApiService.Domain.TestService;

public interface ITestService
{
    Task<List<Test>> GetTestListAsync();
    Task<bool> AddTest(Test test);
    Task<bool> DeleteTest(int id);
    Task<bool> UpdateTest(Test test);
}
