namespace WeatherForecastApi.WeatherApi.Api.DTOs
{
    public class CityWeatherDto
    {
        public Guid CityId { get; set; }
        public string CityName { get; set; }
        public double Temperature { get; set; }
        public double MinTemperature { get; set; }
        public double MaxTemperature { get; set; }
        public int Humidity { get; set; }
        public string WeatherDescription { get; set; }
        public string Icon { get; set; }
    }
}
