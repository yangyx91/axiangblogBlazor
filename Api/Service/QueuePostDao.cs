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
    public class QueuePostDao
    {
        private static readonly string connectionString = @"Server=tcp:ablogqueuedbserver.database.windows.net,1433;Initial Catalog=ABlogQueue_db;Persist Security Info=False;User ID=axiang;Password=Yangyx91;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30";

        public static SqlSugarScope sqlSugar = new SqlSugarScope(
                new ConnectionConfig()
                {
                    IsAutoCloseConnection = true,
                    DbType = SqlSugar.DbType.SqlServer,
                    ConnectionString = connectionString,
                });

        public static List<QueuePostModel> GetQueuePostsByPagerSugar(
            int page = 1, int pageSize = 10)
        {
            return sqlSugar.Queryable<QueuePostModel>().ToPageList(page, pageSize);
        }
    }
}
