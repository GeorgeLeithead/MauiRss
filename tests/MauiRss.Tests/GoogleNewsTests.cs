namespace MauiRss.Tests;

public class GoogleNewsTests
{
	private readonly GoogleNewsService googleNews;
	private readonly IRssService rss;

	/// <summary>Initializes a new instance of the <see cref="GoogleNewsTests"/> class.</summary>
	public GoogleNewsTests()
	{
		rss = new FeedReaderService();
		googleNews = new GoogleNewsService(rss);
	}

	[Fact]
	public async Task GetMainFeed()
	{
		(FeedListItem? feedList, IList<FeedItem>? feedItemsList) = await googleNews.ReadMainPageAsync();
		Assert.NotNull(feedList);
		Assert.NotNull(feedItemsList);
	}

	[Theory]
	[InlineData(NewsSections.Business)]
	[InlineData(NewsSections.Health)]
	[InlineData(NewsSections.Nation)]
	[InlineData(NewsSections.Science)]
	[InlineData(NewsSections.Sports)]
	[InlineData(NewsSections.Technology)]
	[InlineData(NewsSections.World)]
	public async Task GetSectionFeed(NewsSections sections)
	{
		(FeedListItem? feedList, IList<FeedItem>? feedItemsList) = await googleNews.ReadSectionAsync(sections);
		Assert.NotNull(feedList);
		Assert.NotNull(feedItemsList);
	}
}
