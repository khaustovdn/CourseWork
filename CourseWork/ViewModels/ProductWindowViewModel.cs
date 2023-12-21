using System;
using System.Reactive;
using System.Threading.Tasks;
using CourseWork.Models;
using ReactiveUI;

namespace CourseWork.ViewModels;

public class ProductWindowViewModel : ViewModelBase
{
    public ProductWindowViewModel(Warehouse warehouse)
    {
        Warehouse = warehouse;

        var isValid = this.WhenAnyValue(
            x => x.Name.Text,
            x => x.Size.Text,
            x => x.Price.Text,
            x => x.Warehouse,
            x => x.ExpirationDate.Text,
            x => x.WarrantyPeriod.Text,
            (_, _, _, b4, _, _) =>
                Name.IsValid<string>() && Size.IsValid<int>() && Price.IsValid<int>() &&
                ((b4 is RefrigeratedWarehouse && ExpirationDate.IsValid<DateTime>()) ||
                 (b4 is TechnicalWarehouse && WarrantyPeriod.IsValid<DateTime>())));

        CreateCommand = ReactiveCommand.CreateFromTask(() => Task.FromResult(Warehouse is RefrigeratedWarehouse
            ? SetProductType(new FoodProduct(Warehouse.Id, Name.ToString(), Size.ToInt(), Price.ToInt(),
                ExpirationDate.ToDate()))
            : SetProductType(new ElectronicProduct(Warehouse.Id, Name.ToString(), Size.ToInt(), Price.ToInt(),
                WarrantyPeriod.ToDate()))), isValid);
    }

    public Warehouse Warehouse { get; }
    public ValidInput Name { get; set; } = new();
    public ValidInput Size { get; set; } = new();
    public ValidInput Price { get; set; } = new();
    public ValidInput WarrantyPeriod { get; set; } = new();
    public ValidInput ExpirationDate { get; set; } = new();
    public ReactiveCommand<Unit, Product> CreateCommand { get; }

    private static Product SetProductType(Product product)
    {
        return product;
    }
}