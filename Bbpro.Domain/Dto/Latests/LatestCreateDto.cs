using Bbpro.Domain.Entities.Multilanguage;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Bbpro.Domain.Dto.Latests;

public class LatestCreateDto
{
    [Required]
    public IFormFile ImageUrl { get; set; }
    [Required]
    public Language Title { get; set; }
    [Required]
    public Language Description { get; set; }
}
