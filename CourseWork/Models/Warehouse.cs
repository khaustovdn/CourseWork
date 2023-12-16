using System.Collections.ObjectModel;

namespace CourseWork.Models;

public abstract class Warehouse : ISubject, IWarehouse, ICollection
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

    public int Size { get; }
    public string? Name { get; }
    public string? Address { get; }
    public ObservableCollection<ISubject> Products { get; } = new();
}