using System.Reactive;
using System.Threading.Tasks;
using CourseWork.Models;
using ReactiveUI;

namespace CourseWork.ViewModels;

public class ProductWindowViewModel : ViewModelBase
{
    public ProductWindowViewModel(int action = 0)
    {
        Action = action;

        var isValid = this.WhenAnyValue(
            x => x.Name.Text,
            x => x.Size.Text,
            x => x.Action,
            x => x.ExpirationDate.Text,
            x => x.WarrantyPeriod.Text,
            (_, _, b3, _, _) =>
                Name.IsValid<string>() && Size.IsValid<int>() &&
                ((b3 == 0 && ExpirationDate.IsValid<int>()) || (b3 == 1 && WarrantyPeriod.IsValid<int>())));

        CreateCommand = ReactiveCommand.CreateFromTask(() => Task.FromResult(Action == 0
            ? SetProductType(new FoodProduct(Name.ToString(), Size.ToInt(), ExpirationDate.ToInt()))
            : SetProductType(new ElectronicProduct(Name.ToString(), Size.ToInt(), WarrantyPeriod.ToInt()))), isValid);
    }

    public int Action { get; }

    public ConnectedObject Name { get; set; } = new();

    public ConnectedObject Size { get; set; } = new();

    public ConnectedObject WarrantyPeriod { get; set; } = new();

    public ConnectedObject ExpirationDate { get; set; } = new();

    public ReactiveCommand<Unit, Product> CreateCommand { get; }

    private static Product SetProductType(Product product)
    {
        return product switch
        {
            FoodProduct productWare => new FoodProduct(productWare.Name, productWare.Size, productWare.ExpirationDate),
            ElectronicProduct electronicsWare => new ElectronicProduct(electronicsWare.Name, electronicsWare.Size,
                electronicsWare.WarrantyPeriod),
            _ => product
        };
    }
}