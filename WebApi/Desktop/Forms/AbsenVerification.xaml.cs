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
    /// Interaction logic for AbsenVerification.xaml
    /// </summary>
    public partial class AbsenVerification : ModernWindow
    {
        private const int DPFJ_PROBABILITY_ONE = 0x7fffffff;
        public Reader CurrentReader { get; }
        public pegawai Pegawai { get; set; }
        private FingerPrint Finger { get; set; }
        public AbsenVerification(pegawai selectedPegawai)
        {
            InitializeComponent();
             Pegawai = selectedPegawai;
            Finger = ResourcesBase.GetFinger();
            this.DataContext = this;
            this.Loaded += AbsenVerification_Loaded;
        }

        private void AbsenVerification_Loaded(object sender, RoutedEventArgs e)
        {
           // verification = new DPCtlXUru.IdentificationXControl();
           if(verification.Reader==null)
            verification.Reader = Finger.CurrentReader;
            List<Fmd> list = new List<Fmd>();
            list.Add(Fmd.DeserializeXml(Pegawai.Enrollment));
            verification.Fmds = null;
            verification.Fmds = list;
            verification.CapturePriority = Constants.CapturePriority.DP_PRIORITY_COOPERATIVE;
            verification.ThresholdScore = DPFJ_PROBABILITY_ONE * 1 / 100000;
            verification.Location = new System.Drawing.Point(3, 3);
            //verification.Name = "identificationControl";
            verification.Size = new System.Drawing.Size(397, 128);
            verification.TabIndex = 0;
            verification.MaximumResult = 10;
            verification.On_Identify += Verification_On_Identify;
            verification.OnIdentify += Verification_OnIdentify;
            verification.Refresh();
           verification.StartIdentification();
           
         
        }

        private async void Verification_OnIdentify(IdentificationControl IdentificationControl, IdentifyResult IdentificationResult)
        {
            if (IdentificationResult.Indexes!=null && IdentificationResult.Indexes.Count()>0)
            {
                await Task.Delay(6000);
                var ab = new absen { PengaturanId = 1, PegawaiId = Pegawai.Id, Tanggal = DateTime.Now, JamMasuk = DateTime.Now.TimeOfDay };
                var absenCollection = ResourcesBase.GetMainWindowViewModel().AbsenCollection;
                absenCollection.OnChangeSource += AbsenCollection_OnChangeSource;
                absenCollection.Add(ab);
             
        
            this.Close();
            }
        }

        private void Verification_On_Identify(DPCtlXUru.IdentificationXControl IdentificationControl, DPXUru.XIdentifyResult result)
        {
           
        }

        private void AbsenCollection_OnChangeSource(absen obj)
        {
          var  MainCollection = ResourcesBase.GetMainWindowViewModel().AbsenTodayCollection;
            Pegawai.AbsenToday = obj;
            MainCollection.SourceView.Refresh();
        }
    }
}