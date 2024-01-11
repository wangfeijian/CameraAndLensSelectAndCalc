using CameraAndLensSelect.Models;
using CameraAndLensSelect.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CameraAndLensSelect.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CalcDetailPage : ContentPage
	{
		public CalcDetailPage ()
		{
			InitializeComponent ();
		}
				
		protected override void OnAppearing()
        {
            base.OnAppearing();

			var dataContext = BindingContext as CalcDetailViewModel;
			dataContext.SelectCamera = App.Database.SelectCamera;
			dataContext.SelectLens = App.Database.SelectLens;
			dataContext.CalcDetail = App.Database.CalcDetail;

			dataContext.CalcTimes();
			dataContext.CalcDof();
        }
    }
}