using DealerRaterScraper.Common;
using DealerRaterScraper.Domain;
using HtmlAgilityPack;
using Microsoft.Extensions.Options;
using System.Collections;
using System.Text;

namespace DealerRaterScraper.Application.Scraper
{
    public interface IHtmlScraperService
    {
        Task<List<ReviewItem>> ScrapeReviewsAsync();
    }

    public class HtmlScraperService : IHtmlScraperService
    {
        private readonly ConfigurationSettings _configurationSettings;
        private readonly IDataScraperService _dataScraperService;
        public HtmlScraperService(IDataScraperService dataScraperService, IOptions<ConfigurationSettings> options)
        {
            _dataScraperService = dataScraperService;
            _configurationSettings = options.Value;
        }

        public async Task<List<ReviewItem>> ScrapeReviewsAsync()
        {
            var web = new HtmlWeb();
            var result = new List<ReviewItem>();
            var tasks = new List<Task>();

            for (int i = 1; i <= 5; i++)
                tasks.Add(LoadReviewAsync(web, result, i));

            await Task.WhenAll(tasks.ToArray());
            
            return result;
        }

        private async Task LoadReviewAsync(HtmlWeb web, List<ReviewItem> result, int i)
        {
            try
            {
                var document = await web.LoadFromWebAsync(string.Format(_configurationSettings.BaseUrl, i), Encoding.UTF8);
                var reviews = document.DocumentNode.SelectNodes("//div[contains(@class, 'review-entry')]");

                result.AddRange(reviews.Select(
                    r => _dataScraperService.GetReviewDataFromNode(r)
                ));
            }
            catch (Exception ex)
            {
                throw new Exception($"Something wrong reading page {i}. Message {ex.Message}", ex);
            }
        }
    }
}
