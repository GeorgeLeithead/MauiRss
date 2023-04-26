namespace MauiRss.Core.ViewModels;

/// <summary>RSS Feed List View Model.</summary>
public class RssFeedListViewModel : RssFeedBaseViewModel
{
	/// <summary>Initializes a new instance of the <see cref="RssFeedListViewModel"/> class.</summary>
	/// <param name="services"><see cref="IServiceProvider"/>.</param>
	public RssFeedListViewModel(IServiceProvider services) : base(services)
	{
		FeedListItems = new ObservableCollection<FeedListItem>();
		FeedListItemSelectedCommand = new AsyncCommand<FeedListItem>(
		async (item) => OnFeedListItemSelected?.Invoke(this, new FeedListItemSelectedEventArgs(item)),
		null,
		ErrorHandler);
		OnFeedListItemUpdated += RssFeedListViewModel_OnFeedListItemUpdated;
	}

	/// <summary>Fired when a feed list item updates.</summary>
	public event EventHandler<FeedListItemSelectedEventArgs>? OnFeedListItemSelected;

	/// <summary>Gets the list of feed list items.</summary>
	public ObservableCollection<FeedListItem> FeedListItems { get; }

	/// <summary>Gets the UpdateFeedListItem.</summary>
	public AsyncCommand<FeedListItem> FeedListItemSelectedCommand { get; private set; }

	/// <inheritdoc/>
	public override async Task OnLoad()
	{
		await base.OnLoad();
		UpdateFeeds();
	}

	private void RssFeedListViewModel_OnFeedListItemUpdated(object? sender, FeedListItemUpdatedEventArgs e)
	{
		FeedListItem? item = FeedListItems.FirstOrDefault(n => n.Uri == e.FeedListItem.Uri);
		if (item is not null)
		{
			FeedListItems[FeedListItems.IndexOf(item)] = e.FeedListItem;
		}
		else
		{
			FeedListItems.Add(e.FeedListItem);
		}
	}

	private void UpdateFeeds()
	{
		FeedListItems.Clear();

		List<FeedListItem> feedItems = Context.GetFeedListItems();
		foreach (FeedListItem item in feedItems)
		{
			FeedListItems.Add(item);
		}
	}
}
