using Newtonsoft.Json;

namespace Clash.SDK.Models.Response;

public class ClashStatusResponse
{
	[JsonProperty("hello")]
	public string Hello { get; set; }
}
