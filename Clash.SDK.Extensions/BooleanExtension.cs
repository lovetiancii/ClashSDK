namespace Clash.SDK.Extensions;

public static class BooleanExtension
{
	public static string ToLowerString(this bool value)
	{
		if (!value)
		{
			return "false";
		}
		return "true";
	}

	public static string ToLowerString(this bool? value)
	{
		return value.Value.ToLowerString();
	}
}
