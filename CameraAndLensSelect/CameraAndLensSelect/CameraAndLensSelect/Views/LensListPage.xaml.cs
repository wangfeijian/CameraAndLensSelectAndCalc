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
    public partial class LensListPage : ContentPage
    {
        public LensListPage()
        {
            InitializeComponent();
        }

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LensData selectedItem = e.CurrentSelection[0] as LensData;
            SearchItem.Text = selectedItem.Model;
        }

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            var dataContext = BindingContext as LensListViewModel;
            string temp = (sender as SearchBar).Text;
            if(string.IsNullOrEmpty(temp))
            {
                dataContext.LensData = App.Database.QueryData<LensData>("SELECT * FROM lens");
            }
        }
    }
}