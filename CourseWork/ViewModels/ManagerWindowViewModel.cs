using System.Reactive;
using System.Threading.Tasks;
using CourseWork.Models;
using ReactiveUI;

namespace CourseWork.ViewModels;

public class ManagerWindowViewModel : ViewModelBase
{
    private int _action;

    public ManagerWindowViewModel()
    {
        var isValid = this.WhenAnyValue(
            x => x.Name.Text,
            x => x.Size.Text,
            x => x.Address.Text,
            x => x.Action,
            x => x.Temperature.Text,
            x => x.PowerSupplyLevel.Text,
            (_, _, _, b4, _, _) =>
                Name.IsValid<string>() && Size.IsValid<int>() && Size.ToInt() > 0 &&
                Address.IsValid<string>() &&
                ((b4 == 0 && Temperature.IsValid<int>()) || (b4 == 1 &&
                                                             PowerSupplyLevel.IsValid<int>() &&
                                                             PowerSupplyLevel.ToInt() is >= 0 and < 10)));

        CreateCommand = ReactiveCommand.CreateFromTask(() => Task.FromResult(Action == 0
                ? SetWarehouseType(new RefrigeratedWarehouse(Name.ToString(), Size.ToInt(), Address.ToString(),
                    Temperature.ToInt()))
                : SetWarehouseType(new TechnicalWarehouse(Name.ToString(), Size.ToInt(), Address.ToString(),
                    PowerSupplyLevel.ToInt()))),
            isValid);
    }

    public int Action
    {
        get => _action;
        set => this.RaiseAndSetIfChanged(ref _action, value);
    }

    public ConnectedObject Temperature { get; set; } = new();

    public ConnectedObject PowerSupplyLevel { get; set; } = new();

    public ConnectedObject Address { get; set; } = new();

    public ReactiveCommand<Unit, Warehouse> CreateCommand { get; }

    public ConnectedObject Name { get; set; } = new();

    public ConnectedObject Size { get; set; } = new();

    private static Warehouse SetWarehouseType(Warehouse warehouse)
    {
        return warehouse switch
        {
            RefrigeratedWarehouse refrigeratedWarehouse => new RefrigeratedWarehouse(
                refrigeratedWarehouse.Name, refrigeratedWarehouse.Size, refrigeratedWarehouse.Address,
                refrigeratedWarehouse.Temperature),
            TechnicalWarehouse technicalWarehouse => new TechnicalWarehouse(
                technicalWarehouse.Name, technicalWarehouse.Size, technicalWarehouse.Address,
                technicalWarehouse.PowerSupplyLevel),
            _ => warehouse
        };
    }
}