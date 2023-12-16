using System.Reactive;
using System.Threading.Tasks;
using CourseWork.Models;
using ReactiveUI;

namespace CourseWork.ViewModels;

public class ProductWindowViewModel : ViewModelBase
{
    private string? _expirationDate;
    private string? _name;
    private string? _size;
    private string? _warrantyPeriod;

    public ProductWindowViewModel(int action = 0)
    {
        Action = action;

        var isValid = this.WhenAnyValue(
            x => x.Name,
            x => x.Size,
            x => x.Action,
            x => x.ExpirationDate,
            x => x.WarrantyPeriod,
            (b1, b2, b3, b4, b5) =>
                !string.IsNullOrWhiteSpace(b1) &&
                !string.IsNullOrWhiteSpace(b2) &&
                int.TryParse(b2, out _) &&
                int.Parse(b2) > 0 &&
                ((b3 == 0 && !string.IsNullOrWhiteSpace(b4) && int.TryParse(b4, out _)) ||
                 (b3 == 1 && !string.IsNullOrWhiteSpace(b5) && int.TryParse(b5, out _))));

        CreateCommand = ReactiveCommand.CreateFromTask(() => Task.FromResult(Action == 0
            ? SetProductType(new FoodProduct(Name, int.Parse(Size!), int.Parse(ExpirationDate!)))
            : SetProductType(new ElectronicProduct(Name, int.Parse(Size!), int.Parse(WarrantyPeriod!)))), isValid);
    }

    public int Action { get; }

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

    public string? WarrantyPeriod
    {
        get => _warrantyPeriod;
        set => this.RaiseAndSetIfChanged(ref _warrantyPeriod, value);
    }

    public string? ExpirationDate
    {
        get => _expirationDate;
        set => this.RaiseAndSetIfChanged(ref _expirationDate, value);
    }

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