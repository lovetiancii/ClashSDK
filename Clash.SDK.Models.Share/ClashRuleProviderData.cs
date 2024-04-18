using System;
using Clash.SDK.Models.Enums;
using Newtonsoft.Json;

namespace Clash.SDK.Models.Share;

public class ClashRuleProviderData
{
	[JsonProperty("name")]
	public string Name { get; set; }

	[JsonProperty("type")]
	public ProviderType Type { get; set; }

	[JsonProperty("behavior")]
	public BehaviorType BehaviorType { get; set; }

	[JsonProperty("ruleCount")]
	public long RuleCount { get; set; }

	[JsonProperty("updatedAt")]
	public DateTime? UpdatedAt { get; set; }

	[JsonProperty("vehicleType")]
	public VehicleType VehicleType { get; set; }
}
