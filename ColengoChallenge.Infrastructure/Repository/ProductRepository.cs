﻿using ColengoChallenge.Api.Dto;
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
            // Get the IQueryable for products
            var query = _dbContext.Products.Include(x => x.Price).AsQueryable();

            // Search by title if the Title is provided
            if (!string.IsNullOrEmpty(getProductRequest.Title))
            {
                query = query.Where(p => p.Title.Contains(getProductRequest.Title));
            }

            // Sort products by name
            if (!string.IsNullOrEmpty(getProductRequest.Sort))
            {
                if (getProductRequest.Sort.Equals("asc", StringComparison.OrdinalIgnoreCase))
                {
                    query = query.OrderBy(p => p.Name);
                }
                else if (getProductRequest.Sort.Equals("desc", StringComparison.OrdinalIgnoreCase))
                {
                    query = query.OrderByDescending(p => p.Name);
                }
            }
            else
            {
                // Default sorting by Id if no sort option is specified
                query = query.OrderBy(p => p.Id);
            }

            // Get the total number of items after filtering
            var totalItems = await query.CountAsync();

            // Calculate total pages
            var totalPages = (int)Math.Ceiling(totalItems / (double)getProductRequest.PageSize);

            // Fetch the products for the given page with pagination
            var products = await query
                .Skip((getProductRequest.Page - 1) * getProductRequest.PageSize)
                .Take(getProductRequest.PageSize)
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
                Price = new PriceDto()
                {
                    Amount = (double)p.Price.Amount,
                    Currency = p.Price.Currency,
                }
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
            if (productPayloads == null || productPayloads.Count == 0)
                return;

            foreach (var eachPayload in productPayloads)
            {
                if (eachPayload.Product == null)
                    continue;

                // Add product first and save to generate ProductId
                await _dbContext.Products.AddAsync(eachPayload.Product);
                await _dbContext.SaveChangesAsync(); // Save here to generate ProductId

                // Now you can use ProductId for related entities

                if (eachPayload.Brand != null)
                {
                    eachPayload.Brand.ProductId = eachPayload.Product.Id;
                    await _dbContext.Brands.AddAsync(eachPayload.Brand);
                }

                if (eachPayload.Price != null)
                {
                    eachPayload.Price.ProductId = eachPayload.Product.Id;
                    await _dbContext.Prices.AddAsync(eachPayload.Price);
                }

                if (eachPayload.PossibleDiscountPrice != null)
                {
                    eachPayload.PossibleDiscountPrice.ProductId = eachPayload.Product.Id;
                    await _dbContext.PossibleDiscountPrices.AddAsync(eachPayload.PossibleDiscountPrice);
                }

                if (eachPayload.OriginalPrice != null)
                {
                    eachPayload.OriginalPrice.ProductId = eachPayload.Product.Id;
                    await _dbContext.OriginalPrices.AddAsync(eachPayload.OriginalPrice);
                }

                if (eachPayload.FullPriceBeforeOverallDiscount != null)
                {
                    eachPayload.FullPriceBeforeOverallDiscount.ProductId = eachPayload.Product.Id;
                    await _dbContext.FullPriceBeforeOverallDiscounts.AddAsync(eachPayload.FullPriceBeforeOverallDiscount);
                }

                // Add Tags
                if (eachPayload.Tags?.Any() == true)
                {
                    foreach (var tag in eachPayload.Tags)
                    {
                        tag.ProductId = eachPayload.Product.Id;
                    }
                    await _dbContext.Tags.AddRangeAsync(eachPayload.Tags);
                }

                // Add Images
                if (eachPayload.Images?.Any() == true)
                {
                    foreach (var image in eachPayload.Images)
                    {
                        image.ProductId = eachPayload.Product.Id;
                    }
                    await _dbContext.Images.AddRangeAsync(eachPayload.Images);
                }

                // Add Categories
                if (eachPayload.Categories?.Any() == true)
                {
                    foreach (var category in eachPayload.Categories)
                    {
                        category.ProductId = eachPayload.Product.Id;
                    }
                    await _dbContext.Categories.AddRangeAsync(eachPayload.Categories);
                }
            }

            // Save all remaining changes in one batch after processing all entities
            await _dbContext.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
