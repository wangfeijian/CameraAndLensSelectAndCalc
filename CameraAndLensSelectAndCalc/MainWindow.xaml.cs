using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CameraAndLensSelectAndCalc
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int _index;
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
        }

        private void GetDataForLens()
        {
            var viewModel = DataContext as MainWindowViewModel;
            if (viewModel != null)
                viewModel.LensChange();
        }

        private void UpdateSelectLens(string value)
        {
            var viewModel = DataContext as MainWindowViewModel;
            if (viewModel != null)
                viewModel.LoadSelectLens(value);
        }

        private void LensChanged(object sender, SelectionChangedEventArgs e)
        {
            GetDataForLens();
        }

        private void CameraModelChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_index < 2)
            {
                _index++;
                return;
            }

            GetDataForLens();
        }

        private void FinalCalcKeyDown(object sender, KeyEventArgs e)
        {
            var textBox = sender as TextBox;

            if (e.Key != Key.Enter)
            {
                return;
            }

            double temp;
            bool b = double.TryParse(textBox.Text, out temp);

            if(!b)
            {
                MessageBox.Show("请输入合法的数字！！","错误",MessageBoxButton.OK,MessageBoxImage.Error);
                return;
            }

            switch (textBox.Tag)
            {
                case "1":
                    CalcForWidth(textBox);
                    break;
                case "2":
                    CalcForHeight(textBox);
                    break;
                default:
                    CalcForTimes(textBox);
                    break;
            }
        }

        private void FinalCalcLostFocus(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;

            double temp;
            bool b = double.TryParse(textBox.Text, out temp);

            if (!b)
            {
                MessageBox.Show("请输入合法的数字！！", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                return ;
            }

            switch (textBox.Tag)
            {
                case "1":
                    CalcForWidth(textBox);
                    break;
                case "2":
                    CalcForHeight(textBox);
                    break;
                default:
                    CalcForTimes(textBox);
                    break;
            }
        }

        private void CalcForWidth(TextBox textBox)
        {
            var viewModel = DataContext as MainWindowViewModel;
            if (viewModel != null)
                viewModel.FinalCalcForWidth(textBox.Text);
        }

        private void CalcForHeight(TextBox textBox)
        {
            var viewModel = DataContext as MainWindowViewModel;
            if (viewModel != null)
                viewModel.FinalCalcForHeight(textBox.Text);
        }

        private void CalcForTimes(TextBox textBox)
        {
            var viewModel = DataContext as MainWindowViewModel;
            if (viewModel != null)
                viewModel.FinalCalcForTimes(textBox.Text);
        }

        private void LensFocalLengthUpdate(object sender, KeyEventArgs e)
        {
            var textBox = sender as TextBox;

            if (e.Key != Key.Enter)
            {
                return;
            }

            int temp;
            bool b = int.TryParse(textBox.Text, out temp);

            if (!b)
            {
                MessageBox.Show("请输入合法的数字！！", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            UpdateSelectLens(textBox.Text);
        }

        private void LensFocalLengthLostFocus(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;

            int temp;
            bool b = int.TryParse(textBox.Text, out temp);

            if (!b)
            {
                MessageBox.Show("请输入合法的数字！！", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                return ;
            }

            UpdateSelectLens(textBox.Text);
        }

        private void LensFilter(object sender, TextChangedEventArgs e)
        {
            var cvs = CollectionViewSource.GetDefaultView(LensDataGrid.ItemsSource);
            if (cvs != null && cvs.CanFilter)
            {
                cvs.Filter = OnLensFilterApplied;
            }

        }

        private bool OnLensFilterApplied(object obj)
        {
            if (obj is LensData)
            {
                var text = LensFilterTextBox.Text.ToLower();
                return (obj as LensData).Vendors.ToLower().Contains(text)
                    || (obj as LensData).Model.ToLower().Contains(text);
            }
            return false;
        }
        private void CameraFilter(object sender, TextChangedEventArgs e)
        {
            var cvs = CollectionViewSource.GetDefaultView(CameraDataGrid.ItemsSource);
            if (cvs != null && cvs.CanFilter)
            {
                cvs.Filter = OnCameraFilterApplied;
            }
        }

        private bool OnCameraFilterApplied(object obj)
        {
            if (obj is CameraData)
            {
                var text = CameraFilterTextBox.Text.ToLower();
                return (obj as CameraData).Vendors.ToLower().Contains(text)
                    || (obj as CameraData).Model.ToLower().Contains(text)
                    || (obj as CameraData).Frame.ToLower().Contains(text)
                    || (obj as CameraData).Pixels.ToString().ToLower().Contains(text);
            }
            return false;
        }

        private void SendEmail(object sender, RoutedEventArgs e)
        {
            Link.OpenInBrowser("mailto://wangfeijianhao@163.com");
        }

        private void OpenMyGithub(object sender, RoutedEventArgs e)
        {
            Link.OpenInBrowser("https://github.com/wangfeijian");
        }
    }

    public static class Link
    {
        public static void OpenInBrowser(string url)
        {
            //https://github.com/dotnet/corefx/issues/10361
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                url = url.Replace("&", "^&");
                Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
            }
        }
    }
}
