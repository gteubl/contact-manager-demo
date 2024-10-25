using AutoMapper;
using ContactManagerDemo.Application.Dto;
using ContactManagerDemo.Domain.Entities;
using ContactManagerDemo.Infrastructure.DataContext;
using Microsoft.EntityFrameworkCore;

namespace ContactManagerDemo.Application.Services;

public class ContactService : IContactService
{
    private readonly AppDataContext _appDataContext;
    private readonly IMapper _mapper;

    public ContactService(AppDataContext appDataContext, IMapper mapper)
    {
        _appDataContext = appDataContext;
        _mapper = mapper;
    }

    public async Task<List<Contact>> GetContactsAsync()
    {
        return await _appDataContext.Contacts.ToListAsync();
    }
    
    public async Task<ContactDto?> GetContactByIdAsync(int id)
    {
        var contact = await _appDataContext.Contacts.FindAsync(id);
        return contact == null ? null : _mapper.Map<ContactDto>(contact);
            
    }
    
    public async Task<ContactDto> CreateContactAsync(ContactDto contact)
    {
        var newContact = _mapper.Map<Contact>(contact);
        await _appDataContext.Contacts.AddAsync(newContact);
        await _appDataContext.SaveChangesAsync();
        return _mapper.Map<ContactDto>(newContact);
       
    }
    
    public async Task<ContactDto> UpdateContactAsync(ContactDto contact)
    {
        var existingContact = await _appDataContext.Contacts.FindAsync(contact.Id);
        if (existingContact == null)
        {
            throw new Exception("Contact not found");
        }
        
        _mapper.Map(contact, existingContact);
        await _appDataContext.SaveChangesAsync();
        return _mapper.Map<ContactDto>(existingContact);
    }
    
    public async Task DeleteContactAsync(int id)
    {
        var contact = await _appDataContext.Contacts.FindAsync(id);
        if (contact != null)
        {
            _appDataContext.Contacts.Remove(contact);
            await _appDataContext.SaveChangesAsync();
        }
    }
    
    public async Task<List<ContactDto>> SearchContactsAsync(string searchTerm)
    {
        var contacts = await _appDataContext.Contacts
            .Where(c => c.FirstName.Contains(searchTerm) || c.LastName.Contains(searchTerm))
            .ToListAsync();
        return _mapper.Map<List<ContactDto>>(contacts);
    }
    
    
}