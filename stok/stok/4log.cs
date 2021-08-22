using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace stok_programi
{
    public partial class log : Form
    {

        SqlConnection con;
        SqlDataAdapter da;
        SqlCommand cmd = new SqlCommand();
        DataSet ds;
        public log()
        {
            InitializeComponent();
        }
        void veriCek()
        {
            con = new SqlConnection("server=.; Initial Catalog=data; User Id=atakn; password=Atakan!!;");
            da = new SqlDataAdapter("Select *From stok_log ORDER BY id DESC ", con);
            ds = new DataSet();
            con.Open();
            da.Fill(ds, "stok_log");
            dataGridView1.DataSource = ds.Tables["stok_log"];
            con.Close();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            veriCek();
            timer1.Start();

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
        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Silme işlemini gerçekleştirmek istediğinize emin misiniz? Dikkat: Bu işlem geri alınamaz!", "İşlem Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dialogResult == DialogResult.Yes)
            {
                con.Open();

                SqlCommand kmt = new SqlCommand("DELETE FROM stok_log WHERE id=" + dataGridView1.CurrentRow.Cells["id"].Value.ToString(), con);

                kmt.ExecuteNonQuery();
                con.Close();

                string islem_user = dataGridView1.CurrentRow.Cells["islem_user"].Value.ToString();
                string islem_tip = dataGridView1.CurrentRow.Cells["islem_tip"].Value.ToString();
                string islem_zaman = dataGridView1.CurrentRow.Cells["islem_zaman"].Value.ToString();
                string id = dataGridView1.CurrentRow.Cells["id"].Value.ToString();

                veriCek();
            }
            else if (dialogResult == DialogResult.No)
            {
                return;
            }
            else
            {
                MessageBox.Show("Tanımsız bir hata meydana geldi. Lütfen tekrar deneyiniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void geriToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 frm = new Form1();
            frm.Show();
            this.Hide();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = DateTime.Now.ToLongDateString();
            label2.Text = DateTime.Now.ToLongTimeString();
        }

        private void yardımToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Yardım için, 'atakndymn@hotmail.com' adresinden  ulaşabilirsiniz.", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
