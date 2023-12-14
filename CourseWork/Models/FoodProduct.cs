namespace CourseWork.Models;

public class FoodProduct : Product
{
    public FoodProduct(string? name, int size, int expirationDate) : base(name, size)
    {
        ExpirationDate = expirationDate;
    }

    public int ExpirationDate { get; }
}