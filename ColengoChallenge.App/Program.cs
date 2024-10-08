using ColengoChallenge.App.Services;
using ColengoChallenge.Domain.Interfaces;
using ColengoChallenge.Infrastructure.Data;
using ColengoChallenge.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace ColengoChallenge.App
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowBlazorClient", policy =>
                {
                    policy.WithOrigins("https://localhost:7055") // Add your Blazor app's URL
                          .AllowAnyMethod()
                          .AllowAnyHeader()
                          .AllowCredentials();
                });
            });
            // Add services to the container.
            builder.Services.AddControllers();

            // Register Swagger services
            builder.Services.AddSwaggerGen();

            var configuration = builder.Configuration;
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddHttpClient<IProductSyncService, ProductSyncService>();
            builder.Services.AddScoped<IProductSyncService, ProductSyncService>();
            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddHttpClient();
            builder.Services.AddHttpClient("ProductApiClient", client =>
            {
                client.BaseAddress = new Uri("https://localhost:7193"); // Your API base URL
            });
            
            var app = builder.Build();
            app.UseCors("AllowBlazorClient");
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                // Enable middleware to serve generated Swagger as a JSON endpoint.
                app.UseSwagger();

                // Enable middleware to serve Swagger UI (HTML, JS, CSS, etc.)
                // at the specified endpoint /swagger.
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Colengo v1");
                });

                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllers();
            app.MapRazorPages();

            app.Run();
        }
    }
}
