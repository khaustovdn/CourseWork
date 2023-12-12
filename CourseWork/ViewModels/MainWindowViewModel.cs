using System;
using System.Reactive.Linq;
using System.Windows.Input;
using Avalonia.Controls;
using CourseWork.Models;
using CourseWork.Views.Templates;
using DynamicData.Binding;
using ReactiveUI;

namespace CourseWork.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private IWarehouse? _selectedWarehouse;
    private UserControl? _selectedWarehouseControl;

    public MainWindowViewModel()
    {
        Manager = new Manager();
        ShowDialog = new Interaction<ManagerWindowViewModel, IWarehouse?>();
        CreateWarehouseCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            var manager = new ManagerWindowViewModel();
            var result = await ShowDialog.Handle(manager);
            if (result != null) Manager.Add(result);
        });

        this.WhenValueChanged(x => x.SelectedWarehouse).Subscribe(newValue =>
        {
            if (newValue is null) return;
            SelectedWarehouseControl = SelectedWarehouse switch
            {
                RefrigeratedWarehouse => new RefrigeratedWarehouseInfoControl(),
                TechnicalWarehouse => new TechnicalWarehouseInfoControl(),
                _ => SelectedWarehouseControl
            };
        });
    }

    public ICommand CreateWarehouseCommand { get; }

    public UserControl? SelectedWarehouseControl
    {
        get => _selectedWarehouseControl;
        set => this.RaiseAndSetIfChanged(ref _selectedWarehouseControl, value);
    }

    public IWarehouse? SelectedWarehouse
    {
        get
        {
            return _selectedWarehouse switch
            {
                RefrigeratedWarehouse refrigeratedWarehouse => refrigeratedWarehouse,
                TechnicalWarehouse technicalWarehouse => technicalWarehouse,
                _ => null
            };
        }
        set => this.RaiseAndSetIfChanged(ref _selectedWarehouse, value);
    }

    public Manager Manager { get; }
    public Interaction<ManagerWindowViewModel, IWarehouse?> ShowDialog { get; }
}