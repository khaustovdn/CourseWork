using System.Collections.ObjectModel;

namespace CourseWork.Models;

public class Manager : ICollection
{
    public Manager()
    {
        Warehouses = new ObservableCollection<IWarehouse>();
    }

    public ObservableCollection<IWarehouse> Warehouses { get; }

    public void Add(object? o)
    {
        if (o is Warehouse warehouse) Warehouses.Add(warehouse);
    }
}