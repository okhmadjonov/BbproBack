using Microsoft.AspNetCore.Http;

namespace Bbpro.Domain.Dto.Brands;

public class BrandDto
{
    public List<IFormFile> ImageUrl { get; set; }
}
