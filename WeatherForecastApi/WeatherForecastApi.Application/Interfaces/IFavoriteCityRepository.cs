using WeatherForecastApi.WeatherForecastApi.Domain.Entities;

namespace WeatherForecastApi.WeatherForecastApi.Application.Interfaces
{
    public interface IFavoriteCityRepository
    {
        /// <summary>
        /// Retrieves the list of favorite cities for a specific user.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <returns>An enumerable collection of the user's favorite cities.</returns>
        Task<IEnumerable<FavoriteCity>> GetFavoriteCitiesAsync(Guid userId);
    }
}
