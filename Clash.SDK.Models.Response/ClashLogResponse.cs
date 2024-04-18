using Clash.SDK.Models.Enums;
using Newtonsoft.Json;

namespace Clash.SDK.Models.Response;

public class ClashLogResponse
{
	[JsonProperty("type")]
	public LogType Type { get; set; }

	[JsonProperty("payload")]
	public string PayLoad { get; set; }
}
