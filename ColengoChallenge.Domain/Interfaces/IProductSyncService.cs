using ColengoChallenge.Api.Features.Products;

namespace ColengoChallenge.Domain.Interfaces
{
    public interface IProductSyncService
    {
        Task SyncProductsAsync(GetDemoProductsRequest request);
    }
}
