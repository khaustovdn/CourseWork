using System;
using System.Collections.ObjectModel;

namespace CourseWork.Models;

public interface IWarehouse
{
    public RefrigeratedWarehouse ConvertToRefrigeratedWarehouse =>
        this as RefrigeratedWarehouse ?? throw new InvalidOperationException();

    public TechnicalWarehouse ConvertToTechnicalWarehouse =>
        this as TechnicalWarehouse ?? throw new InvalidOperationException();

    public string? Address { get; }
    public int Size { get; }
    public ObservableCollection<IWare> Products { get; }
    public string? Name { get; }
}