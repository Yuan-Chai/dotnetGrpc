using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.SignalR;

namespace dotnetGrpc.Hubs
{
    public class WeatherForecastHub : Hub
    {
        protected IHubContext<WeatherForecastHub> _context;

        public WeatherForecastHub(IHubContext<WeatherForecastHub> context)
        {
            _context = context;
        }
        public async Task NewForecast(WeatherForecast forecast)
        {
            await _context.Clients.All.SendAsync("forecastReceived", forecast);
        }
    }
}
