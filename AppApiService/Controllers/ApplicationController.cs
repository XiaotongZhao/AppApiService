using AppApiService.Domain.ApplicationService;

namespace AppApiService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ApplicationController : ControllerBase
{
    private readonly ILogger<ApplicationController> logger;
    private readonly IApplicationService applicationService;
    public ApplicationController(ILogger<ApplicationController> logger, IApplicationService applicationService)
    {
        this.logger = logger;
        this.applicationService = applicationService;
    }

    [HttpGet, Route("TestThrowException")]
    public void TestThrowException()
    {
        throw new Exception("this is a test for global exception!!!");
    }

    [HttpPost, Route("OrderStudentNos")]
    public int[] OrderStudentNos(int[] studentNos)
    {
        return applicationService.OrderStudentNos(studentNos);
    }

}
