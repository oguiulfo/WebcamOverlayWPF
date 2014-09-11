using System;
using System.Collections.Generic;
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
using System.IO;
using WebcamUserControl;

namespace CameraWPF
{  
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SaveSnapshotButton_Click(object sender, RoutedEventArgs e)
        {
            WebcamCtrl.SaveSnapshot();
        }

        private void StartVideoFeed_Click(object sender, RoutedEventArgs e)
        {
            WebcamCtrl.StartVideoFeed();
        }

        private void StopVideoFeed_Click(object sender, RoutedEventArgs e)
        {
            WebcamCtrl.StopVideoFeed();
        }
    }
}