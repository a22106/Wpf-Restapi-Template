// WpfWebApiHost.App.xaml.cs
using Microsoft.AspNetCore;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using System.Data;
using System.Windows;
using WpfWebApi;

namespace WpfWebApiHost
{
    public partial class App : Application
    {
        private const string hosting = "Hosting:Urls";
        private readonly IHost webHost;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var host = Host.CreateDefaultBuilder(e.Args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<WpfWebApi.Startup>();
                })
                .Build();

            host.RunAsync(); // 백그라운드에서 ASP.NET Core 호스트 실행
        }

        private IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<WpfWebApi.Startup>();
                    webBuilder.ConfigureAppConfiguration((hostingContext, config) =>
                    {
                        var settings = config.Build();
                        webBuilder.UseUrls(urls: settings[hosting]);
                    });
                });

        protected override void OnExit(ExitEventArgs e)
        {
            webHost?.Dispose();
            base.OnExit(e);
        }
    }
}
