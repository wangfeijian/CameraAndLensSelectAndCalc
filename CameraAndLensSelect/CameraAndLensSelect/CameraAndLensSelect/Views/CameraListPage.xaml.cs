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
    public partial class CameraListPage : ContentPage
    {
        public CameraListPage()
        {
            InitializeComponent();
        }

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CameraData selectedItem = e.CurrentSelection[0] as CameraData;
            SearchItem.Text = selectedItem.Model;
        }

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            var dataContext = BindingContext as CameraListViewModel;
            string temp = (sender as SearchBar).Text;
            if(string.IsNullOrEmpty(temp))
            {
                dataContext.CameraData = App.Database.QueryData<CameraData>("SELECT * FROM camera");
            }
        }
    }
}