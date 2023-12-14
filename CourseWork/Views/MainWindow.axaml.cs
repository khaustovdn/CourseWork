using System;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.ReactiveUI;
using CourseWork.Models;
using CourseWork.ViewModels;
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

        this.WhenAnyValue(x => x.ViewModel!.SelectedWarehouse!.Products.Count).Subscribe(_ =>
        {
            DataProducts.ItemsSource = ViewModel?.SelectedWarehouse switch
            {
                RefrigeratedWarehouse => ViewModel.SelectedWarehouse.Products.Select(x => x as FoodProduct),
                TechnicalWarehouse => ViewModel.SelectedWarehouse.Products.Select(x => x as ElectronicProduct),
                _ => DataProducts.ItemsSource
            };
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