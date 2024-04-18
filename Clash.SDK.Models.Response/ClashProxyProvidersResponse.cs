using System.Collections.Generic;
using Clash.SDK.Models.Share;
using Newtonsoft.Json;

namespace Clash.SDK.Models.Response;

public class ClashProxyProvidersResponse
{
	[JsonProperty("providers")]
	public List<ClashProxyProviderData> Providers { get; set; }
}
