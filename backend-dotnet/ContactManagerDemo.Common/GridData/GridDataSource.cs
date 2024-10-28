namespace ContactManagerDemo.Common.GridData;

public class GridDataSource<T>(IEnumerable<T> data, int skip, int take, int count) where T : IGridDataItem
{
    public int Skip { get; set; } = skip;
    public int Take { get; set; } = take;
    public int Count { get; set; } = count;
    public List<T> Data { get; set; } = data.ToList();
}