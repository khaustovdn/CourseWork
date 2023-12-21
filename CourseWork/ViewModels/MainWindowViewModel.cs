using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Text.Json;
using System.Windows.Input;
using CourseWork.Models;
using ReactiveUI;

namespace CourseWork.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private Product? _selectedProduct;
    private Warehouse? _selectedWarehouse;

    public MainWindowViewModel()
    {
        Manager = new Manager();
        ShowWarehouseDialog = new Interaction<ManagerWindowViewModel, Warehouse?>();
        ShowProductDialog = new Interaction<ProductWindowViewModel, Product?>();
        AddWarehouseCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            var manager = new ManagerWindowViewModel();
            var result = await ShowWarehouseDialog.Handle(manager);
            if (result != null && Manager.Warehouses.All(x => x.Name != result.Name && x.Id != result.Id))
            {
                Manager.Warehouses.Add(result);
                await using var fs = new FileStream("../../../DataBase/Manager.json", FileMode.Create);
                await JsonSerializer.SerializeAsync<dynamic>(fs, Manager.Warehouses);
            }
        });
        AddProductCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            if (SelectedWarehouse != null)
            {
                var manager = new ProductWindowViewModel(SelectedWarehouse);
                var result = await ShowProductDialog.Handle(manager);
                if (result != null)
                {
                    SelectedWarehouse?.Products.Add(result);
                    await using var fs = new FileStream("../../../DataBase/Manager.json", FileMode.Create);
                    await JsonSerializer.SerializeAsync<dynamic>(fs, Manager.Warehouses);
                }
            }
        });
        DeleteProductCommand = ReactiveCommand.Create(() =>
        {
            if (SelectedProduct != null)
            {
                SelectedWarehouse?.Products.Remove(SelectedProduct);
                using var fs = new FileStream("../../../DataBase/Manager.json", FileMode.Create);
                JsonSerializer.Serialize(fs, Manager.Warehouses);
            }
        });
    }

    public ICommand AddWarehouseCommand { get; }
    public ICommand? AddProductCommand { get; }
    public ICommand? DeleteProductCommand { get; }

    public Product? SelectedProduct
    {
        get => _selectedProduct;
        set => this.RaiseAndSetIfChanged(ref _selectedProduct, value);
    }

    public Warehouse? SelectedWarehouse
    {
        get => _selectedWarehouse;
        set => this.RaiseAndSetIfChanged(ref _selectedWarehouse, value);
    }

    public Manager Manager { get; }
    public Interaction<ManagerWindowViewModel, Warehouse?> ShowWarehouseDialog { get; }
    public Interaction<ProductWindowViewModel, Product?> ShowProductDialog { get; }
}