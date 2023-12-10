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
        ShowDialog = new Interaction<ManagerWindowViewModel, IWarehouse?>();
        CreateWarehouseCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            var manager = new ManagerWindowViewModel();
            var result = await ShowDialog.Handle(manager);
            if (result != null)
                Manager.Add(result);
        });
    }

    public ICommand CreateWarehouseCommand { get; }
    public Manager Manager { get; }
    public Interaction<ManagerWindowViewModel, IWarehouse?> ShowDialog { get; }
}