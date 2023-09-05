using Microsoft.AspNetCore.Mvc.Filters;

namespace AppApiService.Common;

public class LogFunActionFilter : IActionFilter
{
    private readonly ILogger<LogFunActionFilter> logger;

    public LogFunActionFilter(ILogger<LogFunActionFilter> logger)
    {
        this.logger = logger;
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        var functionName = context.ActionDescriptor.DisplayName;
        var objResult = context.Result as ObjectResult;
        var result = JsonSerializer.Serialize(objResult?.Value);
        var logInformation = $"Request function is {functionName} and result is {result}";
        logger.LogInformation(logInformation);
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        var functionName = context.ActionDescriptor.DisplayName;
        var functionParams = JsonSerializer.Serialize(context.ActionArguments);
        var logInformation = $"Request function is {functionName} and params is {functionParams}";
        logger.LogInformation(logInformation);
    }
}
