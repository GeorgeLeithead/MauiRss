namespace MauiRss.Tests;

/// <summary>Feed Reader tests.</summary>
public class FeedReaderTests
{
	private readonly IRssService rss;

	/// <summary>Initializes a new instance of the <see cref="FeedReaderTests"/> class.</summary>
	public FeedReaderTests() => rss = new FeedReaderService();

	/// <summary>Get Feed.</summary>
	/// <returns>Task.</returns>
	[Fact]
	public async Task GetFeed()
	{
		(FeedListItem? feedList, IList<FeedItem>? feedItemsList) = await rss.ReadFeedAsync("https://www.google.co.uk/alerts/feeds/05445642933184449933/10305745548987223724");
		Assert.NotNull(feedList);
		Assert.NotNull(feedItemsList);
		_ = feedList.Name.Should().Be("Google Alert - \"George Leithead\"");
	}
}
