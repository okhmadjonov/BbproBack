using System.ComponentModel.DataAnnotations;

namespace Bbpro.Domain.Dto.OrdersDto;

public class OrderCreateDto
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string Phone { get; set; }
    public string Email { get; set; } 
    public string Message { get; set; }
}
