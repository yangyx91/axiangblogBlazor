using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using Api.DTO;

namespace Api.Service
{
    public class UserDao
    {
        private static readonly string connectionString = @"Server=tcp:axiang.database.windows.net,1433;Initial Catalog=AXiangDB;Persist Security Info=False;User ID=axiang;Password=Yangyx91;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30";

        public IEnumerable<UserInfoModel> GetUsersByPager(int page=1,int pageSize=10)
        {
            using (System.Data.IDbConnection conn=new System.Data.SqlClient.SqlConnection(connectionString))
            {
                var selectSql = @"SELECT TOP @pageSize 
                            Row_Number() over(order by ID) as ID
                          ,UserID
                          ,DisplayName
                          ,Email
                          ,UserPrincipalName
	                      ,Password
                          ,NickName
                          ,HeadImg
                          ,OpenID
                          ,Phone
                          ,CreateTime
                          ,IsDelete
                      FROM dbo.UserInfos (nolock) where ID>@number";
                return conn.Query<UserInfoModel>(selectSql, new { pageSize=pageSize,number=(page-1)*pageSize  }); ;
            }
        }

    }
}
