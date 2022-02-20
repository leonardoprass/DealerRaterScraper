using DealerRaterScraper.Application.Scraper;
using DealerRaterScraper.Client;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;

var services = new ServiceCollection();

Stopwatch stopwatch = new Stopwatch();
stopwatch.Start();

Startup startup = new Startup();
startup.ConfigureServices(services);
IServiceProvider serviceProvider = services.BuildServiceProvider();
try
{
    var items = await serviceProvider
        .GetService<IHtmlScraperService>()
        .ScrapeReviewsAsync();

    stopwatch.Stop();

    TimeSpan ts = stopwatch.Elapsed;

    Console.WriteLine(items.Count.ToString());
    Console.WriteLine("Elapsed Time is {0:00}:{1:00}:{2:00}.{3}",
                    ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds);
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}