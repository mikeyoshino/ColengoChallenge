using ColengoChallenge.Domain.Entities;

namespace ColengoChallenge.Domain.Contacts
{
    public class ProductPayload
    {
        public ProductPayload()
        {
            Product = new();
            Brand = new();
            Categories = new();
            Images = new();
            Tags = new ();
            Tags = new();
            PossibleDiscountPrice = new();
            Price = new();
            OriginalPrice = new();
            FullPriceBeforeOverallDiscount = new();
        }
        public Product? Product { get; set; }
        public Brand? Brand { get; set; }
        public List<Category>? Categories { get; set; }
        public List<Image>? Images { get; set; }
        public List<Tag>? Tags { get; set; }
        public PossibleDiscountPrice? PossibleDiscountPrice { get; set; }
        public Price? Price { get; set; }
        public OriginalPrice? OriginalPrice { get; set; }
        public FullPriceBeforeOverallDiscount? FullPriceBeforeOverallDiscount { get; set; }
    }
}
