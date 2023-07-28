using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockTrackingApp.Application.Features.Commands.Product.CreateProduct;
using StockTrackingApp.Application.Features.Commands.Product.UpdateProduct;
using StockTrackingApp.Application.Features.Queries.Product.GetAllProduct;
using System.Net;

namespace StockTrackingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            GetAllProductQueryRequest req = new GetAllProductQueryRequest();
            GetAllProductQueryResponse res = await _mediator.Send(req);
            return Ok(res);
        }
        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductCommandRequest req)
        {
            await _mediator.Send(req);
            return StatusCode((int)HttpStatusCode.Created);

        }
        [HttpPut]
        public async Task<IActionResult> UpdateProduct(UpdateProductCommandRequest req)
        {
            await _mediator.Send(req);
            return Ok();
        }
    }
}
