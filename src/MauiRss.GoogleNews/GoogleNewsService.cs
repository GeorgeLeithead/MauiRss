namespace MauiRss.GoogleNews;

/// <summary>Google News Service.</summary>
public class GoogleNewsService
{
#pragma warning disable S1075 // URIs should not be hardcoded
	private readonly string mainFeedUri = "https://news.google.com/rss?gl={1}&hl={0}&ceid={1}:{0}";
	private readonly IRssService rssService;
	private readonly string sectionUri = "https://news.google.com/news/rss/headlines/section/topic/{0}?ned={2}&hl={1}";
#pragma warning restore S1075 // URIs should not be hardcoded

	public GoogleNewsService(IRssService service)
	{
		ArgumentNullException.ThrowIfNull(service);
		rssService = service;
	}

	public async Task<(FeedListItem? FeedList, IList<FeedItem>? FeedItemList)> ReadMainPageAsync(CultureInfo? culture = default, CancellationToken? token = default)
	{
		(var cultureName, var cultureLocale) = GetCultureNameAndLocal(culture);
		var mainFeedFormat = string.Format(CultureInfo.InvariantCulture, mainFeedUri, cultureName, cultureLocale);
		return await rssService.ReadFeedAsync(mainFeedFormat, token);
	}

	public async Task<(FeedListItem? FeedList, IList<FeedItem>? FeedItemList)> ReadSectionAsync(NewsSections section, CultureInfo? culture = default, CancellationToken? token = default)
	{
		if (section is NewsSections.Unknown)
		{
			throw new ArgumentException(null, nameof(section));
		}

		(var cultureName, var cultureLocale) = GetCultureNameAndLocal(culture);
		var sectionFeedFormat = string.Format(CultureInfo.InvariantCulture, sectionUri, section.ToString().ToUpperInvariant(), cultureName, cultureLocale);
		return await rssService.ReadFeedAsync(sectionFeedFormat, token);
	}

	private static (string CultureName, string CultureLocal) GetCultureNameAndLocal(CultureInfo? culture = default)
	{
		culture ??= CultureInfo.CurrentCulture;
		var cultureNameAndLocale = culture.ToString().Split('-');
		var cultureLocale = "GB";
		var cultureName = "en";
		if (cultureNameAndLocale.Length == 2)
		{
			cultureName = cultureNameAndLocale[0];
			cultureLocale = cultureNameAndLocale[1];
		}

		return (cultureName, cultureLocale);
	}
}
