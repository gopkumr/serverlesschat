using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Azure.WebJobs.Extensions.SignalRService;
using ServerlessChat.Shared;

namespace ServerlessChat.Function
{
    public static class Serverlesschatfunction
    {

        [FunctionName("negotiate")]
        public static SignalRConnectionInfo Negotiate([HttpTrigger] HttpRequest request, [SignalRConnectionInfo(HubName="ServerlessChat")] SignalRConnectionInfo connectionInfo){
            return connectionInfo;
        }

        [FunctionName("Chat")]
        public static Task Chat(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] ChatMessage message,
            [SignalR(HubName = "ServerlessChat")]IAsyncCollector<SignalRMessage> signalRMessages)
        {
            return signalRMessages.AddAsync(new SignalRMessage{
                Target="NewChat",
                Arguments=new object[]{message}
            });

        }
    }
}
