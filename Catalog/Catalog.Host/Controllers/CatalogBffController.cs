using System.Net;
using Catalog.Host.Configurations;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Enum;
using Catalog.Host.Models.Requests;
using Catalog.Host.Models.Response;
using Catalog.Host.Services.Interfaces;
using Infrastructure;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Catalog.Host.Controllers;

[ApiController]
[Authorize(Policy = AuthPolicy.AllowEndUserPolicy)]
[Route(ComponentDefaults.DefaultRoute)]
public class CatalogBffController : ControllerBase
{
    private readonly ILogger<CatalogBffController> _logger;
    private readonly ICatalogService _catalogService;
    private readonly IOptions<CatalogConfig> _config;

    public CatalogBffController(
        ILogger<CatalogBffController> logger,
        ICatalogService catalogService,
        IOptions<CatalogConfig> config)
    {
        _logger = logger;
        _catalogService = catalogService;
        _config = config;
    }

    [HttpPost]
    [AllowAnonymous]
    [ProducesResponseType(typeof(PaginatedItemsResponse<CatalogItemDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Items(PaginatedItemsRequest request)
    {
        var result = await _catalogService.GetCatalogItemsAsync(request.PageIndex, request.PageSize);
        return Ok(result);
    }

    [HttpPost]
    [AllowAnonymous]
    [ProducesResponseType(typeof(IEnumerable<CatalogBrandDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Brands(PaginatedBrandsRequest request)
    {
        var result = await _catalogService.GetCatalogBrandsAsync(request.PageIndex, request.PageSize);
        return Ok(result?.Data);
    }

    [HttpPost]
    [AllowAnonymous]
    [ProducesResponseType(typeof(IEnumerable<CatalogTypeDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Types(PaginatedTypesRequest request)
    {
        var result = await _catalogService.GetCatalogTypesAsync(request.PageIndex, request.PageSize);
        return Ok(result?.Data);
    }

    [HttpPost]
    [AllowAnonymous]
    [ProducesResponseType(typeof(PaginatedItemsResponse<CatalogItemDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> ItemsByFilter(PaginatedItemsByFiltersRequest<CatalogFilter> request)
    {
        var result = await _catalogService.GetCatalogByFiltersAsync(request.PageIndex, request.PageSize, request.Filters);
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(PaginatedItemByIdResponse<CatalogItemDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetByIdItem(GetByIdItemRequest request)
    {
        var result = await _catalogService.GetCatalogByIdItemAsync(request.PageIndex, request.PageSize, request.Id);
        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(PaginatedItemByBrandResponse<CatalogItemDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetByBrandItems(GetByBrandItemRequest request)
    {
        var result = await _catalogService.GetCatalogByBrandItemAsync(request.PageIndex, request.PageSize, request.Brand);
        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(PaginatedItemByTypeResponse<CatalogItemDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetByTypeItems(GetByTypeItemRequest request)
    {
        var result = await _catalogService.GetCatalogByTypeItemAsync(request.PageIndex, request.PageSize, request.Type);
        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }
}