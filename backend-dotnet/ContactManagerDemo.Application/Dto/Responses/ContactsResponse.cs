using ContactManagerDemo.Common.GridData;

namespace ContactManagerDemo.Application.Dto.Responses;

public class ContactsResponse : ContactDto, IGridDataItem
{
    public bool? Selected { get; set; }
}