namespace MauiRss.Core.Events;

public class FeedItemSelectedEventArgs : EventArgs
{
	public FeedItemSelectedEventArgs(FeedListItem? feedListItem, FeedItem item)
	{
		FeedItem = item;
		FeedListItem = feedListItem;
	}

	public FeedItem FeedItem { get; }

	public FeedListItem? FeedListItem { get; }
}
