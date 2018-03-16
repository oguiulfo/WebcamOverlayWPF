using System.Windows;

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
            WebcamCtrl.StartVideoFeed(xInput.Text, yInput.Text);
        }

        private void StopVideoFeed_Click(object sender, RoutedEventArgs e)
        {
            WebcamCtrl.StopVideoFeed();
        }

        private void XCoord_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (xInput.Text.Length > 1 && yInput.Text.Length > 1)
                WebcamCtrl.StartVideoFeed(xInput.Text, yInput.Text);
        }

        private void YCoord_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (xInput.Text.Length > 1 && yInput.Text.Length > 1)
                WebcamCtrl.StartVideoFeed(xInput.Text, yInput.Text);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            WebcamCtrl.StopVideoFeed();
        }
    }
}