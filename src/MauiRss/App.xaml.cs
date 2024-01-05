[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace MauiRss;

/// <summary>Application class.</summary>
public partial class App : Application
{
	/// <summary>Initialises a new instance of the <see cref="App" /> class.</summary>
	public App()
	{
		InitializeComponent();
		TheTheme.SetTheme();
		MainPage = Config.Desktop ? new DesktopShell() : new DesktopShell();
	}
}
