using System.Collections.ObjectModel;

namespace CourseWork.Models;

public interface IWarehouse
{
    public string? Address { get; }
    public string? Name { get; }
    public int Size { get; }
    public ObservableCollection<IProduct> Products { get; }
}