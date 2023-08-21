using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DingDingStream.Model
{
    /// <summary>
    /// 获取TicketModel
    /// </summary>
    class TicketModel
    {
        public string? Endpoint { get; set; }
        public string? Ticket { get; set; }
    }
   /// <summary>
   /// 接收到消息
   /// </summary>
   /// <typeparam name="T"></typeparam>
    public class MessageModel
    {
        public string? SpecVersion { get; set; }
        public string? Type { get; set; }
        public MessageHeaders? Headers { get; set; }
        public string? Data { get; set; }
    }
    /// <summary>
    /// 接收到消息头
    /// </summary>
    public class MessageHeaders
    {
        public string? AppId { get; set; }
        public string? ConnectionId { get; set; }
        public string? ContentType { get; set; }
        public string? EventBornTime { get; set; }
        public string? EventCorpId { get; set; }
        public string? EventId { get; set; }
        public string? EventType { get; set; }
        public string? EventUnifiedAppId { get; set; }
        public string? MessageId { get; set; }
        public string? Time { get; set; }
        public string? Topic { get; set; }
    }

}
