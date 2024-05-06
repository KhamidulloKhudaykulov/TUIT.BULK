using Bulk.Model.Products;
using Bulk.Model.Sectors;
using Bulk.Service.Extensions;
using Bulk.Service.Services.Products;
using Bulk.Service.Services.Sectors;
using Microsoft.AspNetCore.Mvc;

namespace Bulk.Blazor.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SectorsController(ISectorService sectorService) : ControllerBase
{
    [HttpPost("post")]
    public async ValueTask<IActionResult> Post(SectorCreateModel model)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "success",
            Data = await sectorService.CreateAsync(model)
        });
    }

    [HttpGet]
    public async ValueTask<IActionResult> GetAll()
    {
        return Ok(new Response 
        {
            StatusCode = 200,
            Message = "success",
            Data = await sectorService.GetAllAsync()
        });
    }

    [HttpPost("addproduct={id}={productId}={count}")]
    public async ValueTask<IActionResult> AddProduct(long id, long productId, int count)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "success",
            Data = await sectorService.InsertProduct(id, productId, count)
        });
    }

    [HttpPost("update={id}")]
    public async ValueTask<IActionResult> Update(long id, SectorUpdateModel model)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await sectorService.UpdateAsync(id, model)
        });
    }

    [HttpDelete("delete={id}")]
    public async ValueTask<IActionResult> Delete(long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "success",
            Data = await sectorService.DeleteAsync(id)
        });
    }
}
