namespace MauiRss.Core.Helpers;

/// <summary>IAsyncCommand.</summary>
public interface IAsyncCommand : ICommand
{
	/// <summary>Can execute Command.</summary>
	/// <returns>true if can execute; Otherwise false..</returns>
	bool CanExecute();

	/// <summary>Execute ASYNC.</summary>
	/// <returns><see cref="Task"/>.</returns>
	Task ExecuteAsync();
}
