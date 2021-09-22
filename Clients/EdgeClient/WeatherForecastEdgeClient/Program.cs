
using System;
using System.Threading.Tasks;

using Grpc.Net.Client;
using dotnetGrpc.Protos.Client;
using Google.Protobuf.WellKnownTypes;
using System.Threading;

namespace WeatherForecastEdgeClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // using http endpoint since MACOS doesn't support tls over http2
            var address = "http://localhost:5000";

            using var channel = GrpcChannel.ForAddress(address);
            var client = new WeatherForecastEdgeService.WeatherForecastEdgeServiceClient(channel);

            while (true)
            {
                var request = new WeatherForecastEdgeRequest()
                {
                    Id = Guid.NewGuid().ToString(),
                    Date = Timestamp.FromDateTime(DateTime.UtcNow),
                    TemperatureC = 12
                };
                var response = await client.ReportAsync(request);
                Console.WriteLine(response.Success.ToString());
                Thread.Sleep(4000);
            }
        }
    }
}
