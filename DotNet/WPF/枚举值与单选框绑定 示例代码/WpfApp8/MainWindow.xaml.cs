using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
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

namespace WpfApp8
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        
        public Week Week
        {
            get => m_Week;
            set
            {
                m_Week = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Week)));
            }
        }
        private Week m_Week;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            Week = Week.未知;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(Week.ToString());
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            Week = Week.星期天;
        }
    }

    public enum Week
    {
        星期一 = 1,
        星期二,
        星期三,
        星期四,
        星期五,
        星期六,
        星期天,

        未知 = -0x9999,
    }

    public class WeekConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || value.GetType() != typeof(Week))
            {
                return false;
            }
            string tag = parameter as string;
            if (string.IsNullOrWhiteSpace(tag))
            {
                return false;
            }

            Week week = (Week)value;
            return week.ToString() == tag;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || value.GetType() != typeof(bool))
            {
                return Week.未知;
            }

            bool val = (bool)value;
            if (!val)
            {
                return Week.未知;
            }

            string tag = parameter.ToString();
            if (string.IsNullOrWhiteSpace(tag))
            {
                return Week.未知;
            }

            return Enum.Parse(typeof(Week), tag);
        }
    }

}
