namespace ColengoChallenge.Api.Features.Products
{
    public class GetProductRequest
    {
        public string Route = "https://tabledusud.nl/_product/simpleFilters";
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string? ProductTitle { get; set; }
    }
}
