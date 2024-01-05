namespace MauiRss.ViewModels;

/// <summary>Shell view model.</summary>
public class ShellViewModel : BaseViewModel
{
	/// <summary>Application section for Dummy.</summary>
	public AppSection Dummy { get; set; }

	/// <summary>Application section for Settings.</summary>
	public AppSection Settings { get; set; }

	/// <summary>Initialises a new instance of the <see cref="ShellViewModel" /> class.</summary>
	public ShellViewModel()
	{
		Dummy = new AppSection(typeof(DummyPage), AppResource.Dummy);
		Settings = new AppSection(typeof(SettingsPage), AppResource.Settings);
	}
}
