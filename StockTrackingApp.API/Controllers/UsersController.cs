using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockTrackingApp.Application.Features.Commands.AppUser.CreateUser;
using StockTrackingApp.Application.Features.Commands.AppUser.LoginUser;

namespace StockTrackingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class UsersController : ControllerBase
    {
        readonly IMediator mediator;

        public UsersController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserCommandRequest req)
        {
            CreateUserCommandResponse response = await mediator.Send(req);
            return Ok(response);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login(LoginUserCommandRequest req)
        {
          LoginUserCommandResponse res = await  mediator.Send(req);
            return Ok(res);
        }
    }
}
