namespace MauiRss.Core.Helpers;

/// <summary>Task Utilities.</summary>
public static class TaskUtilities
{
	/// <summary>Fire and Forget Safe ASYNC.</summary>
	/// <param name="task">Task to Fire and Forget.</param>
	/// <param name="handler">Error Handler.</param>
	public static async Task FireAndForgetSafeAsync(this Task task, IErrorHandlerService? handler = null)
	{
		try
		{
			await task;
		}
		catch (Exception ex)
		{
			handler?.HandleError(ex);
		}
	}

	public static async Task<IEnumerable<TResult>> SelectAsync<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, Task<TResult>> method) => await Task.WhenAll(source.Select(async s => await method(s)));
}
