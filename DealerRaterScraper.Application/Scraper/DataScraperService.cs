using DealerRaterScraper.Domain;
using HtmlAgilityPack;

namespace DealerRaterScraper.Application.Scraper
{
    public interface IDataScraperService
    {
        ReviewItem GetReviewDataFromNode(HtmlNode review);
    }

    public class DataScraperService : IDataScraperService
    {
        public ReviewItem GetReviewDataFromNode(HtmlNode review)
        {
            var reviewDateNode = review.SelectSingleNode(".//div[contains(@class, 'review-date')]");
            var dealershipRating = reviewDateNode.SelectSingleNode(".//div[contains(@class, 'rating-static')]");
            var generalInfo = NormalizeText(reviewDateNode.InnerText);

            var body = review.SelectSingleNode(".//div[contains(@class, 'review-wrapper')]");
            var title = body.SelectSingleNode(".//span[contains(@class, 'review-title')]");
            var reviewWhole = body.SelectSingleNode(".//span[contains(@class, 'review-whole')]");

            var reviewBody = $"{title.InnerText.Trim()} {reviewWhole.InnerText.Trim()}";
            var reviewedBy = NormalizeText(body.SelectSingleNode(".//span[contains(@class, 'font-16')]").InnerText).FirstOrDefault();

            var starEvaluations = review.SelectNodes(".//div[contains(@class, 'rating-static-indv')]");

            float averageRating = starEvaluations.Sum(t => GetRating(t.GetClasses()));
            averageRating /= starEvaluations.Count;

            var dealerRecommended = NormalizeText(review.SelectSingleNode(".//div[@class='td small-text boldest']").InnerText).FirstOrDefault();

            return new ReviewItem
            {
                AverageServiceRating = averageRating,
                Content = reviewBody,
                Date = generalInfo.ElementAt(0),
                DealershipRating = GetRating(dealershipRating.GetClasses()),
                RecommendDealer = dealerRecommended?.ToLower() == "yes",
                Reviewer = reviewedBy ?? string.Empty,
                ServiceType = generalInfo.ElementAt(1)
            };
        }

        private static int GetRating(IEnumerable<string> classes)
        {
            if (classes.Any(t => t == "rating-50"))
                return 5;
            else if (classes.Any(t => t == "rating-40"))
                return 4;
            else if (classes.Any(t => t == "rating-30"))
                return 3;
            else if (classes.Any(t => t == "rating-20"))
                return 2;
            else if (classes.Any(t => t == "rating-10"))
                return 1;

            return 0;
        }

        private static IEnumerable<string> NormalizeText(string text)
        {
            //clean whitespaces and line breaks to only get data with actual text
            return text.Replace("\n", "").Replace("\r", "").Trim().Split("  ").Where(t => t.Length > 0);
        }
    }
}
