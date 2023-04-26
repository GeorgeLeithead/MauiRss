using System.Reflection;
using LiteDB;
using MauiRss.Core.Models;
using MauiRss.Core.Services;

namespace MauiRss.LiteDb;

/// <summary>Database Context.</summary>
public class LiteDBDatabaseContext : IDatabaseContext
{
	private const string DatabaseName = "mauirss.db";
	private const string FeedItemsCollection = "FeedItems";
	private const string FeedsCollection = "Feeds";
	private LiteDatabase? db;

	/// <summary>Initializes a new instance of the <see cref="LiteDBDatabaseContext"/> class.</summary>
	/// <param name="databasePath">Database path.</param>
	public LiteDBDatabaseContext(string databasePath = "") => OnConfiguring(databasePath);

	/// <summary>Gets the Feed Items.</summary>
	public ILiteCollection<FeedItem> FeedItems => db?.GetCollection<FeedItem>(FeedItemsCollection) ?? throw new NullReferenceException(nameof(db));

	/// <summary>Gets the Feed List Items.</summary>
	public ILiteCollection<FeedListItem> FeedListItems => db?.GetCollection<FeedListItem>(FeedsCollection) ?? throw new NullReferenceException(nameof(db));

	/// <inheritdoc/>
	public bool AddOrUpdateFeedItem(FeedItem item)
	{
		FeedItem existingItem = FeedItems.FindOne(n => n.Id == item.Id);
		if (existingItem is not null)
		{
			item.Id = existingItem.Id;
		}

		return FeedItems.Upsert(item);
	}

	/// <inheritdoc/>
	public bool AddOrUpdateFeedListItem(FeedListItem item)
	{
		FeedListItem? existingItem = FeedListItems.FindOne(n => n.Uri == item.Uri);
		if (existingItem is not null)
		{
			item.Id = existingItem.Id;
		}

		return FeedListItems.Upsert(item);
	}

	/// <inheritdoc/>
	public bool DoesFeedListItemExist(FeedListItem item) => FeedListItems.FindOne(n => n.Uri == item.Uri) is not null;

	/// <inheritdoc/>
	public List<FeedItem> GetFeedItems(FeedListItem item) => FeedItems.Find(n => n.FeedListItemId == item.Id).ToList();

	/// <inheritdoc/>
	public FeedListItem? GetFeedListItem(Uri uri) => FeedListItems.FindOne(n => n.Uri == uri);

	/// <inheritdoc/>
	public List<FeedListItem> GetFeedListItems() => FeedListItems.FindAll().ToList();

	/// <inheritdoc/>
	public bool RemoveFeedItem(FeedItem item) => FeedListItems.Delete(item.Id);

	/// <inheritdoc/>
	public bool RemoveFeedListItem(FeedListItem item) => FeedListItems.Delete(item.Id);

	private static string GetLocalPath()
	{
		var location = Assembly.GetExecutingAssembly()?.Location ?? string.Empty;
		if (string.IsNullOrEmpty(location))
		{
			return string.Empty;
		}

		return Path.GetDirectoryName(location) ?? string.Empty;
	}

	private void OnConfiguring(string databasePath = "")
	{
		databasePath = string.IsNullOrEmpty(databasePath) ? GetLocalPath() : databasePath;
		if (!Directory.Exists(databasePath))
		{
			_ = Directory.CreateDirectory(databasePath);
		}

		var dbPath = Path.Combine(databasePath, DatabaseName);
		db = new LiteDatabase(dbPath);
	}
}
