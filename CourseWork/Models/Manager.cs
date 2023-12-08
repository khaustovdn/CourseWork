using System.Collections.ObjectModel;

namespace CourseWork.Models;

public class Manager : ICollection
{
    private ObservableCollection<Warehouse> Warehouses { get; } = new();

    public void Add(object? o)
    {
        if (o is Warehouse warehouse) Warehouses.Add(warehouse);
    }
}