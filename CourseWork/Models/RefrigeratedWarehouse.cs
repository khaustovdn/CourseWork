namespace CourseWork.Models;

public class RefrigeratedWarehouse(string? name, int size, string? city, int temperature)
    : Warehouse(name, size, city)
{
    public int Temperature { get; } = temperature;
}