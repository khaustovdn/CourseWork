using System;

namespace CourseWork.Models;

public abstract class Product
{
    protected Product(string? name, int size, int price)
    {
        var random = new Random();
        Id = random.Next(900) + 100;
        Name = name;
        Size = size;
        Price = price;
    }

    public string? Name { get; }
    public int Size { get; }
    public int Id { get; }
    public int Price { get; }
}