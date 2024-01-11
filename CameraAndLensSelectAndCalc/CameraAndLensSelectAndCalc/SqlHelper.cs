using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CameraAndLensSelectAndCalc
{
    internal class SqlHelper
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        internal static async Task<List<T>> AsyncQuery<T>(string sql)
        {
            using (IDbConnection connection = new SQLiteConnection("Data Source=Camera.db"))
            {
                connection.Open();
                var datas = await connection.QueryAsync<T>(sql);
                return datas.ToList();
            }
        }

        /// <summary>
        /// 增加
        /// insert into Test (Name,Mobile,Mail,Address) VALUES (@Name,@Mobile,@Mail,@Address)
        /// </summary>
        /// <param name="data"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        internal static async void AsnycInsert(BaseData data, string sql)
        {
            using (IDbConnection connection = new SQLiteConnection("Data Source=Camera.db"))
            {
                //"insert into Test (Name,Mobile,Mail,Address) VALUES (@Name,@Mobile,@Mail,@Address)"
                await connection.ExecuteAsync(sql, data);
            }
        }

        /// <summary>
        /// 修改
        /// 需要注意如果是字符串需要额外添加双引号或者单引号
        /// update Test set Name = @Name, Mobile = @Mobile, Mail = @Mail, Address = @Address where Name='{name}'
        /// </summary>
        /// <param name="data"></param>
        /// <param name="sql"></param>
        internal static async void AsyncUpdate(BaseData data, string sql)
        {
            using (IDbConnection connection = new SQLiteConnection("Data Source=Camera.db"))
            {
                await connection.ExecuteAsync(sql, data);
            }
        }

        /// <summary>
        /// 删除
        /// delete from Test where Name='{name}'
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        internal static async Task<int> AsyncDelete(string sql)
        {
            using (IDbConnection connection = new SQLiteConnection("Data Source=Camera.db"))
            {
                return await connection.ExecuteAsync(sql);
            }

        }
    }
}
