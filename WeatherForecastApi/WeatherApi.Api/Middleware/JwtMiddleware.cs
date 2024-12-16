using WeatherForecastApi.WeatherForecastApi.Application.Interfaces;

namespace WeatherForecastApi.WeatherApi.Api.Middleware
{

    /// <summary>
    /// Middleware for processing JWT tokens in HTTP requests.
    /// Validates the token, extracts the user ID, and attaches the user object to the request context.
    /// </summary>
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IUserService userService, IJwtUtils jwtUtils)
        {
            // Skip JWT validation for login requests
            if (context.Request.Path.StartsWithSegments("/api/auth/login"))
            {
                await _next(context);
                return;
            }

            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (!string.IsNullOrEmpty(token))
            {
                var userId = jwtUtils.ValidateJwtToken(token);
                if (userId != null)
                {
                    context.Items["User"] = await userService.GetByIdAsync(userId.Value);
                }
            }

            await _next(context);
        }
    }
}
