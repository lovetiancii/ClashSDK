using System.Collections.Generic;
using Newtonsoft.Json;

namespace Clash.SDK.Models.Share;

public class ClashConnectionData
{
	[JsonProperty("metadata")]
	public ClashMetaData MetaData;

	[JsonProperty("id")]
	public string Id { get; set; }

	[JsonProperty("download")]
	public long Download { get; set; }

	[JsonProperty("upload")]
	public long Upload { get; set; }

	[JsonProperty("start")]
	public string Start { get; set; }

	[JsonProperty("rule")]
	public string Rule { get; set; }

	[JsonProperty("rulePayload")]
	public string RulePayload { get; set; }

	[JsonProperty("chains")]
	public List<string> Chains { get; set; }
}
