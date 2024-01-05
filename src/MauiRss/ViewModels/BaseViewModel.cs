namespace MauiRss.ViewModels;

/// <summary>Base view model.</summary>
public partial class BaseViewModel : ObservableObject
{
	/// <summary>Initializes a new instance of the <see cref="BaseViewModel" /> class.</summary>
	public BaseViewModel()
	{
		title = string.Empty;
		isBusy = true;
	}

	/// <summary>View model title.</summary>
	[ObservableProperty]
	private string title;

	/// <summary>View model is busy indicator.</summary>
	[ObservableProperty]
	[NotifyPropertyChangedFor(nameof(IsNotBusy))]
	private bool isBusy;

	/// <summary>View model is not busy.</summary>
	public bool IsNotBusy => !IsBusy;
}
