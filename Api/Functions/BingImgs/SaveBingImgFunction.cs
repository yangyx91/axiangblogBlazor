using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Api.Service;
using Api.DTO;

namespace Api.Functions.BingImgs
{
    public static class SaveBingImgFunction
    {
        private static Dao mDal = new Dao();

        [FunctionName("SaveBingImgFunction")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = "saveBingImg")] HttpRequest req)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<BingImgModel>(requestBody);
            await mDal.AddOneDocument(data);
            return new OkObjectResult("ok");
        }
    }
}
