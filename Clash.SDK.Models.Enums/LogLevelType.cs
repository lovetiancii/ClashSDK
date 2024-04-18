using System.Runtime.Serialization;

namespace Clash.SDK.Models.Enums;

public enum LogLevelType
{
	[EnumMember(Value = "silent")]
	Silent,
	[EnumMember(Value = "error")]
	Error,
	[EnumMember(Value = "warning")]
	Warning,
	[EnumMember(Value = "info")]
	Info,
	[EnumMember(Value = "debug")]
	Debug
}
