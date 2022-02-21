using DealerRaterScraper.Common.Helper;
using DealerRaterScraper.Domain;
using DealerRaterScraper.Domain.Enums;
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
            var generalInfoNode = review.SelectSingleNode(".//div[contains(@class, 'review-date')]");
            var dealershipRating = generalInfoNode.SelectSingleNode(".//div[contains(@class, 'rating-static')]");

            var bodyNode = review.SelectSingleNode(".//div[contains(@class, 'review-wrapper')]");
            var titleNode = bodyNode.SelectSingleNode(".//span[contains(@class, 'review-title')]");
            var reviewWholeNode = bodyNode.SelectSingleNode(".//span[contains(@class, 'review-whole')]");

            var starEvaluationsNodes = review.SelectNodes(".//div[contains(@class, 'rating-static-indv')]");
            var employeesNode = review.SelectSingleNode(".//div[contains(@class, 'employees-wrapper')]");
            var employeesEvaluationsNodes = employeesNode.SelectNodes(".//div[contains(@class, 'rating-static')]");

            var reviewBody = $"{titleNode.InnerText.Trim()} {reviewWholeNode.InnerText.Trim()}";
            var reviewedBy = StringHelper.NormalizeText(bodyNode.SelectSingleNode(".//span[contains(@class, 'font-16')]").InnerText).FirstOrDefault();
            var generalInfo = StringHelper.NormalizeText(generalInfoNode.InnerText);

            float averageServiceRating = GetAverageRating(starEvaluationsNodes);
            float averageEmployeesRating = GetAverageRating(employeesEvaluationsNodes);

            var dealerRecommended = StringHelper.NormalizeText(review.SelectSingleNode(".//div[@class='td small-text boldest']").InnerText).FirstOrDefault();
            var serviceTypeText = generalInfo.FirstOrDefault(c => c.ToLower().Contains("visit"));
            Enum.TryParse(serviceTypeText?.Replace("-", string.Empty).Replace(" ", string.Empty), ignoreCase: true, out ServiceTypes serviceType);

            return new ReviewItem
            {
                AverageEmployeesRating = averageEmployeesRating,
                AverageServiceRating = averageServiceRating,
                Content = reviewBody,
                Date = generalInfo.ElementAt(0),
                DealershipRating = GetRating(dealershipRating.GetClasses()),
                RecommendDealer = dealerRecommended?.ToLower() == "yes",
                Reviewer = reviewedBy ?? string.Empty,
                ServiceType = serviceType
            };
        }

        private static float GetAverageRating(HtmlNodeCollection evaluationNodes)
        {
            if (evaluationNodes == null || !evaluationNodes.Any())
                return 0;

            float averageServiceRating = evaluationNodes.Sum(t => GetRating(t.GetClasses()));

            return averageServiceRating / evaluationNodes.Count;
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
    }
}
