using Bbpro.Domain.Commons;
using Bbpro.Domain.Entities.Multilanguage;

namespace Bbpro.Domain.Entities.MainContact;

public class Contact : Auditable
{
    public string Title { get; set; }
    public string MapFrame { get; set; }
    public Language Address { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public Language WorkDay { get; set; }
    public Language Weekend { get; set; }

}
