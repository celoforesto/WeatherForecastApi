using Microsoft.EntityFrameworkCore;
using WeatherForecastApi.WeatherApi.Api.DTOs;
using WeatherForecastApi.WeatherForecastApi.Application.Interfaces;
using WeatherForecastApi.WeatherForecastApi.Domain.Entities;
using WeatherForecastApi.WeatherForecastApi.Infrastructure.Data;

namespace WeatherForecastApi.WeatherForecastApi.Application.Services
{
    public class FavoriteCityService : IFavoriteCityService
    {
        private readonly AppDbContext _context;
        private readonly IFavoriteCityRepository _favoriteCityRepository;
        private readonly IWeatherService _weatherService;

        public FavoriteCityService(AppDbContext context,
                                   IFavoriteCityRepository favoriteCityRepository,
                                   IWeatherService weatherService)
        {
            _context = context;
            _favoriteCityRepository = favoriteCityRepository;
            _weatherService = weatherService;
        }

        public async Task<FavoriteCity?> AddToFavoritesAsync(string cityName, Guid userId)
        {
            // Check if the city is already in the user's favorites.
            if (await _context.FavoriteCities.AnyAsync(fc => fc.Name == cityName && fc.UserId == userId))
                return null;

            var favoriteCity = new FavoriteCity { Name = cityName, UserId = userId };

            _context.FavoriteCities.Add(favoriteCity);
            var success = await _context.SaveChangesAsync() > 0;

            return success ? favoriteCity : null;
        }

        public async Task<bool> RemoveFromFavoritesAsync(Guid cityId, Guid userId)
        {
            var city = await _context.FavoriteCities.FirstOrDefaultAsync(fc => fc.Id == cityId && fc.UserId == userId);
            if (city == null) return false;

            _context.FavoriteCities.Remove(city);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<CityWeatherDto>> GetFavoriteCitiesWithWeatherAsync(Guid userId)
        {
            // Retrieve all favorite cities for the user.
            var favoriteCities = await _favoriteCityRepository.GetFavoriteCitiesAsync(userId);

            // Fetch weather data for each city asynchronously.
            var cityWeatherTasks = favoriteCities.Select(async city =>
            {
                var weather = await _weatherService.GetWeatherByCityAsync(city.Name);

                // Return weather details if available.
                return weather == null ? null : new CityWeatherDto
                {
                    CityId = city.Id,
                    CityName = city.Name,
                    Temperature = weather.Temperature,
                    MinTemperature = weather.MinTemperature,
                    MaxTemperature = weather.MaxTemperature,
                    Humidity = weather.Humidity,
                    WeatherDescription = weather.WeatherDescription,
                    Icon = weather.Icon
                };
            });

            var cityWeatherList = await Task.WhenAll(cityWeatherTasks);

            return cityWeatherList.Where(dto => dto != null).ToList();
        }
    }
}