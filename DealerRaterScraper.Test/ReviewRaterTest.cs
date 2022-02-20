using DealerRaterScraper.Application.Scraper;
using DealerRaterScraper.Domain;
using DealerRaterScraper.Domain.Enums;
using HtmlAgilityPack;
using System.Text;
using Xunit;

namespace DealerRaterScraper.Test
{
    public class ReviewRaterTest
    {
        [Fact]
        public void FiveStarReviewShouldBeGreaterThan4Star()
        {
            //criteria
            /*
             * DealershipRating * 2 +
             * AverageServiceRating +
             * AverageEmployeeRating +
             * RecommendedDealer => +5
             * Service type =>
             *   SalesVisit => +1
             *   SalesVisitUsed => +2
             *   SalesVisitNew => +3
             */
            var fourStarReview = new ReviewItem()
            {
                Date = "November 16, 2021",
                DealershipRating = 4,
                ServiceType = ServiceTypes.SalesVisitNew,
                Content = @"Adrian is excellent. I didn稚 get the exact deal I wanted, but close enough to make it work. I drove from Henderson and would do it again. I highly recommend you do the same.",
                Reviewer = "by randyrandel",
                AverageServiceRating = 4.5f,
                AverageEmployeesRating = 4,
                RecommendDealer = true
            };

            var fireStarReview = new ReviewItem()
            {
                Date = "November 29, 2021",
                DealershipRating = 5,
                ServiceType = ServiceTypes.SalesVisitNew,
                Content = @"Great service!  No hassle, honest communication, and no back and forth fake bargaining experience.  We値l be back here for our next vehicle purchase.",
                Reviewer = "by Happy Buyer",
                AverageServiceRating = 5,
                AverageEmployeesRating = 5,
                RecommendDealer = true
            };

            Assert.True(fireStarReview.Score > fourStarReview.Score);
        }

        [Fact]
        public void SalesNewVisitShouldBeGreaterThanServiceVisit()
        {
            var salesVisitReview = new ReviewItem()
            {
                Date = "November 29, 2021",
                DealershipRating = 5,
                ServiceType = ServiceTypes.SalesVisitNew,
                Content = @"Great service!  No hassle, honest communication, and no back and forth fake bargaining experience.  We値l be back here for our next vehicle purchase.",
                Reviewer = "by Happy Buyer",
                AverageServiceRating = 5,
                AverageEmployeesRating = 5,
                RecommendDealer = true
            };

            var serviceVisitReview = new ReviewItem()
            {
                Date = "November 29, 2021",
                DealershipRating = 5,
                ServiceType = ServiceTypes.ServiceVisit,
                Content = @"Great service!  No hassle, honest communication, and no back and forth fake bargaining experience.  We値l be back here for our next vehicle purchase.",
                Reviewer = "by Happy Buyer",
                AverageServiceRating = 5,
                AverageEmployeesRating = 5,
                RecommendDealer = true
            };

            Assert.True(salesVisitReview.Score > serviceVisitReview.Score);
        }

        [Fact]
        public void MaxScoreReviewShouldBe28()
        {
            var review = new ReviewItem()
            {
                Date = "November 29, 2021",
                DealershipRating = 5,
                ServiceType = ServiceTypes.SalesVisitNew,
                Content = @"Great service!  No hassle, honest communication, and no back and forth fake bargaining experience.  We値l be back here for our next vehicle purchase.",
                Reviewer = "by Happy Buyer",
                AverageServiceRating = 5,
                AverageEmployeesRating = 5,
                RecommendDealer = true
            };

            Assert.Equal(28, review.Score);
        }

    }
}