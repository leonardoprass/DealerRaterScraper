using DealerRaterScraper.Application.Ranking;
using DealerRaterScraper.Domain;
using DealerRaterScraper.Domain.Enums;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace DealerRaterScraper.Test
{
    public class ReviewRaterTest
    {
        private readonly Mock<INlpService> _nlpServiceMock;
        public ReviewRaterTest()
        {
            _nlpServiceMock = new Mock<INlpService>();
        }

        [Fact]
        public void ShouldRankBasedOnScore()
        {
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

            var fiveStarReview = new ReviewItem()
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

            var reviewRaterService = new ReviewRaterService(_nlpServiceMock.Object);
            var rankedReviews = reviewRaterService.GetTopReviews(new List<ReviewItem> { fourStarReview, fiveStarReview, serviceVisitReview });

            Assert.Equal(fiveStarReview, rankedReviews.ElementAt(0));
            Assert.Equal(serviceVisitReview, rankedReviews.ElementAt(1));
            Assert.Equal(fourStarReview, rankedReviews.ElementAt(2));
        }

        [Fact]
        public void TiedScoresShouldCallNlp()
        {
            var fiveStarReview = new ReviewItem()
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

            var fiveStarReview2 = new ReviewItem()
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

            var fiveStarReview3 = new ReviewItem()
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

            var fiveStarReview4 = new ReviewItem()
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

            var reviewRaterService = new ReviewRaterService(_nlpServiceMock.Object);
            reviewRaterService.GetTopReviews(new List<ReviewItem> { fiveStarReview, fiveStarReview2, fiveStarReview3, fiveStarReview4 });

            _nlpServiceMock.Verify(m => m.Analyze(It.IsAny<List<ReviewItem>>()), Times.Once());
        }
    }
}