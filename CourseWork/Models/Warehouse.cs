using System.Collections.ObjectModel;

namespace CourseWork.Models;

public abstract class Warehouse : ICollection
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

    public void Add(object? o)
    {
        if (o is Product product) Products.Add(product);
    }

    public void Remove(int id)
    {
    }

    public void ElementAt(int id)
    {
    }
}