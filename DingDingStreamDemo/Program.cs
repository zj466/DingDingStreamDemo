
using DingDingStream.Service;

CallBackService callBackService = new CallBackService("Your clientId", "Your clientScret");
await callBackService.ConnectWebSocket();
