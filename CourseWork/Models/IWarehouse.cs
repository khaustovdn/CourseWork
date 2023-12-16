using System.Collections.ObjectModel;

namespace CourseWork.Models;

public interface IWarehouse
{
    public string? Address { get; }
    public ObservableCollection<ISubject> Products { get; }
}