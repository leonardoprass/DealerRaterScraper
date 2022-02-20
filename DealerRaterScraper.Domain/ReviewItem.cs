using DealerRaterScraper.Domain.Enums;

namespace DealerRaterScraper.Domain
{
    public class ReviewItem
    {
        private float score;
        public string Date { get; set; }
        public int DealershipRating { get; set; }
        public ServiceTypes ServiceType { get; set; }
        public string Content { get; set; }
        public string Reviewer { get; set; }
        public float AverageServiceRating { get; set; }
        public bool RecommendDealer { get; set; }
        public float AverageEmployeesRating { get; set; }

        public float Score
        {
            get {
                score = DealershipRating * 2 + AverageServiceRating + AverageEmployeesRating;

                score += RecommendDealer ? 5 : 0;
                score += (int)ServiceType;

                return score; 
            }
        }
    }
}
