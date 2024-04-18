namespace Clash.SDK.Tools;

public static class LongParser
{
	public static long Parse(string value)
	{
		if (!long.TryParse(value, out var result))
		{
			return -1L;
		}
		return result;
	}
}
