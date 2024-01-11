using CameraAndLensSelect.Models;
using CameraAndLensSelect.Views;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace CameraAndLensSelect.ViewModels
{
    public class CalcViewModel : BaseViewModel
    {
        #region 绑定属性
        private string _cameraModel;

        public string CameraModel
        {
            get { return _cameraModel; }
            set { SetProperty(ref _cameraModel, value); }
        }

        private string _lensModel;

        public string LensModel
        {
            get { return _lensModel; }
            set { SetProperty(ref _lensModel, value); }
        }

        private string _errorMessage;

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set { SetProperty(ref _errorMessage, value); }
        }

        private CameraData _selectCamera;

        public CameraData SelectCamera
        {
            get { return _selectCamera; }
            set { SetProperty(ref _selectCamera, value); }
        }

        private LensData _selectLens;

        public LensData SelectLens
        {
            get { return _selectLens; }
            set { SetProperty(ref _selectLens, value); }
        }

        private double _chipWidth, _chipHeight;

        public double ChipWidth
        {
            get { return _chipWidth; }
            set { SetProperty(ref _chipWidth, value); }
        }

        public double ChipHeight
        {
            get { return _chipHeight; }
            set { SetProperty(ref _chipHeight, value); }
        }

        private string _hAngle;

        public string HAngle
        {
            get { return _hAngle; }
            set { SetProperty(ref _hAngle, value); }
        }

        private string _vAngle;

        public string VAngle
        {
            get { return _vAngle; }
            set { SetProperty(ref _vAngle, value); }
        }

        private string _viewCalcWidth;

        public string ViewCalcWidth
        {
            get { return _viewCalcWidth; }
            set { SetProperty(ref _viewCalcWidth, value); }
        }

        private string _viewCalcHeight;

        public string ViewCalcHeight
        {
            get { return _viewCalcHeight; }
            set { SetProperty(ref _viewCalcHeight, value); }
        }

        private string _viewCalcTimes;

        public string ViewCalcTimes
        {
            get { return _viewCalcTimes; }
            set { SetProperty(ref _viewCalcTimes, value); }
        }

        private int _workingDistance;

        public int WorkingDistance
        {
            get { return _workingDistance; }
            set { SetProperty(ref _workingDistance, value); }
        }

        private double _pixelAccuracy;

        public double PixelAccuracy
        {
            get { return _pixelAccuracy; }
            set { SetProperty(ref _pixelAccuracy, value); }
        }

        private bool _enableDetail;

        public bool EnableDetail
        {
            get { return _enableDetail; }
            set { SetProperty(ref _enableDetail, value); }
        }
        #endregion

        double _pixelSize, _fLength;

        #region 命令
        public ICommand CameraSelectCommand { get; }
        public ICommand LensSelectCommand { get; }
        public ICommand ViewWidthChangedCommand { get; }
        public ICommand ViewHeightChangedCommand { get; }
        public ICommand ViewTimesChangedCommand { get; }
        public ICommand ShowDetailCommand { get; }
        #endregion
        public CalcViewModel()
        {
            Title = "相机镜头参数计算";
            CameraSelectCommand = new Command(CameraSelect);
            LensSelectCommand = new Command(LensSelect);
            ViewWidthChangedCommand = new Command(FinalCalcForWidth);
            ViewHeightChangedCommand = new Command(FinalCalcForHeight);
            ViewTimesChangedCommand = new Command(FinalCalcForTimes);
            ShowDetailCommand = new Command(ShowCalcDetail);
        }

        private void CameraSelect(object obj)
        {
            SelectCamera = QueryData<CameraData>($"SELECT * FROM camera where Model=\"{CameraModel}\"", CameraModel, "相机");

            ShowDetial();
        }

        /// <summary>
        /// 查询单个数据
        /// </summary>
        /// <typeparam name="T">相机或镜头</typeparam>
        /// <param name="sql">查询语句</param>
        /// <param name="model">型号</param>
        /// <param name="str">相机或镜头</param>
        private T QueryData<T>(string sql, string model, string str) where T : class, new()
        {
            if (string.IsNullOrEmpty(model))
            {
                ErrorMessage = $"{str}型号不能为空！";
                return null;
            }

            var data = App.Database.QueryData<T>(sql);
            if (data.Count <= 0)
            {
                ErrorMessage = $"未找到型号为：\"{model}\" 的{str}";
                return null;
            }

            ErrorMessage = string.Empty;
            return data[0];
        }
        private void LensSelect(object obj)
        {
            SelectLens = QueryData<LensData>($"SELECT * FROM lens where Model=\"{LensModel}\"", LensModel, "镜头");

            ShowDetial();
        }

        private void ShowDetial()
        {
            if (SelectLens != null && SelectCamera != null)
            {
                CalcChipSize(SelectCamera);

                _fLength = double.Parse(SelectLens.FocalLength);

                //水平视场角 = 2 * arctan(w / 2f)
                //垂直视场角 = 2 * arctan(h / 2f)
                double arc = Math.Atan(ChipWidth / (2 * _fLength));
                double angle = arc / Math.PI * 180;
                HAngle = Math.Round(2 * angle, 2).ToString() + "°";

                arc = Math.Atan(ChipHeight / (2 * _fLength));
                angle = arc / Math.PI * 180;
                VAngle = Math.Round(2 * angle, 2).ToString() + "°";
                App.Database.SelectCamera = SelectCamera;
                App.Database.SelectLens = SelectLens;
            }
        }

        private void CalcChipSize(CameraData cameraData)
        {
            if (string.IsNullOrEmpty(cameraData.ChipHeight))
            {
                int pixelHeight = int.Parse(cameraData.PixelHeight);
                int pixelWidth = int.Parse(cameraData.PixelWidth);
                _pixelSize = double.Parse(cameraData.PixelSize);
                ChipWidth = pixelWidth * _pixelSize / 1000;
                ChipHeight = pixelHeight * _pixelSize / 1000;
                cameraData.ChipWidth = ChipWidth.ToString();
                cameraData.ChipHeight = ChipHeight.ToString();
            }
            else
            {
                _pixelSize = double.Parse(cameraData.PixelSize);
                ChipWidth = double.Parse(cameraData.ChipWidth);
                ChipHeight = double.Parse(cameraData.ChipHeight);
            }
        }

        public void FinalCalcForWidth(object obj)
        {
            double width = double.Parse(ViewCalcWidth);
            double times = ChipWidth / width;
            double height = ChipHeight / times;
            double focalLength = _fLength / times;

            ViewCalcTimes = Math.Round(times, 3).ToString();
            ViewCalcHeight = Math.Round(height, 2).ToString();
            WorkingDistance = (int)Math.Floor(focalLength);
            PixelAccuracy = Math.Round(_pixelSize / times / 1000, 3);
            EnableDetail = true;
            UpdateCalcDetail();
        }

        public void FinalCalcForHeight(object obj)
        {
            double height = double.Parse(ViewCalcHeight);
            double times = ChipHeight / height;
            double width = ChipWidth / times;
            double focalLength = _fLength / times;

            ViewCalcTimes = Math.Round(times, 3).ToString();
            ViewCalcWidth = Math.Round(width, 2).ToString();
            WorkingDistance = (int)Math.Floor(focalLength);
            EnableDetail = true;
            PixelAccuracy = Math.Round(_pixelSize / times / 1000, 3);
            UpdateCalcDetail();
        }

        public void FinalCalcForTimes(object obj)
        {
            double times = double.Parse(ViewCalcTimes);
            double width = ChipWidth / times;
            double height = ChipHeight / times;
            double focalLength = _fLength / times;

            ViewCalcWidth = Math.Round(width, 2).ToString();
            ViewCalcHeight = Math.Round(height, 2).ToString();
            WorkingDistance = (int)Math.Floor(focalLength);
            EnableDetail = true;
            PixelAccuracy = Math.Round(_pixelSize / times / 1000, 3);
            UpdateCalcDetail();
        }

        private void UpdateCalcDetail()
        {
            double viewCalcTimes = Math.Round(double.Parse(ViewCalcTimes), 3);
            int ringLength = (int)Math.Round((viewCalcTimes - double.Parse(SelectLens.FocalLength) / double.Parse(SelectLens.WorkingDistance)) *
                            double.Parse(SelectLens.FocalLength));
            App.Database.CalcDetail = new FinalCalcData
            {
                PixelAccuracy = PixelAccuracy,
                ViewCalcTimes = viewCalcTimes,
                WorkingDistance = WorkingDistance,
                ViewCalcHeight = double.Parse(ViewCalcHeight),
                ViewCalcWidth = double.Parse(ViewCalcWidth),
                RingLength = ringLength
            };
        }

        private async void ShowCalcDetail()
        {
            await Shell.Current.GoToAsync(nameof(CalcDetailPage));
        }
    }
}
