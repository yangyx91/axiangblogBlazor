using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Api
{
    /// <summary>
    /// 摘自UpYun.NETCore https://github.com/yangzhongke/UpYun.NETCore
    /// </summary>
    public class UpYunClient
    {
        private string bucketname = "axiang";
        private string username = "apiupload";
        private string password = "5PbpHcbYDSn4LAcMdH5lx8bAmKd6ydn0";
        private string api_domain = "v0.api.upyun.com";
        private string bucketDomain = "https://pan.axiangblog.com";
        private string DL = "/";
        private Dictionary<string, object> tmp_infos = new Dictionary<string, object>();
        private bool auto_mkdir = false;
        //UpYunClient upyun = new UpYunClient();
        //var filePath = "C:\\\\Users\\yxyang\\Pictures\\VernalFalls_1920x1080.jpg";
        //System.IO.FileInfo fileInfo = new System.IO.FileInfo(filePath);
        //byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);
        //var a = upyun.WriteFileAsync("/"+DateTime.Now.ToString("yyyyMMdd")+"/"+fileInfo.Name, fileBytes, true).Result;
        public string version() { return "1.0.1"; }

        /**
        * 初始化 UpYun 存储接口
        * @param $bucketname 空间名称
        * @param $username 操作员名称
        * @param $password 密码
        * return UpYun object
        */
        public UpYunClient()
        {

        }

        /**
        * 切换 API 接口的域名
        * @param $domain {默认 v0.api.upyun.com 自动识别, v1.api.upyun.com 电信, v2.api.upyun.com 联通, v3.api.upyun.com 移动}
        * return null;
        */
        public void setApiDomain(string domain)
        {
            this.api_domain = domain;
        }

        private async Task<HttpResponseMessage> newWorker(string method, string Url, byte[] postData, Dictionary<string, object> headers)
        {
            using (HttpClientHandler handler = new HttpClientHandler { UseProxy = false })
            using (HttpClient httpClient = new HttpClient(handler))
            using (ByteArrayContent byteContent = new ByteArrayContent(postData))
            {
                httpClient.BaseAddress = new Uri("https://" + api_domain);
                var value = Convert.ToBase64String(new System.Text.ASCIIEncoding().GetBytes(this.username + ":" + this.password));
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", value);

                foreach (var kv in headers)
                {
                    byteContent.Headers.Add(kv.Key, kv.Value.ToString());
                }

                HttpResponseMessage responseMsg;
                if ("Get".Equals(method, StringComparison.OrdinalIgnoreCase))
                {
                    responseMsg = await httpClient.GetAsync(Url);
                }
                else if ("Post".Equals(method, StringComparison.OrdinalIgnoreCase))
                {
                    responseMsg = await httpClient.PostAsync(Url, byteContent);
                }
                else if ("PUT".Equals(method, StringComparison.OrdinalIgnoreCase))
                {
                    responseMsg = await httpClient.PutAsync(Url, byteContent);
                }
                else if ("Delete".Equals(method, StringComparison.OrdinalIgnoreCase))
                {
                    responseMsg = await httpClient.DeleteAsync(Url);
                }
                else
                {
                    throw new Exception("未知method：" + method);
                }

                this.tmp_infos = new Dictionary<string, object>();
                foreach (var header in responseMsg.Headers)
                {
                    if (header.Key.Length > 7 && header.Key.Substring(0, 7) == "x-upyun")
                    {
                        this.tmp_infos.Add(header.Key, header.Value);
                    }
                }

                return responseMsg;
            }
        }

        /**
        * 获取总体空间的占用信息
        * return 空间占用量，失败返回 null
        */
        public async Task<long> GetFolderUsageAsync(string url)
        {
            Dictionary<string, object> headers = new Dictionary<string, object>();
            long size;
            using (HttpClientHandler handler = new HttpClientHandler { UseProxy = false })
            using (HttpClient httpClient = new HttpClient(handler))
            { 
                httpClient.BaseAddress = new Uri("https://" + api_domain);
                var value = Convert.ToBase64String(new System.Text.ASCIIEncoding().GetBytes(this.username + ":" + this.password));
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", value);
                HttpResponseMessage resp = await httpClient.GetAsync(DL + this.bucketname + url + "?usage");
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
            return size;
        }

        /**
           * 获取某个子目录的占用信息
           * @param $path 目标路径
           * return 空间占用量，失败返回 null
           */
        public async Task<long> GetBucketUsageAsync()
        {
            return await GetFolderUsageAsync("/");
        }

        /**
        * 上传文件
        * @param $file 文件路径（包含文件名）
        * @param $datas 文件内容 或 文件IO数据流
        * return true or false
        */
        public async Task<string> WriteFileAsync(string path, byte[] data, bool auto_mkdir = true)
        {
            Dictionary<string, object> headers = new Dictionary<string, object>();
            this.auto_mkdir = auto_mkdir;
            using (var resp = await newWorker("POST", DL + this.bucketname + path, data, headers))
            {
                if (resp.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return this.bucketDomain + path;
                }
                else
                {
                    return null;
                }
            }

        }
    }

}
