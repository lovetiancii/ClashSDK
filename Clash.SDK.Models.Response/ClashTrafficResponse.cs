using Newtonsoft.Json;

namespace Clash.SDK.Models.Response;

public class ClashTrafficResponse
{
	[JsonProperty("up")]
	public long Up { get; set; }

	[JsonProperty("down")]
	public long Down { get; set; }
}
