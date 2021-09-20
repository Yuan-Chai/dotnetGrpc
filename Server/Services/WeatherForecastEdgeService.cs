using System.Threading.Tasks;

using dotnetGrpc.Protos;

using Grpc.Core;

namespace dotnetGrpc.Services
{
    public class WeatherForecastEdgeServiceImpl : WeatherForecastEdgeService.WeatherForecastEdgeServiceBase
    {
        private readonly IWeatherForecastService _weatherForecastService;

        public WeatherForecastEdgeServiceImpl(IWeatherForecastService weatherForecastService)
        {
            _weatherForecastService = weatherForecastService;
        }

        public override async Task<WeatherForecastEdgeResponse> Report(WeatherForecastEdgeRequest request, ServerCallContext context)
        {
            var data = new WeatherForecast()
            {
                Id = request.Id,
                Date = request.Date.ToDateTime(),
                TemperatureC = request.TemperatureC
            };

            await _weatherForecastService.Save(data);

            return await Task.FromResult(new WeatherForecastEdgeResponse() { Success = true });
        }
    }
}
