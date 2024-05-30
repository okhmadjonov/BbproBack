using Bbpro.Domain.Entities.Multilanguage;
using System.ComponentModel.DataAnnotations;

namespace Bbpro.Domain.Dto.MainContact;


public class ContactCreateDto
{
    [Required]
    public string Title { get; set; }

    [Required]
    public string MapFrame { get; set; }
    [Required]
    public Language Address { get; set; }

    [Required]
    public List<string> Phone { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public Language WorkDay { get; set; }

    [Required]
    public Language Weekend { get; set; }

}
