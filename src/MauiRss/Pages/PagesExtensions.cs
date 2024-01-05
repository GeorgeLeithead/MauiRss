namespace MauiRss.Pages;

/// <summary>Page extensions.</summary>
public static class PagesExtensions
{
	/// <summary>Configure pages.</summary>
	/// <param name="builder">MAUI application builder.</param>
	/// <returns>Builder object.</returns>
	public static MauiAppBuilder ConfigurePages(this MauiAppBuilder builder)
	{
		// main pages of the app, AddSingleton
		_ = builder.Services.AddSingleton<DummyPage>();
		_ = builder.Services.AddSingleton<SettingsPage>();

		// pages that are navigated to, AddTransient
		////_ = builder.Services.AddTransient<ChartPage>();

		return builder;
	}
}
