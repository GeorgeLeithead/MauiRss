namespace MauiRss.Core.ViewModels;

/// <summary>RSS Feed Item List View model.</summary>
public class RssFeedItemListViewModel : RssFeedBaseViewModel
{
	private FeedListItem? feedListItem;

	/// <summary>Initializes a new instance of the <see cref="RssFeedItemListViewModel"/> class.</summary>
	/// <param name="services"><see cref="IServiceProvider"/>.</param>
	public RssFeedItemListViewModel(IServiceProvider services, FeedListItem? item = null) : base(services)
	{
		FeedListItem = item;
		FeedItems = new ObservableCollection<FeedItem>();
		GetCachedFeedItemsCommand = new AsyncCommand<FeedListItem>(
		GetCachedFeedItems,
		null,
		ErrorHandler);
		OnFeedItemUpdated += RssFeedItemListViewModel_OnFeedItemUpdated;
		FeedItemSelectedCommand = new AsyncCommand<FeedItem>(
		async (item) => OnFeedItemSelected?.Invoke(this, new FeedItemSelectedEventArgs(FeedListItem, item)),
		null,
		ErrorHandler);
	}

	/// <summary>Fired when a feed item is selected.</summary>
	public event EventHandler<FeedItemSelectedEventArgs>? OnFeedItemSelected;

	/// <summary>Gets the list of feed items.</summary>
	public ObservableCollection<FeedItem> FeedItems { get; }

	/// <summary>Gets the UpdateFeedListItem.</summary>
	public AsyncCommand<FeedItem> FeedItemSelectedCommand { get; private set; }

	/// <summary>Gets or sets the Feed List Item.</summary>
	public FeedListItem? FeedListItem
	{
		get => feedListItem;
		set => SetProperty(ref feedListItem, value);
	}

	/// <summary>Gets the UpdateFeedListItem.</summary>
	public AsyncCommand<FeedListItem> GetCachedFeedItemsCommand { get; private set; }

	/// <inheritdoc/>
	public override async Task OnLoad()
	{
		await base.OnLoad();
		if (feedListItem is not null)
		{
			await GetCachedFeedItems(feedListItem);
		}
	}

	private async Task GetCachedFeedItems(FeedListItem item)
	{
		ArgumentNullException.ThrowIfNull(item);
		FeedListItem = item;
		Title = item.Name ?? string.Empty;
		FeedItems.Clear();
		var feedItems = Context.GetFeedItems(FeedListItem).OrderByDescending(n => n.PublishingDate).ToList();
		if (feedItems.Count > 0)
		{
			foreach (FeedItem? feedItem in feedItems)
			{
				FeedItems.Add(feedItem);
			}
		}
		else
		{
			_ = await AddOrUpdateNewFeedListItemAsync(item.Uri?.ToString() ?? throw new ArgumentNullException(nameof(item.Uri)));
		}

		OnPropertyChanged(nameof(FeedItems));
	}

	private void RssFeedItemListViewModel_OnFeedItemUpdated(object? sender, FeedItemUpdatedEventArgs e)
	{
		if (FeedListItem?.Id != e.FeedListItem.Id)
		{
			FeedListItem = e.FeedListItem;
			FeedItems.Clear();
		}

		FeedItem? item = FeedItems.FirstOrDefault(n => n.Id == e.FeedItem.Id);
		if (item is null)
		{
			FeedItems.Add(e.FeedItem);
		}
		else
		{
			FeedItems[FeedItems.IndexOf(item)] = item;
		}
	}
}
