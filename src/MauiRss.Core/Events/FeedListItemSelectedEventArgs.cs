namespace MauiRss.Core.Events;

public class FeedListItemSelectedEventArgs : EventArgs
{
	public FeedListItemSelectedEventArgs(FeedListItem item) => FeedListItem = item;

	public FeedListItem FeedListItem { get; }
}
