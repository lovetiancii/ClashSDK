using System.Runtime.Serialization;

namespace Clash.SDK.Models.Enums;

public enum RuleType
{
	[EnumMember(Value = "Domain")]
	Domain,
	[EnumMember(Value = "DomainSuffix")]
	DomainSuffix,
	[EnumMember(Value = "DomainKeyword")]
	DomainKeyword,
	[EnumMember(Value = "SrcIPCIDR")]
	SrcIPCIDR,
	[EnumMember(Value = "IPCIDR")]
	IPCIDR,
	[EnumMember(Value = "GeoIP")]
	GeoIP,
	[EnumMember(Value = "DstPort")]
	DstPort,
	[EnumMember(Value = "SrcPort")]
	SrcPort,
	[EnumMember(Value = "Process")]
	Process,
	[EnumMember(Value = "RuleSet")]
	RuleSet,
	[EnumMember(Value = "Match")]
	Match
}
