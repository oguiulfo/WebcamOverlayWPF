using AForge.Video;
using AForge.Video.DirectShow;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WebcamUserControl;

namespace WebcamUserControl
{
    public partial class VideoPortControl : UserControl
    {
        public VideoPortControl()
        {
            InitializeComponent();

            var videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);

            foreach (var d in videoDevices)
                VideoDevicesComboBox.Items.Add(d);

            VideoDevicesComboBox.SelectedIndex = 0;
        }

        public string OverlayImagePath { get; set; }

        /// <summary>
        /// Displays webcam video and asks for image to overlay
        /// </summary>
        public void StartVideoFeed()
        {
            var openFileDialog = new OpenFileDialog()
            {
                DefaultExt = "png",
                Filter = "PNG Image | *.png",
                Title = "Please select an image to overlay onto the video feed..."
            };

            if (openFileDialog.ShowDialog() == true)
                OverlayImagePath = openFileDialog.FileName;

            var device = (FilterInfo)VideoDevicesComboBox.SelectedItem;
            if (device != null)
            {
                var source = new VideoCaptureDevice(device.MonikerString);
                // register NewFrame event handler
                source.NewFrame += new NewFrameEventHandler(video_NewFrame); 

                videoSourcePlayer.VideoSource = source;
                videoSourcePlayer.Start();
            }
        }

        /// <summary>
        /// Stops the display of webcam video.
        /// </summary>
        public void StopVideoFeed()
        {
            videoSourcePlayer.SignalToStop();
        }

        /// <summary>
        /// Saves a snapshot of current webcam video frame.
        /// </summary>
        public void SaveSnapshot()
        {
            using (Bitmap bmp = videoSourcePlayer.GetCurrentVideoFrame())
            {
                var saveFileDialog = new SaveFileDialog()
                {
                    Filter = "PNG Image | *.png",
                    DefaultExt = "png"
                };

                if (saveFileDialog.ShowDialog() == true)
                    bmp.Save(saveFileDialog.FileName, System.Drawing.Imaging.ImageFormat.Png);
            }
        }

        private void video_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            if (!string.IsNullOrWhiteSpace(OverlayImagePath))
            {
                var frame = eventArgs.Frame; // reference to the current frame   
                var g = Graphics.FromImage(frame);

                using (System.Drawing.Image backImage = (Bitmap)frame.Clone())
                using (System.Drawing.Image frontImage = System.Drawing.Image.FromFile(OverlayImagePath))
                using (System.Drawing.Image newImage = new Bitmap(backImage.Width, backImage.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb))
                {
                    using (Graphics compositeGraphics = Graphics.FromImage(newImage))
                    {
                        compositeGraphics.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
                        compositeGraphics.DrawImageUnscaled(backImage, 0, 0);
                        compositeGraphics.DrawImageUnscaled(frontImage, 250, 350); // TODO: make positioning dynamic or configurable
                        compositeGraphics.Dispose();
                        frontImage.Dispose();
                        backImage.Dispose();
                    }

                    g.DrawImage(newImage, new PointF(0, 0));
                    g.Dispose();
                }
            }
        }
    }
}