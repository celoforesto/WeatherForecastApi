using WeatherForecastApi.WeatherApi.Api.DTOs;
using WeatherForecastApi.WeatherForecastApi.Domain.Entities;

namespace WeatherForecastApi.WeatherForecastApi.Application.Interfaces
{
    /// <summary>
    /// Interface for managing favorite cities and retrieving weather data associated with them.
    /// </summary>
    public interface IFavoriteCityService
    {
        /// <summary>
        /// Adds a city to the user's list of favorites.
        /// </summary>
        /// <param name="cityName">The name of the city to add.</param>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <returns>The added favorite city, or null if the city already exists in the user's favorites.</returns>
        Task<FavoriteCity?> AddToFavoritesAsync(string cityName, Guid userId);

        /// <summary>
        /// Removes a city from the user's list of favorites.
        /// </summary>
        /// <param name="cityId">The unique identifier of the city.</param>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <returns>True if the city was removed successfully, otherwise false.</returns>
        Task<bool> RemoveFromFavoritesAsync(Guid cityId, Guid userId);

        /// <summary>
        /// Retrieves a list of the user's favorite cities with associated weather data.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <returns>A list of CityWeatherDto containing weather information for each favorite city.</returns>
        Task<List<CityWeatherDto>> GetFavoriteCitiesWithWeatherAsync(Guid userId);
    }
}
