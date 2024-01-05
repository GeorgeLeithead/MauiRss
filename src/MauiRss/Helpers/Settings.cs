namespace MauiRss.Helpers;

/// <summary>Settings helper.</summary>
public static class Settings
{
	private const AppTheme theme = AppTheme.Light;

	/// <summary>Application Theme.</summary>
	public static AppTheme Theme
	{
		get => Enum.Parse<AppTheme>(Preferences.Get("Theme", Enum.GetName(theme)));
		set => Preferences.Set(nameof(Theme), value.ToString());
	}
}
