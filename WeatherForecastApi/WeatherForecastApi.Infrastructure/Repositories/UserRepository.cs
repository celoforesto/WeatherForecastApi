using Dapper;
using System.Data;
using WeatherForecastApi.WeatherForecastApi.Application.Interfaces;
using WeatherForecastApi.WeatherForecastApi.Domain.Entities;

namespace WeatherForecastApi.WeatherForecastApi.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbConnection _dbConnection;

        public UserRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<User?> GetByIdAsync(Guid userId)
        {
            var sql = @"
                SELECT * 
                FROM Users
                WHERE Id = @UserId;";

            try
            {
                return await _dbConnection.QueryFirstOrDefaultAsync<User>(sql, new { UserId = userId });
            }
            catch (Exception ex)
            {
                throw new DataException("An error occurred while retrieving the user by ID.", ex);
            }
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            var sql = @"
                SELECT * 
                FROM Users
                WHERE Username = @Username;";

            try
            {
                return await _dbConnection.QueryFirstOrDefaultAsync<User>(sql, new { Username = username });
            }
            catch (Exception ex)
            {
                throw new DataException("An error occurred while retrieving the user by username.", ex);
            }
        }
    }
}
