using Dapper;
using System.Data;
using WeatherForecastApi.WeatherForecastApi.Application.Interfaces;
using WeatherForecastApi.WeatherForecastApi.Domain.Entities;

namespace WeatherForecastApi.WeatherForecastApi.Infrastructure.Repositories
{
    public class FavoriteCityRepository : IFavoriteCityRepository
    {
        private readonly IDbConnection _dbConnection;

        public FavoriteCityRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<IEnumerable<FavoriteCity>> GetFavoriteCitiesAsync(Guid userId)
        {
            var sql = @"
                SELECT * 
                FROM FavoriteCities
                WHERE UserId = @UserId;";

            try
            {
                return await _dbConnection.QueryAsync<FavoriteCity>(sql, new { UserId = userId });
            }
            catch (Exception ex)
            {
                throw new DataException("An error occurred while retrieving favorite cities.", ex);
            }
        }
    }
}
