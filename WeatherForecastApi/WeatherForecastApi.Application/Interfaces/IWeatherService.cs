using WeatherForecastApi.WeatherApi.Api.DTOs;

namespace WeatherForecastApi.WeatherForecastApi.Application.Interfaces
{
    /// <summary>
    /// Interface for weather service operations.
    /// </summary>
    public interface IWeatherService
    {
        /// <summary>
        /// Retrieves current weather information for a given city.
        /// </summary>
        /// <param name="cityName">The name of the city.</param>
        /// <returns>A DTO containing weather details for the specified city.</returns>
        Task<CityWeatherDto> GetWeatherByCityAsync(string cityName);

        /// <summary>
        /// Retrieves a five-day weather forecast for a given city.
        /// </summary>
        /// <param name="cityName">The name of the city.</param>
        /// <returns>A DTO containing the five-day weather forecast.</returns>
        Task<FiveDayForecastDto> GetFiveDayForecastAsync(string cityName);
    }
}
