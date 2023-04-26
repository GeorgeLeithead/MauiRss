namespace MauiRss.Core.Helpers;

/// <summary>ASYNC Command.</summary>
/// <typeparam name="T">Generic Parameter.</typeparam>
public class AsyncCommand<T> : IAsyncCommand<T>
{
	private readonly Func<T, Task>? execute;
	private readonly Func<T, bool>? canExecute;
	private readonly IErrorHandlerService? errorHandler;
	private bool isExecuting;

	/// <summary>Initializes a new instance of the <see cref="AsyncCommand{T}"/> class.</summary>
	/// <param name="execute">Task to Execute.</param>
	/// <param name="canExecute">Can Execute Function.</param>
	/// <param name="errorHandler">Error Handler.</param>
	public AsyncCommand(Func<T, Task>? execute, Func<T, bool>? canExecute = null, IErrorHandlerService? errorHandler = null)
	{
		this.execute = execute;
		this.canExecute = canExecute;
		this.errorHandler = errorHandler;
	}

	/// <summary>Can Execute Event.</summary>
	public event EventHandler? CanExecuteChanged;

	/// <summary>Returns a value if the given command can be executed.</summary>
	/// <param name="parameter">Can Execute Function.</param>
	/// <returns>true if can execute; Otherwise false.</returns>
	public bool CanExecute(T parameter) => !isExecuting && (canExecute?.Invoke(parameter) ?? true);

	/// <summary>Executes Command ASYNC.</summary>
	/// <param name="parameter">Command to be Executed.</param>
	/// <returns>Task.</returns>
	public async Task ExecuteAsync(T parameter)
	{
		if (CanExecute(parameter) && execute is not null)
		{
			try
			{
				isExecuting = true;
				await execute(parameter).ConfigureAwait(false);
			}
			finally
			{
				isExecuting = false;
			}
		}

		RaiseCanExecuteChanged();
	}

	/// <summary>
	/// Raise Can Execute Changed.
	/// </summary>
	public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);

	/// <inheritdoc/>
	bool ICommand.CanExecute(object? parameter)
	{
		if (parameter is not null)
		{
			return CanExecute((T)parameter);
		}

		return false;
	}

	/// <inheritdoc/>
	async void ICommand.Execute(object? parameter)
	{
		if (parameter is not null)
		{
			await ExecuteAsync((T)parameter).FireAndForgetSafeAsync(errorHandler);
		}
	}
}
