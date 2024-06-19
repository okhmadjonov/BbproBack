using Bbpro.Domain.Entities.About;
using Bbpro.Domain.Entities.Orders;

namespace Bbpro.Domain.Models.Orders;

public class OrderModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string Message { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public virtual OrderModel MapFromEntity(Order entity)
    {
        return new OrderModel
        {
            Id = entity.Id,
            Name = entity.Name,
            Phone = entity.Phone,
            Email = entity.Email,
            Message = entity.Message,   
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt
        };
    }
}
