namespace MauiRss.FeedReader;

/// <summary>Feed Reader Service.</summary>
public class FeedReaderService : IRssService
{
	private readonly HttpClient client;
	private readonly HtmlParser parser;

	/// <summary>Initializes a new instance of the <see cref="FeedReaderService"/> class.</summary>
	public FeedReaderService()
	{
		client = new HttpClient();
		parser = new HtmlParser();
	}

	/// <inheritdoc/>
	public async Task<(FeedListItem? FeedList, IList<Core.Models.FeedItem>? FeedItemList)> ReadFeedAsync(string feedUri, CancellationToken? token = default)
	{
		CancellationToken cancelationToken = token ?? CancellationToken.None;
		Feed? feed = await CodeHollow.FeedReader.FeedReader.ReadAsync(feedUri, cancelationToken);
		if (feed is null)
		{
			return (null, null);
		}

		var item = feed.ToFeedListItem(feedUri);
		if (item.ImageCache is null && item.ImageUri is not null)
		{
			item.ImageCache = await client.GetByteArrayAsync(item.ImageUri);
		}
		else
		{
			// TODO: Add a default icon.
			////item.ImageCache ??= Utilities.GetPlaceholderIcon();
		}

		var feedItemList = new List<Core.Models.FeedItem>();
		foreach (CodeHollow.FeedReader.FeedItem? feedItem in feed.Items)
		{
			using AngleSharp.Html.Dom.IHtmlDocument document = await parser.ParseDocumentAsync(feedItem.Content);
			AngleSharp.Dom.IElement? image = document.QuerySelector("img");
			var imageUrl = string.Empty;
			if (image is not null)
			{
				imageUrl = image.GetAttribute("src");
			}

			feedItemList.Add(feedItem.ToFeedItem(item, imageUrl));
		}

		return (item, feedItemList);
	}
}
