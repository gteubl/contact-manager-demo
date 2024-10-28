// ReSharper disable CollectionNeverUpdated.Global

namespace ContactManagerDemo.Common.GridData;

public class GridDataRequest
{
    public int Skip { get; set; } = 0;
    public int Take { get; set; } = 10;
    public string? OrderBy { get; set; }
    public bool OrderDescending { get; set; } = false;

    public List<string> ColumnsToFilter { get; set; } = [];

    public string? MagicFilter { get; set; }
}