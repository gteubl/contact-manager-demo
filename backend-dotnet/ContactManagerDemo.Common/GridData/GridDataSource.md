

```csharp
// Request classes must inherit from GridDataRequest
public class ContactsRequest : GridDataRequest
{
}

// Response classes must inherit from IGridDataItem
public class ContactDto  : ContactDto, IGridDataItem
{
    public bool? Selected { get; set; }
}
    
```

```csharp
 public async Task<GridDataSource<ContactsResponse>> Handle(GetContactsQuery request,
        CancellationToken cancellationToken)
    {
        var query = _appDataContext.Contacts.AsQueryable();

        // Apply Filters
        query = query.ApplyFilters(request.Filter, (column, magicFilter) => column.ToLower() switch
        {
            nameof(Contact.FirstName) => x => x.FirstName.Contains(magicFilter),
            nameof(Contact.LastName) => x => x.LastName.Contains(magicFilter),
            nameof(Contact.Email) => x => x.Email.Contains(magicFilter),
            nameof(Contact.PhoneNumber) => x => x.PhoneNumber != null && x.PhoneNumber.Contains(magicFilter),
            _ => null
        });


        // Apply Sorting
        query = query.ApplySorting(request.Filter);

        //Select
        var data = query
            .ProjectTo<ContactsResponse>(_mapper.ConfigurationProvider)
            .AsNoTracking();

        return await data.ToGridDataSourceAsync(request.Filter);
    }
```
