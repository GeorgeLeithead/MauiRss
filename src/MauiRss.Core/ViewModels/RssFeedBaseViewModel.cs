namespace MauiRss.Core.ViewModels;

/// <summary>RSS Feed Base View Model.</summary>
public class RssFeedBaseViewModel : BaseViewModel
{
	/// <summary>Initializes a new instance of the <see cref="RssFeedBaseViewModel"/> class.</summary>
	/// <param name="services"><see cref="IServiceProvider"/>/</param>
	public RssFeedBaseViewModel(IServiceProvider services) : base(services)
	{
	}

	/// <summary>Adds New Feed List.</summary>
	/// <param name="feedUri">The Feed Uri.</param>
	/// <returns>Task.</returns>
	public async Task<FeedListItem?> AddOrUpdateNewFeedListItemAsync(string feedUri)
	{
		try
		{
			(FeedListItem? feed, IList<FeedItem>? feedListItems) = await Rss.ReadFeedAsync(feedUri);
			FeedListItem? item = Context.GetFeedListItem(new Uri(feedUri));
			item ??= feed;

			if (item is null || feedListItems is null)
			{
				// TODO: Handle error. It shouldn't be null.
				return null;
			}

			_ = Context.AddOrUpdateFeedListItem(item);

			foreach (FeedItem feedItem in feedListItems)
			{
				feedItem.FeedListItemId = item.Id;
				_ = Context.AddOrUpdateFeedItem(feedItem);
				SendFeedUpdateRequest(item, feedItem);
			}

			SendFeedListUpdateRequest(item);

			return item;
		}
		catch (Exception ex)
		{
			ErrorHandler.HandleError(ex);
		}

		return null;
	}
}
