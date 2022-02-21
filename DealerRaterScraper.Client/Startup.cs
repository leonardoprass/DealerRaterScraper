using DealerRaterScraper.Application.Ranking;
using DealerRaterScraper.Application.Scraper;
using DealerRaterScraper.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DealerRaterScraper.Client
{
    public class Startup
    {
        IConfigurationRoot Configuration { get; }

        public Startup()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json");

            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<ConfigurationSettings>(Configuration.GetSection(nameof(ConfigurationSettings)));

            services
             .AddScoped<IDataScraperService, DataScraperService>()
             .AddScoped<IHtmlScraperService, HtmlScraperService>()
             .AddScoped<INlpService, NlpService>()
             .BuildServiceProvider();
        }
    }
}
