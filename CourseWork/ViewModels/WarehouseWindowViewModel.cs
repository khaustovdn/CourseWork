using System.Reactive;
using System.Threading.Tasks;
using CourseWork.Models;
using ReactiveUI;

namespace CourseWork.ViewModels;

public class WarehouseWindowViewModel : ViewModelBase, IObject
{
    public WarehouseWindowViewModel()
    {
        CreateWarehouseCommand =
            ReactiveCommand.CreateFromTask(() => Task.FromResult(new Warehouse(Name, Size, Address)));
    }

    public string Address { get; set; }
    public ReactiveCommand<Unit, Warehouse> CreateWarehouseCommand { get; }

    public string Name { get; set; }
    public int Size { get; set; }
}