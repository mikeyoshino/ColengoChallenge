namespace ColengoChallenge.Api.Features.Products
{
    public class GetProductRequest
    {
        public string Route = "/api/product/get-products";
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? Title { get; set; }
        public string? Sort { get; set; }
    }
}
