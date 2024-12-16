using Newtonsoft.Json.Converters;
using Newtonsoft.Json;

namespace WeatherForecastApi.WeatherApi.Api.ExternalServices.OpenWeatherMap
{
    public class OpenWeatherForecastResponse
    {
        [JsonProperty("city")]
        public CityDetails City { get; set; }

        [JsonProperty("list")]
        public List<ForecastItem> Forecasts { get; set; }

        public class CityDetails
        {
            [JsonProperty("name")]
            public string Name { get; set; }
        }

        public class ForecastItem
        {
            [JsonProperty("dt")]
            [JsonConverter(typeof(UnixDateTimeConverter))]
            public DateTime Date { get; set; }

            [JsonProperty("main")]
            public MainInfo Main { get; set; }

            [JsonProperty("weather")]
            public List<WeatherInfo> Weather { get; set; }
        }

        public class MainInfo
        {
            [JsonProperty("temp_min")]
            public double MinTemperature { get; set; }

            [JsonProperty("temp_max")]
            public double MaxTemperature { get; set; }
        }

        public class WeatherInfo
        {
            [JsonProperty("icon")]
            public string Icon { get; set; }
        }
    }

}
