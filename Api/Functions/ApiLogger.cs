using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Api.DTO;
using System.Net.Http;
using System.Text;
using System.Net.Http.Headers;
using TimeZoneConverter;

namespace Api
{
    public class ApiLogger : ILogger
    {
        private static readonly string _dbName = "apilogdb";  //imgdb
        private static readonly string username = "a208c9f8-c877-4399-86c0-fc1675cee12e-bluemix";
        private static readonly string password = "2e9f799f8f9a467c91612c72bcacf2d600757afd461292a7b4ac38ddd28cdb78";
        private static readonly string host = "a208c9f8-c877-4399-86c0-fc1675cee12e-bluemix.cloudantnosqldb.appdomain.cloud";
        private static readonly string apikey = "QAIv6jfw4a39voYb6aaS5z6Ty-0Av4slPWqOoE83qZ1g";
        private static readonly string port = "443";

        public string LogId { get; set; }

        public string EventId { get; set; }

        public string LogDate { get; set; }

        public string CurrentMethodName { get; set; }

        public ApiLogger(string methodName)
        {
            var datetime=TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TZConvert.GetTimeZoneInfo("Asia/Shanghai"));
            LogId = datetime.ToString("yyyyMMddHHmmss");
            EventId = datetime.ToString("yyyyMMddHHmmss");
            LogDate = datetime.ToString("yyyy-MM-dd HH:mm:ss");
            CurrentMethodName = methodName;
        } 

        public void LogDebug(Exception exception, string message, params object[] args)
        {
            Info(new LogDocument()
            {
                logId = this.LogId,
                eventId = this.EventId,
                logDate = this.LogDate,
                creator = this.CurrentMethodName,
                logLevel = LogLevel.Debug.ToString(),
                exception = System.Text.Json.JsonSerializer.Serialize(exception),
                message = exception.Message+message,
                args = System.Text.Json.JsonSerializer.Serialize(args)
            }); 
        }

        public void LogDebug(string message, params object[] args)
        {
            Info(new LogDocument()
            {
                logId = this.LogId,
                eventId = this.EventId,
                logDate = this.LogDate,
                creator = this.CurrentMethodName,
                logLevel = LogLevel.Debug.ToString(),
                exception = "",
                message = message,
                args = System.Text.Json.JsonSerializer.Serialize(args)
            });
        }

        public void LogTrace(Exception exception, string message, params object[] args)
        {
            Info(new LogDocument()
            {
                logId = this.LogId,
                eventId = this.EventId,
                logDate = this.LogDate,
                creator = this.CurrentMethodName,
                logLevel = LogLevel.Trace.ToString(),
                exception = System.Text.Json.JsonSerializer.Serialize(exception),
                message = exception.Message + message,
                args = System.Text.Json.JsonSerializer.Serialize(args)
            });
        }


        public void LogTrace(string message, params object[] args)
        {
            Info(new LogDocument()
            {
                logId = this.LogId,
                eventId = this.EventId,
                logDate = this.LogDate,
                creator = this.CurrentMethodName,
                logLevel = LogLevel.Trace.ToString(),
                exception = "",
                message = message,
                args = System.Text.Json.JsonSerializer.Serialize(args)
            });
        }

        public void LogInformation(Exception exception, string message, params object[] args)
        {
            Info(new LogDocument()
            {
                logId = this.LogId,
                eventId = this.EventId,
                logDate = this.LogDate,
                creator = this.CurrentMethodName,
                logLevel = LogLevel.Information.ToString(),
                exception = System.Text.Json.JsonSerializer.Serialize(exception),
                message = exception.Message + message,
                args = System.Text.Json.JsonSerializer.Serialize(args)
            });
        }


        public void LogInformation(string message, params object[] args)
        {
            Info(new LogDocument()
            {
                logId = this.LogId,
                eventId = this.EventId,
                logDate = this.LogDate,
                creator = this.CurrentMethodName,
                logLevel = LogLevel.Information.ToString(),
                exception = "",
                message = message,
                args = System.Text.Json.JsonSerializer.Serialize(args)
            });
        }

        public void LogWarning(Exception exception, string message, params object[] args)
        {
            Info(new LogDocument()
            {
                logId = this.LogId,
                eventId = this.EventId,
                logDate = this.LogDate,
                creator = this.CurrentMethodName,
                logLevel = LogLevel.Warning.ToString(),
                exception = System.Text.Json.JsonSerializer.Serialize(exception),
                message = exception.Message + message,
                args = System.Text.Json.JsonSerializer.Serialize(args)
            });
        }


        public void LogWarning(string message, params object[] args)
        {
            Info(new LogDocument()
            {
                logId = this.LogId,
                eventId = this.EventId,
                logDate = this.LogDate,
                creator = this.CurrentMethodName,
                logLevel = LogLevel.Warning.ToString(),
                exception = "",
                message =message,
                args = System.Text.Json.JsonSerializer.Serialize(args)
            });
        }

        public void LogError(Exception exception, string message, params object[] args)
        {
            Info(new LogDocument()
            {
                logId = this.LogId,
                eventId = this.EventId,
                logDate = this.LogDate,
                creator = this.CurrentMethodName,
                logLevel = LogLevel.Error.ToString(),
                exception = System.Text.Json.JsonSerializer.Serialize(exception),
                message = exception.Message + message,
                args = System.Text.Json.JsonSerializer.Serialize(args)
            });
        }


        public void LogError(string message, params object[] args)
        {
            Info(new LogDocument()
            {
                logId = this.LogId,
                eventId = this.EventId,
                logDate = this.LogDate,
                creator = this.CurrentMethodName,
                logLevel = LogLevel.Error.ToString(),
                exception = "",
                message = message,
                args = System.Text.Json.JsonSerializer.Serialize(args)
            });
        }

        public void LogCritical(Exception exception, string message, params object[] args)
        {
            Info(new LogDocument()
            {
                logId = this.LogId,
                eventId = this.EventId,
                logDate = this.LogDate,
                creator = this.CurrentMethodName,
                logLevel = LogLevel.Critical.ToString(),
                exception = System.Text.Json.JsonSerializer.Serialize(exception),
                message = exception.Message + message,
                args = System.Text.Json.JsonSerializer.Serialize(args)
            });
        }


        public void LogCritical(string message, params object[] args)
        {
            Info(new LogDocument()
            {
                logId = this.LogId,
                eventId = this.EventId,
                logDate = this.LogDate,
                creator = this.CurrentMethodName,
                logLevel = LogLevel.Critical.ToString(),
                exception ="",
                message =message,
                args = System.Text.Json.JsonSerializer.Serialize(args)
            });
        }

        private static void Info(LogDocument log)  
        {
            if (username == null || password == null || host == null)
            {
                throw new Exception("Missing Cloudant NoSQL DB service credentials");
            }
            using (HttpClientHandler handler = new HttpClientHandler { UseProxy = false })
            using (var client = new HttpClient())
            {
                var auth = Convert.ToBase64String(Encoding.ASCII.GetBytes(username + ":" + password));
                client.BaseAddress = new Uri("https://" + host);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", auth);
                var todoItemJson = new StringContent(
                        System.Text.Json.JsonSerializer.Serialize(log),
                        Encoding.UTF8,
                        "application/json");
                client.PostAsync(_dbName, todoItemJson);
                //if (response.IsSuccessStatusCode)
                //{
                    //var responseJson = await response.Content.ReadAsStringAsync();
                //}
            }
        }

        

        public IDisposable BeginScope<TState>(TState state)
        {
            throw new NotImplementedException();
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            throw new NotImplementedException();
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            throw new NotImplementedException();
        }
    }
}
