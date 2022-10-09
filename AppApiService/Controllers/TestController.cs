using AppApiService.Domain.TestService;
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

    [HttpGet, Route("GetTestListAsync")]
    public async Task<List<Test>> GetTestListAsync()
    {
        var res = await testService.GetTestListAsync();
        return res;
    }
}
