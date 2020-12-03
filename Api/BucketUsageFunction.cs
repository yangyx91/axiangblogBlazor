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

namespace Api
{
    /// <summary>
    /// Õª×ÔUpYun.NETCore https://github.com/yangzhongke/UpYun.NETCore
    /// </summary>
    public static class BucketUsageFunction
    {
        private static readonly string bucketname = "axiang";
        private static readonly string username = "apiupload";
        private static readonly string password = "5PbpHcbYDSn4LAcMdH5lx8bAmKd6ydn0";
        private static readonly string api_domain = "v0.api.upyun.com";
        private static readonly string bucketDomain = "https://pan.axiangblog.com";
        private static readonly string DL = "/";

        [FunctionName("BucketUsageFunction")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = "upYunUsage")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];

            long size;
            using (HttpClientHandler handler = new HttpClientHandler { UseProxy = false })
            using (HttpClient httpClient = new HttpClient(handler))
            {
                httpClient.BaseAddress = new Uri("https://" + api_domain);
                var value = Convert.ToBase64String(new System.Text.ASCIIEncoding().GetBytes(username + ":" + password));
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", value);
                HttpResponseMessage resp = await httpClient.GetAsync(DL + bucketname + "/" + "?usage");
                try
                {
                    string strhtml = await resp.Content.ReadAsStringAsync();
                    size = long.Parse(strhtml);
                }
                catch (Exception)
                {
                    size = 0;
                }
            }

            return new OkObjectResult(size);
        }
    }
}
