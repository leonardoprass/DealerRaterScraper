using DealerRaterScraper.Application.Scraper;
using DealerRaterScraper.Domain;
using DealerRaterScraper.Domain.Enums;
using HtmlAgilityPack;
using System.Text;
using Xunit;

namespace DealerRaterScraper.Test
{
    public class DealerRaterIntegrationTest
    {
        [Fact]
        public void ShouldScrapeFiveStarReview()
        {
            var review = new ReviewItem()
            {
                Date = "November 29, 2021",
                DealershipRating = 5,
                ServiceType = ServiceTypes.SalesVisitNew,
                Content = @"Great service!  No hassle, honest communication, and no back and forth fake bargaining experience.  We’ll be back here for our next vehicle purchase.",
                Reviewer = "by Happy Buyer",
                AverageServiceRating = 5,
                AverageEmployeesRating = 5,
                RecommendDealer = true
            };

            var doc = new HtmlDocument();
            doc.Load("./ReviewSamples/FiveStarReviewEntry.Html", Encoding.UTF8);
            var dataScraperService = new DataScraperService();

            //act
            var result = dataScraperService.GetReviewDataFromNode(doc.DocumentNode.SelectSingleNode("//div[contains(@class, 'review-entry')]"));

            //assert
            Assert.Equal(review.Date, result.Date);
            Assert.Equal(review.DealershipRating, result.DealershipRating);
            Assert.Equal(review.ServiceType, result.ServiceType);
            Assert.Equal(review.Content, result.Content);
            Assert.Equal(review.Reviewer, result.Reviewer);
            Assert.Equal(review.AverageEmployeesRating, result.AverageEmployeesRating);
            Assert.Equal(review.AverageServiceRating, result.AverageServiceRating);
            Assert.Equal(review.RecommendDealer, result.RecommendDealer);
        }

        [Fact]
        public void ShouldScrapeFourStarReview()
        {
            var review = new ReviewItem()
            {
                Date = "November 16, 2021",
                DealershipRating = 4,
                ServiceType = ServiceTypes.SalesVisitNew,
                Content = @"Adrian is excellent. I didn’t get the exact deal I wanted, but close enough to make it work. I drove from Henderson and would do it again. I highly recommend you do the same.",
                Reviewer = "by randyrandel",
                AverageServiceRating = 4.5f,
                AverageEmployeesRating = 4f,
                RecommendDealer = true
            };

            var doc = new HtmlDocument();
            doc.Load("./ReviewSamples/FourStarReviewEntry.Html", Encoding.UTF8);
            var dataScraperService = new DataScraperService();

            //act
            var result = dataScraperService.GetReviewDataFromNode(doc.DocumentNode.SelectSingleNode("//div[contains(@class, 'review-entry')]"));

            //assert
            Assert.Equal(review.Date, result.Date);
            Assert.Equal(review.DealershipRating, result.DealershipRating);
            Assert.Equal(review.ServiceType, result.ServiceType);
            Assert.Equal(review.Content, result.Content);
            Assert.Equal(review.Reviewer, result.Reviewer);
            Assert.Equal(review.AverageServiceRating, result.AverageServiceRating);
            Assert.Equal(review.AverageEmployeesRating, result.AverageEmployeesRating);
            Assert.Equal(review.RecommendDealer, result.RecommendDealer);
        }

        [Fact]
        public void ShouldScrapeFiveStarServiceVisitReview()
        {
            var review = new ReviewItem()
            {
                Date = "January 15, 2022",
                DealershipRating = 5,
                ServiceType = ServiceTypes.ServiceVisit,
                Content = @"I came in for an appointment Thursday morning for a transmission flush service. Mariela Hernandez got my truck checked in pretty quickly. When they where all done she gave me a call to come pick it up when I had time.  Overall great job she did at getting me in and great job on the transmission flush I can tell that it made a big difference bc it shifts an runs a lot smoother than previously.  Thanks you guys again for taking care of me and my vehicle.",
                Reviewer = "by Richard Irvine",
                AverageServiceRating = 5,
                AverageEmployeesRating = 5,
                RecommendDealer = true
            };

            var doc = new HtmlDocument();
            doc.Load("./ReviewSamples/FiveStarServiceReviewEntry.Html", Encoding.UTF8);
            var dataScraperService = new DataScraperService();

            //act
            var result = dataScraperService.GetReviewDataFromNode(doc.DocumentNode.SelectSingleNode("//div[contains(@class, 'review-entry')]"));

            //assert
            Assert.Equal(review.Date, result.Date);
            Assert.Equal(review.DealershipRating, result.DealershipRating);
            Assert.Equal(review.ServiceType, result.ServiceType);
            Assert.Equal(review.Content, result.Content);
            Assert.Equal(review.Reviewer, result.Reviewer);
            Assert.Equal(review.AverageEmployeesRating, result.AverageEmployeesRating);
            Assert.Equal(review.AverageServiceRating, result.AverageServiceRating);
            Assert.Equal(review.RecommendDealer, result.RecommendDealer);
        }

    }
}