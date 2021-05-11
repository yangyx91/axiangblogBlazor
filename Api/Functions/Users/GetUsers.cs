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

namespace Api.Functions.Users
{
    public static class GetUsers
    {
        private static UserDao userDao = new UserDao();

        [FunctionName("GetUsers")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = "getusers")] HttpRequest req,
            ILogger log)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            //var condition = JsonConvert.DeserializeObject<CountOneBingImgCondition>(requestBody);
            //var count = await mDal.CountDocuments<BingImgModel>("imgId", condition.imgId);
            var res = userDao.GetUsersByPager();
            return new OkObjectResult(res);
        }
    }
}
