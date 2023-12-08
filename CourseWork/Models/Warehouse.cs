namespace CourseWork.Models;

public abstract class Warehouse : IObject, ICollection
{
    protected Warehouse(string name, int size)
    {
        Name = name;
        Size = size;
    }

    //private ObservableCollection<Product> Products { get; } = new();

    public void Add(object? o)
    {
        //if (o is Product product) Products.Add(product);
    }

    public string Name { get; }
    public int Size { get; }
}