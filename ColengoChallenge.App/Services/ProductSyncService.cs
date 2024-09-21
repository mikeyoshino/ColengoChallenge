using AutoMapper;
using ColengoChallenge.Api.Dto;
using ColengoChallenge.Api.Features.Products;
using ColengoChallenge.Domain.Contacts;
using ColengoChallenge.Domain.Entities;
using ColengoChallenge.Domain.Interfaces;
using Newtonsoft.Json;
using System.Text;
using Image = ColengoChallenge.Domain.Entities.Image;

namespace ColengoChallenge.App.Services
{
    public class ProductSyncService : IProductSyncService
    {
        private readonly IProductRepository _productRepository;
        private readonly HttpClient _httpClient;
        private readonly IMapper _mapper;

        public ProductSyncService(IProductRepository productRepository, HttpClient httpClient, IMapper mapper)
        {
            _productRepository = productRepository;
            _httpClient = httpClient;
            _mapper = mapper;
        }

        public async Task SyncProductsAsync(GetDemoProductsRequest request)
        {
            var jsonContent = JsonConvert.SerializeObject(request);

            // Create StringContent for the HTTP body, set the content type to application/json
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(request.Route, content);

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var getDemoProductsResponse = JsonConvert.DeserializeObject<GetDemoProductsResponse>(responseBody);

                // Default ProductDtos to an empty list if it's null
                var productDtos = getDemoProductsResponse?.Products ?? new List<ProductDto>();

                var productPayloads = new List<ProductPayload>();


                foreach (var product in productDtos)
                {
                    // Check if the product is null and skip if it is
                    if (product == null)
                    {
                        continue;
                    }

                    // Create a new Product object
                    var newProduct = new Product
                    {
                        Sold = product.Sold,
                        AllowMultipleConfigs = product.AllowMultipleConfigs,
                        Created = DateTime.UtcNow,
                        Name = product.Name,
                        Url = product.Url,
                        Title = product.Title,
                        ThumbnailImage = product.ThumbnailImage,
                        Has3DAssets = product.Has3DAssets,
                        ReviewScore = product.ReviewScore,
                        ReviewCount = product.ReviewCount,
                        OverallCampaignEndDate = product.OverallCampaignEndDate,
                    };


                    var tags = new List<Tag>();
                    if (product.Tags != null)
                    {
                        foreach (var tag in product.Tags)
                        {
                            tags.Add(new Tag()
                            {
                                CollectionName = tag.CollectionName,
                                Name = tag.Name,
                                ParentName = tag.ParentName,
                                ThumbnailImage = tag.ThumbnailImage,
                                CollectionId = tag.CollectionId,
                                UserIdentifier = tag.UserIdentifier
                            });
                        }
                    }

                    var categories = new List<Category>();
                    if (product.Categories != null)
                    {
                        foreach (var category in product.Categories)
                        {
                            categories.Add(new Category()
                            {
                                Name = category.Name,
                                Title = category.Title
                            });
                        }
                    }
                    var images = new List<Image>();
                    if (product.Images != null)
                    {
                        foreach (var image in product.Images)
                        {
                            images.Add(new Image()
                            {
                                Name = image.Name,
                                Thumbnail = image.Thumbnail,
                                SmallThumbnail = image.SmallThumbnail,
                                Small = image.Small,
                                MediumSmall = image.SmallThumbnail,
                                Description = image.Description,
                                Large = image.Large,
                                Original = image.Original,
                                Alt = image.Alt,
                                Medium = image.Medium,
                                MediumLarge = image.MediumLarge
                            });
                        }
                    }


                    productPayloads.Add(new ProductPayload()
                    {
                        Product = newProduct,
                        Brand = new Brand()
                        {
                            Name = product.Brand?.Name,
                            UserIdentifier = product.Brand?.UserIdentifier,
                        },
                        Categories = categories,
                        Images = images,
                        Tags = tags,
                        Price = new Price()
                        {
                            Amount = product.Price?.Amount,
                            Currency = product.Price?.Currency
                        },
                        PossibleDiscountPrice = new PossibleDiscountPrice()
                        {
                            Amount = product.PossibleDiscountPrice?.Amount,
                            Currency = product.PossibleDiscountPrice?.Currency
                        },
                        OriginalPrice = new OriginalPrice()
                        {
                            Amount = product.OriginalPrice?.Amount,
                            Currency = product.OriginalPrice?.Currency
                        },
                        FullPriceBeforeOverallDiscount = new FullPriceBeforeOverallDiscount()
                        {
                            Amount = product.FullPriceBeforeOverallDiscount?.Amount,
                            Currency = product.FullPriceBeforeOverallDiscount?.Currency
                        }
                    });


                }
                await _productRepository.AddProductsAsync(productPayloads);

            }
            else
            {
                throw new Exception($"Failed to sync products. Status code: {response.StatusCode}");
            }
        }

    }
}
