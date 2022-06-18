using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using Api.DTO;
using Microsoft.Data.SqlClient;
using System.Data;
using SqlSugar;

namespace Api.Service
{
    public class UserDao
    {
        private static readonly string connectionString = @"Server=tcp:axiang.database.windows.net,1433;Initial Catalog=AXiangDB;Persist Security Info=False;User ID=axiang;Password=Yangyx91;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30";

        public static SqlSugarScope sqlSugar = new SqlSugarScope(
                new ConnectionConfig()
                    {
                        IsAutoCloseConnection=true,
                        DbType= SqlSugar.DbType.SqlServer,
                        ConnectionString=connectionString,
                    } );

        public IEnumerable<UserInfoModel> GetUsersByPager(int page=1,int pageSize=10)
        {
            using (IDbConnection conn=new SqlConnection(connectionString))
            {
                var selectSql =string.Format( @"SELECT TOP {0} 
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
                      FROM dbo.UserInfos (nolock) where ID>{1}",pageSize,(page-1)*pageSize);

                return conn.Query<UserInfoModel>(selectSql); 
            }
        }

        public static List<UserInfoModel> GetUsersByPagerSugar(int page = 1, int pageSize = 10)
        {
            return sqlSugar.Queryable<UserInfoModel>().ToPageList(page,pageSize);
        }
    }
}
