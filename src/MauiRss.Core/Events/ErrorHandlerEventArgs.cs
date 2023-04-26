namespace MauiRss.Core.Events;

/// <summary>Error Handler Event arguments.</summary>
public class ErrorHandlerEventArgs : EventArgs
{
	/// <summary>Initializes a new instance of the <see cref="ErrorHandlerEventArgs"/> class.</summary>
	/// <param name="ex">Exception.</param>
	public ErrorHandlerEventArgs(Exception ex) => Exception = ex;

	/// <summary>Gets the Exception.</summary>
	public Exception Exception { get; }
}
