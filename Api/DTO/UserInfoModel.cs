using System;
using System.Collections.Generic;
using System.Text;
using SqlSugar;

namespace Api.DTO
{
    [SugarTable("UserInfos")]
    public class UserInfoModel
    {
        [SugarColumn(IsPrimaryKey =true)]
        public int ID { get; set; }
        public string UserID { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string UserPrincipalName { get; set; }
        public string Password { get; set; }
        public string NickName { get; set; }
        public string HeadImg { get; set; }
        public string OpenID { get; set; }
        public string Phone { get; set; }
        public DateTime CreateTime { get; set; }
        public int IsDelete { get; set; }
    }
}
