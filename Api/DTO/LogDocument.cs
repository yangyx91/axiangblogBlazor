using System;
using System.Collections.Generic;
using System.Text;

namespace Api.DTO
{
    public class LogDocument
    {
        public string id { get; set; }

        public string rev { get; set; }
        public string LogId { get; set; }
        public string LogDate { get; set; }
        public string LogLevel { get; set; }
        public string LogInfo { get; set; }
        public string LogDebug { get; set; }
        public string LogError { get; set; } 
        public string Creator { get; set; }
    }

    public class QueryIBMDocumentResult
    {
        public int total_rows { get; set; }

        public int offset { get; set; }

        public List<IBMDocumentKeyValue> rows { get; set; }
    }

    public class IBMDocumentKeyValue
    {
        public string id { get; set; }

        public string key { get; set; }

        public revDoc value { get; set; }

        public LogDocument doc { get; set; }
    }

    public class revDoc
    {
        public string rev { get; set; }
    }
}
