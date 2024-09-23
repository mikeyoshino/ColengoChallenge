namespace ColengoChallenge.Api.Features.Products
{
    public class GetProductRequest
    {
        public string Route = "/api/product/get-products";
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string? ProductTitle { get; set; }
    }
}
