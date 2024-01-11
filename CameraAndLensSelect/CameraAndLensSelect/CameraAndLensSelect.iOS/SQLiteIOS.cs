using CameraAndLensSelect.iOS;
using CameraAndLensSelect.Services;
using Foundation;
using SQLite;
//using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLiteIOS))]
namespace CameraAndLensSelect.iOS
{
    public class SQLiteIOS : ISQLite
    {
        public SQLiteIOS()
        {
        }
        public SQLiteConnection GetConnection()
        {
            var sqliteFilename = "Camera.db";
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libraryPath = Path.Combine(documentsPath, "..", "Library");
            var path = Path.Combine(libraryPath, sqliteFilename);

            // This is where we copy in the prepopulated database
            Console.WriteLine(path);
            if (!File.Exists(path))
            {
                File.Copy(sqliteFilename, path);
            }
            var conn = new SQLiteConnection(path);
            return conn;
        }
    }
}