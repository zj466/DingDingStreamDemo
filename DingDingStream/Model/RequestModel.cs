using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DingDingStream.Model
{

    /// <summary>
    /// 连接请求
    /// </summary>
    class GatewayConnectionRequestModel
    {
        public string? ClientId { get; set; }
        public string? ClientSecret { get; set; }
        public SubscriptionModel[]? Subscriptions { get; set; }
        public string? Ua { get; set; }
        public string? LocalIp { get; set; }
    }
    /// <summary>
    /// 订阅
    /// </summary>
    class SubscriptionModel
    {
        public string? Type { get; set; }
        public string? Topic { get; set; }
    }
    /// <summary>
    /// 响应
    /// </summary>
    public class ResponseModel
    {
        [JsonProperty(PropertyName = "code")]
        public int Code { get; set; } = 200;
        [JsonProperty(PropertyName = "headers")]
        public ResponseHeaders? Headers { get; set; }
        [JsonProperty(PropertyName = "message")]
        public string? Message { get; set; } = "OK";
        [JsonProperty(PropertyName = "data")]
        public string? Data { get; set; }
    }
    /// <summary>
    /// 响应头
    /// </summary>
    public class ResponseHeaders
    {
        [JsonProperty(PropertyName = "contentType")]
        public string? ContentType { get; set; } = "application/json";
        [JsonProperty(PropertyName = "messageId")]
        public string? MessageId { get; set; }
    }
    /// <summary>
    /// 探活相应data
    /// </summary>
    public class PingData
    {
        [JsonProperty(PropertyName = "opaque")]
        public string? Opaque { get; set; }
       
    }
    /// <summary>
    /// 事件相应data
    /// </summary>
    public class EventData
    {
        [JsonProperty(PropertyName = "status")]
        public string? Status { get; set; } = "SUCCESS";
        [JsonProperty(PropertyName = "message")]
        public string? Message { get; set; } = "success";
    }




}
