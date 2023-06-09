namespace MauiRss.Core.Helpers;

/// <summary>ASYNC Command.</summary>
public class AsyncCommand : IAsyncCommand
{
	private readonly Func<Task>? execute;
	private readonly Func<bool>? canExecute;
	private readonly IErrorHandlerService? errorHandler;
	private readonly IAppDispatcher? dispatcher;
	private bool isExecuting;

	/// <summary>Initializes a new instance of the <see cref="AsyncCommand"/> class.</summary>
	/// <param name="execute">Command to execute.</param>
	/// <param name="canExecute">Can execute command.</param>
	/// <param name="dispatcher">Dispatcher.</param>
	/// <param name="errorHandler">Error handler.</param>
	public AsyncCommand(
		Func<Task> execute,
		Func<bool>? canExecute = null,
		IAppDispatcher? dispatcher = null,
		IErrorHandlerService? errorHandler = null)
	{
		this.dispatcher = dispatcher;
		this.execute = execute;
		this.canExecute = canExecute;
		this.errorHandler = errorHandler;
	}

	/// <summary>Can Execute Changed.</summary>
	public event EventHandler? CanExecuteChanged;

	/// <summary>Gets or sets a value indicating whether the command is executing.</summary>
	protected bool IsExecuting
	{
		get => isExecuting;

		set
		{
			isExecuting = value;
			RaiseCanExecuteChanged();
		}
	}

	/// <inheritdoc/>
	public bool CanExecute() => !IsExecuting && (canExecute?.Invoke() ?? true);

	/// <inheritdoc/>
	public async Task ExecuteAsync()
	{
		if (CanExecute() && execute is not null)
		{
			try
			{
				IsExecuting = true;
				await execute().ConfigureAwait(false);
			}
			finally
			{
				IsExecuting = false;
			}
		}
	}

	/// <summary>Raises Can Execute Changed.</summary>
	public void RaiseCanExecuteChanged() => dispatcher?.Dispatch(() => CanExecuteChanged?.Invoke(this, EventArgs.Empty));

	/// <inheritdoc/>
	bool ICommand.CanExecute(object? parameter) => CanExecute();

	/// <inheritdoc/>
	async void ICommand.Execute(object? parameter) => await ExecuteAsync().FireAndForgetSafeAsync(errorHandler);
}
