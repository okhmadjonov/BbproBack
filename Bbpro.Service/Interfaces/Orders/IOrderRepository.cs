using Bbpro.Domain.Configurations;
using Bbpro.Domain.Dto.About;
using Bbpro.Domain.Dto.OrdersDto;
using Bbpro.Domain.Entities.About;
using Bbpro.Domain.Entities.Orders;
using Bbpro.Domain.Models.AboutModel;
using Bbpro.Domain.Models.Orders;
using System.Linq.Expressions;

namespace Bbpro.Service.Interfaces.Orders;

public interface IOrderRepository
{
    ValueTask<IEnumerable<OrderModel>> GetAll(PaginationParams @params, Expression<Func<Order, bool>> expression = null);
    ValueTask<OrderModel> GetAsync(Expression<Func<Order, bool>> expression);
    ValueTask<OrderModel> CreateAsync(OrderCreateDto order);
    ValueTask<bool> DeleteAsync(int id);
   
}
