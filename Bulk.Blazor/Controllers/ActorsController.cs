using Bulk.Model.Actors;
using Bulk.Model.Logins;
using Bulk.Service.Extensions;
using Bulk.Service.Services.Actors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bulk.Blazor.Controllers;

[Route("api/[controller]")]
[ApiController]
[RequireHttps]
public class ActorsController(IActorService actorService) : ControllerBase
{
    [HttpPost("post")]
    public async ValueTask<IActionResult> Post(ActorCreateModel model)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await actorService.CreateAsync(model)
        });
    }


    [HttpGet("{id}")]
    public async ValueTask<IActionResult> GetById(long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await actorService.GetByIdAsync(id)
        });
    }

    [HttpGet("username={email}+{password}")]
    public async ValueTask<IActionResult> Login(string email, string password)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await actorService.LoginAsync(email, password)
        });
    }

    [HttpGet]
    public async ValueTask<IActionResult> Get()
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await actorService.GetAllAsync()
        });
    }
}
