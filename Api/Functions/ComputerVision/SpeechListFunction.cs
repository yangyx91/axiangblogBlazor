using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Api.Functions.ComputerVision
{
    //ÎÄ±¾×ªÓïÒô Rest API (v3.1)
    //https://docs.microsoft.com/zh-cn/azure/cognitive-services/speech-service/rest-text-to-speech#convert-text-to-speech
    public static class SpeechListFunction
    {
        private static readonly string endpointkey = "f59840607fb4472bbab3e756ea4ce739";
        private static readonly string endpointTokenURL = "https://southeastasia.api.cognitive.microsoft.com/sts/v1.0/issuetoken";
        private static readonly string endpointList = "https://southeastasia.tts.speech.microsoft.com/cognitiveservices/voices/list";

        private static readonly string endpointApi = "https://southeastasia.tts.speech.microsoft.com/cognitiveservices/v1";
        private static readonly string endpointLocation = "southeastasia"; 

       

        [FunctionName("SpeechListFunction")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = "speechList")] HttpRequest req)
        {
            using (HttpClientHandler handler = new HttpClientHandler { UseProxy = false })
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", endpointkey);
                //client.DefaultRequestHeaders.Accept.Clear();
                //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.GetAsync(endpointList);
                if (response.IsSuccessStatusCode)
                {
                    var responseJson = await response.Content.ReadAsStringAsync();
                    return new OkObjectResult(responseJson);
                }
                else
                {
                    string msg = "Failure to POST. Status Code: " + response.StatusCode + ". Reason: " + response.ReasonPhrase;
                    return new OkObjectResult(msg);
                }
            }
        }
    }
}
