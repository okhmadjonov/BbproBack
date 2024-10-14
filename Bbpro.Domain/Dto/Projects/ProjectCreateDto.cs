using Bbpro.Domain.Entities.Multilanguage;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Bbpro.Domain.Dto.Projects;

public class ProjectCreateDto
{
    [Required]
    public IFormFile ImageUrl { get; set; }
    [Required]
    public Language Title { get; set; }
    [Required]
    public Language Description { get; set; }

    public IFormFile? DownloadLink { get; set; }
}
