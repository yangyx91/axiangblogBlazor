using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;

namespace Api.DTO
{
    [SugarTable("Queue_Posts")]
    public class QueuePostModel
    {
        [SugarColumn(IsPrimaryKey = true)]
        public int ID { get; set; }

        public string Post_Id { get; set; }

        public DateTime Post_Date { get; set; }

        public string Post_Title { get; set; }

        public string Post_Content { get; set; }

        public string Post_CategoryName { get; set; }

        public string Post_Status { get; set; }

        public string Post_Author { get; set; }
    }
}
