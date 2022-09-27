using AppApiService.Domain.TestService;
using Microsoft.AspNetCore.Mvc;

namespace AppApiService.Controllers;

[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{
    private readonly ITestService testService;
    private readonly ILogger<TestController> logger;

    public TestController(ILogger<TestController> logger, ITestService testService)
    {
        this.logger = logger;
        this.testService = testService;
    }

    [HttpGet(Name = "GetTestListAsync")]
    public async Task<List<Test>> GetTestListAsync()
    {
        logger.LogInformation("this is a test");
        var res = await testService.GetTestListAsync();
        return res;
    }
}
