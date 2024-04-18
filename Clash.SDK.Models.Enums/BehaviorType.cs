using System.Runtime.Serialization;

namespace Clash.SDK.Models.Enums;

public enum BehaviorType
{
	[EnumMember(Value = "Domain")]
	Domain,
	[EnumMember(Value = "IPCIDR")]
	IPCIDR,
	[EnumMember(Value = "Classical")]
	Classical
}
