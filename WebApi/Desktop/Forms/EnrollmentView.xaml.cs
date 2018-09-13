using DPCtlUruNet;
using DPUruNet;
using DPXUru;
using FirstFloor.ModernUI.Windows.Controls;
using Library.DataModels;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
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
using System.Windows.Threading;

namespace Desktop.Forms
{
    /// <summary>
    /// Interaction logic for EnrollmentView.xaml
    /// </summary>
    public partial class EnrollmentView : ModernWindow
    {
        public EnrollmentView(Library.DataModels.pegawai selectedItem)
        {
            InitializeComponent();
            this.Pegawai = selectedItem;
            enrollpage.BackColor = System.Drawing.SystemColors.Window;
            enrollpage.Location = new System.Drawing.Point(3, 3);
            //   enrollpage.Name = "ctlEnrollmentControl";
            enrollpage.Size = new System.Drawing.Size(482, 346);
            enrollpage.TabIndex = 0;
            enrollpage.OnCancel += Enrollpage_OnCancel;
            enrollpage.OnDelete += Enrollpage_OnDelete;
            // enrollpage.OnEnroll += Enrollpage_OnEnroll;
            enrollpage.On_Enroll += Enrollpage_On_Enroll;
            enrollpage.OnStartEnroll += Enrollpage_OnStartEnroll;
            enrollpage.On_StartEnroll += Enrollpage_On_StartEnroll;
            enrollpage.On_Captured += Enrollpage_On_Captured;
            this.Finger = ResourcesBase.GetFinger();
            this.DataContext = this;
            this.Loaded += MainWindow_Loaded;
        }

        private void Enrollpage_On_Enroll(DPCtlXUru.EnrollmentXControl enrollmentControl, XFmdResult result, int fingerPosition)
        {
           // Helper.Fmds.Add(fingerPosition, result.Fmd.Fmd);//Helper.Source.Add(result.Fmd.Fmd.Bytes);
          var fmdxml= Fmd.SerializeXml(result.Fmd.Fmd);
            var main = ResourcesBase.GetMainWindowViewModel();
            Pegawai.Enrollment = fmdxml;
            var res = main.PegawaiCollection.Updated(Pegawai);
            SendMessage(ActionMessage.SendMessage, "Enroll Complete");
        }


        private void Enrollpage_On_Captured(DPCtlXUru.EnrollmentXControl enrollmentControl, DPXUru.XCaptureResult captureResult, int fingerPosition)
        {

            if (enrollmentControl.Reader != null)
            {
                SendMessage(ActionMessage.SendMessage, "OnCaptured:  " + enrollmentControl.Reader.Description.Name + ", finger " + fingerPosition + ", quality " + captureResult.Quality.ToString());
            }
            else
            {
                SendMessage(ActionMessage.SendMessage, "OnCaptured:  No Reader Connected, finger " + fingerPosition);
            }

            if (captureResult.ResultCode != Constants.ResultCode.DP_SUCCESS.ToString())
            {
                if (Finger.CurrentReader != null)
                {
                    Finger.CurrentReader.Dispose();
                    Finger.CurrentReader = null;
                }

                // Disconnect reader from enrollment control
                enrollpage.Reader = null;

                MessageBox.Show("Error:  " + captureResult.ResultCode);
                //       btnCancel.Enabled = false;
            }
            else
            {
                if (captureResult.Fid.Fivs != null)
                {
                    foreach (XFid.XFiv fiv in captureResult.Fid.Fivs)
                    {

                        Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
                        {
                            pbFingerprint.Source = ToBitmapImage(Finger.CreateBitmap(fiv.Fiv.RawImage, fiv.Fiv.Width, fiv.Fiv.Height));

                        }));
                    }
                }
            }
            SendMessage(ActionMessage.SendMessage, "Start");
        }

        private void Enrollpage_On_StartEnroll(DPCtlXUru.EnrollmentXControl enrollmentControl, string result, int fingerPosition)
        {
            SendMessage(ActionMessage.SendMessage, "Start e");
        }

        private void Enrollpage_OnStartEnroll(EnrollmentControl enrollmentControl, Constants.ResultCode result, int fingerPosition)
        {
            SendMessage(ActionMessage.SendMessage, "Start 2");
        }

        private void Enrollpage_OnEnroll(EnrollmentControl enrollmentControl, DataResult<Fmd> enrollmentResult, int fingerPosition)
        {
            SendMessage(ActionMessage.SendMessage, "Start 3");
        }

        private void Enrollpage_OnDelete(EnrollmentControl enrollmentControl, Constants.ResultCode result, int fingerPosition)
        {
            SendMessage(ActionMessage.SendMessage, "Delete");
        }

        private void Enrollpage_OnCaptured(EnrollmentControl enrollmentControl, CaptureResult captureResult, int fingerPosition)
        {

            SendMessage(ActionMessage.SendMessage, "Capture");
        }

        private void Enrollpage_OnCancel(EnrollmentControl enrollmentControl, Constants.ResultCode result, int fingerPosition)
        {
            SendMessage(ActionMessage.SendMessage, "Cancel");
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            enrollpage.Reader = Finger.CurrentReader;
            // var result=  enrollpage.Reader.Open(Constants.CapturePriority.DP_PRIORITY_COOPERATIVE);
            //var rescap= enrollpage.Reader.CaptureAsync(Constants.Formats.Fid.ANSI, Constants.CaptureProcessing.DP_IMG_PROC_DEFAULT, Finger.CurrentReader.Capabilities.Resolutions[0]);
            //   var open= Finger.OpenReader();
            //var cap= Finger.StartCaptureAsync(OnCaptured);

            //new DPCtlUruNet.EnrollmentControl(Finger.CurrentReader, Constants.CapturePriority.DP_PRIORITY_COOPERATIVE);

            // enrollpage.Reader.On_Captured += Reader_On_Captured;
            //s     enrollpage.Reader.CaptureAsync(Constants.Formats.Fid.ANSI, Constants.CaptureProcessing.DP_IMG_PROC_DEFAULT, 100);
            //enrollpage.Controls.Add(_enrollmentControl);
        }



        private enum ActionMessage
        {
            SendMessage
        }
        private delegate void SendMessageCallback(ActionMessage action, string payload);
        private void SendMessage(ActionMessage action, string payload)
        {
            try
            {
                switch (action)
                {
                    case ActionMessage.SendMessage:
                        Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
                        {
                           // txtEnroll.Text += payload + "\r\n\r\n";

                        }));

                        break;
                }
            }
            catch (Exception)
            {
            }
        }

        public BitmapImage ToBitmapImage(Bitmap bitmap)
        {
            using (var memory = new MemoryStream())
            {
                bitmap.Save(memory, ImageFormat.Png);
                memory.Position = 0;

                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                bitmapImage.Freeze();

                return bitmapImage;
            }
        }

        
        public pegawai Pegawai { get; set; }
        public FingerPrint Finger { get; }
    }
}
