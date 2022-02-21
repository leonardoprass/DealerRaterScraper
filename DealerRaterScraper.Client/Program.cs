using DealerRaterScraper.Application.Ranking;
using DealerRaterScraper.Application.Scraper;
using DealerRaterScraper.Client;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();

Startup startup = new Startup();
startup.ConfigureServices(services);

IServiceProvider serviceProvider = services.BuildServiceProvider();

try
{
    var items = await serviceProvider
        .GetService<IHtmlScraperService>()
        .ScrapeReviewsAsync();

    var topReviews = serviceProvider
        .GetService<INlpService>()
        .Analyze(items);

    foreach (var item in topReviews)
    {
        Console.WriteLine($"Date: {item.Date}");
        Console.WriteLine($"Reason: {item.ServiceType}");
        Console.WriteLine($"Review: {item.Content}");
        Console.WriteLine($"Dealership Rating: {item.DealershipRating}");
        Console.WriteLine($"Average Employees Rating: {item.AverageEmployeesRating}");
        Console.WriteLine($"Average Service Rating: {item.AverageServiceRating}");
        Console.WriteLine($"Reviewed {item.Reviewer}");
        Console.WriteLine();
    }
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}