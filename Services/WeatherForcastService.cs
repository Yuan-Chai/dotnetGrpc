using System.Collections.Generic;
using System.Threading.Tasks;

using dotnetGrpc.Repositories;

namespace dotnetGrpc.Services
{
    public interface IWeatherForcastService
    {
        Task Save(WeatherForecast data);
        Task<IEnumerable<WeatherForecast>> GetAll();
    }

    public class WeatherForcastService : IWeatherForcastService
    {
        private readonly IWeatherForecastRepository _weatherForecastRepository;

        public WeatherForcastService(IWeatherForecastRepository weatherForecastRepository)
        {
            _weatherForecastRepository = weatherForecastRepository;
        }

        public async Task<IEnumerable<WeatherForecast>> GetAll()
        {
            return await _weatherForecastRepository.GetAll();
        }

        public async Task Save(WeatherForecast data)
        {
            await _weatherForecastRepository.Save(data);
        }
    }
}