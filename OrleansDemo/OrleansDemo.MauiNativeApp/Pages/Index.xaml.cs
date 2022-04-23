using OrleansDemo.Domain.ViewModels;

namespace OrleansDemo.MauiNativeApp;

public partial class Index : ContentPage
{
	public Index(IndexViewModel indexViewModel)
	{
		BindingContext = indexViewModel;

		InitializeComponent();
	}
}