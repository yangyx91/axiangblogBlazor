using System;
using System.Collections.Generic;
using System.Text;

namespace Api.DTO
{
    public class LogDocument
    {
        public string id { get; set; }
        public string rev { get; set; }
        public string logId { get; set; }
        public string eventId { get; set; } 
        public string logLevel { get; set; }
        public string exception { get; set; }
        public string message { get; set; }
        public string args { get; set; }
        public string logDate { get; set; }
        public string creator { get; set; }
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
