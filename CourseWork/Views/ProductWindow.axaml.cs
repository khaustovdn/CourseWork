using System;
using Avalonia.ReactiveUI;
using CourseWork.Models;
using CourseWork.ViewModels;
using ReactiveUI;

namespace CourseWork.Views;

public partial class ProductWindow : ReactiveWindow<ProductWindowViewModel>
{
    public ProductWindow()
    {
        InitializeComponent();

        this.WhenActivated(_ =>
        {
            this.WhenAnyObservable(x => x.ViewModel!.CreateCommand).Subscribe(Close);

            this.WhenAnyValue(x => x.ViewModel!.Warehouse)
                .Subscribe(newValue =>
                {
                    switch (newValue)
                    {
                        case RefrigeratedWarehouse:
                            this.WhenAnyValue(x => x.TextBoxProduct.Text)
                                .BindTo(ViewModel, t => t.ExpirationDate.Text);
                            TextBoxProduct.Watermark = "Expiration Date";
                            break;
                        case TechnicalWarehouse:
                            this.WhenAnyValue(x => x.TextBoxProduct.Text)
                                .BindTo(ViewModel, t => t.WarrantyPeriod.Text);
                            TextBoxProduct.Watermark = "Warranty Period";
                            break;
                    }
                });
        });
    }
}