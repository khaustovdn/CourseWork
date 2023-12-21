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
            (_, _) =>
                Name.IsValid<string>() && Size.IsValid<int>() && Size.ToInt() < 120);

        CreateCommand = ReactiveCommand.CreateFromTask(() => Task.FromResult(Action == 0
                ? SetWarehouseType(new RefrigeratedWarehouse(Name.ToString(), Size.ToInt(), City.ToString(),
                    Temperature.Number))
                : SetWarehouseType(new TechnicalWarehouse(Name.ToString(), Size.ToInt(), City.ToString(),
                    PowerSupplyLevel.Number))),
            isValid);
    }

    public int Action
    {
        get => _action;
        set => this.RaiseAndSetIfChanged(ref _action, value);
    }

    public ValidInput Temperature { get; set; } = new();
    public ValidInput PowerSupplyLevel { get; set; } = new();

    public ValidInput City { get; set; } = new();
    public ReactiveCommand<Unit, Warehouse> CreateCommand { get; }
    public ValidInput Name { get; set; } = new();
    public ValidInput Size { get; set; } = new();

    private static Warehouse SetWarehouseType(Warehouse warehouse)
    {
        return warehouse;
    }
}