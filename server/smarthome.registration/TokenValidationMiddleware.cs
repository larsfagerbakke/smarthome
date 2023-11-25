namespace smarthome.registration;

public class TokenValidationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IConfiguration _configuration;

    public TokenValidationMiddleware(RequestDelegate next, IConfiguration configuration)
    {
        _next = next;
        _configuration = configuration;
    }

    public async Task Invoke(HttpContext context)
    {
        var expectedToken = _configuration["AccessToken"];
        var requestToken = context.Request.Headers["Authorization"];

        if (requestToken == expectedToken)
        {
            await _next.Invoke(context);
        }
        else
        {
            context.Response.StatusCode = 401; // Unauthorized
            await context.Response.WriteAsync("Invalid access token");
        }
    }
}
