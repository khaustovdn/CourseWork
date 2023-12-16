using System;
using Avalonia.ReactiveUI;
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

            this.WhenAnyValue(x => x.ViewModel!.Action)
                .Subscribe(newValue =>
                {
                    switch (newValue)
                    {
                        case 0:
                            this.WhenAnyValue(x => x.TextBoxProduct.Text)
                                .BindTo(ViewModel, t => t.ExpirationDate.Text);
                            TextBoxProduct.Watermark = "Expiration Date";
                            break;
                        case 1:
                            this.WhenAnyValue(x => x.TextBoxProduct.Text)
                                .BindTo(ViewModel, t => t.WarrantyPeriod.Text);
                            TextBoxProduct.Watermark = "Warranty Period";
                            break;
                    }
                });
        });
    }
}