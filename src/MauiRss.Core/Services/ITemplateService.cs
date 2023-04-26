namespace MauiRss.Core.Services;

/// <summary>Template Service.</summary>
public interface ITemplateService
{
	/// <summary>Render Feed Item.</summary>
	/// <param name="item">FeedItem.</param>
	/// <returns>Html String.</returns>
	public Task<string> RenderFeedItemAsync(FeedListItem feedListItem, FeedItem item);
}
