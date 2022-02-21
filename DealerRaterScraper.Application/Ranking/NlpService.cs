using DealerRaterScraper.Common.Helper;
using DealerRaterScraper.Domain;

namespace DealerRaterScraper.Application.Ranking
{
    public interface INlpService
    {
        IEnumerable<ReviewItem> Analyze(IEnumerable<ReviewItem> reviews);
    }

    public class NlpService : INlpService
    {
        private IEnumerable<string> positiveWords;
        private IEnumerable<string> negativeWords;

        public IEnumerable<ReviewItem> Analyze(IEnumerable<ReviewItem> reviews)
        {
            reviews = reviews.OrderBy(r => r.Score).ThenBy(r => r.Reviewer);
            CreateWordsBags(reviews.OrderBy(r => r.Score));

            var result = new List<NlpScoredReview>();

            foreach (var review in reviews)
            {
                var wordsFromPhrase = StringHelper.GetWords(review.Content);
                
                var score = wordsFromPhrase.Where(t =>
                    positiveWords.Contains(t)
                ).Count();

                score -= wordsFromPhrase.Where(t =>
                    negativeWords.Contains(t)
                ).Count();

                result.Add(new NlpScoredReview
                {
                    NlpScore = score,
                    Review = review
                });
            }

            var mostPositiveScore = result.OrderByDescending(t => t.NlpScore).ThenBy(r => r.Review.Reviewer);
            return mostPositiveScore.Select(s => s.Review).Take(3);
        }

        private void CreateWordsBags(IEnumerable<ReviewItem> orderedReviews)
        {
            int half = orderedReviews.Count() / 2;

            negativeWords = MapWords(orderedReviews.Take(half));
            positiveWords = MapWords(orderedReviews.Skip(half)); 
        }

        private static IEnumerable<string> MapWords(IEnumerable<ReviewItem> orderedReviews)
        {
            return orderedReviews
                            .SelectMany(
                                r => StringHelper.GetWords(r.Content)
                            ).Select(t => StringHelper.RemoveSpecialCharacters(t));
        }
    }
}
