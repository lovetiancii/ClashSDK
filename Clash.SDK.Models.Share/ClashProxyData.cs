using System.Collections.Generic;
using Clash.SDK.Models.Enums;
using Newtonsoft.Json;

namespace Clash.SDK.Models.Share;

public class ClashProxyData
{
	[JsonProperty("history")]
	public List<ClashDelayData> History { get; set; }

	[JsonProperty("all")]
	public List<string> All { get; set; }

	[JsonProperty("now")]
	public string Now { get; set; }

	[JsonProperty("name")]
	public string Name { get; set; }

	[JsonProperty("type")]
	public ProxyType Type { get; set; }

	[JsonProperty("udp")]
	public bool Udp { get; set; }
}
