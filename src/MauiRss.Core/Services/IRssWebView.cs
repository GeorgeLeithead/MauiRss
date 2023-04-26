namespace MauiRss.Core.Services;

/// <summary>RSS Web View.</summary>
public interface IRssWebView
{
	/// <summary>Sets the HTML source.</summary>
	/// <param name="html">HTML to display in Web View.</param>
	void SetSource(string html);
}
