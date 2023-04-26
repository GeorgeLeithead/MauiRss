namespace MauiRss.Core.Events;

public class NavigationEventArgs : EventArgs
{
	public NavigationEventArgs(Type type, object? arguments)
	{
		ArgumentNullException.ThrowIfNull(type);
		Type = type;
		Arguments = arguments;
	}

	public object? Arguments { get; }

	public Type Type { get; }
}
