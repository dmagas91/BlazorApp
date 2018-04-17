using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorApp.Shared.ModelsDTO
{
    public class WeatherForecastDTO
    {
        public Int64 Id { get; set; }
        public DateTime Date { get; set; }
        public int TemperatureC { get; set; }
        public string Summary { get; set; }
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
    }
}
