using Newtonsoft.Json;
using WeatherForecastApi.WeatherApi.Api.DTOs;
using WeatherForecastApi.WeatherApi.Api.ExternalServices.OpenWeatherMap;
using WeatherForecastApi.WeatherForecastApi.Application.Interfaces;
using WeatherForecastApi.WeatherForecastApi.Infrastructure.Data;

namespace WeatherForecastApi.WeatherForecastApi.Application.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly AppDbContext _context;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _apiKey;
        private readonly ILogger<WeatherService> _logger;

        public WeatherService(AppDbContext context, IHttpClientFactory httpClientFactory, IConfiguration configuration, ILogger<WeatherService> logger)
        {
            _context = context;
            _httpClientFactory = httpClientFactory;
            _apiKey = configuration["OpenWeatherMap:ApiKey"] ?? throw new ArgumentNullException("OpenWeatherMap:ApiKey not configured.");
            _logger = logger;
        }

        public async Task<CityWeatherDto?> GetWeatherByCityAsync(string cityName)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("OpenWeatherMap");
                var formattedCity = Uri.EscapeDataString(cityName);
                var response = await client.GetAsync($"weather?q={formattedCity}&appid={_apiKey}&units=metric");

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogWarning("Failed to fetch weather data for city: {CityName}. Status Code: {StatusCode}", cityName, response.StatusCode);
                    return null;
                }

                var content = await response.Content.ReadAsStringAsync();
                var weatherData = JsonConvert.DeserializeObject<dynamic>(content);

                return new CityWeatherDto
                {
                    CityName = weatherData.name,
                    Temperature = weatherData.main.temp,
                    MinTemperature = weatherData.main.temp_min,
                    MaxTemperature = weatherData.main.temp_max,
                    Humidity = weatherData.main.humidity,
                    WeatherDescription = weatherData.weather[0].description,
                    Icon = weatherData.weather[0].icon
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching weather data for city: {CityName}", cityName);
                return null;
            }
        }

        public async Task<FiveDayForecastDto?> GetFiveDayForecastAsync(string cityName)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("OpenWeatherMap");
                var formattedCity = Uri.EscapeDataString(cityName);
                var response = await client.GetAsync($"forecast?q={formattedCity}&appid={_apiKey}&units=metric");

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogWarning("Failed to fetch forecast data for city: {CityName}. Status Code: {StatusCode}", cityName, response.StatusCode);
                    return null;
                }

                var content = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<OpenWeatherForecastResponse>(content);

                if (apiResponse?.Forecasts == null || apiResponse.City == null)
                {
                    _logger.LogWarning("No forecast data returned for city: {CityName}", cityName);
                    return null;
                }

                var tomorrow = DateTime.UtcNow.Date.AddDays(1);
                var filteredForecasts = apiResponse.Forecasts
                    .Where(f => f.Date.Date >= tomorrow)
                    .ToList();

                return new FiveDayForecastDto
                {
                    CityName = apiResponse.City.Name,
                    ForecastDays = filteredForecasts
                        .GroupBy(f => f.Date.Date)
                        .Take(5)
                        .Select(group => new ForecastDayDto
                        {
                            Date = group.Key.ToString("dd/MM/yyyy"),
                            MinTemperature = group.Min(f => f.Main.MinTemperature),
                            MaxTemperature = group.Max(f => f.Main.MaxTemperature),
                            WeatherIcon = group.First().Weather.FirstOrDefault()?.Icon
                        })
                        .ToList()
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching forecast data for city: {CityName}", cityName);
                return null;
            }
        }
    }
}