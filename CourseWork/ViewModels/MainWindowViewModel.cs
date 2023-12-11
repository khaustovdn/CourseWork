using System.Reactive.Linq;
using System.Windows.Input;
using Avalonia.Controls;
using CourseWork.Models;
using CourseWork.Views.Templates;
using ReactiveUI;

namespace CourseWork.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private RefrigeratedWarehouse? _refrigeratedWarehouse;
    private IWarehouse? _selectedWarehouse;
    private UserControl? _selectedWarehouseControl;
    private TechnicalWarehouse? _technicalWarehouse;

    public MainWindowViewModel()
    {
        Manager = new Manager();
        ShowDialog = new Interaction<ManagerWindowViewModel, IWarehouse?>();
        CreateWarehouseCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            var manager = new ManagerWindowViewModel();
            var result = await ShowDialog.Handle(manager);
            if (result != null) Manager.Add(result);
        });
    }

    public RefrigeratedWarehouse? RefrigeratedWarehouse
    {
        get => _refrigeratedWarehouse;
        private set => this.RaiseAndSetIfChanged(ref _refrigeratedWarehouse, value);
    }

    public TechnicalWarehouse? TechnicalWarehouse
    {
        get => _technicalWarehouse;
        private set => this.RaiseAndSetIfChanged(ref _technicalWarehouse, value);
    }

    public ICommand CreateWarehouseCommand { get; }

    public UserControl? SelectedWarehouseControl
    {
        get => _selectedWarehouseControl;
        set => this.RaiseAndSetIfChanged(ref _selectedWarehouseControl, value);
    }

    public IWarehouse SelectedWarehouse
    {
        get
        {
            switch (_selectedWarehouse)
            {
                case RefrigeratedWarehouse refrigeratedWarehouse:
                    SelectedWarehouseControl = new RefrigeratedWarehouseTextBlock();
                    RefrigeratedWarehouse = refrigeratedWarehouse;
                    return refrigeratedWarehouse;
                case TechnicalWarehouse technicalWarehouse:
                    SelectedWarehouseControl = new TechnicalWarehouseTextBlock();
                    TechnicalWarehouse = technicalWarehouse;
                    return technicalWarehouse;
                default:
                    return null!;
            }
        }
        set => this.RaiseAndSetIfChanged(ref _selectedWarehouse, value);
    }

    public Manager Manager { get; }
    public Interaction<ManagerWindowViewModel, IWarehouse?> ShowDialog { get; }
}