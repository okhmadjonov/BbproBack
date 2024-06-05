using Bbpro.Domain.Entities.Multilanguage;
using Microsoft.AspNetCore.Http;

namespace Bbpro.Domain.Dto.About;

public class AboutUpdateDto
{
    public IFormFile ImageUrl { get; set; }
    public Language Title { get; set; }
    public Language Description { get; set; }
}