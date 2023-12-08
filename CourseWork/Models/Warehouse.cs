namespace CourseWork.Models;

public class Warehouse : IObject, ICollection
{
    public Warehouse(string name, int size, string address)
    {
        Name = name;
        Size = size;
        Address = address;
    }

    public string Address { get; }

    //private ObservableCollection<Product> Products { get; } = new();

    public void Add(object? o)
    {
        //if (o is Product product) Products.Add(product);
    }

    public string Name { get; }
    public int Size { get; }
}