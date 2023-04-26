namespace MauiRss.Core.Events;

public class FeedListItemUpdatedEventArgs : EventArgs
{
	public FeedListItemUpdatedEventArgs(FeedListItem item) => FeedListItem = item;

	public FeedListItem FeedListItem { get; }
}
