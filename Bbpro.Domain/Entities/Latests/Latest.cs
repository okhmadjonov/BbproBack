using Bbpro.Domain.Commons;
using Bbpro.Domain.Entities.Multilanguage;

namespace Bbpro.Domain.Entities.Latests;

public class Latest : Auditable
{
    public string ImageUrl { get; set; }
    public Language Title { get; set; }
    public Language Description { get; set; }
}
