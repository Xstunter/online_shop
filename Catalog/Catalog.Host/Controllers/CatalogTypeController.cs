using System.Net;
using Catalog.Host.Models.Requests;
using Catalog.Host.Models.Response;
using Catalog.Host.Services;
using Catalog.Host.Services.Interfaces;
using Infrastructure;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Host.Controllers;

[ApiController]
[Authorize(Policy = AuthPolicy.AllowClientPolicy)]
[Scope("catalog.catalogType")]
[Route(ComponentDefaults.DefaultRoute)]
public class CatalogTypeController : ControllerBase
{
    private readonly ILogger<CatalogTypeController> _logger;
    private ICatalogTypeService _catalogTypeService;
    public CatalogTypeController(ILogger<CatalogTypeController> logger, ICatalogTypeService catalogTypeService)
    {
        _catalogTypeService = catalogTypeService;
        _logger = logger;
    }

    [HttpPost]
    [ProducesResponseType(typeof(AddItemResponse<int?>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Add(CreateTypeRequest request)
    {
        var result = await _catalogTypeService.AddAsync(request.Type);
        return Ok(new AddItemResponse<int?>() { Id = result });
    }

    [HttpDelete]
    [ProducesResponseType(typeof(AddItemResponse<int?>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Delete(DeleteTypeRequest request)
    {
        var isDeleted = await _catalogTypeService.DeleteAsync(request.Id);
        if (!isDeleted)
        {
            return NotFound();
        }

        return Ok();
    }

    [HttpPost]
    [ProducesResponseType(typeof(AddItemResponse<int?>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Update(UpdateTypeRequest request)
    {
        var isUpdated = await _catalogTypeService.UpdateAsync(request.Id, request.Type);
        if (!isUpdated)
        {
            return NotFound();
        }

        return Ok();
    }
}