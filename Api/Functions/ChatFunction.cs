using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.SignalRService;
using System.Threading.Tasks;

namespace Api
{
    public static class ChatFunction
    {

        [FunctionName("negotiate")]
        public static SignalRConnectionInfo Negotiate(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequest req,
            [SignalRConnectionInfo(HubName = "chatHub")] SignalRConnectionInfo connectionInfo)
        {
            return connectionInfo;
        }


        [FunctionName("messages")]
        public static Task SendMessage(
           [HttpTrigger(AuthorizationLevel.Anonymous, "post")] ClientMessage clientMessage,
           [SignalR(HubName = "chatHub")] IAsyncCollector<SignalRMessage> signalRMessages)
        {
            return signalRMessages.AddAsync(
                new SignalRMessage
                {
                    Target = "clientMessage",
                    Arguments = new[] { clientMessage }
                });
        }

        public class ClientMessage
        {
            public string UID { get; set; }
            public string Name { get; set; }
            public string Message { get; set; }

            public string CreateTime { get; set; }
        }
    }
}