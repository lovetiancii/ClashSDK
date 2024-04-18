using System.Collections.Generic;
using Clash.SDK.Models.Share;
using Newtonsoft.Json;

namespace Clash.SDK.Models.Response;

public class ClashConnectionResponse
{
	[JsonProperty("uploadTotal")]
	public long UploadTotal { get; set; }

	[JsonProperty("downloadTotal")]
	public long DownloadTotal { get; set; }

	[JsonProperty("connections")]
	public List<ClashConnectionData> Connections { get; set; }
}
