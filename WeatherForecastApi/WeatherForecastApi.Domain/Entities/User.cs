namespace WeatherForecastApi.WeatherForecastApi.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public ICollection<FavoriteCity> FavoriteCities { get; set; }
    }
}