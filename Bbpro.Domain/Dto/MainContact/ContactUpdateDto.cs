using Bbpro.Domain.Entities.Multilanguage;

namespace Bbpro.Domain.Dto.MainContact;


public class ContactUpdateDto
{
    public string Title { get; set; }
    public string MapFrame { get; set; }
    public Language Address { get; set; }
    public List<string> Phone { get; set; }
    public string Email { get; set; }
    public Language WorkDay { get; set; }
    public Language Weekend { get; set; }
}
