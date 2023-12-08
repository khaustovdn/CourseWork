using Avalonia.ReactiveUI;
using CourseWork.ViewModels;

namespace CourseWork.Views;

public partial class WarehouseWindow : ReactiveWindow<WarehouseWindowViewModel>
{
    public WarehouseWindow()
    {
        InitializeComponent();
    }
}