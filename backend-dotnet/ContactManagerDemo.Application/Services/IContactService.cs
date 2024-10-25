using ContactManagerDemo.Application.Dto;
using ContactManagerDemo.Domain.Entities;

namespace ContactManagerDemo.Application.Services;

public interface IContactService
{
    Task<List<Contact>> GetContactsAsync();
    Task<ContactDto?> GetContactByIdAsync(int id);
    Task<ContactDto> CreateContactAsync(ContactDto contact);
    Task<ContactDto> UpdateContactAsync(ContactDto contact);
    Task DeleteContactAsync(int id);
    Task<List<ContactDto>> SearchContactsAsync(string searchTerm);
}