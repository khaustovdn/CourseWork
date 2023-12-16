using System.Collections.ObjectModel;

namespace CourseWork.Models;

public abstract class Warehouse
{
    protected Warehouse(string? name, int size, string? address)
    {
        Name = name;
        Size = size;
        Address = address;
    }

    public int Size { get; }
    public string? Name { get; }
    public string? Address { get; }
    public ObservableCollection<Product> Products { get; } = new();
}