using System;

namespace CourseWork.Models;

public class FoodProduct(int warehouseId, string? name, int size, int price, DateTime? expirationDate)
    : Product(warehouseId, name, size, price)
{
    public DateTime? ExpirationDate { get; } = expirationDate;
}