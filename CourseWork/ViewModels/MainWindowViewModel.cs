using System.Reactive.Linq;
using System.Windows.Input;
using CourseWork.Models;
using ReactiveUI;

namespace CourseWork.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public MainWindowViewModel()
    {
        Manager = new Manager();
        ShowDialog = new Interaction<WarehouseWindowViewModel, Warehouse>();
        CreateWarehouseCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            var manager = new WarehouseWindowViewModel();
            var result = await ShowDialog.Handle(manager);
            Manager.Add(result);
        });
    }

    public ICommand CreateWarehouseCommand { get; }
    public Manager Manager { get; }
    public Interaction<WarehouseWindowViewModel, Warehouse> ShowDialog { get; }
}