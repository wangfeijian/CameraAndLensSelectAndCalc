using CameraAndLensSelect.Models;
using CameraAndLensSelect.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace CameraAndLensSelect.ViewModels
{
    public class LensListViewModel : BaseViewModel
    {
        private List<LensData> _lensData;

        public List<LensData> LensData
        {
            get { return _lensData; }
            set { SetProperty(ref _lensData, value); }
        }

        private List<LensData> _tempFilterData;

        public ICommand FilterCommand { get; }

        public LensListViewModel()
        {
            Title = "镜头";
            LensData = App.Database.QueryData<LensData>("SELECT * FROM lens");
            FilterCommand = new Command<object>(Filter);
        }

        private void Filter(object obj)
        {
            string filter = $"\"{obj.ToString()}%\"";
            string sql = $"select * from lens where Model like {filter} or Vendors like {filter} or FocalLength like {filter} or Interface like {filter}";
            _tempFilterData = App.Database.QueryData<LensData>(sql);
            LensData = _tempFilterData;
        }
    }
}
