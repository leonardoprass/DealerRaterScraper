namespace DealerRaterScraper.Domain
{
    public class ReviewItem
    {
        public string Date { get; set; }
        public int DealershipRating { get; set; }
        public string ServiceType { get; set; }
        public string Content { get; set; }
        public string Reviewer { get; set; }
        public float AverageServiceRating { get; set; }
        public bool RecommendDealer { get; set; }
    }
}
