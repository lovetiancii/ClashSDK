using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.WebSockets;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using Clash.SDK.Extensions;
using Clash.SDK.Models.Enums;
using Clash.SDK.Models.Events;
using Clash.SDK.Models.Response;
using Clash.SDK.Models.Share;
using Clash.SDK.Tools;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Websocket.Client;

namespace Clash.SDK;

public sealed class ClashClient : IDisposable
{
	private WebsocketClient TrafficWebSocketClient;

	private WebsocketClient LoggingWebSocketClient;

	private WebsocketClient ConnectionWebSocketClient;

	private static string _baseUrl = string.Empty;

	private static string _baseWsUrl = string.Empty;

	private static string _secret = string.Empty;

	internal HttpClient _httpClient;

	internal HttpClientHandler _httpClientHandler;

	private string API_TRAFFIC => _baseWsUrl + "/traffic";

	private string API_LOGS => _baseWsUrl + "/logs";

	private string API_CONNECTIONS_WS => _baseWsUrl + "/connections";

	private string API_STATUS => _baseUrl;

	private string API_VERSION => _baseUrl + "/version";

	private string API_PROXY_PROVIDERS => _baseUrl + "/providers/proxies";

	private string API_PROXY_PROVIDER_NAME => _baseUrl + "/providers/proxies/{0}";

	private string API_PROXY_PROVIDER_HEALTHCHECK => _baseUrl + "/providers/proxies/{0}/healthcheck";

	private string API_RULE_PROVIDERS => _baseUrl + "/providers/rules";

	private string API_RULE_PROVIDER_NAME => _baseUrl + "/providers/rules/{0}";

	private string API_PROXIES => _baseUrl + "/proxies";

	private string API_PROXIES_NAME => _baseUrl + "/proxies/{0}";

	private string API_PROXIES_DELAY => _baseUrl + "/proxies/{0}/delay";

	private string API_CONNECTIONS => _baseUrl + "/connections";

	private string API_CONNECTIONS_UUID => _baseUrl + "/connections/{0}";

	private string API_CONFIGS => _baseUrl + "/configs";

	private string API_RULES => _baseUrl + "/rules";

	public event TrafficEvt TrafficReceivedEvt;

	public event LoggingEvt LoggingReceivedEvt;

	public event ConnectionEvt ConnectionUpdatedEvt;

	private string GetToken()
	{
		if (string.IsNullOrWhiteSpace(_secret))
		{
			return string.Empty;
		}
		return "?token=" + _secret;
	}

