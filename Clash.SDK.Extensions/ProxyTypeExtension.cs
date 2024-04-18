using Clash.SDK.Models.Enums;

namespace Clash.SDK.Extensions;

public static class ProxyTypeExtension
{
	public static bool IsUnUsedProxy(this ProxyType Type)
	{
		if ((uint)Type <= 1u)
		{
			return true;
		}
		return false;
	}

	public static bool IsProxy(this ProxyType Type)
	{
		if ((uint)(Type - 3) <= 5u || Type == ProxyType.Trojan)
		{
			return true;
		}
		return false;
	}

	public static bool IsPolicyGroup(this ProxyType Type)
	{
		if ((uint)(Type - 11) <= 4u)
		{
			return true;
		}
		return false;
	}

	public static bool IsPolicyGroupSelectable(this ProxyType Type)
	{
		if (Type == ProxyType.Selector)
		{
			return true;
		}
		return false;
	}
}
