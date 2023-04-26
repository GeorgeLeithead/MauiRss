namespace MauiRss.Core.Services;

public interface IAppDispatcher
{
	bool Dispatch(Action action);
}
