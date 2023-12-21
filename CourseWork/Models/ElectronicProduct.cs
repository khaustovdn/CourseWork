using System;

namespace CourseWork.Models;

public class ElectronicProduct(int warehouseId, string? name, int size, int price, DateTime? warrantyPeriod)
    : Product(warehouseId, name, size, price)
{
    public DateTime? WarrantyPeriod { get; } = warrantyPeriod;
}