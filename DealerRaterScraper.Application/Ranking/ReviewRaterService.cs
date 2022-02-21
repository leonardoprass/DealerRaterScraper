using DealerRaterScraper.Domain;

namespace DealerRaterScraper.Application.Ranking
{
    public interface IReviewRaterService
    {
        IEnumerable<ReviewItem> GetTopReviews(IEnumerable<ReviewItem> reviews);
    }

    public class ReviewRaterService : IReviewRaterService
    {
        private readonly INlpService _nlpService;
        public ReviewRaterService(INlpService nlpService)
        {
            _nlpService = nlpService;
        }

        public IEnumerable<ReviewItem> GetTopReviews(IEnumerable<ReviewItem> reviews)
        {
            var gReviews = reviews.Where(r => r != null).GroupBy(r => r.Score).OrderByDescending(r => r.Key);
            var topReviews = gReviews.FirstOrDefault();

            if (topReviews.Count() > 3)
            {
                return _nlpService.Analyze(reviews);
            }

            return gReviews.SelectMany(r => r).Take(3);
        }
    }
}
