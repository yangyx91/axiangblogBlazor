using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;

namespace Api
{
    /// <summary>
    /// ժ��UpYun.NETCore https://github.com/yangzhongke/UpYun.NETCore
    /// </summary>
    public static class UploadFunction
    {
        private static readonly string bucketname = "axiang";
        private static readonly string username = "apiupload";
        private static readonly string password = "5PbpHcbYDSn4LAcMdH5lx8bAmKd6ydn0";
        private static readonly string api_domain = "v0.api.upyun.com";
        private static readonly string bucketDomain = "https://pan.axiangblog.com";
        private static readonly string DL = "/";

        [FunctionName("UploadFunction")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = "uploadImg")] HttpRequest req)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            if (!string.IsNullOrEmpty(requestBody) && requestBody.Contains("data:") && requestBody.Contains("base64"))
            {
                var base64Array = JsonConvert.DeserializeObject<string>(requestBody).Split(',');
                var format = base64Array[0].Split(";")[0].Split(":")[1];
                byte[] postData = Convert.FromBase64String(base64Array[1]);
                try
                {
                    using (HttpClientHandler handler = new HttpClientHandler { UseProxy = false })
                    using (HttpClient httpClient = new HttpClient(handler))
                    using (ByteArrayContent byteContent = new ByteArrayContent(postData)) 
                    {
                        httpClient.BaseAddress = new Uri("https://" + api_domain);
                        var value = Convert.ToBase64String(new System.Text.ASCIIEncoding().GetBytes(username + ":" + password));
                        httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", value);
                        string path = "/"+DateTime.Now.ToString("yyyyMMdd")+"/"+Guid.NewGuid().ToString("N")+"."+ format.Split("/")[1];
                        string Url = DL + bucketname + path;
                        HttpResponseMessage responseMsg = await httpClient.PostAsync(Url, byteContent);
                        var tmp_infos = new Dictionary<string, object>();
                        foreach (var header in responseMsg.Headers)
                        {
                            if (header.Key.Length > 7 && header.Key.Substring(0, 7) == "x-upyun")
                            {
                                tmp_infos.Add(header.Key, header.Value);
                            }
                        }

                        if (responseMsg!=null&&responseMsg.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            await new ApiLogger("UploadFunction").LogInformation(await responseMsg.Content.ReadAsStringAsync(), new
                            {
                                BaseAddress = httpClient.BaseAddress,
                                RequestUri = Url
                            });
                            return new OkObjectResult(path);
                        }
                        else
                        {
                            string msg = "Failure to POST. Status Code: " + responseMsg.StatusCode + ". Reason: " + responseMsg.ReasonPhrase;
                            await new ApiLogger("UploadFunction").LogError(msg, new
                            {
                                BaseAddress = httpClient.BaseAddress,
                                RequestUri = Url
                            });
                        }
                        return new OkObjectResult(responseMsg);

                    }
                }
                catch (Exception ex)
                {

                    return new OkObjectResult(ex.ToString());
                }
               
            }
            return new OkObjectResult(requestBody);
        }
    }
}
