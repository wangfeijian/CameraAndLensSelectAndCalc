using CameraAndLensSelect.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CameraAndLensSelect.Services
{
    public class SqlHelper
    {
        readonly SQLiteConnection _database;
        public CameraData SelectCamera { get; set; }
        public LensData SelectLens { get; set; }
        public FinalCalcData CalcDetail { get; set; }

        public SqlHelper()
        {
            _database = Xamarin.Forms.DependencyService.Get<ISQLite>().GetConnection();
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <sql>sql语句</sql>
        /// <returns></returns>
        public  List<T> QueryData<T>(string sql, params object[] args) where T : new()
        {
            var datas =  _database.Query<T>(sql, args);

            return datas;
        }

    }
}
