using System.Collections.ObjectModel;
using System.Linq;

namespace CourseWork.Models;

public class Manager
{
    public ObservableCollection<Warehouse> Warehouses { get; } = [];

    public bool IsValid(Warehouse? warehouse)
    {
        return warehouse != null &&
               !string.IsNullOrEmpty(warehouse.Name) &&
               warehouse.Name.Length > 0 &&
               warehouse.Size is >= 20 and <= 200 &&
               Warehouses.All(x => x.Name != warehouse.Name && x.Id != warehouse.Id);
    }

    public void AddWarehouse(Warehouse? warehouse)
    {
        if (warehouse != null && Warehouses.All(x => x.Name != warehouse.Name && x.Id != warehouse.Id))
            Warehouses.Add(warehouse);
    }
}