using Bbpro.Api.FluentValidation;
using Bbpro.Domain.Configurations;
using Bbpro.Domain.Dto.OrdersDto;
using Bbpro.Domain.Dto.Users;
using Bbpro.Domain.Models.Orders;
using Bbpro.Domain.Models.Response;
using Bbpro.Domain.Models.Users;
using Bbpro.Service.Extentions;
using Bbpro.Service.Interfaces.Auths;
using Bbpro.Service.Interfaces.Orders;
using Bbpro.Service.Interfaces.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bbpro.Api.Controllers.Orders;

[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IOrderRepository _orderRepository;

    public OrderController(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    [HttpGet]
    [ProducesResponseType(typeof(ResponseModel<OrderModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseModel<>), StatusCodes.Status400BadRequest)]
    public async ValueTask<IActionResult> GetAll([FromQuery] PaginationParams @params)
     => ResponseHandler.ReturnResponseList(await _orderRepository.GetAll(@params));

    [HttpPost]
    [Authorize]
    [ProducesResponseType(typeof(ResponseModel<OrderModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseModel<>), StatusCodes.Status400BadRequest)]
    public async ValueTask<IActionResult> CreateAsync(OrderCreateDto order)
    {
        return ResponseHandler.ReturnIActionResponse(await _orderRepository.CreateAsync(order));
    }

    [HttpDelete("{id}")]
    [Authorize]
    [ProducesResponseType(typeof(ResponseModel<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseModel<>), StatusCodes.Status400BadRequest)]
    public async ValueTask<IActionResult> DeleteAsync([FromRoute] int id)
         => ResponseHandler.ReturnIActionResponse(await _orderRepository.DeleteAsync(id));

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ResponseModel<OrderModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseModel<>), StatusCodes.Status400BadRequest)]
    public async ValueTask<IActionResult> GetAsync([FromRoute] int id)
        => ResponseHandler.ReturnIActionResponse(await _orderRepository.GetAsync(u => u.Id == id));

}
