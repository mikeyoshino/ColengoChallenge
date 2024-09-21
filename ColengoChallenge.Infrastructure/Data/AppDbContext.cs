using ColengoChallenge.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ColengoChallenge.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<FullPriceBeforeOverallDiscount> FullPriceBeforeOverallDiscounts { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<OriginalPrice> OriginalPrices { get; set; }
        public DbSet<PossibleDiscountPrice> PossibleDiscountPrices { get; set; }
        public DbSet<Price> Prices { get; set; }
        public DbSet<Tag> Tags { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
    }
}
