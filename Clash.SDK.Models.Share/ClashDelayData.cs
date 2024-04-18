using System;
using Newtonsoft.Json;

namespace Clash.SDK.Models.Share;

public class ClashDelayData
{
	[JsonProperty("time")]
	public DateTime Time { get; set; }

	[JsonProperty("delay")]
	public long Delay { get; set; }
}
