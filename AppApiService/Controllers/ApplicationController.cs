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

    [HttpPost("order-student-nos")]
    public ActionResult<int[]> OrderStudentNos([FromBody] int[] studentNos)
    {
        if (studentNos == null || studentNos.Length == 0)
        {
            return BadRequest("学生编号列表不能为空");
        }

        var result = applicationService.OrderStudentNos(studentNos);
        return Ok(result);
    }
}
