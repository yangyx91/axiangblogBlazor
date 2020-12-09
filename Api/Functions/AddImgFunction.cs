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
using System.Text;
using System.Net.Http.Headers;

namespace Api.Functions
{
    public static class AddImgFunction
    {
        private static readonly string _dbName = "imgdb";
        private static readonly string username = "a208c9f8-c877-4399-86c0-fc1675cee12e-bluemix";
        private static readonly string password = "2e9f799f8f9a467c91612c72bcacf2d600757afd461292a7b4ac38ddd28cdb78";
        private static readonly string host = "a208c9f8-c877-4399-86c0-fc1675cee12e-bluemix.cloudantnosqldb.appdomain.cloud";
        private static readonly string apikey = "QAIv6jfw4a39voYb6aaS5z6Ty-0Av4slPWqOoE83qZ1g";
        private static readonly string port = "443";

        [FunctionName("AddImgFunction")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = "addImg")] HttpRequest req)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            if (!string.IsNullOrEmpty(requestBody))
            {
                using (HttpClientHandler handler = new HttpClientHandler { UseProxy = false })
                using (var client = new HttpClient())
                {
                    var auth = Convert.ToBase64String(Encoding.ASCII.GetBytes(username + ":" + password));
                    client.BaseAddress = new Uri("https://" + host);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", auth);

                    var todoItemJson = new StringContent(
                        requestBody,
                        Encoding.UTF8,
                        "application/json");
                    var response = await client.PostAsync(_dbName, todoItemJson);
                    if (response.IsSuccessStatusCode)
                    {
                        var responseJson = await response.Content.ReadAsStringAsync();
                        await new ApiLogger("AddImgFunction").LogInformation(responseJson,
                            new {
                            BaseAddress=client.BaseAddress,
                            RequestUri= _dbName
                            });
                        return new OkObjectResult(responseJson);
                    }
                    else
                    {
                        string msg = "Failure to POST. Status Code: " + response.StatusCode + ". Reason: " + response.ReasonPhrase;
                        await new ApiLogger("AddImgFunction").LogError(msg, new
                        {
                            BaseAddress = client.BaseAddress,
                            RequestUri = _dbName
                        });
                        return new OkObjectResult(msg);
                    }
                }
            }
            return new OkObjectResult("");
        }
    }
}
