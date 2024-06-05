using Bbpro.Domain.Commons;

namespace Bbpro.Domain.Entities.Brands;


public class Brand : Auditable
{
    public List<string> ImageUrl { get; set; }
}
