namespace WeatherForecastApi.WeatherApi.Api.DTOs
{
    public class FiveDayForecastDto
    {
        public string CityName { get; set; }
        public List<ForecastDayDto> ForecastDays { get; set; }
    }

    public class ForecastDayDto
    {
        public string Date { get; set; }
        public double MinTemperature { get; set; }
        public double MaxTemperature { get; set; }
        public string WeatherIcon { get; set; }
    }
}
