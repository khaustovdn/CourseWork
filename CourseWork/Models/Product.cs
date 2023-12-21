namespace CourseWork.Models;

public abstract class Product(int warehouseId, string? name, int size, int price)
{
    public int WarehouseId { get; } = warehouseId;
    public string? Name { get; } = name;
    public int Size { get; } = size;
    public int Price { get; } = price;
}