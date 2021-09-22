using System.Collections.Generic;
using System.Threading.Tasks;

using dotnetGrpc.Hubs;
using dotnetGrpc.Repositories;

namespace dotnetGrpc.Services
{
    public interface IWeatherForecastService
    {
        Task Save(WeatherForecast data);
        Task<IEnumerable<WeatherForecast>> GetAll();
    }

    public class WeatherForecastService : IWeatherForecastService
    {
        private readonly IWeatherForecastRepository _weatherForecastRepository;
        private readonly WeatherForecastHub _hub;

        public WeatherForecastService(IWeatherForecastRepository weatherForecastRepository, WeatherForecastHub hub)
        {
            _weatherForecastRepository = weatherForecastRepository;
            _hub = hub;
        }

        public async Task<IEnumerable<WeatherForecast>> GetAll()
        {
            return await _weatherForecastRepository.GetAll();
        }

        public async Task Save(WeatherForecast data)
        {
            await _weatherForecastRepository.Save(data);
            await _hub.NewForecast(data);
        }
    }
}