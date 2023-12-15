using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            action(ViewModel!.ShowWarehouseDialog.RegisterHandler(DoShowDialogAsync)));
        this.WhenActivated(action =>
            action(ViewModel!.ShowProductDialog.RegisterHandler(DoShowDialogAsync)));

        this.WhenAnyValue(x => x.ViewModel!.SelectedWarehouse!.Products.Count)
            .Subscribe(_ =>
            {
                DataProducts.ItemsSource = ViewModel?.SelectedWarehouse switch
                {
                    RefrigeratedWarehouse => ViewModel.SelectedWarehouse.Products.Select(x => x as FoodProduct),
                    TechnicalWarehouse => ViewModel.SelectedWarehouse.Products.Select(x => x as ElectronicProduct),
                    _ => DataProducts.ItemsSource
                };

                if (ViewModel?.SelectedWarehouse?.Products.Count > 0)
                    DataWarehouse.Columns.RemoveAt(3);
            });

        this.WhenValueChanged(x => x.ViewModel!.SelectedWarehouse)
            .Subscribe(newValue =>
            {
                if (newValue != null)
                    DataWarehouse.ItemsSource = newValue switch
                    {
                        RefrigeratedWarehouse value => new List<RefrigeratedWarehouse?> { value }
                            .Cast<RefrigeratedWarehouse>()
                            .Select(x => new
                            {
                                x.Name,
                                x.Size,
                                x.Address,
                                x.Temperature
                            }).ToList(),
                        TechnicalWarehouse value => new List<TechnicalWarehouse?> { value }
                            .Cast<TechnicalWarehouse>()
                            .Select(x => new
                            {
                                x.Name,
                                x.Size,
                                x.Address,
                                x.PowerSupplyLevel
                            }).ToList(),
                        _ => DataWarehouse.ItemsSource
                    };

                AddProductButton.IsVisible = true;
            });
    }

    private async Task DoShowDialogAsync(InteractionContext<ManagerWindowViewModel, IWarehouse?> interaction)
    {
        var dialog = new ManagerWindow { DataContext = interaction.Input };

        var result = await dialog.ShowDialog<IWarehouse?>(this);
        interaction.SetOutput(result);
    }

    private async Task DoShowDialogAsync(InteractionContext<ProductWindowViewModel, IProduct?> interaction)
    {
        var dialog = new ProductWindow { DataContext = interaction.Input };

        var result = await dialog.ShowDialog<IProduct?>(this);
        interaction.SetOutput(result);
    }
}