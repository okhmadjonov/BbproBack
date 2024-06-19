using Bbpro.Domain.Configurations;
using Bbpro.Domain.Dto.OrdersDto;
using Bbpro.Domain.Entities.Orders;
using Bbpro.Domain.Interface;
using Bbpro.Domain.Models.Orders;
using Bbpro.Service.Exceptions;
using Bbpro.Service.Extentions;
using Bbpro.Service.Interfaces.Orders;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Bbpro.Service.Services.Orders;

internal sealed class OrderService : IOrderRepository
{
    private readonly IGenericRepository<Order> _orderRepository;

    public OrderService(IGenericRepository<Order> orderRepository)
    {
        _orderRepository = orderRepository;
    }


    public async ValueTask<OrderModel> CreateAsync(OrderCreateDto order)
    {

        var newOrder = new Order
        {
            Name = order.Name,
            Phone = order.Phone,
            Email = order.Email,
            Message = order.Message,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow

        };
        var createdOrder = await _orderRepository.CreateAsync(newOrder);
        await _orderRepository.SaveChangesAsync();
        return new OrderModel().MapFromEntity(createdOrder);
    }

    public async ValueTask<bool> DeleteAsync(int id)
    {
        var findOrder = await _orderRepository.GetAsync(p => p.Id == id);
        if (findOrder is null)
        {
            throw new BbproException(404, "order_not_found");
        }
        await _orderRepository.DeleteAsync(id);
        await _orderRepository.SaveChangesAsync();
        return true;
    }

    public async ValueTask<IEnumerable<OrderModel>> GetAll(PaginationParams @params, Expression<Func<Order, bool>> expression = null)
    {
        var orders = _orderRepository.GetAll(expression: expression, isTracking: false);
        var ordersList = await orders.ToPagedList(@params).ToListAsync();
        return ordersList.Select(e => new OrderModel().MapFromEntity(e)).ToList();
    }

    public async ValueTask<OrderModel> GetAsync(Expression<Func<Order, bool>> expression)
    {
        var order = await _orderRepository.GetAsync(expression);
        if (order is null)
            throw new BbproException(404, "order_not_found");

        return new OrderModel().MapFromEntity(order);
    }
}
