﻿using System.Reactive.Linq;
using System.Windows.Input;
using Avalonia.Controls;
using CourseWork.Models;
using ReactiveUI;

namespace CourseWork.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private IWarehouse? _selectedWarehouse;
    private UserControl? _selectedWarehouseControl;

    public MainWindowViewModel()
    {
        Manager = new Manager();
        ShowWarehouseDialog = new Interaction<ManagerWindowViewModel, IWarehouse?>();
        ShowProductDialog = new Interaction<ProductWindowViewModel, IProduct?>();
        CreateWarehouseCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            var manager = new ManagerWindowViewModel();
            var result = await ShowWarehouseDialog.Handle(manager);
            if (result != null) Manager.Add(result);
        });
        AddProductCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            var manager = new ProductWindowViewModel(SelectedWarehouse);
            var result = await ShowProductDialog.Handle(manager);
            if (result != null) SelectedWarehouse?.Products.Add(result);
        });
    }

    public ICommand CreateWarehouseCommand { get; }
    public ICommand? AddProductCommand { get; }

    public UserControl? SelectedWarehouseControl
    {
        get => _selectedWarehouseControl;
        set => this.RaiseAndSetIfChanged(ref _selectedWarehouseControl, value);
    }

    public IWarehouse? SelectedWarehouse
    {
        get => _selectedWarehouse;
        set => this.RaiseAndSetIfChanged(ref _selectedWarehouse, value);
    }

    public Manager Manager { get; }
    public Interaction<ManagerWindowViewModel, IWarehouse?> ShowWarehouseDialog { get; }
    public Interaction<ProductWindowViewModel, IProduct?> ShowProductDialog { get; }
}