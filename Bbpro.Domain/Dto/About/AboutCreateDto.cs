using Bbpro.Domain.Entities.Multilanguage;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Bbpro.Domain.Dto.About;

public class AboutCreateDto
{

    [Required]
    public IFormFile ImageUrl { get; set; }
    [Required]
    public Language Title { get; set; }
    [Required]
    public Language Description { get; set; }

}
