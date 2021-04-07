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
    public static class CountOneBingImg
    {
        private static Dao mDal = new Dao();

        [FunctionName("CountOneBingImg")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = "countOneBingImg")] HttpRequest req)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var condition= JsonConvert.DeserializeObject<CountOneBingImgCondition>(requestBody);
            var count=await mDal.CountDocuments<BingImgModel>("imgId", condition.imgId);
            return new OkObjectResult(count);
        }
    }
}
