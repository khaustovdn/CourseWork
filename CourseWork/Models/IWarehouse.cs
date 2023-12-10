namespace CourseWork.Models;

public interface IWarehouse
{
    public string? Address { get; }
    public int Size { get; }

    public string? Name { get; }
}