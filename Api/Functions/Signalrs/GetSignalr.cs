using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.SignalRService;
using Newtonsoft.Json;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Api.Functions.Signalrs
{
    public static class GetSignalr
    {
        [FunctionName("negotiate")]
        public static SignalRConnectionInfo Negotiate(
            [HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequest req,
            [SignalRConnectionInfo(HubName = "serverless")] SignalRConnectionInfo connectionInfo)
        {
            return connectionInfo;
        }

        [FunctionName("broadcast")]
        public static async Task Broadcast(
            [HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequest req,
            [SignalR(HubName = "serverless")] IAsyncCollector<SignalRMessage> signalRMessages)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
           
            await signalRMessages.AddAsync(
                new SignalRMessage
                {
                    Target = "Broadcast",
                    Arguments = new[] { requestBody }
                });
        }
    }
}