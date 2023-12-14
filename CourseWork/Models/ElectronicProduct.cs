namespace CourseWork.Models;

public class ElectronicProduct : Product
{
    public ElectronicProduct(string? name, int size, int warrantyPeriod) : base(name, size)
    {
        WarrantyPeriod = warrantyPeriod;
    }

    public int WarrantyPeriod { get; }
}