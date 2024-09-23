using ColengoChallenge.Api.Dto;
using ColengoChallenge.Api.Features.Products;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace ColengoChallenge.App.Views.Product
{
    public class ProductPageModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public ProductPageModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public List<ProductDto> Products { get; set; } = new List<ProductDto>();
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public async Task OnGetAsync(int page = 1)
        {
            CurrentPage = page;
            await LoadProducts();
        }

        public async Task LoadProducts()
        {
            var requestUrl = $"https://localhost:7193/api/product/get-products?page={CurrentPage}&pageSize={PageSize}";

            var response = await _httpClient.GetAsync(requestUrl);
            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<GetProductResponse>(jsonResponse, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                Products = result.Products;
                TotalPages = result.TotalPages;
            }
        }

        public async Task<IActionResult> OnGetChangePage(int page)
        {
            CurrentPage = page;
            await LoadProducts(); // Reload products for the selected page
            return Page();
        }
    }
}
