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
using Api.Service;

namespace Api.Functions
{
    public static class GetImgsFunction
    {
        private static Dao mDal = new Dao();

        [FunctionName("GetImgsFunction")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route ="allimgs")] HttpRequest req)
        {
            var res=await mDal.FindDocumentsByPage<BingImgModel>("Creator", "admin", "CreateDate");
            return new OkObjectResult(res); 
        }
    }
}
