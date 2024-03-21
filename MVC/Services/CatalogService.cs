using Microsoft.AspNetCore.Mvc.RazorPages;
using MVC.Dtos;
using MVC.Models.Enums;
using MVC.Services.Interfaces;
using MVC.ViewModels;

namespace MVC.Services;

public class CatalogService : ICatalogService
{
    private readonly IOptions<AppSettings> _settings;
    private readonly IHttpClientService _httpClient;
    private readonly ILogger<CatalogService> _logger;

    public CatalogService(IHttpClientService httpClient, ILogger<CatalogService> logger, IOptions<AppSettings> settings)
    {
        _httpClient = httpClient;
        _settings = settings;
        _logger = logger;
    }

    public async Task<Catalog> GetCatalogItems(int page, int take, int? brand, int? type)
    {
        var filters = new Dictionary<CatalogTypeFilter, int>();

        if (brand.HasValue)
        {
            filters.Add(CatalogTypeFilter.Brand, brand.Value);
        }
        
        if (type.HasValue)
        {
            filters.Add(CatalogTypeFilter.Type, type.Value);
        }
        
        var result = await _httpClient.SendAsync<Catalog, PaginatedItemsRequest<CatalogTypeFilter>>($"{_settings.Value.CatalogUrl}/items",
           HttpMethod.Post, 
           new PaginatedItemsRequest<CatalogTypeFilter>()
            {
                PageIndex = page,
                PageSize = take,
                Filters = filters
            });

        return result;
    }

    public async Task<IEnumerable<SelectListItem>> GetBrands(int page, int take)
    {
        var result = await _httpClient.SendAsync<IEnumerable<CatalogBrand>, object>($"{_settings.Value.CatalogUrl}/brands", 
            HttpMethod.Post,
            new PaginatedItemsRequest<CatalogTypeFilter>()
            {
                PageIndex = page,
                PageSize = take
            });

        return result.Select(x => new SelectListItem()
        {
            Value = x.Id.ToString(),
            Text = x.Brand
        });
    }

    public async Task<IEnumerable<SelectListItem>> GetTypes(int page, int take)
    {
        var result = await _httpClient.SendAsync<IEnumerable<CatalogType>, object>($"{_settings.Value.CatalogUrl}/types",
            HttpMethod.Post,
            new PaginatedItemsRequest<CatalogTypeFilter>()
            {
                PageIndex = page,
                PageSize = take
            });

        return result.Select(x => new SelectListItem()
        {
            Value = x.Id.ToString(),
            Text = x.Type
        });
    }
}
