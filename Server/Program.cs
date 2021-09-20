using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace dotnetGrpc
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    // MACOS doesn't support tls over http2
                    // 5001 over https for webapi
                    // 5000 over http for grpc
                    if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                    {
                        webBuilder.ConfigureKestrel(options =>
                        {
                            options.ListenLocalhost(5001, o =>
                            {
                                o.Protocols = Microsoft.AspNetCore.Server.Kestrel.Core.HttpProtocols.Http1AndHttp2;
                                o.UseHttps();
                            });
                            options.ListenLocalhost(5000, o => o.Protocols = Microsoft.AspNetCore.Server.Kestrel.Core.HttpProtocols.Http2);
                        });
                    }
                    webBuilder.UseStartup<Startup>();
                });
    }
}
