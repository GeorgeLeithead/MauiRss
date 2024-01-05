namespace MauiRss.Pages;

/// <summary>Dummy page.</summary>
public partial class DummyPage : ContentPage
{
	/// <summary>Initialises a new instance of the <see cref="DummyPage" /> class.</summary>
	public DummyPage(DummyViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}
