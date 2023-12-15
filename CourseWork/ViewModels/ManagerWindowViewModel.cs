using System.Reactive;
using System.Threading.Tasks;
using Avalonia.Controls;
using CourseWork.Models;
using CourseWork.Views.Templates;
using ReactiveUI;

namespace CourseWork.ViewModels;

public class ManagerWindowViewModel : ViewModelBase
{
    private bool _action;
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
                b1.Length > 4 &&
                !string.IsNullOrWhiteSpace(b2) && b2.Length < 4 &&
                int.TryParse(b2, out _) &&
                int.Parse(b2) > 0 &&
                !string.IsNullOrWhiteSpace(b3) &&
                b3.Length > 4 &&
                ((b4 == false && !string.IsNullOrWhiteSpace(b5) && b5.Length < 4 && int.TryParse(b5, out _)) ||
                 (b4 && !string.IsNullOrWhiteSpace(b6) && b6.Length < 4 && int.TryParse(b6, out _) &&
                  int.Parse(b6) > 0 && int.Parse(b6) <= 10)));

        CreateCommand = ReactiveCommand.CreateFromTask(() => Task.FromResult(Action
            ? SetWarehouseType(new TechnicalWarehouse(Name, int.Parse(Size!), Address,
                int.Parse(PowerSupplyLevel!)))
            : SetWarehouseType(new RefrigeratedWarehouse(Name, int.Parse(Size!), Address,
                int.Parse(Temperature!)))), isValid);
    }

    private bool Action
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

    public ReactiveCommand<Unit, IWarehouse> CreateCommand { get; }

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

    public void SetAction(string type)
    {
        if (type == "refrigerated")
        {
            Action = false;
            WarehouseType = new RefrigeratedWarehouseTextBox();
        }
        else
        {
            Action = true;
            WarehouseType = new TechnicalWarehouseTextBox();
        }
    }

    private static IWarehouse SetWarehouseType(IWarehouse warehouse)
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