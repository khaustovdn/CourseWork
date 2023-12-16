using System;
using Avalonia.ReactiveUI;
using CourseWork.ViewModels;
using ReactiveUI;

namespace CourseWork.Views;

public partial class ManagerWindow : ReactiveWindow<ManagerWindowViewModel>
{
    public ManagerWindow()
    {
        InitializeComponent();

        this.WhenActivated(_ =>
        {
            this.WhenAnyObservable(x => x.ViewModel!.CreateCommand).Subscribe(Close);

            this.WhenAnyValue(x => x.ComboBoxAction.SelectedIndex)
                .Subscribe(newValue =>
                {
                    switch (newValue)
                    {
                        case 0:
                            this.WhenAnyValue(x => x.TextBoxWarehouse.Text)
                                .BindTo(ViewModel, t => t.Temperature.Text);
                            TextBoxWarehouse.Watermark = "Temperature";
                            break;
                        case 1:
                            this.WhenAnyValue(x => x.TextBoxWarehouse.Text)
                                .BindTo(ViewModel, t => t.PowerSupplyLevel.Text);
                            TextBoxWarehouse.Watermark = "Power Supply Level";
                            break;
                    }

                    ViewModel!.Action = newValue;
                });
        });
    }
}