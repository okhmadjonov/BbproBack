﻿using System.ComponentModel.DataAnnotations;

namespace Bbpro.Domain.Dto.Users;

public class LoginDto
{
    [Required]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }
}
