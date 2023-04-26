namespace MauiRss.Core.Helpers;

/// <summary>IAsyncCommand.</summary>
/// <typeparam name="T">Type of Command.</typeparam>
public interface IAsyncCommand<T> : ICommand
{
	/// <summary>Execute ASYNC.</summary>
	/// <param name="parameter">parameter.</param>
	/// <returns><see cref="Task"/>.</returns>
	Task ExecuteAsync(T parameter);

	/// <summary>Can Execute.</summary>
	/// <param name="parameter">parameter.</param>
	/// <returns>Bool.</returns>
	bool CanExecute(T parameter);
}
