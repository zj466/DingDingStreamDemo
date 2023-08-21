using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DingDingStream.Service
{
    public class WebSocket
    {
        private ClientWebSocket _webSocket = new ClientWebSocket();

        public async Task ConnectAsync(Uri uri)
        {
            await _webSocket.ConnectAsync(uri, CancellationToken.None);
            Console.WriteLine("WebSocket connected.");
        }

        public async Task SendAsync(string message)
        {
            Console.WriteLine($"Send Message:{message}");
            byte[] buffer = Encoding.UTF8.GetBytes(message);
            await _webSocket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None);
        }

        public bool IsRunning()
        {
            if (_webSocket != null)
            {
               if( _webSocket.State == WebSocketState.Open)
                {
                    return true;
                }
            }
            return false;
        }
        public async Task<string?> ReceiveAsync()
        {
            byte[] buffer = new byte[10240];
            while (_webSocket.State == WebSocketState.Open)
            {
                WebSocketReceiveResult result = await _webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                if (result.MessageType == WebSocketMessageType.Text)
                {
                    string message = Encoding.UTF8.GetString(buffer, 0, result.Count);
                    Console.WriteLine($"Received message: {message}");
                    return message;
                }
            }
            return null;
        }

        public async Task CloseAsync()
        {
            if (_webSocket.State == WebSocketState.Open || _webSocket.State == WebSocketState.CloseSent)
            {
                await _webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing the connection", CancellationToken.None);
                Console.WriteLine("WebSocket closed.");
            }
        }
    }
}

