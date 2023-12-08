using System;
using Avalonia;
using Avalonia.ReactiveUI;
using CourseWork.ViewModels;
using ReactiveUI;

namespace CourseWork.Views;

public partial class WarehouseWindow : ReactiveWindow<WarehouseWindowViewModel>
{
    public WarehouseWindow()
    {
        InitializeComponent();
#if DEBUG
        this.AttachDevTools();
#endif

        this.WhenActivated(_ =>
            this.WhenAnyObservable(x => x.ViewModel!.CreateWarehouseCommand).Subscribe(Close));
    }
}