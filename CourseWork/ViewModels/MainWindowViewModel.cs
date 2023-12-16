using System.Reactive.Linq;
using System.Windows.Input;
using Avalonia.Controls;
using CourseWork.Models;
using ReactiveUI;

namespace CourseWork.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private Warehouse? _selectedWarehouse;
    private UserControl? _selectedWarehouseControl;

    public MainWindowViewModel()
    {
        Manager = new Manager();
        ShowWarehouseDialog = new Interaction<ManagerWindowViewModel, Warehouse?>();
        ShowProductDialog = new Interaction<ProductWindowViewModel, Product?>();
        CreateWarehouseCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            var manager = new ManagerWindowViewModel();
            var result = await ShowWarehouseDialog.Handle(manager);
            if (result != null) Manager.Warehouses.Add(result);
        });
        AddProductCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            if (SelectedWarehouse != null)
            {
                var manager = new ProductWindowViewModel(SelectedWarehouse);
                var result = await ShowProductDialog.Handle(manager);
                if (result != null) SelectedWarehouse?.Products.Add(result);
            }
        });
    }

    public ICommand CreateWarehouseCommand { get; }
    public ICommand? AddProductCommand { get; }

    public UserControl? SelectedWarehouseControl
    {
        get => _selectedWarehouseControl;
        set => this.RaiseAndSetIfChanged(ref _selectedWarehouseControl, value);
    }

    public Warehouse? SelectedWarehouse
    {
        get => _selectedWarehouse;
        set => this.RaiseAndSetIfChanged(ref _selectedWarehouse, value);
    }

    public Manager Manager { get; }
    public Interaction<ManagerWindowViewModel, Warehouse?> ShowWarehouseDialog { get; }
    public Interaction<ProductWindowViewModel, Product?> ShowProductDialog { get; }
}