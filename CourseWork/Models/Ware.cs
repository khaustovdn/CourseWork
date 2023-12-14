namespace CourseWork.Models;

public abstract class Ware : IWare
{
    protected Ware(string? name, int size)
    {
        Name = name;
        Size = size;
    }

    public string? Name { get; }
    public int Size { get; }
}