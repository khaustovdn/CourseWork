using System.Reactive;
using System.Threading.Tasks;
using CourseWork.Models;
using ReactiveUI;

namespace CourseWork.ViewModels;

public class ProductWindowViewModel : ViewModelBase
{
    private Product? _product;

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
            (_, _, _, _, _, _) =>
            {
                _product = Warehouse is RefrigeratedWarehouse
                    ? SetProductType(new FoodProduct(Name.ToString(), Size.ToInt(), Price.ToInt(),
                        ExpirationDate.ToDate(), RequiredTemperature.Number))
                    : SetProductType(new ElectronicProduct(Name.ToString(), Size.ToInt(), Price.ToInt(),
                        WarrantyPeriod.ToDate()));
                return warehouse.IsValid(_product);
            });

        CreateCommand = ReactiveCommand.CreateFromTask(() => Task.FromResult(_product!), isValid);
    }

    public Warehouse Warehouse { get; }
    public ValidInput Name { get; set; } = new();
    public ValidInput Size { get; set; } = new();
    public ValidInput Price { get; set; } = new();
    public ValidInput WarrantyPeriod { get; set; } = new();
    public ValidInput ExpirationDate { get; set; } = new();

    public ValidInput RequiredTemperature { get; set; } = new();
    public ReactiveCommand<Unit, Product> CreateCommand { get; }

    private static Product SetProductType(Product product)
    {
        return product;
    }
}