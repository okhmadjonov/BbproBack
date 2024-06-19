using Bbpro.Domain.Commons;

namespace Bbpro.Domain.Entities.Orders;

public class Order : Auditable
{
    public string Name { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; } 
    public string Message { get; set; } 
}
