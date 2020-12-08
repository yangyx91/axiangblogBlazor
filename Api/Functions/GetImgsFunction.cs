using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Text;
using System.Net.Http.Headers;
using System.Net.Http;
using Api.DTO;

namespace Api.Functions
{
    public static class GetImgsFunction
    {
        private static readonly string _dbName = "imgdb";
        private static readonly string username = "a208c9f8-c877-4399-86c0-fc1675cee12e-bluemix";
        private static readonly string password = "2e9f799f8f9a467c91612c72bcacf2d600757afd461292a7b4ac38ddd28cdb78";
        private static readonly string host = "a208c9f8-c877-4399-86c0-fc1675cee12e-bluemix.cloudantnosqldb.appdomain.cloud";
        private static readonly string apikey = "QAIv6jfw4a39voYb6aaS5z6Ty-0Av4slPWqOoE83qZ1g";
        private static readonly string port = "443";

        [FunctionName("GetImgsFunction")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route ="allimgs")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];

            //string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            //dynamic data = JsonConvert.DeserializeObject(requestBody);
            //name = name ?? data?.name;

            //string responseMessage = string.IsNullOrEmpty(name)
            //? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
            // : $"Hello, {name}. This HTTP triggered function executed successfully.";

            //return new OkObjectResult(responseMessage);

            using (HttpClientHandler handler = new HttpClientHandler { UseProxy = false })
            using (var client = new HttpClient())
            {
                var auth = Convert.ToBase64String(Encoding.ASCII.GetBytes(username + ":" + password));
                client.BaseAddress = new Uri("https://" + host);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", auth);

                var response = await client.GetAsync(_dbName + "/_all_docs?include_docs=true");
                if (response.IsSuccessStatusCode)
                {
                    var responseJson = await response.Content.ReadAsStringAsync();
                    return new OkObjectResult(System.Text.Json.JsonSerializer.Deserialize<QueryImgDocumentResult>(responseJson));
                }
                else
                {
                    string msg = "Failure to Get. Status Code: " + response.StatusCode + ". Reason: " + response.ReasonPhrase;
                    return new OkObjectResult(msg);
                }
            }
        }
    }
}
