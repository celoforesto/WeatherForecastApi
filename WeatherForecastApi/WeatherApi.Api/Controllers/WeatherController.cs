using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WeatherForecastApi.WeatherForecastApi.Application.Interfaces;

namespace WeatherForecastApi.WeatherForecastApi.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class WeatherController : ControllerBase
    {
        private readonly IWeatherService _weatherService;

        public WeatherController(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        /// <summary>
        /// Gets current weather by city name.
        /// </summary>
        [HttpGet("current/{cityName}")]
        [SwaggerOperation(Summary = "Get current weather for a city.")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetWeatherByCity(string cityName)
        {
            var result = await _weatherService.GetWeatherByCityAsync(cityName);
            if (result == null)
                return NotFound("City not found or unable to fetch weather data.");

            return Ok(result);
        }

        /// <summary>
        /// Gets 5-day weather forecast by city name.
        /// </summary>
        [HttpGet("forecast/{cityName}")]
        [SwaggerOperation(Summary = "Get 5-day weather forecast for a city.")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetFiveDayForecast(string cityName)
        {
            var result = await _weatherService.GetFiveDayForecastAsync(cityName);
            if (result == null)
                return NotFound("City not found or unable to fetch forecast data.");

            return Ok(result);
        }
    }
}
