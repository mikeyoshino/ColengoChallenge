namespace ColengoChallenge.Api.Features.Products
{
    public class GetDemoProductsRequest
    {
        public string Route = "https://tabledusud.nl/_product/simpleFilters";
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
