namespace CourseWork.Models;

public class Product : Ware
{
    public Product(string? name, int size, int expirationDate) : base(name, size)
    {
        ExpirationDate = expirationDate;
    }

    public int ExpirationDate { get; }
}