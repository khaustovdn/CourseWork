using Avalonia.Controls;

namespace CourseWork.Views.Templates;

public partial class TechnicalWarehouseInfoControl : UserControl
{
    public TechnicalWarehouseInfoControl()
    {
        InitializeComponent();
        ContentControl.Content = new WarehouseInfoControl();
    }
}