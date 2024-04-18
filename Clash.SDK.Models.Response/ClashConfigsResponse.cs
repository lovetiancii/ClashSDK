using Clash.SDK.Models.Enums;
using Newtonsoft.Json;

namespace Clash.SDK.Models.Response;

public class ClashConfigsResponse
{
	[JsonProperty("socks-port")]
	public int SocksPort { get; set; }

	[JsonProperty("port")]
	public int HttpPort { get; set; }

	[JsonProperty("redir-port")]
	public int RedirPort { get; set; }

	[JsonProperty("tproxy-port")]
	public int TProxyPort { get; set; }

	[JsonProperty("mixed-port")]
	public int MixedPort { get; set; }

	[JsonProperty("allow-lan")]
	public bool AllowLan { get; set; }

	[JsonProperty("bind-address")]
	public string BindAddress { get; set; }

	[JsonProperty("mode")]
	public ModeType Mode { get; set; }

	[JsonProperty("log-level")]
	public LogLevelType LogLevel { get; set; }

	[JsonProperty("ipv6")]
	public bool IPV6 { get; set; }

	[JsonProperty("interface-name")]
	public string InterfaceName { get; set; }
}
