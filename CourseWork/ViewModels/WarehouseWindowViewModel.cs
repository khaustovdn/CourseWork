using System.Reactive;
using CourseWork.Models;
using ReactiveUI;
using ReactiveUI.Validation.Extensions;
using ReactiveUI.Validation.Helpers;

namespace CourseWork.ViewModels;

public class WarehouseWindowViewModel : ReactiveValidationObject
{
    private readonly ValidationHelper _isAddressValid;
    private readonly ValidationHelper _isNameValid;
    private readonly ValidationHelper _isSizeValid;
    private string? _address;
    private bool _isEnabled;
    private string? _name;
    private string? _size;

    public WarehouseWindowViewModel()
    {
        _isNameValid = this.ValidationRule(
            viewModel => viewModel.Name,
            name => !string.IsNullOrWhiteSpace(name),
            "Name shouldn't be null or white space.");
        _isSizeValid = this.ValidationRule(
            viewModel => viewModel.Size,
            size => !string.IsNullOrWhiteSpace(size) && int.TryParse(size, out _) && int.Parse(size) > 0,
            "Size shouldn't be 0 or less.");
        _isAddressValid = this.ValidationRule(
            viewModel => viewModel.Address,
            address => !string.IsNullOrWhiteSpace(address),
            "Address shouldn't be null or white space.");

        CreateWarehouseCommand = ReactiveCommand.Create(() => new Warehouse(Name, int.Parse(Size!), Address));
    }

    public bool IsEnabled
    {
        get => _isEnabled;
        set => _isEnabled = this.RaiseAndSetIfChanged(ref _isEnabled, value);
    }

    public string? Address
    {
        get => _address;
        set
        {
            this.RaiseAndSetIfChanged(ref _address, value);
            IsEnabled = IsValid();
        }
    }

    public ReactiveCommand<Unit, Warehouse> CreateWarehouseCommand { get; }

    public string? Name
    {
        get => _name;
        set
        {
            this.RaiseAndSetIfChanged(ref _name, value);
            IsEnabled = IsValid();
        }
    }

    public string? Size
    {
        get => _size;
        set
        {
            this.RaiseAndSetIfChanged(ref _size, value);
            IsEnabled = IsValid();
        }
    }

    private bool IsValid()
    {
        return _isNameValid.IsValid && _isSizeValid.IsValid && _isAddressValid.IsValid;
    }
}