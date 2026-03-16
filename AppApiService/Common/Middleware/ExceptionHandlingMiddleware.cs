using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace AppApiService.Common.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate next;
    private readonly ILogger<ExceptionHandlingMiddleware> logger;
    private readonly IWebHostEnvironment env;

    public ExceptionHandlingMiddleware(
     RequestDelegate next,
     ILogger<ExceptionHandlingMiddleware> logger,
     IWebHostEnvironment env)
    {
        this.next = next;
        this.logger = logger;
        this.env = env;
    }


    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }


    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        logger.LogError(exception, "HTTP {Method} {Path} 发生异常: {Message}",
            context.Request.Method, context.Request.Path, exception.Message);

        var response = context.Response;
        response.ContentType = "application/json";

        var errorResponse = new ErrorResponse
        {
            Success = false,
            Timestamp = DateTime.UtcNow,
            Path = context.Request.Path,
            TraceId = Activity.Current?.Id ?? context.TraceIdentifier
        };

        // 根据异常类型设置状态码和响应内容
        switch (exception)
        {
            case DbUpdateException dbEx:
                response.StatusCode = 500;
                errorResponse.Message = env.IsDevelopment()
                    ? dbEx.InnerException?.Message ?? dbEx.Message
                    : "数据库操作失败";
                errorResponse.Code = "DATABASE_ERROR";
                errorResponse.StatusCode = 500;
                break;

            default:
                response.StatusCode = 500;
                errorResponse.Message = env.IsDevelopment()
                    ? exception.Message
                    : "服务器内部错误";
                errorResponse.Code = "INTERNAL_SERVER_ERROR";
                errorResponse.StatusCode = 500;
                break;
        }

        // 开发环境下添加调试信息
        if (env.IsDevelopment())
        {
            errorResponse.StackTrace = exception.StackTrace ?? string.Empty;
            errorResponse.InnerException = exception.InnerException?.Message ?? string.Empty;
        }

        var jsonResponse = JsonSerializer.Serialize(errorResponse, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = env.IsDevelopment()
        });

        await response.WriteAsync(jsonResponse);
    }
}

public static class ExceptionHandlingMiddlewareExtensions
{
    public static IApplicationBuilder UseExceptionHandling(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ExceptionHandlingMiddleware>();
    }
}

internal class ErrorResponse
{
    public bool Success { get; set; }
    public DateTime Timestamp { get; set; }
    public string Path { get; set; }
    public string TraceId { get; set; }
    public string Message { get; set; }
    public string InnerException { get; set; }
    public string StackTrace { get; set; }
    public string Code { get; set; }
    public int StatusCode { get; set; }
}