using System;

namespace TestTheCoreWebAPI.Models
{
    public class WeatherForecast
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public DateTime Date { get; set; }
        public int TemperatureC { get; set; }
        public int TemperatureF => 32 + (int)(TemperatureC * 1.8);
        public string Summary
        {
            get
            {
                int _step = 6;
                int _index = Math.Max(0, (TemperatureC / _step) + 1);

                return Summaries[Math.Min(_index, Summaries.Length - 1)];
            }
        }
    }
}
