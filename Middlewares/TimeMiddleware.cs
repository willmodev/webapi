

public class TimeMiddleware(RequestDelegate next)
{
    readonly RequestDelegate next = next;


    public async Task Invoke(HttpContext context)
    {

        if( context.Request.Query.Any( p => p.Key == "time" )) {
             await context.Response.WriteAsync(DateTime.Now.ToShortTimeString());
             return;
        }
        await next(context);
    }
}

public  static class TimeMiddlewareExtensions
{
    public static IApplicationBuilder UseTimeMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<TimeMiddleware>();
    }
}