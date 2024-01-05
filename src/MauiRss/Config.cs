namespace MauiRss;

/// <summary>Basic application configuration settings.</summary>
public static class Config
{
	/// <summary>Application base URL.</summary>
	public static string Base = DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2" : "http://localhost";

	/// <summary>Application base web URL.</summary>
	public static string BaseWeb = $"{Base}:5002/";

	/// <summary>Gets whether the application is desktop.</summary>
	public static bool Desktop =>
#if WINDOWS || MACCATALYST
			true;
#else
			false;
#endif

}
