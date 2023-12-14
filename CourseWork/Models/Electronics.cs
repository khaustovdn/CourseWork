namespace CourseWork.Models;

public class Electronics : Ware
{
    public Electronics(string? name, int size, int warrantyPeriod) : base(name, size)
    {
        WarrantyPeriod = warrantyPeriod;
    }

    public int WarrantyPeriod { get; }
}