using CameraAndLensSelect.Models;
using CameraAndLensSelect.Services;
using CameraAndLensSelect.Views;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CameraAndLensSelect
{
    public partial class App : Application
    {
        static SqlHelper database;
        public static SqlHelper Database
        {
            get
            {
                if (database == null)
                {
                    database = new SqlHelper();
                }
                return database;
            }
        }

        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
