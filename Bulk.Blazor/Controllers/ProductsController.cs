using Bulk.Model.Products;
using Bulk.Service.Extensions;
using Bulk.Service.Services.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bulk.Blazor.Controllers;

[Route("api/[controller]")]
[ApiController]
[RequireHttps]
public class ProductsController(IProductService productService) : ControllerBase
{
    [HttpPost("post")]
    public async ValueTask<IActionResult> Post(ProductCreateModel model)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await productService.CreateAsync(model)
        });
    }

    [HttpPost("update={id}")]
    public async ValueTask<IActionResult> Update(long id, ProductUpdateModel model)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await productService.UpdateAsync(id, model)
        });
    }

    [HttpGet("{id}")]
    public async ValueTask<IActionResult> GetById(long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await productService.GetAsync(id)
        });
    }

    [HttpGet]
    public async ValueTask<IActionResult> GetAll()
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await productService.GetAllAsync()
        });
    }

    [HttpDelete("delete={id}")]
    public async ValueTask<IActionResult> Delete(long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await productService.DeleteAsync(id)
        });
    }
}
