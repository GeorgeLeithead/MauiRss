namespace MauiRss.Core.Events;

public class FeedItemUpdatedEventArgs : EventArgs
{
	public FeedItemUpdatedEventArgs(FeedListItem feedItem, FeedItem item)
	{
		FeedListItem = feedItem;
		FeedItem = item;
	}

	public FeedItem FeedItem { get; }

	public FeedListItem FeedListItem { get; }
}
