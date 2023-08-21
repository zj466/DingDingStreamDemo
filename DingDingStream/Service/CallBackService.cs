using DingDingStream.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tea;

namespace DingDingStream.Service
{
    public class CallBackService
    {

        private readonly string ClientID;
        private readonly string ClientSecret;

        public CallBackService(string clientID,string clientSecret)
        {
            this.ClientID = clientID;
            this.ClientSecret = clientSecret;
        }
        /// <summary>
        /// 获取Ticket
        /// </summary>
        /// <returns></returns>
        private async Task<TicketModel?> GetTicket()
        {
            try
            {
                // API的URL
                string apiUrl = "https://api.dingtalk.com/v1.0/gateway/connections/open";
                // 创建请求数据的模型对象
                GatewayConnectionRequestModel requestModel = new GatewayConnectionRequestModel
                {
                    ClientId = this.ClientID,
                    ClientSecret = this.ClientSecret,
                    Subscriptions = new[]
                    {
                    new SubscriptionModel { Type = "EVENT", Topic = "*" },
                    new SubscriptionModel { Type = "CALLBACK", Topic = "/v1.0/im/bot/messages/get" }
                },
                    Ua = "dingtalk-sdk-C#/1.0.0",
                    LocalIp = "127.0.0.1"
                };
                // 将模型对象序列化为JSON字符串
                string requestBody = JsonConvert.SerializeObject(requestModel);
                using HttpClient client = new HttpClient();
                // 设置请求头
                client.DefaultRequestHeaders.Add("Host", "api.dingtalk.com");
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                // 发送 POST 请求
                HttpResponseMessage response = await client.PostAsync(apiUrl, new StringContent(requestBody, Encoding.UTF8, "application/json"));
                // 处理响应
                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    TicketModel? ticketModel = JsonConvert.DeserializeObject<TicketModel>(responseContent);
                    return ticketModel;

                }
                else
                {
                    return null;
                }

            }
            catch (Exception)
            {

                return null;
            }
        }
        
        public async Task ConnectWebSocket()
        {
            TicketModel? ticketModel = await GetTicket();
            if(ticketModel == null)
            {
                return;
            }
            Uri uri = new Uri(ticketModel.Endpoint + "?ticket=" + ticketModel.Ticket);
            WebSocket webSocketClient = new WebSocket();
            try
            {
                await webSocketClient.ConnectAsync(uri);
               
                while (webSocketClient.IsRunning())
                {
                    var receive = await webSocketClient.ReceiveAsync();
                    await HandleMessage(webSocketClient,receive);
                }
               //await ConnectWebSocket();
            }
            catch (Exception)
            {

                return;
            }

        }
        private static async Task HandleMessage(WebSocket client,string? message)
        {
            if (message == null) { return; }
            MessageModel? model = JsonConvert.DeserializeObject<MessageModel>(message);
            if (model == null) { return; }
            if (model.Type == "SYSTEM")
            {
                if (model.Headers?.Topic == "ping" && !string.IsNullOrEmpty(model.Data))
                {
                   
                    ResponseModel response=new ResponseModel();
                    response.Headers = new ResponseHeaders
                    {
                        ContentType = "application/json",
                        MessageId = model.Headers.MessageId
                    };
                    response.Data = model.Data; 
                    string ret=JsonConvert.SerializeObject(response);
                    await client.SendAsync(ret);
                }
            }
            else if(model.Type == "EVENT")
            {
                ResponseModel response=new ResponseModel();
                response.Headers = new ResponseHeaders
                {
                    MessageId = model?.Headers?.MessageId,
                    ContentType = "application/json",
                };
                EventData eventData = new EventData();
                response.Data =JsonConvert.SerializeObject(eventData).ToString();
                string ret=JsonConvert.SerializeObject(response);
                await client.SendAsync(ret);
            }
            else if(model.Type == "CALLBACK")
            {

            }
        }
    }

}
