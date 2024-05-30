using Bbpro.Domain.Configurations;
using Bbpro.Domain.Dto.MainContact;
using Bbpro.Domain.Entities.MainContact;
using Bbpro.Domain.Interface;
using Bbpro.Domain.Models.Contacts;
using Bbpro.Service.Exceptions;
using Bbpro.Service.Extentions;
using Bbpro.Service.Interfaces.Contacts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Bbpro.Service.Services.Contacts;


internal sealed class ContactService : IContactRepository
{

    private readonly IGenericRepository<Contact> _contactRepository;

    public ContactService(IGenericRepository<Contact> contactRepository)
    {
        _contactRepository = contactRepository;
    }
    public async ValueTask<ContactModel> CreateAsync(ContactCreateDto contact)
    {
        var addContact = new Contact
        {
            Title = contact.Title,
            MapFrame = contact.MapFrame,
            Address = contact.Address,
            Phone = string.Join(",", contact.Phone),
            Email = contact.Email,
            WorkDay = contact.WorkDay,
            Weekend = contact.Weekend,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        var createdContact = await _contactRepository.CreateAsync(addContact);
        await _contactRepository.SaveChangesAsync();
        var contactModel = new ContactModel().MapFromEntity(createdContact);
        return contactModel;
    }
    public async ValueTask<bool> DeleteAsync(int id)
    {
        var findContact = await _contactRepository.GetAsync(p => p.Id == id);
        if (findContact is null)
        {
            throw new BbproException(404, "contact_not_found");
        }
        await _contactRepository.DeleteAsync(id);
        await _contactRepository.SaveChangesAsync();
        return true;
    }

    public async ValueTask<IEnumerable<ContactModel>> GetAll(PaginationParams @params, Expression<Func<Contact, bool>> expression = null)
    {
        var contacts = _contactRepository.GetAll(expression: expression, isTracking: false);
        var contactsList = await contacts.ToPagedList(@params).ToListAsync();
        return contactsList.Select(e => new ContactModel().MapFromEntity(e)).ToList();
    }

    public async ValueTask<ContactModel> GetAsync(Expression<Func<Contact, bool>> expression)
    {
        var contact = await _contactRepository.GetAsync(expression, false);
        if (contact is null)
            throw new BbproException(404, "contact_not_found");
        return new ContactModel().MapFromEntity(contact);
    }

    public async ValueTask<ContactModel> UpdateAsync(int id, ContactUpdateDto contactForUpdateDTO)
    {
        var existingContact = await _contactRepository.GetAsync(p => p.Id == id) ?? throw new BbproException(404, "contact_not_found");

        if (contactForUpdateDTO.Title != null)
        {
            existingContact.Title = contactForUpdateDTO.Title;
        }
        if (contactForUpdateDTO.MapFrame != null)
        {
            existingContact.MapFrame = contactForUpdateDTO.MapFrame;
        }
        if (contactForUpdateDTO.Address != null)
        {
            if (contactForUpdateDTO.Address.UZ != null)
            {
                existingContact.Address.UZ = contactForUpdateDTO.Address.UZ;
            }
            if (contactForUpdateDTO.Address.RU != null)
            {
                existingContact.Address.RU = contactForUpdateDTO.Address.RU;
            }
            if (contactForUpdateDTO.Address.EN != null)
            {
                existingContact.Address.EN = contactForUpdateDTO.Address.EN;
            }
        }
        if (contactForUpdateDTO.Phone != null)
        {
            existingContact.Phone = string.Join(",", contactForUpdateDTO.Phone);
        }
        if (contactForUpdateDTO.Email != null)
        {
            existingContact.Email = contactForUpdateDTO.Email;
        }
        if (contactForUpdateDTO.WorkDay != null)
        {
            if (contactForUpdateDTO.WorkDay.UZ != null)
            {
                existingContact.WorkDay.UZ = existingContact.WorkDay.UZ;
            }
            if (contactForUpdateDTO.WorkDay.RU != null)
            {
                existingContact.WorkDay.RU = existingContact.WorkDay.RU;
            }
            if (contactForUpdateDTO.WorkDay.EN != null)
            {
                existingContact.WorkDay.EN = existingContact.WorkDay.EN;
            }
        }
        if (contactForUpdateDTO.Weekend != null)
        {
            if (contactForUpdateDTO.Weekend.UZ != null)
            {
                existingContact.Weekend.UZ = existingContact.Weekend.UZ;
            }
            if (contactForUpdateDTO.Weekend.RU != null)
            {
                existingContact.Weekend.RU = existingContact.Weekend.RU;
            }
            if (contactForUpdateDTO.Weekend.EN != null)
            {
                existingContact.Weekend.EN = existingContact.Weekend.EN;
            }
        }
        existingContact.UpdatedAt = DateTime.UtcNow;

        await _contactRepository.SaveChangesAsync();
        return new ContactModel().MapFromEntity(existingContact);
    }
}
