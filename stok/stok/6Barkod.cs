using AForge.Video.DirectShow;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZXing;

namespace stok_programi
{
    public partial class _6Barkod : Form
    {
        public _6Barkod()
        {
            InitializeComponent();
        }
        //FilterInfoCollection ve VideoCaptureDevice sınıfından nesnelerimi türettim. FilterInfoCollection cihazımdaki tüm kameraları, yakalama cihazlarını vs bulur. VideoCaptureDevice ise benim kullanacağım kamera için değişkenim olacak.
        FilterInfoCollection Cihazlar;
        VideoCaptureDevice kameram;
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void _6Barkod_Load(object sender, EventArgs e)
        {

            Cihazlar = new FilterInfoCollection(FilterCategory.VideoInputDevice);

            //FilterInfo cihazdaki görüntü yakalama cihazları hakkında bilgi tutar.
            foreach (FilterInfo cihaz in Cihazlar)
            {
                cmbKamera.Items.Add(cihaz.Name);
            }
            //İlk bulduğu kamera ismi görünsün diye ilk atamayı yaptık, 0 verdik.
            cmbKamera.SelectedIndex = 0;
        }

        private void btnBasla_Click(object sender, EventArgs e)
        {

            kameram = new VideoCaptureDevice(Cihazlar[cmbKamera.SelectedIndex].MonikerString);

            kameram.NewFrame += VideoCaptureDevice_NewFrame;
            kameram.Start();

        }

        private void VideoCaptureDevice_NewFrame(object sender, AForge.Video.NewFrameEventArgs eventArgs)
        {
            Bitmap GoruntulenenBarkod = (Bitmap)eventArgs.Frame.Clone();
            BarcodeReader okuyucu = new BarcodeReader();
            var sonuc = okuyucu.Decode(GoruntulenenBarkod);

            if (sonuc != null)
            {
                txtBarcode.Invoke(new MethodInvoker(delegate ()
                {
                    txtBarcode.Text = sonuc.ToString();
                }
                ));
            }

            pictureBox1.Image = GoruntulenenBarkod;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void cmbKamera_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(kameram != null)
            {
                if (kameram.IsRunning)
                {
                    kameram.Stop();
                    this.Hide();
                }
            }
           
        }
    }
}
