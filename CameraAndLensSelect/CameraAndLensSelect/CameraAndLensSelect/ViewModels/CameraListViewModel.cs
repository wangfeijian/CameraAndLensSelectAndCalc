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
    public class CameraListViewModel : BaseViewModel
    {
        private List<CameraData> _cameraData;

        public List<CameraData> CameraData
        {
            get { return _cameraData; }
            set { SetProperty(ref _cameraData, value); }
        }

        private List<CameraData> _tempFilterData;

        public ICommand FilterCommand { get; }

        public CameraListViewModel()
        {
            Title = "相机";
            CameraData = App.Database.QueryData<CameraData>("SELECT * FROM camera");
            FilterCommand = new Command<object>(Filter);
        }

        private void Filter(object obj)
        {
            string filter = $"\"{obj.ToString()}%\"";
            string sql = $"select * from camera where Model like {filter} or Vendors like {filter} or Pixels like {filter} or Interface like {filter} or Color like {filter}";
            _tempFilterData = App.Database.QueryData<CameraData>(sql);
            CameraData = _tempFilterData;
        }
    }
}
