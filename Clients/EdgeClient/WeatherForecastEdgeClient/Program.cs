
using System;
using System.Threading.Tasks;

using Grpc.Net.Client;
using dotnetGrpc.Protos.Client;
using Google.Protobuf.WellKnownTypes;

namespace WeatherForecastEdgeClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var address = "http://localhost:5000";

            using var channel = GrpcChannel.ForAddress(address);
            var client = new WeatherForecastEdgeService.WeatherForecastEdgeServiceClient(channel);

            var request = new WeatherForecastEdgeRequest()
            {
                Id = Guid.NewGuid().ToString(),
                Date = Timestamp.FromDateTime(DateTime.UtcNow),
                TemperatureC = 12
            };
            var response = await client.ReportAsync(request);
            Console.WriteLine(response.Success.ToString());
            Console.ReadLine();
        }
    }
}
