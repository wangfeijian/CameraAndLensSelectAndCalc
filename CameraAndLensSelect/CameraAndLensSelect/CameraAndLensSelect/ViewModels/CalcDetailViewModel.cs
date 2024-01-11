using CameraAndLensSelect.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace CameraAndLensSelect.ViewModels
{
    public class CalcDetailViewModel : BaseViewModel
    {
        private CameraData _selectCamera;
        public CameraData SelectCamera
        {
            get { return _selectCamera; }
            set { SetProperty(ref _selectCamera, value); }
        }

        private LensData _lensData;
        public LensData SelectLens
        {
            get { return _lensData; }
            set { SetProperty(ref _lensData, value); }
        }

        private FinalCalcData _finalCalcData;
        public FinalCalcData CalcDetail
        {
            get { return _finalCalcData; }
            set { SetProperty(ref _finalCalcData, value); }
        }

        private double _displaySize = 17;

        public double DisplaySize
        {
            get { return _displaySize; }
            set { SetProperty(ref _displaySize, value); }
        }

        private double _defaultApet = 2.4;

        public double DefaultApet
        {
            get { return _defaultApet; }
            set { SetProperty(ref _defaultApet, value); }
        }


        private double _elecTimes;

        public double ElecTimes
        {
            get { return _elecTimes; }
            set { SetProperty(ref _elecTimes, value); }
        }

        private double _showTimes;

        public double ShowTimes
        {
            get { return _showTimes; }
            set { SetProperty(ref _showTimes, value); }
        }

        private string _dof;

        public string Dof
        {
            get { return _dof; }
            set { SetProperty(ref _dof, value); }
        }

        private string _dofNear;

        public string DofNear
        {
            get { return _dofNear; }
            set { SetProperty(ref _dofNear, value); }
        }

        private string _dofFar;

        public string DofFar
        {
            get { return _dofFar; }
            set { SetProperty(ref _dofFar, value); }
        }

        public ICommand CalcTimesCommand { get; }
        public ICommand CalcDofCommand { get; }
        public CalcDetailViewModel()
        {
            Title = "计算后的结果";
            CalcTimesCommand = new Command(CalcTimes);
            CalcDofCommand = new Command(CalcDof);
        }

        public void CalcTimes()
        {
            double chipWidth = double.Parse(SelectCamera.ChipWidth);
            double chipHeight = double.Parse(SelectCamera.ChipHeight);
            double temp = DisplaySize * 25.4 / Math.Sqrt(chipHeight * chipHeight + chipWidth * chipWidth);
            ElecTimes = Math.Round(temp, 3);
            ShowTimes = Math.Round(ElecTimes * CalcDetail.ViewCalcTimes, 3);
        }

        public void CalcDof()
        {
            //超焦点位置 =（焦距* 焦距）/（光圈大小* 弥散圆直径（工业上取0.04））+焦距
            double focalLength = double.Parse(SelectLens.FocalLength);
            double superFocalStart = (focalLength * focalLength) / (DefaultApet * 0.04) + focalLength;
            //近端距离 = （（超焦点位置 - 焦距）*物距）/ （超焦点位置 + 物距 - （2 * 焦距））
            double nearDis = ((superFocalStart - focalLength) * CalcDetail.WorkingDistance) / (superFocalStart + CalcDetail.WorkingDistance - 2 * focalLength);
            //远端距离 = （（超焦点位置 - 焦距）*物距）/ （超焦点位置 - 物距 ）
            double farDis = ((superFocalStart - focalLength) * CalcDetail.WorkingDistance) / (superFocalStart - CalcDetail.WorkingDistance);
            //前景深 = 物距 - 近端距离
            double forward = CalcDetail.WorkingDistance - nearDis;
            //后景深 = 远端距离 - 物距
            double far = farDis - CalcDetail.WorkingDistance;
            //总景深 = 远端距离 - 近端距离
            double total = forward + far;

            DofNear = $"{Math.Round(forward, 3)} mm";
            DofFar = far > 1000 ? "无穷大" : $"{Math.Round(far, 3)} mm";
            Dof = total > 1000 ? "无穷大" : $"{Math.Round(total, 3)} mm";
        }
    }
}
