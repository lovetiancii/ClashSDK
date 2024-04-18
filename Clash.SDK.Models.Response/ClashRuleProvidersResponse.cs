using System.Collections.Generic;
using Clash.SDK.Models.Share;
using Newtonsoft.Json;

namespace Clash.SDK.Models.Response;

public class ClashRuleProvidersResponse
{
	[JsonProperty("providers")]
	public List<ClashRuleProviderData> Providers { get; set; }
}
