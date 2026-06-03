namespace MyMvcApp.Middlewares;

public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;

    public RequestLoggingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        var method = context.Request.Method;
        var path = context.Request.Path.ToString();

        Console.WriteLine($"[{time}] Method: {method} - Path: {path}");

        if (path == "/Book/Detail/0" || path == "/Book/Detail/-1")
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsync("Book id không hợp lệ");
            Console.WriteLine($"Status Code: {context.Response.StatusCode}");
            return;
        }

        await _next(context);

        Console.WriteLine($"Status Code: {context.Response.StatusCode}");
    }
}
