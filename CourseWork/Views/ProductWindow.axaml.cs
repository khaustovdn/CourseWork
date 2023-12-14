using System;
using Avalonia.ReactiveUI;
using CourseWork.ViewModels;
using ReactiveUI;

namespace CourseWork.Views;

public partial class ProductWindow : ReactiveWindow<ProductWindowViewModel>
{
    public ProductWindow()
    {
        InitializeComponent();

        this.WhenActivated(_ =>
            this.WhenAnyObservable(x => x.ViewModel!.CreateCommand).Subscribe(Close));
    }
}