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

namespace Api.Functions
{
    public static class DescribeImgFunction
    {
        private static readonly string endpointkey = "00933b7af07c4936957315222dbf349d";
        private static readonly string endpointURL= "axiangcomputervision.cognitiveservices.azure.com";
        private static readonly string endpointLocation= "eastasia";
        //https://www.axiangblog.com/res/img/author.jpg


        [FunctionName("DescribeImgFunction")] 
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = "describeimg")] HttpRequest req)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            if (!string.IsNullOrEmpty(requestBody))
            {
                using (HttpClientHandler handler = new HttpClientHandler { UseProxy = false })
                using (var client = new HttpClient())
                {
                    var queryString = System.Web.HttpUtility.ParseQueryString(string.Empty);
                    queryString["maxCandidates"] = "1";
                    queryString["language"] = "zh";
                    client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", endpointkey);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var uri = "https://"+ endpointURL + "/vision/v3.1/describe?" + queryString;

                    var describeimgJson = new StringContent(
                        requestBody,
                        System.Text.Encoding.UTF8,
                        "application/json");
                    var response = await client.PostAsync(uri, describeimgJson);
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
            return new OkObjectResult("");
        }
    }
}
