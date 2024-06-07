using System.ComponentModel.DataAnnotations;

namespace Bbpro.Domain.Dto.CategoryDto;

public class CategoryCreateDto
{
    [Required]
    public string Title { get; set; }
}
