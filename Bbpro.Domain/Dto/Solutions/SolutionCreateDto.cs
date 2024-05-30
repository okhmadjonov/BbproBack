using Bbpro.Domain.Entities.Multilanguage;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Bbpro.Domain.Dto.Solutions;

public class SolutionCreateDto
{
    [Required]
    public IFormFile ImageUrl { get; set; }
    [Required]
    public Language Title { get; set; }
    [Required]
    public Language Description { get; set; }
}
