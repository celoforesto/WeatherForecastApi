using WeatherForecastApi.WeatherForecastApi.Domain.Entities;

namespace WeatherForecastApi.WeatherForecastApi.Application.Interfaces
{
    public interface IUserRepository
    {
        /// <summary>
        /// Retrieves a user by their unique identifier.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <returns>The user object if found; otherwise, null.</returns>
        Task<User?> GetByIdAsync(Guid userId);

        /// <summary>
        /// Retrieves a user by their username.
        /// </summary>
        /// <param name="username">The username of the user.</param>
        /// <returns>The user object if found; otherwise, null.</returns>
        Task<User?> GetByUsernameAsync(string username);
    }
}
