namespace MauiRss.Core.ViewModels;

/// <summary>Base View Model.</summary>
public class BaseViewModel : INotifyPropertyChanged
{
	private bool isBusy;
	private string title = string.Empty;

	/// <summary>Initializes a new instance of the <see cref="BaseViewModel"/> class.</summary>
	/// <param name="services"><see cref="IServiceProvider"/>.</param>
	public BaseViewModel(IServiceProvider services)
	{
		ArgumentNullException.ThrowIfNull(services);
		Services = services;
		Templates = services.GetService(typeof(ITemplateService)) as ITemplateService ?? throw new NullReferenceException(nameof(ITemplateService));
		Dispatcher = services.GetService(typeof(IAppDispatcher)) as IAppDispatcher ?? throw new NullReferenceException(nameof(IAppDispatcher));
		ErrorHandler = services.GetService(typeof(IErrorHandlerService)) as IErrorHandlerService ?? throw new NullReferenceException(nameof(IErrorHandlerService));
		Context = services.GetService(typeof(IDatabaseContext)) as IDatabaseContext ?? throw new NullReferenceException(nameof(IDatabaseContext));
		Rss = services.GetService(typeof(IRssService)) as IRssService ?? throw new NullReferenceException(nameof(IRssService));
		Platform = services.GetService(typeof(IPlatformService)) as IPlatformService ?? throw new NullReferenceException(nameof(IPlatformService));
	}

	/// <summary>Gets a baseline navigation handler.</summary>summary>
	/// <remarks>Handle this to handle navigation events within the view model.</remarks>
	public event EventHandler<NavigationEventArgs>? Navigation;

	/// <summary>Fired when a feed item updates.</summary>
	public event EventHandler<FeedItemUpdatedEventArgs>? OnFeedItemUpdated;

	/// <summary>Fired when a feed list item updates.</summary>
	public event EventHandler<FeedListItemUpdatedEventArgs>? OnFeedListItemUpdated;

	/// <inheritdoc/>
	public event PropertyChangedEventHandler? PropertyChanged;

	/// <summary>Gets or sets a value indicating whether the VM is busy.</summary>
	public bool IsBusy
	{
		get => isBusy;
		set => SetProperty(ref isBusy, value);
	}

	/// <summary>Gets or sets the title.</summary>
	public string Title
	{
		get => title;
		set => SetProperty(ref title, value);
	}

	/// <summary>Gets the database context.</summary>
	internal IDatabaseContext Context { get; }

	/// <summary>Gets the Dispatcher.</summary>
	internal IAppDispatcher Dispatcher { get; }

	/// <summary>Gets the Error Handler.</summary>
	internal IErrorHandlerService ErrorHandler { get; }

	/// <summary>Gets the Platform services.</summary>
	internal IPlatformService Platform { get; }

	/// <summary>Gets the RSS context.</summary>
	internal IRssService Rss { get; }

	/// <summary>Gets the <see cref="IServiceProvider"/>.</summary>
	internal IServiceProvider Services { get; }

	/// <summary>Gets the templates context.</summary>
	internal ITemplateService Templates { get; }

	/// <summary>Called on VM Load.</summary>
	/// <returns><see cref="Task"/>.</returns>
	public virtual Task OnLoad() => Task.CompletedTask;

	/// <summary>Called when wanting to raise a Command Can Execute.</summary>
	public virtual void RaiseCanExecuteChanged()
	{
	}

	/// <summary>Sends a navigation request to whatever handlers attach to it.</summary>
	/// <param name="viewModel">The view model type.</param>
	/// <param name="arguments">Arguments to send to the view model.</param>
	public void SendNavigationRequest(Type viewModel, object? arguments = default)
	{
		if (viewModel.IsSubclassOf(typeof(BaseViewModel)))
		{
			Navigation?.Invoke(this, new NavigationEventArgs(viewModel, arguments));
		}
	}

	internal Task OpenBrowserAsync(FeedItem item)
	{
		if (item.Link is null)
		{
			return Task.CompletedTask;
		}

		return Platform.OpenBrowserAsync(item.Link);
	}

	/// <summary>Call OnFeedListItemUpdated event handler.</summary>
	/// <param name="item">Feed List Item.</param>
	internal void SendFeedListUpdateRequest(FeedListItem item)
	{
		ArgumentNullException.ThrowIfNull(item);
		OnFeedListItemUpdated?.Invoke(this, new FeedListItemUpdatedEventArgs(item));
	}

	/// <summary>Call OnFeedListItemUpdated event handler.</summary>
	/// <param name="feedItem">Feed List Item.</param>
	/// <param name="item">Feed Item.</param>
	internal void SendFeedUpdateRequest(FeedListItem feedItem, FeedItem item)
	{
		ArgumentNullException.ThrowIfNull(feedItem);
		ArgumentNullException.ThrowIfNull(item);
		OnFeedItemUpdated?.Invoke(this, new FeedItemUpdatedEventArgs(feedItem, item));
	}

	internal Task SetIsFavoriteFeedItemAsync(FeedItem item)
	{
		item.IsFavorite = !item.IsFavorite;
		_ = Context.AddOrUpdateFeedItem(item);
		return Task.CompletedTask;
	}

	internal Task SetIsReadFeedItemAsync(FeedItem item)
	{
		item.IsRead = !item.IsRead;
		_ = Context.AddOrUpdateFeedItem(item);
		return Task.CompletedTask;
	}

	internal Task ShareLinkAsync(FeedItem item)
	{
		if (item.Link is null)
		{
			return Task.CompletedTask;
		}

		return Platform.ShareUrlAsync(item.Link);
	}

	/// <summary>On Property Changed.</summary>
	/// <param name="propertyName">Name of the property.</param>
	protected void OnPropertyChanged([CallerMemberName] string propertyName = "") => Dispatcher?.Dispatch(() =>
																						  {
																							  PropertyChangedEventHandler? changed = PropertyChanged;
																							  if (changed == null)
																							  {
																								  return;
																							  }

																							  changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
																						  });

	protected bool SetProperty<T>(ref T backingStore, T value, Action? onChanged = null, [CallerMemberName] string propertyName = "")
	{
		if (EqualityComparer<T>.Default.Equals(backingStore, value))
		{
			return false;
		}

		backingStore = value;
		onChanged?.Invoke();
		OnPropertyChanged(propertyName);
		RaiseCanExecuteChanged();
		return true;
	}
}
