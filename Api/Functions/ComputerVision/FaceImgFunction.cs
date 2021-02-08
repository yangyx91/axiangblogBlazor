using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http;

namespace Api.Functions.ComputerVision
{
    public static class FaceImgFunction
    {
        //Face API - v1.0
        //https://eastasia.dev.cognitive.microsoft.com/docs/services/563879b61984550e40cbbe8d/operations/563879b61984550f30395236
        private static readonly string endpointkey = "63b3f5d26b2b48a1bc0c1d77e048a519";
        private static readonly string endpointURL = "axiangface.cognitiveservices.azure.com";
        private static readonly string endpointLocation = "eastasia";

        [FunctionName("FaceImgFunction")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = "faceimg")] HttpRequest req)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            if (!string.IsNullOrEmpty(requestBody))
            {
                using (HttpClientHandler handler = new HttpClientHandler { UseProxy = false })
                using (var client = new HttpClient())
                {
                    var queryString = System.Web.HttpUtility.ParseQueryString(string.Empty);
                    queryString["returnFaceId"] = "true";
                    queryString["returnFaceLandmarks"] = "false";
                    queryString["returnFaceAttributes"] = "age,gender,smile,emotion,hair";
                    queryString["recognitionModel"] = "recognition_03";
                    queryString["returnRecognitionModel"] = "false";
                    queryString["detectionModel"] = "detection_01";
                    queryString["faceIdTimeToLive"] = "86400";
                    client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", endpointkey);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var uri = "https://" + endpointURL + "/face/v1.0/detect?" + queryString;

                    var faceimgJson = new StringContent(
                        requestBody,
                        System.Text.Encoding.UTF8,
                        "application/json");
                    var response = await client.PostAsync(uri, faceimgJson);
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
