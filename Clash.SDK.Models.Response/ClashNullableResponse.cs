using Newtonsoft.Json;

namespace Clash.SDK.Models.Response;

public class ClashNullableResponse
{
	[JsonProperty("message")]
	public string Message { get; set; }

	[JsonProperty("error")]
	public string Error { get; set; }
}
