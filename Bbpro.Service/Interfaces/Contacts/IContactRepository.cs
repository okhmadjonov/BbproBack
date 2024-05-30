using Bbpro.Domain.Configurations;
using Bbpro.Domain.Dto.MainContact;
using Bbpro.Domain.Entities.MainContact;
using Bbpro.Domain.Models.Contacts;
using System.Linq.Expressions;

namespace Bbpro.Service.Interfaces.Contacts;

public interface IContactRepository
{
    ValueTask<IEnumerable<ContactModel>> GetAll(PaginationParams @params, Expression<Func<Contact, bool>> expression = null);
    ValueTask<ContactModel> GetAsync(Expression<Func<Contact, bool>> expression);
    ValueTask<ContactModel> CreateAsync(ContactCreateDto contactCreateDTO);
    ValueTask<bool> DeleteAsync(int id);
    ValueTask<ContactModel> UpdateAsync(int id, ContactUpdateDto contactUpdateDTO);
}
