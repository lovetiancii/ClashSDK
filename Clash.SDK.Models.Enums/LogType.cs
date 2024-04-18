using System.Runtime.Serialization;

namespace Clash.SDK.Models.Enums;

public enum LogType
{
	[EnumMember(Value = "error")]
	Error,
	[EnumMember(Value = "warning")]
	Warning,
	[EnumMember(Value = "info")]
	Info,
	[EnumMember(Value = "debug")]
	Debug
}
