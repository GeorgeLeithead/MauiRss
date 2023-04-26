namespace MauiRss.Core.Services;

/// <summary>Cross Platform Services.</summary>
public interface IPlatformService
{
	/// <summary>Open a URL.</summary>
	/// <param name="url">The URL.</param>
	/// <returns>Task.</returns>
	Task OpenBrowserAsync(string url);

	/// <summary>Share a URL.</summary>
	/// <param name="url">The URL.</param>
	/// <returns>Task.</returns>
	Task ShareUrlAsync(string url);
}
