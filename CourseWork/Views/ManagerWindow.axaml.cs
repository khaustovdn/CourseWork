using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Avalonia.ReactiveUI;
using CourseWork.ViewModels;
using Newtonsoft.Json;
using ReactiveUI;

namespace CourseWork.Views;

public partial class ManagerWindow : ReactiveWindow<ManagerWindowViewModel>
{
    public ManagerWindow()
    {
        InitializeComponent();

        this.WhenActivated(_ =>
        {
            using (var r = new StreamReader("../../../DataBase/Cities.json"))
            {
                var json = r.ReadToEnd();
                var items = JsonConvert.DeserializeObject<List<dynamic>>(json);
                if (items != null) ComboBoxCity.ItemsSource = items.Select(x => x.name);
                ComboBoxCity.SelectedIndex = 0;
            }

            this.WhenAnyObservable(x => x.ViewModel!.CreateCommand).Subscribe(Close);

            this.WhenAnyValue(x => x.ComboBoxAction.SelectedIndex)
                .Subscribe(newValue =>
                {
                    switch (newValue)
                    {
                        case 0:
                            this.WhenAnyValue(x => x.SliderWarehouse.Value)
                                .BindTo(ViewModel, t => t.Temperature.Number);
                            this.WhenAnyValue(x => x.SliderWarehouse.Value)
                                .Subscribe(newTemp => { TextBlockWarehouse.Text = $"Temperature {(int)newTemp}"; });
                            SliderWarehouse.Minimum = -25;
                            SliderWarehouse.Maximum = 25;
                            break;
                        case 1:
                            this.WhenAnyValue(x => x.SliderWarehouse.Value)
                                .BindTo(ViewModel, t => t.PowerSupplyLevel.Number);
                            this.WhenAnyValue(x => x.SliderWarehouse.Value)
                                .Subscribe(newPowerSupplyLevel =>
                                {
                                    TextBlockWarehouse.Text = $"Power Supply Level {(int)newPowerSupplyLevel}";
                                });
                            SliderWarehouse.Minimum = 0;
                            SliderWarehouse.Maximum = 10;
                            break;
                    }

                    ViewModel!.Action = newValue;
                });
        });
    }
}