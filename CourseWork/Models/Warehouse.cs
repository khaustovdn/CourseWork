namespace CourseWork.Models;

public class Warehouse : IWarehouse, ICollection
{
    protected Warehouse(string? name, int size, string? address)
    {
        Name = name;
        Size = size;
        Address = address;
    }

    //private ObservableCollection<Product> Products { get; } = new();

    public void Add(object? o)
    {
        //if (o is Product product) Products.Add(product);
    }

    public string? Address { get; }
    public int Size { get; }

    public string? Name { get; }
}