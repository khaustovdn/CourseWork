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
                            ViewModel!.Temperature = string.Empty;
                            this.WhenAnyValue(x => x.TextBoxWarehouse.Text)
                                .BindTo(ViewModel, t => t.Temperature);
                            TextBoxWarehouse.Watermark = "Temperature";
                            break;
                        case 1:
                            ViewModel!.PowerSupplyLevel = string.Empty;
                            if (ViewModel?.PowerSupplyLevel != null)
                                this.WhenAnyValue(x => x.TextBoxWarehouse.Text)
                                    .BindTo(ViewModel, t => t.PowerSupplyLevel);
                            TextBoxWarehouse.Watermark = "Power Supply Level";
                            break;
                    }

                    ViewModel!.Action = newValue;
                });
        });
    }
}