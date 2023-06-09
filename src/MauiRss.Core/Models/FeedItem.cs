namespace MauiRss.Core.Models;

/// <summary>Feed Item.</summary>
public class FeedItem : INotifyPropertyChanged
{
	private bool isFavorite;
	private bool isRead;

	/// <summary>Initializes a new instance of the <see cref="FeedItem"/> class.</summary>
	public FeedItem()
	{
	}

	/// <inheritdoc/>
	public event PropertyChangedEventHandler? PropertyChanged;

	/// <summary>Gets or sets The author of the feed item.</summary>
	public string? Author
	{
		get;
		set;
	}

	/// <summary>Gets or sets The content of the feed item.</summary>
	public string? Content
	{
		get;
		set;
	}

	/// <summary>Gets or sets description of the feed item.</summary>
	public string? Description
	{
		get;
		set;
	}

	/// <summary>Gets or sets the feed list item id.</summary>
	public int FeedListItemId { get; set; }

	/// <summary>Gets or sets The html of the feed item.</summary>
	public string? Html
	{
		get;
		set;
	}

	/// <summary>Gets or sets the id.</summary>
	public int Id { get; set; }

	/// <summary>Gets or sets The image URL of the feed item.</summary>
	public string? ImageUrl
	{
		get;
		set;
	}

	/// <summary>Gets or sets a value indicating whether the feed is favourite.</summary>
	public bool IsFavorite
	{
		get => isFavorite;
		set => SetProperty(ref isFavorite, value);
	}

	/// <summary>Gets or sets a value indicating whether the feed item has been read.</summary>
	public bool IsRead
	{
		get => isRead;
		set => SetProperty(ref isRead, value);
	}

	/// <summary>Gets or sets link (URL) to the feed item.</summary>
	public string? Link
	{
		get;
		set;
	}

	/// <summary>Gets or sets The published date as DateTime.</summary>
	public DateTime? PublishingDate
	{
		get;
		set;
	}

	/// <summary>Gets or sets The publishing date as string.</summary>
	public string? PublishingDateString
	{
		get;
		set;
	}

	/// <summary>Gets or sets the id from the RSS feed.</summary>
	public string? RssId { get; set; }

	/// <summary>Gets or sets the title of the feed item.</summary>
	public string? Title
	{
		get;
		set;
	}

	/// <summary>On Property Changed.</summary>
	/// <param name="propertyName">Name of the property.</param>
	protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
	{
		PropertyChangedEventHandler? changed = PropertyChanged;
		if (changed == null)
		{
			return;
		}

		changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}

	protected bool SetProperty<T>(ref T backingStore, T value, Action? onChanged = null, [CallerMemberName] string propertyName = "")
	{
		if (EqualityComparer<T>.Default.Equals(backingStore, value))
		{
			return false;
		}

		backingStore = value;
		onChanged?.Invoke();
		OnPropertyChanged(propertyName);
		return true;
	}
}
