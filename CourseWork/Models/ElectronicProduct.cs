using System;

namespace CourseWork.Models;

public class ElectronicProduct(string? name, int size, int price, DateTime? warrantyPeriod)
    : Product(name, size, price)
{
    public DateTime? WarrantyPeriod { get; } = warrantyPeriod;
}