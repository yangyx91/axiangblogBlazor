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
using System.Linq;
using Data;

namespace Api
{
    public static class Function1
    {
        private static string bingDailyImgApi = "https://www.bing.com/HPImageArchive.aspx?format=js&idx=0&n=1&mkt=zh-CN";

        private static async Task<BingImg> GetBingImgAsync()
        {
            string result = "";
            var httpClient = new HttpClient();
            var responseMessage = await httpClient.GetAsync(bingDailyImgApi);
            if (responseMessage.IsSuccessStatusCode)
            {
                result = await responseMessage.Content.ReadAsStringAsync();
            }
            return System.Text.Json.JsonSerializer.Deserialize<BingImgResponse>(result).images.FirstOrDefault();
        }

        [FunctionName("Function1")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "todayBingImg")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];

            //string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            //dynamic data = JsonConvert.DeserializeObject(requestBody);
            //name = name ?? data?.name;
            var bingImg = await GetBingImgAsync();

            string responseMessage = string.IsNullOrEmpty(name)
                ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
                : $"Hello, {name}. This HTTP triggered function executed successfully.";

            return new OkObjectResult(bingImg);
        }
    }
}
