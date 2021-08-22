using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MessagingToolkit.QRCode;
using MessagingToolkit.QRCode.Codec; 

namespace stok_programi
{
    public partial class qrCode : Form
    {
        public SqlConnection bag = new SqlConnection(@"Data Source=.;Initial Catalog=data; User Id=atakn; password=Atakan!!;");

        public qrCode()
        {
            InitializeComponent();
        }

        public DataTable tablo = new DataTable();
        public SqlDataAdapter adtr = new SqlDataAdapter();
        public SqlCommand kmt = new SqlCommand();

        protected void listele()
        {//grid yazdırma
            tablo.Clear();
            bag.Open();
            string systemUser = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            SqlDataAdapter adtr = new SqlDataAdapter("select stokAdi,stokModeli,stokSeriNo,stokAdedi,stokTarih,kayitYapan,urunOzellik,systemUser,alisFiyat,satisFiyat From stokbil ORDER BY id DESC ", bag);
            adtr.Fill(tablo); // dosya doldur
            dataGridView1.DataSource = tablo;
            adtr.Dispose();
            bag.Close();
            try
            {
                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                //satırları seç

                dataGridView1.Columns[0].HeaderText = "PARÇA ADI";
                dataGridView1.Columns[1].HeaderText = "PARÇA MODELİ";
                dataGridView1.Columns[2].HeaderText = "PARÇA SERİNO";
                dataGridView1.Columns[3].HeaderText = "PARÇA ADEDİ";
                dataGridView1.Columns[4].HeaderText = "PARÇA TARİH";
                dataGridView1.Columns[5].HeaderText = "KAYIT YAPAN";
                dataGridView1.Columns[6].HeaderText = "PARÇA ÖZELLİKLERİ";
                dataGridView1.Columns[7].HeaderText = "KAYIT YAPILAN MAKİNA";
                dataGridView1.Columns[8].HeaderText = "PARÇA FİYATI ($)";
                dataGridView1.Columns[9].HeaderText = "PARÇA FİYATI ($)";
            }
            catch (Exception)
            {
                MessageBox.Show("Veriler eklenemedi.Lütfen daha sonra tekrar deneyiniz!", "Bilgi", MessageBoxButtons.OK);
            }
        }


        QRCodeEncoder code = new QRCodeEncoder();
        Image rsm;

        private void button1_Click(object sender, EventArgs e)
        {
            rsm = code.Encode(textBox1.Text);
            pictureBox1.Image = rsm;

        }

        private void button2_Click(object sender, EventArgs e)
        {

            
               SaveFileDialog sfd = new SaveFileDialog();//yeni bir kaydetme diyaloğu oluşturuyoruz.
               sfd.Filter = "jpeg dosyası(*.jpg)|*.jpg|Bitmap(*.bmp)|*.bmp";//.bmp veya .jpg olarak kayıt imkanı sağlıyoruz.
               sfd.Title = "Kayıt";//diğaloğumuzun başlığını belirliyoruz.
               sfd.FileName = "resim";//kaydedilen resmimizin adını 'resim' olarak belirliyoruz.

               DialogResult sonuç = sfd.ShowDialog();

               if (sonuç == DialogResult.OK)
               {
                   pictureBox1.Image.Save(sfd.FileName);//Böylelikle resmi istediğimiz yere kaydediyoruz.
                   MessageBox.Show("KAYDETME İŞLEMİ BAŞARILI");
                }
            else
            {
                MessageBox.Show("KAYDETME İŞLEMİ BAŞARISIZ");
            }
              
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            listele();

            dataGridView1.BorderStyle = BorderStyle.None;

            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);

            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;

            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;

            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;

            dataGridView1.BackgroundColor = Color.White;



            dataGridView1.EnableHeadersVisualStyles = false;

            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);

            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
           
        }
    }
}
