using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using WeatherForecastApi.WeatherForecastApi.Application.Interfaces;

namespace WeatherForecastApi.WeatherApi.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class FavoritesController : ControllerBase
    {
        private readonly IFavoriteCityService _favoriteCityService;

        public FavoritesController(IFavoriteCityService favoriteCityService)
        {
            _favoriteCityService = favoriteCityService;
        }

        /// <summary>
        /// Adds a city to the user's favorites.
        /// </summary>
        [HttpPost]
        [SwaggerOperation(Summary = "Add a city to favorites.")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddToFavorites([FromBody] CityRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.CityName))
                return BadRequest("The cityName field is required.");

            var userId = GetUserId();
            var addedCity = await _favoriteCityService.AddToFavoritesAsync(request.CityName, userId);

            if (addedCity == null)
                return BadRequest("City is already in favorites or an error occurred.");

            return Ok(addedCity);
        }

        /// <summary>
        /// Removes a city from the user's favorites.
        /// </summary>
        [HttpDelete("{cityId}")]
        [SwaggerOperation(Summary = "Remove a city from favorites.")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RemoveFromFavorites(Guid cityId)
        {
            var userId = GetUserId();
            var success = await _favoriteCityService.RemoveFromFavoritesAsync(cityId, userId);

            if (!success)
                return NotFound("City not found in favorites.");

            return Ok();
        }

        /// <summary>
        /// Retrieves the user's favorite cities along with weather information.
        /// </summary>
        [HttpGet("weather")]
        [SwaggerOperation(Summary = "Get favorite cities with weather information.")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetFavoriteCitiesWithWeather()
        {
            var userId = GetUserId();

            if (userId == Guid.Empty)
                return BadRequest("Invalid user ID.");

            var citiesWithWeather = await _favoriteCityService.GetFavoriteCitiesWithWeatherAsync(userId);
            return Ok(citiesWithWeather);
        }

        private Guid GetUserId()
        {
            // Retrieve the user ID claim using the new simplified claim name
            var userIdClaim = User.FindFirst("userId")?.Value;
            return userIdClaim != null ? Guid.Parse(userIdClaim) : Guid.Empty;
        }
    }

    public class CityRequest
    {
        [Required(ErrorMessage = "CityName is required.")]
        public string CityName { get; set; }
    }
}
