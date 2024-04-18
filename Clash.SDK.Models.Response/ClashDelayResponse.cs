using Newtonsoft.Json;

namespace Clash.SDK.Models.Response;

public class ClashDelayResponse
{
	[JsonProperty("delay")]
	public string Delay { get; set; }

	public long DelayLong { get; set; }
}
