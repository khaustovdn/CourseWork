using System;
using Avalonia.ReactiveUI;
using CourseWork.Models;
using CourseWork.ViewModels;
using ReactiveUI;

namespace CourseWork.Views;

public partial class ProductWindow : ReactiveWindow<ProductWindowViewModel>
{
    public ProductWindow(Warehouse selectedWarehouse)
    {
        InitializeComponent();

        DataContext = new ProductWindowViewModel(selectedWarehouse);

        this.WhenActivated(_ =>
        {
            this.WhenAnyObservable(x => x.ViewModel!.CreateCommand).Subscribe(Close);

            this.WhenAnyValue(x => x.ViewModel!.SelectedWarehouse)
                .Subscribe(newValue =>
                {
                    switch (newValue)
                    {
                        case RefrigeratedWarehouse:
                            this.WhenAnyValue(x => x.TextBoxProduct.Text)
                                .BindTo(ViewModel, t => t.ExpirationDate);
                            TextBoxProduct.Watermark = "Expiration Date";
                            break;
                        case TechnicalWarehouse:
                            this.WhenAnyValue(x => x.TextBoxProduct.Text)
                                .BindTo(ViewModel, t => t.WarrantyPeriod);
                            TextBoxProduct.Watermark = "Warranty Period";
                            break;
                    }
                });
        });
    }
}