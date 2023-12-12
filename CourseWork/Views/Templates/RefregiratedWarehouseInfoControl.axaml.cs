using Avalonia.Controls;

namespace CourseWork.Views.Templates;

public partial class RefrigeratedWarehouseInfoControl : UserControl
{
    public RefrigeratedWarehouseInfoControl()
    {
        InitializeComponent();
        ContentControl.Content = new WarehouseInfoControl();
    }
}