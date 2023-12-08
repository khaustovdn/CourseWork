namespace CourseWork.Models;

public class Warehouse : ICollection
{
    public string? Address;
    public int Size;

    public Warehouse(string? name, int size, string? address)
    {
        Name = name;
        Size = size;
        Address = address;
    }

    public string? Name { get; }

    //private ObservableCollection<Product> Products { get; } = new();

    public void Add(object? o)
    {
        //if (o is Product product) Products.Add(product);
    }
}