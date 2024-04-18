using System;
using System.Collections.Generic;
using Clash.SDK.Models.Enums;
using Newtonsoft.Json;

namespace Clash.SDK.Models.Share;

public class ClashProxyProviderData
{
	[JsonProperty("name")]
	public string Name { get; set; }

	[JsonProperty("proxies")]
	public List<ClashProxyData> Proxies { get; set; }

	[JsonProperty("type")]
	public ProviderType Type { get; set; }

	[JsonProperty("updatedAt")]
	public DateTime? UpdatedAt { get; set; }

	[JsonProperty("vehicleType")]
	public VehicleType VehicleType { get; set; }
}
