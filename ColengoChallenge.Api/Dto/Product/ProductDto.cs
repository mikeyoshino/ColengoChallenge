namespace ColengoChallenge.Api.Dto
{
    public class ProductDto
    {
        public int ProductId { get; set; }
        public string? Name { get; set; }
        public string? Title { get; set; }
        public string? ThumbnailImage { get; set; }
        public PriceDto? Price { get; set; }
        public PriceDto? OriginalPrice { get; set; }
        public BrandDto? Brand { get; set; }
        public int Sold { get; set; }
        public bool AllowMultipleConfigs { get; set; }
        public string? Url { get; set; }
        public DateTime Created { get; set; }
        public DateTime? OverallCampaignEndDate { get; set; }
        public double ReviewScore { get; set; }
        public int ReviewCount { get; set; }
        public bool Has3DAssets { get; set; }
        public PriceDto? FullPriceBeforeOverallDiscount { get; set; }
        public PriceDto? PossibleDiscountPrice { get; set; }
        public object? Layout { get; set; }
        public object? Location { get; set; }
        public List<CategoryDto>? Categories { get; set; }
        public List<TagDto>? Tags { get; set; }
        public List<ImageDto>? Images { get; set; }
        public Dictionary<int, List<ImageDto>>? ImageTaxonomyTags { get; set; }
    }
}
