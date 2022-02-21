using DealerRaterScraper.Application.Ranking;
using DealerRaterScraper.Domain;
using DealerRaterScraper.Domain.Enums;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace DealerRaterScraper.Test
{
    public class NlpServiceTest
    {
        [Fact]
        public void NlpShouldMapContentWords()
        {
            var topReview = new ReviewItem()
            {
                Date = "November 29, 2021",
                DealershipRating = 5,
                ServiceType = ServiceTypes.SalesVisitNew,
                Content = @"Amazing incredible good good great great",
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
                Content = @"Great service!  No hassle, honest communication, and no back and forth fake bargaining experience.  We’ll be back here for our next vehicle purchase.",
                Reviewer = "by aaa",
                AverageServiceRating = 5,
                AverageEmployeesRating = 5,
                RecommendDealer = true
            };

            var fiveStarReview3 = new ReviewItem()
            {
                Date = "November 29, 2021",
                DealershipRating = 5,
                ServiceType = ServiceTypes.SalesVisitNew,
                Content = @"Great service!  No hassle, honest communication, and no back and forth fake bargaining experience.  We’ll be back here for our next vehicle purchase.",
                Reviewer = "by bbb",
                AverageServiceRating = 5,
                AverageEmployeesRating = 5,
                RecommendDealer = true
            };

            var fiveStarReview4 = new ReviewItem()
            {
                Date = "November 29, 2021",
                DealershipRating = 5,
                ServiceType = ServiceTypes.SalesVisitNew,
                Content = @"Great service!  No hassle, honest communication, and no back and forth fake bargaining experience.  We’ll be back here for our next vehicle purchase.",
                Reviewer = "by ccc",
                AverageServiceRating = 5,
                AverageEmployeesRating = 5,
                RecommendDealer = true
            };

            var reviewRaterService = new NlpService();
            var result = reviewRaterService.Analyze(new List<ReviewItem> { topReview, fiveStarReview2, fiveStarReview3, fiveStarReview4 });

            Assert.Equal(topReview, result.ElementAt(0));
            Assert.Equal(fiveStarReview2, result.ElementAt(1));
            Assert.Equal(fiveStarReview3, result.ElementAt(2));
        }
    }
}