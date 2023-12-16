using System.Reactive;
using System.Threading.Tasks;
using Avalonia.Controls;
using CourseWork.Models;
using ReactiveUI;

namespace CourseWork.ViewModels;

public class ManagerWindowViewModel : ViewModelBase
{
    private int _action;
    private string? _address;
    private string? _name;
    private string? _powerSupplyLevel;
    private string? _size;
    private string? _temperature;
    private UserControl? _warehouseType;

    public ManagerWindowViewModel()
    {
        var isValid = this.WhenAnyValue(
            x => x.Name,
            x => x.Size,
            x => x.Address,
            x => x.Action,
            x => x.Temperature,
            x => x.PowerSupplyLevel,
            (b1, b2, b3, b4, b5, b6) =>
                !string.IsNullOrWhiteSpace(b1) &&
                !string.IsNullOrWhiteSpace(b2) &&
                int.TryParse(b2, out _) &&
                int.Parse(b2) > 0 &&
                !string.IsNullOrWhiteSpace(b3) &&
                ((b4 == 0 && !string.IsNullOrWhiteSpace(b5) && int.TryParse(b5, out _)) ||
                 (b4 == 1 && !string.IsNullOrWhiteSpace(b6) && int.TryParse(b6, out _) &&
                  int.Parse(b6) > 0 && int.Parse(b6) <= 10)));

        CreateCommand = ReactiveCommand.CreateFromTask(() => Task.FromResult(Action == 1
            ? SetWarehouseType(new TechnicalWarehouse(Name, int.Parse(Size!), Address,
                int.Parse(PowerSupplyLevel!)))
            : SetWarehouseType(new RefrigeratedWarehouse(Name, int.Parse(Size!), Address,
                int.Parse(Temperature!)))), isValid);
    }

    public int Action
    {
        get => _action;
        set => this.RaiseAndSetIfChanged(ref _action, value);
    }

    public UserControl? WarehouseType
    {
        get => _warehouseType;
        set => this.RaiseAndSetIfChanged(ref _warehouseType, value);
    }

    public string? Temperature
    {
        get => _temperature;
        set => this.RaiseAndSetIfChanged(ref _temperature, value);
    }

    public string? PowerSupplyLevel
    {
        get => _powerSupplyLevel;
        set => this.RaiseAndSetIfChanged(ref _powerSupplyLevel, value);
    }

    public string? Address
    {
        get => _address;
        set => this.RaiseAndSetIfChanged(ref _address, value);
    }

    public ReactiveCommand<Unit, Warehouse> CreateCommand { get; }

    public string? Name
    {
        get => _name;
        set => this.RaiseAndSetIfChanged(ref _name, value);
    }

    public string? Size
    {
        get => _size;
        set => this.RaiseAndSetIfChanged(ref _size, value);
    }

    private static Warehouse SetWarehouseType(Warehouse warehouse)
    {
        return warehouse switch
        {
            TechnicalWarehouse technicalWarehouse => new TechnicalWarehouse(
                technicalWarehouse.Name, technicalWarehouse.Size, technicalWarehouse.Address,
                technicalWarehouse.PowerSupplyLevel),
            RefrigeratedWarehouse refrigeratedWarehouse => new RefrigeratedWarehouse(
                refrigeratedWarehouse.Name, refrigeratedWarehouse.Size, refrigeratedWarehouse.Address,
                refrigeratedWarehouse.Temperature),
            _ => warehouse
        };
    }
}