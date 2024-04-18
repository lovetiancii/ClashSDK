using Newtonsoft.Json;

namespace Clash.SDK.Models.Response;

public class ClashVersionResponse
{
	[JsonProperty("experimental")]
	public bool Experimental { get; set; }

	[JsonProperty("meta")]
	public bool Meta { get; set; }

	[JsonProperty("premium")]
	public bool Premium { get; set; }

	[JsonProperty("version")]
	public string Version { get; set; }
}
