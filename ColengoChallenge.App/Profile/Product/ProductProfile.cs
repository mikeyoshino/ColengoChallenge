using AutoMapper;
using Azure;
using ColengoChallenge.Api.Dto;
using ColengoChallenge.Domain.Entities;
using Image = ColengoChallenge.Domain.Entities.Image;

namespace ColengoChallenge.App.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            // Mapping PriceDto to Price and similar classes
            CreateMap<PriceDto, Price>();
            CreateMap<PriceDto, PossibleDiscountPrice>();
            CreateMap<PriceDto, OriginalPrice>();
            CreateMap<PriceDto, FullPriceBeforeOverallDiscount>();

            // Mapping DTOs to Entities
            CreateMap<CategoryDto, Category>();
            CreateMap<TagDto, Tag>();
            CreateMap<ImageDto, Image>();

            // Mapping ProductDto to Product
            CreateMap<ProductDto, Product>()
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => MapPrice(src.Price)))
                .ForMember(dest => dest.PossibleDiscountPrice, opt => opt.MapFrom(src => MapPossibleDiscountPrice(src.PossibleDiscountPrice)))
                .ForMember(dest => dest.OriginalPrice, opt => opt.MapFrom(src => MapOriginalPrice(src.OriginalPrice)))
                .ForMember(dest => dest.FullPriceBeforeOverallDiscount, opt => opt.MapFrom(src => MapFullPriceBeforeOverallDiscount(src.FullPriceBeforeOverallDiscount)))
                .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => MapBrand(src.Brand)))
                .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => MapCategories(src.Categories)))
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => MapTags(src.Tags)))
                .ForMember(dest => dest.Images, opt => opt.MapFrom(src => MapImages(src.Images)));
        }

        private Price MapPrice(PriceDto priceDto) => priceDto == null ? null : new Price
        {
            Amount = priceDto.Amount,
            Currency = priceDto.Currency
        };

        private PossibleDiscountPrice MapPossibleDiscountPrice(PriceDto priceDto) => priceDto == null ? null : new PossibleDiscountPrice
        {
            Amount = priceDto.Amount,
            Currency = priceDto.Currency
        };

        private OriginalPrice MapOriginalPrice(PriceDto priceDto) => priceDto == null ? null : new OriginalPrice
        {
            Amount = priceDto.Amount,
            Currency = priceDto.Currency
        };

        private FullPriceBeforeOverallDiscount MapFullPriceBeforeOverallDiscount(PriceDto priceDto) => priceDto == null ? null : new FullPriceBeforeOverallDiscount
        {
            Amount = priceDto.Amount,
            Currency = priceDto.Currency
        };

        private ICollection<Category> MapCategories(IEnumerable<CategoryDto> categoryDtos) =>
            categoryDtos?.Select(c => new Category
            {
                Name = c.Name,
                Title = c.Title,
                ParentId = c.ParentId // Adjust as needed
            }).ToList();

        private ICollection<Tag> MapTags(IEnumerable<TagDto> tagDtos) =>
            tagDtos?.Select(c => new Tag
            {
                CollectionName = c.CollectionName,
                Name = c.Name,
                ParentName = c.ParentName,
                ThumbnailImage = c.ThumbnailImage,
                CollectionId = c.CollectionId,
                UserIdentifier = c.UserIdentifier
            }).ToList();

        private Brand MapBrand(BrandDto brandDto) => brandDto == null ? null : new Brand
        {
            Name = brandDto.Name,
            UserIdentifier = brandDto.UserIdentifier
            // Map other fields as needed
        };

        private ICollection<Image> MapImages(IEnumerable<ImageDto> imageDtos) =>
            imageDtos?.Select(c => new Image
            {
                Name = c.Name,
                Thumbnail = c.Thumbnail,
                SmallThumbnail = c.SmallThumbnail,
                Small = c.Small,
                MediumSmall = c.SmallThumbnail,
                Description = c.Description,
                Large = c.Large,
                Original = c.Original,
                Alt = c.Alt,
                Medium = c.Medium,
                MediumLarge = c.MediumLarge
            }).ToList();
    }
}
