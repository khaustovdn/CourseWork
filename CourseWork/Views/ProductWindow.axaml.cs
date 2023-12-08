using Avalonia.ReactiveUI;
using CourseWork.ViewModels;

namespace CourseWork.Views;

public partial class ProductWindow : ReactiveWindow<ProductWindowViewModel>
{
    public ProductWindow()
    {
        InitializeComponent();
    }
}