using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using dotnetGrpc.Services;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace dotnetGrpc.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IWeatherForecastService _service;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherForecastService weatherForcastService)
        {
            _logger = logger;
            _service = weatherForcastService;
        }

        [HttpGet]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            return await _service.GetAll();
        }
    }
}
