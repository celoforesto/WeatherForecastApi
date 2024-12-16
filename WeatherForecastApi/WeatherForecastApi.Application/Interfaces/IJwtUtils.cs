using WeatherForecastApi.WeatherForecastApi.Domain.Entities;

namespace WeatherForecastApi.WeatherForecastApi.Application.Interfaces
{
    public interface IJwtUtils
    {
        /// <summary>
        /// Generates a JWT token for the specified user.
        /// </summary>
        /// <param name="user">The user for whom the token is being generated.</param>
        /// <returns>A JWT token as a string.</returns>
        string GenerateJwtToken(User user);

        /// <summary>
        /// Validates a JWT token and extracts the user ID if valid.
        /// </summary>
        /// <param name="token">The JWT token to validate.</param>
        /// <returns>The user ID if validation is successful; otherwise, null.</returns>
        Guid? ValidateJwtToken(string token);
    }
}
