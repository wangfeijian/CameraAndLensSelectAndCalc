using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace CameraAndLensSelectAndCalc
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private double _accuracy;

        public double Accuracy
        {
            get { return _accuracy; }
            set { _accuracy = value; NotifyPropertyChange(nameof(Accuracy)); }
        }

        private double _viewWidth;

        public double ViewWidth
        {
            get { return _viewWidth; }
            set { _viewWidth = value; NotifyPropertyChange(nameof(ViewWidth)); }
        }

        private double _viewHeight;

        public double ViewHeight
        {
            get { return _viewHeight; }
            set { _viewHeight = value; NotifyPropertyChange(nameof(ViewHeight)); }
        }

        private int _safetyFactor = 5;

        public int SafetyFactor
        {
            get { return _safetyFactor; }
            set { _safetyFactor = value; NotifyPropertyChange(nameof(SafetyFactor)); }
        }

        private bool _enableSafetyFactor = true;

        public bool EnableSafetyFactor
        {
            get { return _enableSafetyFactor; }
            set { _enableSafetyFactor = value; NotifyPropertyChange(nameof(EnableSafetyFactor)); }
        }

        private ObservableCollection<CameraShowData>? _roughCameraList;

        public ObservableCollection<CameraShowData>? RoughCameraList
        {
            get { return _roughCameraList; }
            set { _roughCameraList = value; NotifyPropertyChange(nameof(RoughCameraList)); }
        }

        private List<CameraData>? _allCameraData;

        public List<CameraData>? AllCameraData
        {
            get { return _allCameraData; }
            set { _allCameraData = value; NotifyPropertyChange(nameof(AllCameraData)); }
        }

        private List<LensData>? _allLensData;

        public List<LensData>? AllLensData
        {
            get { return _allLensData; }
            set { _allLensData = value; NotifyPropertyChange(nameof(AllLensData)); }
        }

        private List<string>? _selectCameraList;

        public List<string>? SelectCameraList
        {
            get { return _selectCameraList; }
            set { _selectCameraList = value; NotifyPropertyChange(nameof(SelectCameraList)); }
        }

        private string? _selectCamera;

        public string? SelectCamera
        {
            get { return _selectCamera; }
            set { _selectCamera = value; NotifyPropertyChange(nameof(SelectCamera)); }
        }

        private ObservableCollection<string>? _selectLensList;

        public ObservableCollection<string>? SelectLensList
        {
            get { return _selectLensList; }
            set { _selectLensList = value; NotifyPropertyChange(nameof(SelectLensList)); }
        }

        private string? _selectLens;

        public string? SelectLens
        {
            get { return _selectLens; }
            set { _selectLens = value; NotifyPropertyChange(nameof(SelectLens)); }
        }

        private int _workingDistance;

        public int WorkingDistance
        {
            get { return _workingDistance; }
            set { _workingDistance = value; NotifyPropertyChange(nameof(WorkingDistance)); }
        }

        private string? _focalLength;

        public string? FocalLength
        {
            get { return _focalLength; }
            set { _focalLength = value; NotifyPropertyChange(nameof(FocalLength)); }
        }

        private string? _hAngle;

        public string? HAngle
        {
            get { return _hAngle; }
            set { _hAngle = value; NotifyPropertyChange(nameof(HAngle)); }
        }

        private string? _vAngle;

        public string? VAngle
        {
            get { return _vAngle; }
            set { _vAngle = value; NotifyPropertyChange(nameof(VAngle)); }
        }

        private string? _viewCalcWidth;

        public string? ViewCalcWidth
        {
            get { return _viewCalcWidth; }
            set { _viewCalcWidth = value; NotifyPropertyChange(nameof(ViewCalcWidth)); }
        }

        private string? _viewCalcHeight;

        public string? ViewCalcHeight
        {
            get { return _viewCalcHeight; }
            set { _viewCalcHeight = value; NotifyPropertyChange(nameof(ViewCalcHeight)); }
        }

        private string? _viewCalcTimes;

        public string? ViewCalcTimes
        {
            get { return _viewCalcTimes; }
            set { _viewCalcTimes = value; NotifyPropertyChange(nameof(ViewCalcTimes)); }
        }

        private string? _chipSizeStr;

        public string? ChipSizeStr
        {
            get { return _chipSizeStr; }
            set { _chipSizeStr = value; NotifyPropertyChange(nameof(ChipSizeStr)); }
        }

        private string? _chipLensSizeStr;

        public string? ChipLensSizeStr
        {
            get { return _chipLensSizeStr; }
            set { _chipLensSizeStr = value; NotifyPropertyChange(nameof(ChipLensSizeStr)); }
        }

        private string? _lensDistance;

        public string? LensDistance
        {
            get { return _lensDistance; }
            set { _lensDistance = value; NotifyPropertyChange(nameof(LensDistance)); }
        }


        private CameraData? _cameraSelectData;
        public CameraData? CameraSelectData
        {
            get { return _cameraSelectData; }
            set { _cameraSelectData = value; NotifyPropertyChange(nameof(CameraSelectData)); }
        }

        private LensData? _lensData;
        public LensData? LensData
        {
            get { return _lensData; }
            set { _lensData = value; NotifyPropertyChange(nameof(LensData)); }
        }
        private double _chipWidth, _chipHeight, _pixelSize, _fLength, _chipSize, _lenMatchChip;

        public double ChipWidth
        {
            get { return _chipWidth; }
            set { _chipWidth = value; NotifyPropertyChange(nameof(ChipWidth)); }
        }

        public double ChipHeight
        {
            get { return _chipHeight; }
            set { _chipHeight = value; NotifyPropertyChange(nameof(ChipHeight)); }
        }

        private int _ringLength;

        public int RingLength
        {
            get => _ringLength;
            set { _ringLength = value; NotifyPropertyChange(nameof(RingLength)); }
        }
        private double _pixelAccuracy;

        public double PixelAccuracy
        {
            get { return _pixelAccuracy; }
            set { _pixelAccuracy = value; NotifyPropertyChange(nameof(PixelAccuracy)); }
        }

        private bool _enableDetail;  

        public bool EnableDetail
        {
            get { return _enableDetail; }
            set { _enableDetail = value; NotifyPropertyChange(nameof(EnableDetail)); }
        }

        private int _workingDis;
        public DelegateCommand RoughCalcCommand { get; }
        public DelegateCommand ExportReportCommand { get; }

        public MainWindowViewModel()
        {
            SelectCameraList = new List<string>();
            SelectLensList = new ObservableCollection<string>();
            AllCameraData = SqlHelper.AsyncQuery<CameraData>("SELECT * FROM camera").Result.ToList();
            AllLensData = SqlHelper.AsyncQuery<LensData>("SELECT * FROM lens").Result.ToList();            
            AllCameraData.ForEach((data) => SelectCameraList.Add(data.Model));

            RoughCalcCommand = new DelegateCommand { ActionExecute = RoughCalc };
            ExportReportCommand = new DelegateCommand { ActionExecute = ExportReport };
        }

        private void ExportReport(object obj)
        {
            if (CameraSelectData == null || LensData == null)
            {
                MessageBox.Show("先完成相机镜头的选型计算，再进行导出操作");
                return;
            }

            string dir = "D:/ExportReport";
            string fullFileName = Path.Combine(dir, $"{DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss")}_report.xlsx");

            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            IWorkbook workbook = null; //创建一个新的Excel文件
            ICell cell = null;
            using (FileStream fs = File.Open("Model.xlsx", FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                //把xls文件读入workbook变量里，之后就可以关闭了
                workbook = new XSSFWorkbook(fs);
            }

            ISheet sheet = workbook.GetSheetAt(0); //按名称获取工作表

            if (sheet == null)
            {
                return;
            }

            // 相机型号
            cell = sheet.GetRow(7).GetCell(1);
            cell.SetCellValue(CameraSelectData.Model);

            // 相机品牌
            cell = sheet.GetRow(7).GetCell(4);
            cell.SetCellValue(CameraSelectData.Vendors);

            // 镜头型号
            cell = sheet.GetRow(10).GetCell(1);
            cell.SetCellValue(LensData.Model);

            // 镜头品牌
            cell = sheet.GetRow(10).GetCell(4);
            cell.SetCellValue(LensData.Vendors);

            // 镜头焦距
            cell = sheet.GetRow(20).GetCell(2);
            cell.SetCellValue(FocalLength);
            
            // 物距
            cell = sheet.GetRow(23).GetCell(2);
            cell.SetCellValue(WorkingDistance);

            // 接圈长度
            cell = sheet.GetRow(24).GetCell(2);
            cell.SetCellValue(RingLength);

            // 视野宽高
            cell = sheet.GetRow(37).GetCell(1);
            cell.SetCellValue(ViewCalcWidth);
            cell = sheet.GetRow(37).GetCell(3);
            cell.SetCellValue(ViewCalcHeight);

            // 分辨率宽高
            cell = sheet.GetRow(39).GetCell(1);
            cell.SetCellValue(CameraSelectData.PixelWidth);
            cell = sheet.GetRow(39).GetCell(3);
            cell.SetCellValue(CameraSelectData.PixelHeight);

            // 芯片尺寸
            cell = sheet.GetRow(41).GetCell(1);
            cell.SetCellValue(ChipWidth);
            cell = sheet.GetRow(41).GetCell(3);
            cell.SetCellValue(ChipHeight);

            // 像元大小
            cell = sheet.GetRow(43).GetCell(1);
            cell.SetCellValue(_pixelSize);

            // 光学放大倍率
            cell = sheet.GetRow(42).GetCell(5);
            cell.SetCellValue(ViewCalcTimes);

            sheet.ForceFormulaRecalculation = true;
            workbook.GetCreationHelper().CreateFormulaEvaluator().EvaluateAll();

            using (FileStream fileStream = File.Open(fullFileName, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                workbook.Write(fileStream);
            }

            MessageBox.Show($"导出成功！！\n文件保存在{fullFileName}中。");
        }
        private void RoughCalc(object obj)
        {
            RoughCameraList = new ObservableCollection<CameraShowData>();

            if (Accuracy <= 0 || ViewWidth <= 0 || ViewHeight <= 0 || SafetyFactor <= 0)
            {
                MessageBox.Show("必须将参数输入完整再进行计算！！");
                return;
            }

            if (SafetyFactor < 3) SafetyFactor = 3;

            if (!EnableSafetyFactor) SafetyFactor = 1;

            // 相机像素 = 安全系数*视场宽*视场高/(设备精度*设备精度)
            int roughResult = (int)Math.Floor(SafetyFactor * ViewHeight * ViewWidth / (Accuracy * Accuracy) / 10000) * 10000;

            int minSafeFactor = SafetyFactor - 2 >= 3 ? SafetyFactor - 2 : 3;

            if (!EnableSafetyFactor) minSafeFactor = 1;
            int minRought = roughResult / SafetyFactor * minSafeFactor;
            int maxRought = roughResult / SafetyFactor * (SafetyFactor + 2);

            string sql = $"SELECT * FROM camera WHERE Pixels >= {minRought} AND Pixels <= {maxRought}";
            var allDatas = SqlHelper.AsyncQuery<CameraData>(sql);

            allDatas.Result.ToList().ForEach(data => RoughCameraList.Add(new CameraShowData
            {
                Vendors = data.Vendors,
                Model = data.Model,
                Interface = data.Interface,
                Shutter = data.Shutter,
                ChipSize = data.ChipSize,
                PixelSize = data.PixelSize,
                Frame = data.Frame,
                Color = data.Color,
                Pixels = $"{data.Pixels / 10000}万像素"
            }));

            MessageBox.Show($"预估相机分辨率完成，请到第二页查看结果。\n根据软件中的相机数据，一共查询到{allDatas.Result.Count}个匹配相机。");
        }

        public void LoadSelectLens(string value)
        {
            FocalLength = value;

            if (string.IsNullOrWhiteSpace(FocalLength))
                return;

            SelectLensList = new ObservableCollection<string>();

            AllLensData.ForEach(data => { if (data.FocalLength == FocalLength) SelectLensList.Add(data.Model); });
        }

        public void LensChange()
        {
            if (string.IsNullOrEmpty(SelectCamera))
            {
                MessageBox.Show("请先确定相机型号！");
                SelectLens = string.Empty;
                return;
            }

            string sql = $"SELECT * FROM camera WHERE Model=\'{SelectCamera}\'";
            CameraSelectData = GetSingleData<CameraData>(sql);
            if (CameraSelectData == null) return;

            ChipSizeStr = CameraSelectData.ChipSize;

            if (CameraSelectData.ChipSize.Contains("\""))
            {
                GetDoubleForStr(CameraSelectData.ChipSize, ref _chipSize);
                //string chip = _cameraData.ChipSize.Split('\"')[0];
                //_chipSize = double.Parse(chip);
            }
            else
            {
                _chipSize = 0;
            }

            if (string.IsNullOrWhiteSpace(SelectLens))
                return;

            sql = $"SELECT * FROM lens WHERE Model=\'{SelectLens}\'";
            LensData = GetSingleData<LensData>(sql);
            if (LensData == null) return;

            ChipLensSizeStr = LensData.MatchingChip;
            LensDistance = LensData.WorkingDistance;

            LensDataGetForStrSplit(LensData);

            FocalLength = LensData.FocalLength;

            CalcChipSize(CameraSelectData);

            _fLength = double.Parse(LensData.FocalLength);

            //水平视场角 = 2 * arctan(w / 2f)
            //垂直视场角 = 2 * arctan(h / 2f)
            double arc = Math.Atan(ChipWidth / (2 * _fLength));
            double angle = arc / Math.PI * 180;
            HAngle = Math.Round(2 * angle, 2).ToString() + "°";

            arc = Math.Atan(ChipHeight / (2 * _fLength));
            angle = arc / Math.PI * 180;
            VAngle = Math.Round(2 * angle, 2).ToString() + "°";

        }

        private void LensDataGetForStrSplit(LensData lensData)
        {
            if (lensData.MatchingChip.Contains("|"))
            {
                string matchChip = lensData.MatchingChip.Split('|')[0].Trim();

                GetDoubleForStr(matchChip, ref _lenMatchChip);
                //string match = matchChip.Split('\"')[0];
                //_lenMatchChip = double.Parse(match);
            }
            else if (lensData.MatchingChip.Contains("\""))
            {
                GetDoubleForStr(lensData.MatchingChip, ref _lenMatchChip);
                //string matchChip = lensData.MatchingChip.Split('\"')[0];
                //_lenMatchChip = double.Parse(matchChip);
            }
            else
            {
                _lenMatchChip = 0;
            }

            if (lensData.WorkingDistance.Contains("-"))
            {
                string dis = lensData.WorkingDistance.Split('-')[0];
                _workingDis = int.Parse(dis);
            }
            else
            {
                _workingDis = int.Parse(lensData.WorkingDistance);
            }
        }

        private void GetDoubleForStr(string value, ref double target)
        {
            string temp = value.Split('\"')[0];
            if (temp.Contains("/"))
            {
                string[] tempArr = temp.Split('/');
                target = double.Parse(tempArr[0]) / double.Parse(tempArr[1]);
            }
            else
            {
                target = double.Parse(temp);
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
            }
            else
            {
                _pixelSize = double.Parse(cameraData.PixelSize);
                ChipWidth = double.Parse(cameraData.ChipWidth);
                ChipHeight = double.Parse(cameraData.ChipHeight);
            }
        }

        public void FinalCalcForWidth(string value)
        {
            if (string.IsNullOrEmpty(value))
                return;

            ViewCalcWidth = value;
            double width = double.Parse(ViewCalcWidth);
            double times = ChipWidth / width;
            double height = ChipHeight / times;
            double focalLength = _fLength / times;

            ViewCalcTimes = Math.Round(times, 6).ToString();
            ViewCalcHeight = Math.Round(height, 2).ToString();
            WorkingDistance = (int)Math.Floor(focalLength);

            PixelAccuracy =  Math.Round(_pixelSize/times/1000, 3);
            EnableDetail = true;
            CheckLensAndCamera();
        }

        public void FinalCalcForHeight(string value)
        {
            if (string.IsNullOrEmpty(value))
                return;

            ViewCalcHeight = value;
            double height = double.Parse(ViewCalcHeight);
            double times = ChipHeight / height;
            double width = ChipWidth / times;
            double focalLength = _fLength / times;

            ViewCalcTimes = Math.Round(times, 6).ToString();
            ViewCalcWidth = Math.Round(width, 2).ToString();
            WorkingDistance = (int)Math.Floor(focalLength);
            EnableDetail = true;
            PixelAccuracy =  Math.Round(_pixelSize/times/1000, 3);
            CheckLensAndCamera();
        }

        public void FinalCalcForTimes(string value)
        {
            if (string.IsNullOrEmpty(value))
                return;

            ViewCalcTimes = value;
            double times = double.Parse(ViewCalcTimes);
            double width = ChipWidth / times;
            double height = ChipHeight / times;
            double focalLength = _fLength / times;

            ViewCalcWidth = Math.Round(width, 2).ToString();
            ViewCalcHeight = Math.Round(height, 2).ToString();
            WorkingDistance = (int)Math.Floor(focalLength);
            EnableDetail = true;
            PixelAccuracy = Math.Round(_pixelSize/times/1000, 3);
            CheckLensAndCamera();
        }

        private void CheckLensAndCamera()
        {
            if (_chipSize == 0 || _lenMatchChip == 0)
            {
                MessageBox.Show("软件数据中缺小相机芯片大小数据或缺小镜头适配芯片数据，请人工确认相机镜头是否匹配！");
                return;
            }

            if (_lenMatchChip < _chipSize)
            {
                MessageBox.Show("镜头适配的芯片大小不适合此相机，请确认相机或镜头型号！");
                return;
            }

            if (_workingDis > WorkingDistance)
            {
                MessageBox.Show("镜头最小工作距离大于核算出来的工作距离，相机和镜头不匹配，请确认相机或镜头型号或者添加接圈！");
                double actualTimes = double.Parse(ViewCalcTimes);
                double minTimes = double.Parse(LensData.FocalLength) / double.Parse(LensData.WorkingDistance);
                double changeTime = actualTimes - minTimes;
                RingLength = (int)Math.Round(changeTime * double.Parse(LensData.FocalLength));
            }
            else
            {
                RingLength = 0;
            }
        }

        private T GetSingleData<T>(string sql) where T : class
        {
            var result = SqlHelper.AsyncQuery<T>(sql);

            if (result.Result.Count <= 0)
            {
                MessageBox.Show($"软件数据中不存在此型号的数据：{SelectLens}");
                return null;
            }

            return result.Result[0];
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyPropertyChange(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

    public class DispatcherHelper
    {
        public static void Delay(int milliSecond)
        {
            int start = Environment.TickCount;
            while (Math.Abs(Environment.TickCount - start) < milliSecond) //毫秒
            {
                DoEvents();
            }
        }

        public static void DoEvents()
        {
            DispatcherFrame frame = new DispatcherFrame();
            Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.Background,
                new DispatcherOperationCallback(ExitFrames), frame);
            try
            {
                Dispatcher.PushFrame(frame);
            }
            catch (InvalidOperationException)
            {

            }
        }

        private static object ExitFrames(object frame)
        {
            ((DispatcherFrame)frame).Continue = false;
            return null;
        }
    }
}
