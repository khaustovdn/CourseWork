using System;
using Avalonia;
using Avalonia.Markup.Xaml;
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

        this.WhenActivated(action => action(ViewModel!.CreateWarehouseCommand.Subscribe(Close)));
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}