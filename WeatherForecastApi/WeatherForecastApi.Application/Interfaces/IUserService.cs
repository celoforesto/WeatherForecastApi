using WeatherForecastApi.WeatherForecastApi.Domain.Entities;

namespace WeatherForecastApi.WeatherForecastApi.Application.Interfaces
{
    /// <summary>
    /// Interface defining user-related operations.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Retrieves a user by their unique identifier.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <returns>The user entity, or null if not found.</returns>
        Task<User?> GetByIdAsync(Guid userId);

        /// <summary>
        /// Authenticates a user using their username and password.
        /// </summary>
        /// <param name="username">The username of the user.</param>
        /// <param name="password">The password of the user.</param>
        /// <returns>The authenticated user entity, or null if authentication fails.</returns>
        Task<User?> AuthenticateAsync(string username, string password);
    }
}
