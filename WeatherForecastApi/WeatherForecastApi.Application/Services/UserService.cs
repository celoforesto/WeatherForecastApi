using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using WeatherForecastApi.WeatherForecastApi.Application.Interfaces;
using WeatherForecastApi.WeatherForecastApi.Domain.Entities;
using WeatherForecastApi.WeatherForecastApi.Infrastructure.Data;

namespace WeatherForecastApi.WeatherForecastApi.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly AppDbContext _context;

        public UserService(IUserRepository userRepository, AppDbContext context)
        {
            _userRepository = userRepository;
            _context = context;
        }

        public async Task<User?> GetByIdAsync(Guid userId)
        {
            return await _userRepository.GetByIdAsync(userId);
        }

        public async Task<User?> AuthenticateAsync(string username, string password)
        {
            var user = await _userRepository.GetByUsernameAsync(username);
            if (user == null || !VerifyPasswordHash(password, user.PasswordHash))
            {
                return null;
            }
            return user;
        }

        private bool VerifyPasswordHash(string password, string storedHash)
        {
            using var sha256 = SHA256.Create();
            var computedHash = Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(password)));
            return computedHash == storedHash;
        }
    }
}
