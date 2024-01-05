namespace MauiRss.Helpers;

/// <summary>The Theme for the application.</summary>
public static class TheTheme
{
	/// <summary>Set the application theme.</summary>
	public static void SetTheme()
	{
		if (Application.Current is null)
		{
			return;
		}

		Application.Current.UserAppTheme = Settings.Theme switch
		{
			AppTheme.Dark => AppTheme.Dark,
			_ => AppTheme.Light,
		};
		_ = WeakReferenceMessenger.Default.Send("MauiRss", "ChangeWebTheme");
	}
}
