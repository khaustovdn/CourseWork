using System.Collections.ObjectModel;

namespace CourseWork.Models;

public abstract class Warehouse : IWarehouse, ICollection
{
    protected Warehouse(string? name, int size, string? address)
    {
        Name = name;
        Size = size;
        Address = address;
    }

    public void Add(object? o)
    {
        if (o is Product product) Products.Add(product);
    }

    public string? Address { get; }
    public int Size { get; }
    public ObservableCollection<IProduct> Products { get; } = new();
    public string? Name { get; }
}