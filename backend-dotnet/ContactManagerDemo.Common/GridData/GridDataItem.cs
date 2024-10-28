namespace ContactManagerDemo.Common.GridData;

public interface IGridDataItem
{
    Guid Id { get; set; }
    bool? Selected { get; set; }
}