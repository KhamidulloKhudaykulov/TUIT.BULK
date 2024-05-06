using Bulk.Model.Categories;
using Bulk.Service.Extensions;
using Bulk.Service.Services.Categories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Bulk.Blazor.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController(ICategoryService categoryService) : ControllerBase
{
    [HttpPost("post")]
    public async ValueTask<IActionResult> Post(CategoryCreateModel model)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await categoryService.CreateAsync(model)
        });
    }

    [HttpGet("{id}")]
    public async ValueTask<IActionResult> GetById(long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await categoryService.GetAsync(id)
        });
    }

    [HttpGet]
    public async ValueTask<IActionResult> Get()
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await categoryService.GetAllAsync()
        });
    }

    [HttpDelete("delete={id}")]
    public async ValueTask<IActionResult> Delete(long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await categoryService.DeleteAsync(id)
        });
    }
}
