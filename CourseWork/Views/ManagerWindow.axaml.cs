using System;
using Avalonia;
using Avalonia.ReactiveUI;
using CourseWork.ViewModels;
using ReactiveUI;

namespace CourseWork.Views;

public partial class ManagerWindow : ReactiveWindow<ManagerWindowViewModel>
{
    public ManagerWindow()
    {
        InitializeComponent();
#if DEBUG
        this.AttachDevTools();
#endif

        this.WhenActivated(_ =>
            this.WhenAnyObservable(x => x.ViewModel!.CreateCommand).Subscribe(Close));
    }
}