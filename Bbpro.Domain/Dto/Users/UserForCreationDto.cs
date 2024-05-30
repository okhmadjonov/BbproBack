using System.ComponentModel.DataAnnotations;

namespace Bbpro.Domain.Dto.Users;

public class UserForCreationDto
{
    [Required]
    public string Username { get; set; }

    [Required]
    public string Phonenumber { get; set; }

    [Required]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }
}
