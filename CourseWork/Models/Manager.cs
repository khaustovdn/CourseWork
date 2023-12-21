using System.Collections.ObjectModel;

namespace CourseWork.Models;

public class Manager
{
    public ObservableCollection<Warehouse> Warehouses { get; } = [];
}