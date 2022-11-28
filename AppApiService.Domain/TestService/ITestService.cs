namespace AppApiService.Domain.TestService;

public interface ITestService
{
    IQueryable<Test> GetTestList(string keyword);
    Task<bool> AddTest(Test test);
    Task<bool> DeleteTest(int id);
    Task<bool> UpdateTest(Test test);
}
