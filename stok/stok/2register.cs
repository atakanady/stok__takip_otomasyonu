using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace stok_programi
{
    public partial class register : Form
    {
        SqlConnection baglanti;
        SqlCommand komut;
        SqlDataAdapter eth;

        public register()
        {
            InitializeComponent();
        }
        void MusteriGetir()
        {
            //database ile bağlantı metodu
            baglanti = new SqlConnection("server=.;Initial Catalog=data;Integrated Security=True;");
            baglanti.Open();
            eth = new SqlDataAdapter("SELECT *FROM giiris", baglanti);
            baglanti.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (txtKullanici.Text.Trim() == "") errorProvider1.SetError(txtKullanici, "Boş geçilmez");
            else errorProvider1.SetError(txtKullanici, "");
            if (txtParola.Text.Trim() == "") errorProvider1.SetError(txtParola, "Boş geçilmez");
            else errorProvider1.SetError(txtParola, "");
            if (txtEposta.Text.Trim() == "") errorProvider1.SetError(txtEposta, "Boş geçilmez");
            else errorProvider1.SetError(txtEposta, "");
            if (txtTelefon.Text.Trim() == "") errorProvider1.SetError(txtTelefon, "Boş geçilmez");
            else errorProvider1.SetError(txtTelefon, "");
            if (dateTimePicker1.Text.Trim() == "") errorProvider1.SetError(dateTimePicker1, "Boş geçilmez");
            else errorProvider1.SetError(dateTimePicker1, "");


            MusteriGetir();
            //veri tabanına ekleme
            string sorgu = "INSERT INTO giiris(Kullanici,parola,eposta,tarih,number) VALUES(@Kullanici,@parola,@eposta,@tarih,@number)";
            komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@Kullanici", txtKullanici.Text);
            komut.Parameters.AddWithValue("@tarih", dateTimePicker1.Value);
            komut.Parameters.AddWithValue("@parola", txtParola.Text);
            komut.Parameters.AddWithValue("@eposta", txtEposta.Text);
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(txtEposta.Text);
            if (match.Success)
            {

                //MessageBox.Show("Eposta adresi kullanıma uygun.");
                txtEposta.Text = txtParola.Text.Trim();
                komut.Parameters.AddWithValue("@number", txtTelefon.Text);
                baglanti.Open();
                komut.ExecuteNonQuery(); // sorgu nesnesiini çalıştır. ilgili satırları çalıştırır.
                baglanti.Close();

                txtEposta.Clear();
                txtKullanici.Clear();
                txtParola.Clear();
                txtTelefon.Clear();
                MessageBox.Show("Kayıt İşlemi Başarılı. Şimdi Giriş Yapabilirisiniz.", "BİLGİ", MessageBoxButtons.OK);
                login frm = new login();
                frm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Eposta adresi kullanıma uygun bir biçim içermiyor.", "HATA", MessageBoxButtons.OK);
                txtEposta.Clear();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            //checkBox işaretli ise
            if (checkBox1.Checked)
            {
                //karakteri göster.
                txtParola.PasswordChar = '\0';
            }
            //değilse karakterlerin yerine * koy.
            else
            {
                txtParola.PasswordChar = '*';
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            txtParola.PasswordChar = '*';
        }

        private void button2_Click(object sender, EventArgs e)
        {
            login frm = new login();
            frm.Show();
            this.Hide();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
