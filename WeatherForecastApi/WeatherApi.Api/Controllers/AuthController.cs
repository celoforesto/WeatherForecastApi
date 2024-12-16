using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using WeatherForecastApi.WeatherForecastApi.Application.Interfaces;

namespace WeatherForecastApi.WeatherApi.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;
        private readonly IJwtUtils _jwtUtils;

        public AuthController(IConfiguration configuration, IUserService userService, IJwtUtils jwtUtils)
        {
            _configuration = configuration;
            _userService = userService;
            _jwtUtils = jwtUtils;
        }

        /// <summary>
        /// Authenticates a user and generates a JWT token.
        /// </summary>
        /// <param name="request">LoginRequest containing username and password.</param>
        /// <returns>A JWT token if authentication succeeds, otherwise 401.</returns>
        [HttpPost("login")]
        [SwaggerOperation(Summary = "Authenticate a user and get a JWT token.")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            // Validate input
            if (string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
                return BadRequest("Username and password are required.");

            // Authenticate user
            var user = await _userService.AuthenticateAsync(request.Username, request.Password);
            if (user == null)
                return Unauthorized("Invalid username or password.");

            // Generate token
            var token = _jwtUtils.GenerateJwtToken(user);
            return Ok(new { token });
        }
    }

    public class LoginRequest
    {
        [Required(ErrorMessage = "Username is required.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
    }
}