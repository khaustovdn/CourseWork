namespace CourseWork.Models;

public class TechnicalWarehouse : Warehouse
{
    public TechnicalWarehouse(string? name, int size, string? address, int powerSupplyСlass) : base(name, size, address)
    {
        PowerSupplyСlass = powerSupplyСlass;
    }

    public int PowerSupplyСlass { get; }
}