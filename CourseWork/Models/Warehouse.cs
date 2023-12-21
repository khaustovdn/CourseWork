using System;
using System.Collections.ObjectModel;
using System.Linq;

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

    public bool CheckDeadline()
    {
        return Products.Any(x =>
            (x as FoodProduct)?.ExpirationDate < DateTime.Now ||
            (x as ElectronicProduct)?.WarrantyPeriod < DateTime.Now);
    }

    private bool CheckOverflow(Product product)
    {
        return Products.Sum(x => x.Size) + product.Size >= Size;
    }

    public bool IsValid(Product? product)
    {
        return product != null &&
               !string.IsNullOrEmpty(product.Name) &&
               product.Name.Length > 0 &&
               product is { Size: >= 2, Price: >= 100 and <= 1000000 } &&
               Products.All(x => x.Name != product.Name && x.Id != product.Id) &&
               !CheckOverflow(product);
    }


    public void AddProduct(Product? product)
    {
        if (IsValid(product)) Products.Add(product!);
    }
}