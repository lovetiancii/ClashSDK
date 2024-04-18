using Newtonsoft.Json;

namespace Clash.SDK.Models.Enums;

public enum VehicleType
{
	[JsonProperty("Compatible")]
	Compatible,
	[JsonProperty("File")]
	File,
	[JsonProperty("HTTP")]
	Http
}
