﻿namespace WeatherForecastApi.WeatherForecastApi.Domain.Entities
{
    public class FavoriteCity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid UserId { get; set; }
    }
}
