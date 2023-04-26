namespace MauiRss.Core.Services;

/// <summary>RSS service.</summary>
public interface IRssService
{
	/// <summary>Read a feed.</summary>
	/// <param name="feedUri">A feed URI.</param>
	/// <param name="token">A cancellation token.</param>
	/// <returns>A task result tuple of the feed list item and a list of items.</returns>
	Task<(FeedListItem? FeedList, IList<FeedItem>? FeedItemList)> ReadFeedAsync(string feedUri, CancellationToken? token = default);
}
