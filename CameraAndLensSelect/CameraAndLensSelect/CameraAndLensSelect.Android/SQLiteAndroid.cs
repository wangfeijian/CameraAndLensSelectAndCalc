using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using CameraAndLensSelect.Droid;
using CameraAndLensSelect.Services;
using SQLite;
//using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLiteAndroid))]
namespace CameraAndLensSelect.Droid
{
        public class SQLiteAndroid : ISQLite
        {
        public SQLiteConnection GetConnection()
        {
            var sqliteFilename = "Camera.db";
            string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var path = Path.Combine(documentsPath, sqliteFilename);
            Console.WriteLine(path);
            if (!File.Exists(path))
            {

                var readStream = MainActivity.Instance.Resources.OpenRawResource(Resource.Raw.Camera);
                FileStream writeStream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);
                int Length = 256;
                Byte[] buffer = new Byte[Length];
                int bytesRead = readStream.Read(buffer, 0, Length);
                while (bytesRead > 0)
                {
                    writeStream.Write(buffer, 0, bytesRead);
                    bytesRead = readStream.Read(buffer, 0, Length);
                }
                readStream.Close();
                writeStream.Close();
            }
            var conn = new SQLiteConnection(path);
            return conn;
        }
    }
}