namespace MauiRss.Core.ViewModels;

/// <summary>RSS Feed Article View Model.</summary>
public class RssFeedArticleViewModel : BaseViewModel
{
	private FeedItem? feedItem;
	private FeedListItem? feedListItem;
	private string html;
	private readonly IRssWebView webView;

	/// <summary>Initializes a new instance of the <see cref="RssFeedArticleViewModel"/> class.</summary>
	/// <param name="webView">RSS WebView.</param>
	/// <param name="services"><see cref="IServiceProvider"/>.</param>
	/// <param name="item">Feed Item.</param>
	public RssFeedArticleViewModel(IRssWebView webView, IServiceProvider services, FeedListItem? feedListItem = null, FeedItem? item = null) : base(services)
	{
		this.feedListItem = feedListItem;
		feedItem = item;
		this.webView = webView;
		html = string.Empty;
		SetIsFavoriteFeedItem = new AsyncCommand<FeedItem>(
		async (item) => await SetIsFavoriteFeedItemAsync(item),
		(FeedItem item) => item is not null,
		ErrorHandler);
		SetIsReadFeedItem = new AsyncCommand<FeedItem>(
		async (item) => await SetIsReadFeedItemAsync(item),
		(FeedItem item) => item is not null,
		ErrorHandler);
		ShareLinkCommand = new AsyncCommand<FeedItem>(
		async (item) => await ShareLinkAsync(item),
		(FeedItem item) => item is not null && !IsBusy,
		ErrorHandler);
		OpenBrowserCommand = new AsyncCommand<FeedItem>(
		async (item) => await OpenBrowserAsync(item),
		(FeedItem item) => item is not null && !IsBusy,
		ErrorHandler);
	}

	/// <summary>Gets or sets the Feed Item.</summary>
	public FeedItem? FeedItem
	{
		get => feedItem;
		set => SetProperty(ref feedItem, value);
	}

	/// <summary>Gets or sets the Feed Html.</summary>
	public string Html
	{
		get => html;
		set => SetProperty(ref html, value);
	}

	/// <summary>Gets the ShareLinkCommand.</summary>
	public AsyncCommand<FeedItem> OpenBrowserCommand { get; private set; }

	/// <summary>Gets the SetIsFavoriteFeedItem.</summary>
	public AsyncCommand<FeedItem> SetIsFavoriteFeedItem { get; private set; }

	/// <summary>Gets the SetIsReadFeedItem.</summary>
	public AsyncCommand<FeedItem> SetIsReadFeedItem { get; private set; }

	/// <summary>Gets the ShareLinkCommand.</summary>
	public AsyncCommand<FeedItem> ShareLinkCommand { get; private set; }

	public override async Task OnLoad()
	{
		await base.OnLoad();
		await UpdateFeedItem(feedListItem, feedItem);
	}

	/// <inheritdoc/>
	public override void RaiseCanExecuteChanged()
	{
		base.RaiseCanExecuteChanged();
		SetIsFavoriteFeedItem.RaiseCanExecuteChanged();
		SetIsReadFeedItem.RaiseCanExecuteChanged();
		OpenBrowserCommand.RaiseCanExecuteChanged();
		ShareLinkCommand.RaiseCanExecuteChanged();
	}

	public async Task UpdateFeedItem(FeedListItem? feedListItem, FeedItem? item)
	{
		if (item is null || feedListItem is null)
		{
			return;
		}

		this.feedListItem = feedListItem;
		FeedItem = item;
		Title = FeedItem.Title ?? string.Empty;
		await RenderHtmlAsync();
		FeedItem.IsRead = true;
		_ = Context.AddOrUpdateFeedItem(FeedItem);
	}

	private async Task RenderHtmlAsync()
	{
		if (feedItem is null || feedListItem is null)
		{
			return;
		}

		Html = await Templates.RenderFeedItemAsync(feedListItem, feedItem);
		webView.SetSource(Html);
	}
}
