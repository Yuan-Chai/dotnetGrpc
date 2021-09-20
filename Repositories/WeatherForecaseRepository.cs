using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnetGrpc.Repositories
{

    public interface IWeatherForecastRepository
    {
        Task Save(WeatherForecast data);
        Task<IEnumerable<WeatherForecast>> GetAll();
    }

    public class WeatherForecastRepository : IWeatherForecastRepository
    {
        private ConcurrentDictionary<string, WeatherForecast> _db;

        public WeatherForecastRepository()
        {
            _db = new ConcurrentDictionary<string, WeatherForecast>();
            Seed();
        }

        public async Task Save(WeatherForecast data)
        {
            _db.TryAdd(data.Id, data);
            await Task.CompletedTask;
        }

        public async Task<IEnumerable<WeatherForecast>> GetAll()
        {
            return await Task.FromResult(_db.Values.AsEnumerable());
        }

        private void Seed()
        {
            var summaries = new[]{
                "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
            };

            var rng = new Random();

            for (int i = 0; i < 5; i++)
            {
                var data = new WeatherForecast()
                {
                    Id = Guid.NewGuid().ToString(),
                    Date = DateTime.Now.AddDays(i),
                    TemperatureC = rng.Next(-20, 55),
                    Summary = summaries[rng.Next(summaries.Length)]
                };

                _ = Save(data);
            }
        }
    }

}