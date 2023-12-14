using System.Reactive;
using System.Threading.Tasks;
using Avalonia.Controls;
using CourseWork.Models;
using CourseWork.Views.Templates;
using ReactiveUI;

namespace CourseWork.ViewModels;

public class ProductWindowViewModel : ReactiveObject
{
    private string? _expirationDate;
    private string? _name;
    private string? _size;
    private UserControl? _wareType;
    private string? _warrantyPeriod;

    public ProductWindowViewModel(IWarehouse? selectedWarehouse)
    {
        WareType = selectedWarehouse switch
        {
            RefrigeratedWarehouse => new ProductWareTextBox(),
            TechnicalWarehouse => new ElectronicsWareTextBox(),
            _ => WareType
        };

        var isValid = this.WhenAnyValue(
            x => x.Name,
            x => x.Size,
            x => x.WarrantyPeriod,
            x => x.ExpirationDate,
            (b1, b2, b3, b4) =>
                !string.IsNullOrWhiteSpace(b1) &&
                b1.Length > 4 &&
                !string.IsNullOrWhiteSpace(b2) && b2.Length < 4 &&
                int.TryParse(b2, out _) &&
                int.Parse(b2) > 0 &&
                ((selectedWarehouse is TechnicalWarehouse && !string.IsNullOrWhiteSpace(b3) && b3.Length < 4 &&
                  int.TryParse(b3, out _)) ||
                 (selectedWarehouse is RefrigeratedWarehouse && !string.IsNullOrWhiteSpace(b4) && b4.Length < 4 &&
                  int.TryParse(b4, out _))));

        CreateCommand = ReactiveCommand.CreateFromTask(() => Task.FromResult(selectedWarehouse is RefrigeratedWarehouse
            ? SetProductType(new Product(Name, int.Parse(Size!), int.Parse(ExpirationDate!)))
            : SetProductType(new Electronics(Name, int.Parse(Size!), int.Parse(WarrantyPeriod!)))), isValid);
    }

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

    public UserControl? WareType
    {
        get => _wareType;
        set => this.RaiseAndSetIfChanged(ref _wareType, value);
    }

    public ReactiveCommand<Unit, IWare> CreateCommand { get; }

    private static IWare SetProductType(IWare ware)
    {
        return ware switch
        {
            Electronics electronicsWare => new Electronics(electronicsWare.Name, electronicsWare.Size,
                electronicsWare.WarrantyPeriod),
            Product productWare => new Product(productWare.Name, productWare.Size, productWare.ExpirationDate),
            _ => ware
        };
    }
}