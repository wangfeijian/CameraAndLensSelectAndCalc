using CameraAndLensSelect.ViewModels;
using CameraAndLensSelect.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace CameraAndLensSelect
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(CalcDetailPage), typeof(CalcDetailPage));
        }

    }
}
