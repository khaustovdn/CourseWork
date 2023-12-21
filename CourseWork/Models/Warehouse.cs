using System;
using System.Collections.ObjectModel;

namespace CourseWork.Models;

public abstract class Warehouse
{
    protected Warehouse(string? name, int size, string? city)
    {
        var random = new Random();
        Id = random.Next(90000) + 10000;
        Name = name;
        Size = size;
        City = city;
    }

    public int Id { get; }
    public int Size { get; }
    public string? Name { get; }
    public string? City { get; }
    public ObservableCollection<Product> Products { get; } = [];

    public static bool CheckDate(Product product)
    {
        switch (product)
        {
            case FoodProduct foodProduct when foodProduct.ExpirationDate <= DateTime.Now:
            case ElectronicProduct electronicProduct when electronicProduct.WarrantyPeriod <= DateTime.Now:
                return false;
            default:
                return true;
        }
    }
}