namespace CourseWork.Models;

public class TechnicalWarehouse(string? name, int size, string? city, int powerSupplyLevel)
    : Warehouse(name, size, city)
{
    public int PowerSupplyLevel { get; } = powerSupplyLevel;
}