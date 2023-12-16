namespace CourseWork.Models;

public abstract class Product
{
    protected Product(string? name, int size)
    {
        Name = name;
        Size = size;
    }

    public string? Name { get; }
    public int Size { get; }
}