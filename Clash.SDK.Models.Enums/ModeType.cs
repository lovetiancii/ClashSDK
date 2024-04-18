using System.Runtime.Serialization;

namespace Clash.SDK.Models.Enums;

public enum ModeType
{
	[EnumMember(Value = "direct")]
	Direct,
	[EnumMember(Value = "rule")]
	Rule,
	[EnumMember(Value = "global")]
	Global,
	[EnumMember(Value = "script")]
	Script
}
