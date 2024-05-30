using Bbpro.Domain.Entities.MainContact;
using Bbpro.Domain.Entities.Multilanguage;

namespace Bbpro.Domain.Models.Contacts;


public class ContactModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string MapFrame { get; set; }
    public Language Address { get; set; }
    public List<string> Phone { get; set; }
    public string Email { get; set; }
    public Language WorkDay { get; set; }
    public Language Weekend { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public virtual ContactModel MapFromEntity(Contact entity)
    {
        var contactModel = new ContactModel
        {
            Id = entity.Id,
            Title = entity.Title,
            MapFrame = entity.MapFrame,
            Address = entity.Address,
            Email = entity.Email,
            WorkDay = entity.WorkDay,
            Weekend = entity.Weekend,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt == DateTime.MinValue ? null : entity.UpdatedAt,
            Phone = new List<string>()
        };

        if (!string.IsNullOrEmpty(entity.Phone))
        {
            contactModel.Phone = entity.Phone.Split(',').ToList();
        }

        return contactModel;
    }
}
