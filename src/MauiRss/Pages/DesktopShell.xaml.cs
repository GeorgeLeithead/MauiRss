namespace MauiRss.Pages;

/// <summary>Desktop Shell.</summary>
public partial class DesktopShell
{
	/// <summary>Initialises a new instance of the <see cref="DesktopShell" /> class.</summary>
	public DesktopShell()
	{
		InitializeComponent();
		BindingContext = new ShellViewModel();
	}
}
