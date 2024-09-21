using ColengoChallenge.Api.Features.Products;
using ColengoChallenge.Domain.Contacts;
namespace ColengoChallenge.Domain.Interfaces
{
    public interface IProductRepository
    {
        Task<GetProductResponse> GetProducts(GetProductRequest getProductRequest);
        Task AddProductsAsync(List<ProductPayload> products);
        Task SaveChangesAsync();
    }
}
