namespace MauiRss.ViewModels;

/// <summary>View model extensions.</summary>
public static class ViewModelExtensions
{
	/// <summary>Configure view models.</summary>
	/// <param name="builder">MAUI application builder.</param>
	/// <returns>Builder object.</returns>
	public static MauiAppBuilder ConfigureViewModels(this MauiAppBuilder builder)
	{
		_ = builder.Services.AddSingleton<DummyViewModel>();
		_ = builder.Services.AddSingleton<SettingsViewModel>();
		_ = builder.Services.AddSingleton<RssFeedItemListViewModel>();
		_ = builder.Services.AddSingleton<RssFeedListViewModel>();
		return builder;
	}
}
