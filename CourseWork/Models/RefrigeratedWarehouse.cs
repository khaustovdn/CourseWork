namespace CourseWork.Models;

public class RefrigeratedWarehouse : Warehouse
{
    public RefrigeratedWarehouse(string? name, int size, string? address, int temperature) : base(name, size, address)
    {
        Temperature = temperature;
    }

    public int Temperature { get; }
}