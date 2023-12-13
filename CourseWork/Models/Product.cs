namespace CourseWork.Models;

public abstract class Product : IProduct
{
    public string? Name;
    public int Size;

    protected Product(string? name, int size)
    {
        Name = name;
        Size = size;
    }
}