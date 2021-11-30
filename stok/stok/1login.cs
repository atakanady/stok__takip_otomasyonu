using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace stok_programi
{
    public partial class login :Form
    {

        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        public login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() == "") errorProvider1.SetError(textBox1, "Boş geçilmez");
            else errorProvider1.SetError(textBox1, "");
            if (textBox2.Text.Trim() == "") errorProvider1.SetError(textBox2, "Boş geçilmez");
            else errorProvider1.SetError(textBox2, "");
           

            string sorgu = "SELECT * FROM  giiris where Kullanici=@Kullanici AND parola=@parola";
            con = new SqlConnection("server=.; Initial Catalog=data; Integrated Security=True;");
            cmd = new SqlCommand(sorgu, con);
            cmd.Parameters.AddWithValue("@Kullanici", textBox1.Text);
            cmd.Parameters.AddWithValue("@parola", textBox2.Text);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                //MessageBox.Show("Tebrikler! Başarılı bir şekilde giriş yaptınız.");
                con.Close();
                Form1 frm = new Form1();
                frm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Kullanıcı adını ve şifrenizi kontrol ediniz.");
                textBox1.Clear();
                textBox2.Clear();
            } 
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

            {
                //checkBox işaretli ise
                if (checkBox1.Checked)
                {
                    //karakteri göster.
                    textBox2.PasswordChar = '\0';
                }
                //değilse karakterlerin yerine * koy.
                else
                {
                    textBox2.PasswordChar = '*';
                }
            }
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            textBox2.PasswordChar = '*';
        }
        private void button2_Click(object sender, EventArgs e)
        {
            register FRM = new register();
            FRM.Show();
            this.Hide();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
