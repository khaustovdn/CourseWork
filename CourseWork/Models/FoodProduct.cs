using System;

namespace CourseWork.Models;

public class FoodProduct(
    string? name,
    int size,
    int price,
    DateTime? expirationDate,
    int requiredTemperature)
    : Product(name, size, price)
{
    public DateTime? ExpirationDate { get; } = expirationDate;
    public int RequiredTemperature { get; } = requiredTemperature;
}