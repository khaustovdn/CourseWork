namespace CourseWork.Models;

public class TechnicalWarehouse : Warehouse
{
    public TechnicalWarehouse(string? name, int size, string? address, int powerSupplyLevel) : base(name, size, address)
    {
        PowerSupplyLevel = powerSupplyLevel;
    }

    public int PowerSupplyLevel { get; }
}