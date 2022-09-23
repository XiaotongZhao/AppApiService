namespace AppApiService.Domain.TestService;

public class TestService : ITestService
{
    private IUnitOfWork unitOfWork;

    public TestService(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }

    public async Task<bool> AddTest(Test test)
    {
        await unitOfWork.Get().Set<Test>().AddAsync(test);
        var changeCount = await unitOfWork.Get().SaveChangesAsync();
        return changeCount > 0;
    }

    public async Task<bool> DeleteTest(int id)
    {
        var test = await unitOfWork.Get().Set<Test>().FindAsync(id);
        if (test != null)
        {
            test.IsDeleted = true;
            unitOfWork.Get().Set<Test>().Update(test);
            var changeCount = await unitOfWork.Get().SaveChangesAsync();
            return changeCount >= 0;
        }
        else
            return false;
    }

    public async Task<List<Test>> GetTestListAsync()
    {
        var tests = await unitOfWork.Get().Set<Test>().ToListAsync();
        return tests;
    }

    public async Task<bool> Update(Test test)
    {
        unitOfWork.Get().Set<Test>().Update(test);
        var changeCount = await unitOfWork.Get().SaveChangesAsync();
        return changeCount >= 0;
    }
}
