﻿namespace AppApiService.Domain.TestService;

public class TestService : CommonService<Test, int>, ITestService
{
    private IUnitOfWork unitOfWork;

    public TestService(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }

    public async Task<bool> AddTest(Test test)
    {
        var res = await Add(test);
        return res;
    }

    public async Task<bool> DeleteTest(int id)
    {
        var res = await Delete(id);
        return res;
    }

    public async Task<Test> FindTest(int id)
    {
        var test = await unitOfWork.Get().Set<Test>().FindAsync(id);
        return test;
    }

    public IQueryable<Test> GetTestList(string keyword)
    {
        var datas = unitOfWork.Get().Set<Test>().AsQueryable();
        if(!string.IsNullOrEmpty(keyword))
            datas = datas.Where(a => a.Name.Contains(keyword));
        return datas.OrderByDescending(a => a.Id);
    }

    public async Task<bool> UpdateTest(Test test)
    {
        var res = await Update(test);
        return res;
    }
}
