using ColengoChallenge.Api.Dto;

namespace ColengoChallenge.Api.Features.Products
{
    public class GetDemoProductsResponse
    {
        public int TotalPages { get; set; }
        public int TotalItems { get; set; }
        public int PageSize { get; set; }

        public List<ProductDto>? Products { get; set; }
    }
}
