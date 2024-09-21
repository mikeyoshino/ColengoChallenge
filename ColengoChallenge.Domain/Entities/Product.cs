namespace ColengoChallenge.Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Title { get; set; }
        public string? ThumbnailImage { get; set; }
        public Brand? Brand { get; set; }
        public int Sold { get; set; }
        public bool AllowMultipleConfigs { get; set; }
        public string? Url { get; set; }
        public DateTime Created { get; set; }
        public DateTime? OverallCampaignEndDate { get; set; }
        public double ReviewScore { get; set; }
        public int ReviewCount { get; set; }
        public bool Has3DAssets { get; set; }

        public Price? Price { get; set; }
        public PossibleDiscountPrice? PossibleDiscountPrice { get; set; }
        public OriginalPrice? OriginalPrice { get; set; }
        public FullPriceBeforeOverallDiscount? FullPriceBeforeOverallDiscount { get; set; }
        public ICollection<Category>? Categories { get; set; }
        public ICollection<Tag>? Tags { get; set; }
        public ICollection<Image>? Images { get; set; }
    }
}
