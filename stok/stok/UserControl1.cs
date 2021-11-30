using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

namespace stok_programi
{
    /// <summary>
    /// UserControl1.xaml etkileşim mantığı
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        public UserControl1()
        {
            InitializeComponent();
        }

        private void btnSumbit_Click(object sender, RoutedEventArgs e)
        {
            string sorgu = "SELECT * FROM  giiris where Kullanici=@Kullanici AND parola=@parola";
            con = new SqlConnection("server=.; Initial Catalog=data; Integrated Security=True;");
            cmd = new SqlCommand(sorgu, con);
            cmd.Parameters.AddWithValue("@Kullanici", txtUsername.DataContext);
            cmd.Parameters.AddWithValue("@parola", txtPassword.DataContext);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                //MessageBox.Show("Tebrikler! Başarılı bir şekilde giriş yaptınız.");
                con.Close();
                Form1 frm = new Form1();
                frm.Show();

            }
            else
            {
                MessageBox.Show("Kullanıcı adını ve şifrenizi kontrol ediniz.");
                txtUsername.Clear();
                txtPassword.Clear();
            }
        }
    }
}