	public void GetClashTraffic()
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Expected O, but got Unknown
		CloseClashTraffic();
		WebsocketClient val = new WebsocketClient(new Uri(API_TRAFFIC + GetToken()), (Func<ClientWebSocket>)null);
		val.Start();
		TrafficWebSocketClient = val;
		ObservableExtensions.Subscribe<ResponseMessage>(Observable.ObserveOn<ResponseMessage>(TrafficWebSocketClient.MessageReceived, (IScheduler)(object)TaskPoolScheduler.Default), (Action<ResponseMessage>)delegate(ResponseMessage msg)
		{
			if (this.TrafficReceivedEvt != null)
			{
				ClashTrafficResponse response = JsonConvert.DeserializeObject<ClashTrafficResponse>(msg.Text);
				this.TrafficReceivedEvt(null, new TrafficEvtArgs
				{
					Response = response
				});
			}
		});
	}

	public void GetClashLog()
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Expected O, but got Unknown
		CloseClashLog();
		WebsocketClient val = new WebsocketClient(new Uri(API_LOGS + GetToken()), (Func<ClientWebSocket>)null);
		val.Start();
		LoggingWebSocketClient = val;
		ObservableExtensions.Subscribe<ResponseMessage>(Observable.ObserveOn<ResponseMessage>(LoggingWebSocketClient.MessageReceived, (IScheduler)(object)TaskPoolScheduler.Default), (Action<ResponseMessage>)delegate(ResponseMessage msg)
		{
			if (this.LoggingReceivedEvt != null)
			{
				ClashLogResponse response = JsonConvert.DeserializeObject<ClashLogResponse>(msg.Text);
				this.LoggingReceivedEvt(null, new LoggingEvtArgs
				{
					Response = response
				});
			}
		});
	}

	public void GetClashConnection()
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Expected O, but got Unknown
		CloseClashConnection();
		WebsocketClient val = new WebsocketClient(new Uri(API_CONNECTIONS_WS + GetToken()), (Func<ClientWebSocket>)null);
		val.Start();
		ConnectionWebSocketClient = val;
		ObservableExtensions.Subscribe<ResponseMessage>(Observable.ObserveOn<ResponseMessage>(ConnectionWebSocketClient.MessageReceived, (IScheduler)(object)TaskPoolScheduler.Default), (Action<ResponseMessage>)delegate(ResponseMessage msg)
		{
			if (this.ConnectionUpdatedEvt != null)
			{
				ClashConnectionResponse response = JsonConvert.DeserializeObject<ClashConnectionResponse>(msg.Text);
				this.ConnectionUpdatedEvt(null, new ConnectionEvtArgs
				{
					Response = response
				});
			}
		});
	}

	public void CloseClashTraffic()
	{
		if (TrafficWebSocketClient != null)
		{
			try
			{
				TrafficWebSocketClient.Stop(WebSocketCloseStatus.Empty, string.Empty);
				TrafficWebSocketClient = null;
			}
			catch
			{
			}
		}
	}

	public void CloseClashLog()
	{
		if (LoggingWebSocketClient != null)
		{
			try
			{
				LoggingWebSocketClient.Stop(WebSocketCloseStatus.Empty, string.Empty);
				LoggingWebSocketClient = null;
			}
			catch
			{
			}
		}
	}

	public void CloseClashConnection()
	{
		if (ConnectionWebSocketClient != null)
		{
			try
			{
				ConnectionWebSocketClient.Stop(WebSocketCloseStatus.Empty, string.Empty);
				ConnectionWebSocketClient = null;
			}
			catch
			{
			}
		}
	}

	public async Task<bool> GetClashStatus()
	{
		try
		{
			await GetAsync<ClashStatusResponse>(API_STATUS);
			return true;
		}
		catch
		{
			return false;
		}
	}

	public async Task<ClashVersionResponse> GetClashVersion()
	{
		return await GetAsync<ClashVersionResponse>(API_VERSION);
	}

	public async Task<ClashConfigsResponse> GetClashConfigs()
	{
		return await GetAsync<ClashConfigsResponse>(API_CONFIGS);
	}

	public async Task<ClashNullableResponse> ChangeClashConfigs(Dictionary<string, dynamic> dict)
	{
		return await PatchAsync<ClashNullableResponse>(API_CONFIGS, dict);
	}

	public async Task<ClashNullableResponse> ReloadClashConfig(ConfigType type = ConfigType.Path, bool force = false, string value = "")
	{
		Dictionary<string, object> dictionary = new Dictionary<string, object>();
		dictionary.Add("force", force.ToLowerString());
		object obj = new object();
		switch (type)
		{
		case ConfigType.Path:
			obj = new
			{
				path = value
			};
			break;
		case ConfigType.Payload:
			obj = new
			{
				payload = value
			};
			break;
		}
		return await PutAsync<ClashNullableResponse>(API_CONFIGS, dictionary, string.IsNullOrWhiteSpace(value) ? null : obj);
	}

	public async Task<ClashRulesResponse> GetClashRules()
	{
		return await GetAsync<ClashRulesResponse>(API_RULES);
	}

	public async Task<ClashRuleProvidersResponse> GetClashRuleProviders()
	{
		ClashRuleProvidersResponse result = new ClashRuleProvidersResponse
		{
			Providers = new List<ClashRuleProviderData>()
		};
		foreach (JProperty item in (IEnumerable<JToken>)JObject.Parse(await GetAsync<string>(API_RULE_PROVIDERS))["providers"])
		{
			JProperty val = item;
			result.Providers.Add(val.Value.ToObject<ClashRuleProviderData>());
		}
		return result;
	}

	public async Task<ClashRuleProviderData> GetClashRuleProvider(string name)
	{
		string url = string.Format(API_RULE_PROVIDER_NAME, Uri.EscapeDataString(name));
		return await PutAsync<ClashRuleProviderData>(url);
	}

	public async Task<ClashNullableResponse> UpdateClashRuleProvider(string name)
	{
		string url = string.Format(API_RULE_PROVIDER_NAME, Uri.EscapeDataString(name));
		return await PutAsync<ClashNullableResponse>(url);
	}

	public ClashClient()
	{
		_baseUrl = "http://127.0.0.1:8080";
		_baseWsUrl = "ws://127.0.0.1:8080";
		_httpClientHandler = new HttpClientHandler
		{
			UseProxy = false,
			AutomaticDecompression = (DecompressionMethods.GZip | DecompressionMethods.Deflate | DecompressionMethods.Brotli)
		};
		_httpClient = new HttpClient(_httpClientHandler);
		_httpClient.Timeout = TimeSpan.FromSeconds(6.0);
	}

	public ClashClient(int port)
	{
		_baseUrl = $"http://127.0.0.1:{port}";
		_baseWsUrl = $"ws://127.0.0.1:{port}";
		_httpClientHandler = new HttpClientHandler
		{
			UseProxy = false,
			AutomaticDecompression = (DecompressionMethods.GZip | DecompressionMethods.Deflate | DecompressionMethods.Brotli)
		};
		_httpClient = new HttpClient(_httpClientHandler);
		_httpClient.Timeout = TimeSpan.FromSeconds(6.0);
	}

	public ClashClient(string address)
	{
		_baseUrl = "http://" + address;
		_baseWsUrl = "ws://" + address;
		_httpClientHandler = new HttpClientHandler
		{
			AutomaticDecompression = (DecompressionMethods.GZip | DecompressionMethods.Deflate | DecompressionMethods.Brotli)
		};
		_httpClient = new HttpClient(_httpClientHandler);
		_httpClient.Timeout = TimeSpan.FromSeconds(6.0);
	}

	public ClashClient(string http, string ws)
	{
		_baseUrl = http;
		_baseWsUrl = ws;
		_httpClientHandler = new HttpClientHandler
		{
			AutomaticDecompression = (DecompressionMethods.GZip | DecompressionMethods.Deflate | DecompressionMethods.Brotli)
		};
		_httpClient = new HttpClient(_httpClientHandler);
		_httpClient.Timeout = TimeSpan.FromSeconds(6.0);
	}

	public void SetSecret(string secret)
	{
		if (_httpClient != null && !string.IsNullOrWhiteSpace(secret))
		{
			_secret = secret;
			_httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + secret);
		}
	}

	public string GetSecret()
	{
		return _secret;
	}

	internal async Task<T> GetAsync<T>(string url, Dictionary<string, dynamic> parameters = null)
	{
		if (parameters != null && parameters.Count > 0)
		{
			string text = "?" + string.Join("&", parameters.Select((KeyValuePair<string, dynamic> p) => p.Key + "=" + Uri.EscapeDataString(p.Value)));
			url += text;
		}
		string text2 = await (await _httpClient.GetAsync(url)).Content.ReadAsStringAsync();
		if (typeof(T) == typeof(string))
		{
			return (T)(object)text2;
		}
		return JsonConvert.DeserializeObject<T>(text2);
	}

	internal async Task<T> PatchAsync<T>(string url, object patchData = null)
	{
		StringContent content = new StringContent(JsonConvert.SerializeObject(patchData), Encoding.UTF8, "application/json");
		return JsonConvert.DeserializeObject<T>(await (await _httpClient.SendAsync(new HttpRequestMessage
		{
			Method = new HttpMethod("PATCH"),
			RequestUri = new Uri(url),
			Content = content
		})).Content.ReadAsStringAsync());
	}

	internal async Task<T> PutAsync<T>(string url, Dictionary<string, dynamic> parameters = null, object putData = null)
	{
		if (parameters != null && parameters.Count > 0)
		{
			string text = "?" + string.Join("&", parameters.Select((KeyValuePair<string, dynamic> p) => p.Key + "=" + Uri.EscapeDataString(p.Value)));
			url += text;
		}
		StringContent content = new StringContent(JsonConvert.SerializeObject(putData), Encoding.UTF8, "application/json");
		return JsonConvert.DeserializeObject<T>(await (await _httpClient.PutAsync(url, content)).Content.ReadAsStringAsync());
	}

	internal async Task<T> DeleteAsync<T>(string url)
	{
		return JsonConvert.DeserializeObject<T>(await (await _httpClient.DeleteAsync(url)).Content.ReadAsStringAsync());
	}

	public void Dispose()
	{
		_httpClient.Dispose();
		_httpClientHandler.Dispose();
		GC.SuppressFinalize(this);
	}

	public async Task<ClashProxiesResponse> GetClashProxies()
	{
		ClashProxiesResponse result = new ClashProxiesResponse
		{
			Proxies = new List<ClashProxyData>()
		};
		foreach (JProperty item in (IEnumerable<JToken>)JObject.Parse(await GetAsync<string>(API_PROXIES))["proxies"])
		{
			JProperty val = item;
			result.Proxies.Add(val.Value.ToObject<ClashProxyData>());
		}
		return result;
	}

	public async Task<ClashProxyProvidersResponse> GetClashProxyProviders()
	{
		ClashProxyProvidersResponse result = new ClashProxyProvidersResponse
		{
			Providers = new List<ClashProxyProviderData>()
		};
		foreach (JProperty item in (IEnumerable<JToken>)JObject.Parse(await GetAsync<string>(API_PROXY_PROVIDERS))["providers"])
		{
			JProperty val = item;
			result.Providers.Add(val.Value.ToObject<ClashProxyProviderData>());
		}
		return result;
	}

	public async Task<ClashProxyData> GetClashProxyDetail(string name)
	{
		string url = string.Format(API_PROXIES_NAME, Uri.EscapeDataString(name));
		return await GetAsync<ClashProxyData>(url);
	}

	public async Task<ClashDelayResponse> GetClashProxyDelay(string name, int timeout = 5000, string testUrl = "http://www.gstatic.com/generate_204")
	{
		string url = string.Format(API_PROXIES_DELAY, Uri.EscapeDataString(name));
		Dictionary<string, object> dictionary = new Dictionary<string, object>();
		dictionary.Add("timeout", Convert.ToString(timeout));
		dictionary.Add("url", testUrl);
		ClashDelayResponse obj = await GetAsync<ClashDelayResponse>(url, dictionary);
		obj.DelayLong = LongParser.Parse(obj.Delay);
		return obj;
	}

	public async Task<ClashNullableResponse> SwitchClashProxy(string selector, string proxy)
	{
		string url = string.Format(API_PROXIES_NAME, Uri.EscapeDataString(selector));
		var putData = new
		{
			name = proxy
		};
		return await PutAsync<ClashNullableResponse>(url, null, putData);
	}

	public async Task<ClashNullableResponse> DisconnectConnection(string uuid)
	{
		string url = string.Format(API_CONNECTIONS_UUID, Uri.EscapeDataString(uuid));
		return await DeleteAsync<ClashNullableResponse>(url);
	}

	public async Task<ClashNullableResponse> DisconnectAllConnections()
	{
		return await DeleteAsync<ClashNullableResponse>(API_CONNECTIONS);
	}

	public async Task<ClashProxyProviderData> GetClashProxyProvider(string name)
	{
		string url = string.Format(API_PROXY_PROVIDER_NAME, Uri.EscapeDataString(name));
		return await GetAsync<ClashProxyProviderData>(url);
	}

	public async Task<ClashNullableResponse> HealthCheckProxyProvider(string name)
	{
		string url = string.Format(API_PROXY_PROVIDER_HEALTHCHECK, Uri.EscapeDataString(name));
		return await GetAsync<ClashNullableResponse>(url);
	}

	public async Task<ClashNullableResponse> UpdateClashProxyProvider(string name)
	{
		string url = string.Format(API_PROXY_PROVIDER_NAME, Uri.EscapeDataString(name));
		return await PutAsync<ClashNullableResponse>(url);
	}
}
