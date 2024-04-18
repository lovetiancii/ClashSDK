using System.Collections.Generic;
using Clash.SDK.Models.Share;
using Newtonsoft.Json;

namespace Clash.SDK.Models.Response;

public class ClashRulesResponse
{
	[JsonProperty("rules")]
	public List<ClashRuleData> Rules;
}
