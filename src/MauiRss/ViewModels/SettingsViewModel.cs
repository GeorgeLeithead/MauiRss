namespace MauiRss.ViewModels;

/// <summary>Settings view model.</summary>
public partial class SettingsViewModel : BaseViewModel
{
	private bool isDarkModeEnabled;

	/// <summary>Initialises a new instance of the <see cref="SettingsViewModel" /> class.</summary>
	public SettingsViewModel()
	{
		IsBusy = true;
		Title = "Settings";
		appVersion = AppInfo.VersionString;
		isDarkModeEnabled = Settings.Theme == AppTheme.Dark;
		IsBusy = false;
	}

	/// <summary>Gets the application version.</summary>
	[ObservableProperty]
	private string appVersion;

	/// <summary>Gets or sets whether is dark mode.</summary>
	public bool IsDarkModeEnabled
	{
		get => isDarkModeEnabled;
		set
		{
			if (SetProperty(ref isDarkModeEnabled, value))
			{
				ChangeUserAppTheme(value);
			}
		}
	}

	/// <summary>Change the app theme for the user.</summary>
	/// <param name="activateDarkMode">Indicates whether to activate dark mode.</param>
	private static void ChangeUserAppTheme(bool activateDarkMode)
	{
		Settings.Theme = activateDarkMode
			? AppTheme.Dark
			: AppTheme.Light;

		TheTheme.SetTheme();
	}
}
