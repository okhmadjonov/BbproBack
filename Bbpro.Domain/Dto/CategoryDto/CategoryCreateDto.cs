using Bbpro.Domain.Entities.Multilanguage;
using System.ComponentModel.DataAnnotations;

namespace Bbpro.Domain.Dto.CategoryDto;

public class CategoryCreateDto
{
    [Required]
    public Language Title { get; set; }
}
