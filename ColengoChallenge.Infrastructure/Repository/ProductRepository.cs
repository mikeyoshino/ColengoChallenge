using ColengoChallenge.Api.Dto;
using ColengoChallenge.Api.Features.Products;
using ColengoChallenge.Domain.Contacts;
using ColengoChallenge.Domain.Interfaces;
using ColengoChallenge.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ColengoChallenge.Infrastructure.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _dbContext;

        public ProductRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<GetProductResponse> GetProducts(GetProductRequest getProductRequest)
        {
            // Get total number of items
            var totalItems = await _dbContext.Products.CountAsync();

            // Calculate total pages
            var totalPages = (int)Math.Ceiling(totalItems / (double)getProductRequest.PageSize);

            // Fetch the products for the given page
            var products = await _dbContext.Products
                .OrderBy(p => p.Id) // Order by a specific column (e.g., Id) for consistent paging
                .Skip((getProductRequest.Page - 1) * getProductRequest.PageSize) // Skip previous pages
                .Take(getProductRequest.PageSize) // Take the items for the current page
                .ToListAsync();

            // Manually map Product to ProductDto
            var productDtos = products.Select(p => new ProductDto
            {
                Sold = p.Sold,
                AllowMultipleConfigs = p.AllowMultipleConfigs,
                Created = p.Created,
                Name = p.Name,
                Url = p.Url,
                Title = p.Title,
                ThumbnailImage = p.ThumbnailImage,
                Has3DAssets = p.Has3DAssets,
                ReviewScore = p.ReviewScore,
                ReviewCount = p.ReviewCount,
                OverallCampaignEndDate = p.OverallCampaignEndDate,
                // Map any other relevant properties if needed
            }).ToList();

            // Return the response
            return new GetProductResponse
            {
                TotalItems = totalItems,
                TotalPages = totalPages,
                PageSize = getProductRequest.PageSize,
                Products = productDtos
            };
        }


        public async Task AddProductsAsync(List<ProductPayload> productPayloads)
        {
            // Add all products
            if(productPayloads != null)
            {

                foreach (var eachPayload in productPayloads)
                {
                    if (eachPayload.Product == null)
                        continue;

                    await _dbContext.Products.AddAsync(eachPayload.Product);
                    await _dbContext.SaveChangesAsync();

                    if (eachPayload.Brand == null)
                        continue;
                    eachPayload.Brand.ProductId = eachPayload.Product.Id;
                    await _dbContext.Brands.AddAsync(eachPayload.Brand);

                    if (eachPayload.Price == null)
                        continue;
                    eachPayload.Price.ProductId = eachPayload.Product.Id;
                    await _dbContext.Prices.AddAsync(eachPayload.Price);

                    if (eachPayload.PossibleDiscountPrice == null)
                        continue;
                    eachPayload.PossibleDiscountPrice.ProductId = eachPayload.Product.Id;
                    await _dbContext.PossibleDiscountPrices.AddAsync(eachPayload.PossibleDiscountPrice);

                    if (eachPayload.OriginalPrice == null)
                        continue;
                    eachPayload.OriginalPrice.ProductId = eachPayload.Product.Id;
                    await _dbContext.OriginalPrices.AddAsync(eachPayload.OriginalPrice);

                    if (eachPayload.FullPriceBeforeOverallDiscount == null)
                        continue;
                    eachPayload.FullPriceBeforeOverallDiscount.ProductId = eachPayload.Product.Id;
                    await _dbContext.FullPriceBeforeOverallDiscounts.AddAsync(eachPayload.FullPriceBeforeOverallDiscount);

                    if (eachPayload.Tags == null)
                        continue;
                    foreach (var eachTag in eachPayload.Tags)
                    {
                        eachTag.ProductId = eachPayload.Product.Id;
                        await _dbContext.Tags.AddAsync(eachTag);
                    }
                    if (eachPayload.Images == null)
                        continue;
                    foreach (var eachImage in eachPayload.Images)
                    {
                        eachImage.ProductId = eachPayload.Product.Id;
                        await _dbContext.Images.AddAsync(eachImage);
                    }
                    if (eachPayload.Categories == null)
                        continue;
                    foreach (var eachCate in eachPayload.Categories)
                    {
                        eachCate.ProductId = eachPayload.Product.Id;
                        await _dbContext.Categories.AddAsync(eachCate);
                    }

                }
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
