namespace MauiRss.Core.Services;

/// <summary>Error Handler Service.</summary>
public class LoggerErrorHandlerService : IErrorHandlerService
{
	private readonly IEnumerable<ILogger> loggerList;

	/// <summary>Initializes a new instance of the <see cref="LoggerErrorHandlerService"/> class.</summary>
	/// <param name="loggers">Loggers.</param>
	public LoggerErrorHandlerService(IEnumerable<ILogger>? loggers) => loggerList = loggers ?? new List<ILogger>();

	/// <inheritdoc/>
	public event EventHandler<ErrorHandlerEventArgs>? OnError;

	/// <inheritdoc/>
	public void HandleError(Exception ex)
	{
		if (ex is null)
		{
			return;
		}

		foreach (ILogger logger in loggerList)
		{
			logger.Log(LogLevel.Error, ex.Message);
		}

		OnError?.Invoke(this, new ErrorHandlerEventArgs(ex));
	}
}
