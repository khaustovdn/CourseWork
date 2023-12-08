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
            action(ViewModel!.ShowDialog.RegisterHandler(DoShowDialogAsync)));
    }

    private async Task DoShowDialogAsync(InteractionContext<WarehouseWindowViewModel, Warehouse?> interaction)
    {
        var dialog = new WarehouseWindow { DataContext = interaction.Input };

        var result = await dialog.ShowDialog<Warehouse?>(this);
        interaction.SetOutput(result);
    }
}