using Newtonsoft.Json;

namespace Clash.SDK.Models.Enums;

public enum ProviderType
{
	[JsonProperty("Proxy")]
	Proxy,
	[JsonProperty("Rule")]
	Rule
}
