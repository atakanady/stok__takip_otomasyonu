using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace stok_programi
{

    public partial class Form1 : Form
    {
        public SqlConnection bag = new SqlConnection(@"Data Source=.;Initial Catalog=data;Integrated Security=True; ");
        public Form1()
        {
            InitializeComponent();
            listele();
            WriteLog("Program çalıştırıldı");

        }

        public DataTable tablo = new DataTable();
        public SqlDataAdapter adtr = new SqlDataAdapter();
        public SqlCommand kmt = new SqlCommand();
        string DosyaYolu, DosyaAdi = "";
        int id;
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
        protected void WriteLog(string islem)
        {
            try
            {
                bag.Open();
                string systemUser = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                SqlCommand kmt = new SqlCommand("INSERT INTO [dbo].[stok_log]([islem_user] ,[islem_tip] ,[islem_zaman]) VALUES ('" + systemUser + "', '" + islem + "', GETDATE())", bag);
                kmt.ExecuteNonQuery();
                bag.Close();

                label10.Text = "Sistem log kaydı başarılı biçimde oluşturuldu. İşlem Yapan Kullanıcı: " + systemUser + " İşlem Tipi: " + islem + " İşlem Zamanı: " + DateTime.Now.ToString();
            }
            catch (Exception)
            {

                MessageBox.Show("Log işlemi yapılırken bir hata meydana geldi. Lütfen sistem yöneticiniz ile görüşünüz.");
            }
        }
        private void Form1_Load(object sender, EventArgs e)
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

        public void programClose()
        {
            DialogResult dialogResult = MessageBox.Show("Programı kapatmak istediğinize emin misiniz? Bilgileriniz kontrol etmeyi unutmayın !", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dialogResult == DialogResult.Yes)
            {
                WriteLog("Program kapatıldı");

                Application.Exit();
            }
            else if (dialogResult == DialogResult.No)
            {
                return;
            }
        }
        private void btnStokEkle_Click(object sender, EventArgs e)
        {

            try
            {
                if (textBox1.Text.Trim() == "") errorProvider1.SetError(textBox1, "Boş geçilmez");
                else errorProvider1.SetError(textBox1, "");
                if (textBox2.Text.Trim() == "") errorProvider1.SetError(textBox2, "Boş geçilmez");
                else errorProvider1.SetError(textBox2, "");
                if (textBox3.Text.Trim() == "") errorProvider1.SetError(textBox3, "Boş geçilmez");
                else errorProvider1.SetError(textBox3, "");
                if (textBox4.Text.Trim() == "") errorProvider1.SetError(textBox4, "Boş geçilmez");
                else errorProvider1.SetError(textBox4, "");
                if (textBox5.Text.Trim() == "") errorProvider1.SetError(textBox5, "Boş geçilmez");
                else errorProvider1.SetError(textBox5, "");
                if (textBox8.Text.Trim() == "") errorProvider1.SetError(textBox8, "Boş geçilmez");
                else errorProvider1.SetError(textBox8, "");
                if (alınan.Text.Trim() == "") errorProvider1.SetError(alınan, "Boş geçilmez");
                else errorProvider1.SetError(alınan, "");
                if (satılan.Text.Trim() == "") errorProvider1.SetError(satılan, "Boş geçilmez");
                else errorProvider1.SetError(satılan, "");

                if (textBox1.Text.Trim() != "" && textBox2.Text.Trim() != "" && textBox3.Text.Trim() != "" && textBox4.Text.Trim() != "" && textBox5.Text.Trim() != "" && textBox8.Text.Trim() != "" && alınan.Text.Trim() != "" && satılan.Text.Trim() != "")
                {
                    bag.Open();
                    string systemUser = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                    kmt.Connection = bag;
                    kmt.CommandText = "INSERT INTO stokbil(stokAdi,stokModeli,stokSeriNo,stokAdedi,stokTarih,kayitYapan,dosyaAdi,urunOzellik,systemUser,alisFiyat,satisFiyat) VALUES ('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + dateTimePicker1.Text + "','" + textBox5.Text + "','" + DosyaAdi + "','" + textBox8.Text + "','" + systemUser + "','"+ alınan.Text+"','" +satılan.Text+ "') ";
                    kmt.ExecuteNonQuery();
                    kmt.Dispose();
                    bag.Close();
                    for (int i = 0; i < this.Controls.Count; i++)
                    {
                        if (this.Controls[i] is TextBox) this.Controls[i].Text = "";
                    }
                    listele(); // yenileme
                    //log bilsisi
                    WriteLog("Kayıt işlemi gerçekleştirildi: ÜrünAdıModeli: " + textBox1.Text + " " + textBox2.Text + " SeriNo: " + textBox3.Text + "  ");

                    MessageBox.Show("Kayıt İşlemi Tamamlandı ! ", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch
            {
                MessageBox.Show("Sistemde bir hata oluştu.Tekrar deneyiniz ya da iletişime geçiniz.");
                bag.Close();
            }
          
        }
        private void btnResimEkle_Click(object sender, EventArgs e)
        {
            // resim ekle
            if (DosyaAc.ShowDialog() == DialogResult.OK)
            {
                foreach (string i in DosyaAc.FileName.Split('\\'))
                {
                    if (i.Contains(".jpg")) { DosyaAdi = i; } // jpg modu
                    else if (i.Contains(".png")) { DosyaAdi = i; } // png modu
                    else { DosyaYolu += i + "\\"; }
                }
                pictureBox1.ImageLocation = DosyaAc.FileName;
            }
            else
            {
                MessageBox.Show("Sistemde kritik bir hata meydana geldi. Lütfen daha sonra tekrar deneyiniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            dateTimePicker1.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            textBox5.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            textBox8.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            alınan.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
            satılan.Text = dataGridView1.CurrentRow.Cells[9].Value.ToString();

            try
            {
                kmt = new SqlCommand("select * from stokbil where stokSeriNo='" + dataGridView1.CurrentRow.Cells[2].Value.ToString() + "'", bag);
                bag.Open();
                SqlDataReader oku = kmt.ExecuteReader();
                oku.Read();
                if (oku.HasRows)
                {
                    pictureBox1.ImageLocation = oku[7].ToString();
                    id=Convert.ToInt32(oku[0].ToString());
                }
                bag.Close();
            }
            catch
            {
                bag.Close();
            }
            DosyaAdi = pictureBox1.ImageLocation;

        }
        private void btnStokAdiAra_Click(object sender, EventArgs e)
        {
            SqlDataAdapter adtr = new SqlDataAdapter("select * From stokbil", bag);
            if (textBox6.Text.Trim()== "")
            {
                tablo.Clear();
                kmt.Connection = bag;
                kmt.CommandText = "Select * from stokbil";
                adtr.SelectCommand = kmt;
                adtr.Fill(tablo);               
            }
            if (Convert.ToBoolean(bag.State) == false)
            {
                bag.Open();

                if (textBox6.Text.Trim() != "")
                {
                    adtr.SelectCommand.CommandText = " Select * From stokbil" +
                         " where(stokAdi='" + textBox6.Text + "' )";
                    tablo.Clear();
                    adtr.Fill(tablo);
                    bag.Close();
                    textBox6.Clear();
                }
            }
        }              
        private void btnStokModelAra_Click(object sender, EventArgs e)
        {
            SqlDataAdapter adtr = new SqlDataAdapter("select * From stokbil", bag);
            if (textBox7.Text.Trim() == "")
            {
                tablo.Clear();
                kmt.Connection = bag;
                kmt.CommandText = "Select * from stokbil";
                adtr.SelectCommand = kmt;
                adtr.Fill(tablo);
      
            }
            if (Convert.ToBoolean(bag.State) == false)
            {
                bag.Open();
 
            }
            if (textBox7.Text.Trim() != "")
            {
                adtr.SelectCommand.CommandText = " Select * From stokbil" +
                     " where(stokModeli='" + textBox7.Text + "' )";
                tablo.Clear();
                adtr.Fill(tablo);
                bag.Close();
                textBox7.Clear();
            }
        }
        private void btnResimSil_Click(object sender, EventArgs e)
        {
            pictureBox1.ImageLocation = "";
            DosyaAdi = "";
        }
        private void btnStokSil_Click(object sender, EventArgs e)
        {
            //Onay Kutusu: Kullanıcıdan silme işlemi için bir onay alıyoruz.
            DialogResult dialogResult = MessageBox.Show("Silme işlemini gerçekleştirmek istediğinize emin misiniz? Dikkat: Bu işlem geri alınamaz!", "İşlem Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    DialogResult cevap;
                    string systemUser = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                    cevap = MessageBox.Show("Kaydı silmek istediğinizden eminmisiniz", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (cevap == DialogResult.Yes && dataGridView1.CurrentRow.Cells[0].Value.ToString().Trim() != "")
                    {
                        bag.Open();
                        kmt.Connection = bag;
                        kmt.CommandText = "DELETE from stokbil WHERE stokSeriNo='" + dataGridView1.CurrentRow.Cells[2].Value.ToString() + "' ";
                        kmt.ExecuteNonQuery();
                        kmt.Dispose();
                        bag.Close();
                        WriteLog("Silme işlemi gerçekleştirildi: StokAdi: " + textBox1.Text+" StokSeriNo: " + textBox3.Text + " ");
                        listele(); // yenile
                    }
                }
                catch
                {
                    MessageBox.Show("Bir hata oluştu.Lütfen tekrar deneyiniz.","HATA",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
            }
        }          
        private void btnCikis_Click(object sender, EventArgs e)
        {
            programClose();
        }
        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) == false && e.KeyChar != (char)08 && e.KeyChar != (char)44)
            {
                e.Handled = true;
                MessageBox.Show("Sadece Harf Girişi Yapabilirsiniz ! ", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }      
        }
        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) == false && e.KeyChar != (char)08 && e.KeyChar != (char)44)
            {
                e.Handled = true;
                MessageBox.Show("Sadece Harf Girişi Yapabilirsiniz ! ", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }      
        }
        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) == false && e.KeyChar != (char)08 && e.KeyChar != (char)44)
            {
                e.Handled = true;
                MessageBox.Show("Sadece Harf Girişi Yapabilirsiniz ! ", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }      
        }
        private void kapatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void logBilgisiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            log frm = new log();
            frm.Show();

        }
        private void yenileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listele();
        }
        private void kToolStripMenuItem_Click(object sender, EventArgs e)
        {
            programClose();
        }
        private void yapılanİşlemlerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            log frm = new log();
            frm.Show();
            this.Hide();
        }
        private void logKaydıToolStripMenuItem_Click(object sender, EventArgs e)
        {
            log frm = new log();
            frm.Show();
            this.Hide();
        }
        private void yardımToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Yardım için, 'atakndymn@hotmail.com' adresinden ulaşabilirsiniz.", "BİLGİ",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            qrCode frm = new qrCode();
            frm.Show();
        }


        private void textBox8_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Lütfen Ürün Detaylarını Eksiksiz Giriniz.");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _6Barkod frm = new _6Barkod();
            frm.Show();
        }

        private void btnStokGuncelle_Click(object sender, EventArgs e)
        {  
                if (textBox1.Text.Trim() != "" && textBox2.Text.Trim() != "" && textBox3.Text.Trim() != "" && textBox4.Text.Trim() != "" && textBox5.Text.Trim() != "" && alınan.Text.Trim() != "" && satılan.Text.Trim() != "")
                {

                    string systemUser = System.Security.Principal.WindowsIdentity.GetCurrent().Name;

                   string sorgu = "UPDATE stokbil SET stokAdi='" + textBox1.Text + "',stokModeli='" + textBox2.Text + "',stokSeriNo='" + textBox3.Text + "',stokAdedi='" + textBox4.Text + "',stokTarih='" + dateTimePicker1.Text + "',kayitYapan='" + textBox5.Text + "',dosyaAdi='" + DosyaAdi + "',urunOzellik='" + textBox8.Text + "',alisFiyat='" + alınan.Text + "',satisFiyat='" + satılan.Text + "', systemUser='" + systemUser + "' WHERE id=" + id;
                    SqlCommand kmt = new SqlCommand(sorgu,bag);

                    bag.Open();
                    kmt.ExecuteNonQuery();
                    kmt.Dispose();
                    bag.Close();
                    WriteLog("Güncelleme işlemi gerçekleştirildi");
                    listele();
                    
                    MessageBox.Show("Güncelleme İşlemi Tamamlandı !","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Boş Alan Bırakmayınız !");
                }
            
        }        
    }
}
//ATAKAN ADIYAMAN 
