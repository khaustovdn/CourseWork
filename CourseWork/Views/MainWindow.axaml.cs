using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Media;
using Avalonia.ReactiveUI;
using CourseWork.Models;
using CourseWork.ViewModels;
using DynamicData.Binding;
using ReactiveUI;

namespace CourseWork.Views;

public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
{
    public MainWindow()
    {
        InitializeComponent();

        this.WhenActivated(action =>
        {
            action(ViewModel!.ShowProductDialog.RegisterHandler(DoShowDialogAsync));
            action(ViewModel!.ShowWarehouseDialog.RegisterHandler(DoShowDialogAsync));

            this.WhenAnyValue(x => x.ViewModel!.SelectedWarehouse, x => x.ViewModel!.SelectedWarehouse!.Products.Count)
                .Subscribe(newValue =>
                {
                    if (newValue.Item1 == null) return;
                    DataProducts.Background = newValue.Item1.CheckDeadline()
                        ? Brushes.Red
                        : DataWarehouse.Background;
                    DataProducts.ItemsSource = newValue.Item1 switch
                    {
                        RefrigeratedWarehouse => newValue.Item1.Products.Cast<FoodProduct>().ToList(),
                        TechnicalWarehouse => newValue.Item1.Products.Cast<ElectronicProduct>().ToList(),
                        _ => DataProducts.ItemsSource
                    };
                });

            this.WhenAnyValue(x => x.ViewModel!.SelectedProduct)
                .Subscribe(value => { DeleteProductButton.IsVisible = value != null; });

            this.WhenValueChanged(x => x.ViewModel!.SelectedWarehouse)
                .Subscribe(newValue =>
                {
                    if (newValue == null) return;
                    DataWarehouse.ItemsSource = newValue switch
                    {
                        RefrigeratedWarehouse value => new List<RefrigeratedWarehouse?> { value }
                            .Cast<RefrigeratedWarehouse>()
                            .Select(x => new
                            {
                                ID = x.Id,
                                x.Name,
                                x.Size,
                                x.City,
                                x.Temperature
                            }).ToList(),
                        TechnicalWarehouse value => new List<TechnicalWarehouse?> { value }
                            .Cast<TechnicalWarehouse>()
                            .Select(x => new
                            {
                                ID = x.Id,
                                x.Name,
                                x.Size,
                                x.City,
                                x.PowerSupplyLevel
                            }).ToList(),
                        _ => DataWarehouse.ItemsSource
                    };
                    AddProductButton.IsVisible = true;
                });
        });
    }

    private async Task DoShowDialogAsync(InteractionContext<ManagerWindowViewModel, Warehouse?> interaction)
    {
        var dialog = new ManagerWindow { DataContext = interaction.Input };

        var result = await dialog.ShowDialog<Warehouse?>(this);
        interaction.SetOutput(result);
    }

    private async Task DoShowDialogAsync(InteractionContext<ProductWindowViewModel, Product?> interaction)
    {
        var dialog = new ProductWindow { DataContext = interaction.Input };

        var result = await dialog.ShowDialog<Product?>(this);
        interaction.SetOutput(result);
    }
}