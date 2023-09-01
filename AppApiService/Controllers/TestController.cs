using AppApiService.Domain.TestService;
using AppApiService.Infrastructure.Common;
using AppApiService.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace AppApiService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestController : ControllerBase
{
    private readonly ITestService testService;
    private readonly ILogger<TestController> logger;

    public TestController(ILogger<TestController> logger, ITestService testService)
    {
        this.logger = logger;
        this.testService = testService;
    }

    [HttpPost, Route("AddTest")]
    public async Task<bool> AddTest(Test test)
    {
        var res = await testService.AddTest(test);
        return res;
    }

    [HttpPost, Route("UpdateTest")]
    public async Task<bool> UpdateTest(Test test)
    {
        var res = await testService.UpdateTest(test);
        return res;
    }

    [HttpPost, Route("DeleteTest")]
    public async Task<bool> DeleteTest(int id)
    {
        var res = await testService.DeleteTest(id);
        return res;
    }

    [HttpPost, Route("GetTestList")]
    public async Task<DataSource<Test>> GetTestList(SeachModel seachModel)
    {
        var res = await testService.GetTestList(seachModel.Keyword).TakePageDataAndCountAsync(seachModel.Skip, seachModel.Size);
        return res;
    }

    [HttpGet, Route("FindTest")]
    public async Task<Test> FindTest(int id)
    {
        return await testService.FindTest(id);
    }


    [HttpGet, Route("TestLog")]
    public void TestLog()
    {
        for (var i = 0; i < 1000; i++) 
        {
            logger.LogInformation($"Test loop 1000 and curent index is {i}", DateTime.UtcNow);
        }
    }

    [HttpGet, Route("TestErrorLog")]
    public void TestErrorLog()
    {
        for (var i = 0; i < 100; i++)
        {
            logger.LogError("Test input error log!!!!");
        }
    }

    [HttpGet, Route("TestWarningLog")]
    public void TestWarningLog()
    {
        for (var i = 0; i < 100; i++)
        {
            logger.LogWarning("Test input warning log!!!!");
        }
    }

}
