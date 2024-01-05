namespace MauiRss.Pages;

/// <summary>Settings page.</summary>
public partial class SettingsPage : ContentPage
{
	/// <summary>Initialises a new instance of the <see cref="SettingsPage" /> class.</summary>
	public SettingsPage(SettingsViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}
