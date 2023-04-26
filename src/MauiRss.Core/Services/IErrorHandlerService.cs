namespace MauiRss.Core.Services;

/// <summary>Error Handler Service.</summary>
public interface IErrorHandlerService
{
	/// <summary>Called when an error is hit through the service.</summary>
	event EventHandler<ErrorHandlerEventArgs> OnError;

	/// <summary>Handle error in UI.</summary>
	/// <param name="ex">Exception being thrown.</param>
	void HandleError(Exception ex);
}
