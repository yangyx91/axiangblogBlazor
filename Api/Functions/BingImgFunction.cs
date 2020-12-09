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
using Api.DTO;

namespace Api
{
    public static class BingImgFunction
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
                await new ApiLogger("BingImgFunction").LogInformation(result, new
                {
                    BaseAddress = httpClient.BaseAddress,
                    RequestUri = bingDailyImgApi
                });
            }
            else
            {
                string msg = "Failure to POST. Status Code: " + responseMessage.StatusCode + ". Reason: " + responseMessage.ReasonPhrase;
                await new ApiLogger("BingImgFunction").LogError(msg, new
                {
                    BaseAddress = httpClient.BaseAddress,
                    RequestUri = bingDailyImgApi
                });
                return new BingImg();
            }
            return System.Text.Json.JsonSerializer.Deserialize<BingImgResponse>(result).images.FirstOrDefault();
        }

        [FunctionName("BingImgFunction")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = "todayBingImg")] HttpRequest req)
        {
            var bingImg = await GetBingImgAsync();
            return new OkObjectResult(bingImg);
        }
    }
}
