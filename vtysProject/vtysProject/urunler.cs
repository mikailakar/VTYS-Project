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

namespace vtysProject
{
    public partial class urunler : Form
    {
        public urunler()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-M9SLCJM; Initial Catalog = dbmarket; Integrated Security =True");

        void urun()
        {

        }

        private void urunler_Load(object sender, EventArgs e)
        {
            listele();
        }

        private void listele()
        {
            baglanti.Open();
            SqlDataAdapter da = new SqlDataAdapter("Select *from urunler", baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglanti.Close();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            AnaSayfa back = new AnaSayfa();
            back.Show();
            Hide();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            using(var adap = new SqlDataAdapter("SELECT (urunKodu, birimGirdiFiyatı) From stok", baglanti))
            {
                try
                {
                    baglanti.Open();
                    DataTable dt = new DataTable();
                    adap.Fill(dt);
                    dataGridView1.DataSource = dt;
                } catch
                {
                    MessageBox.Show("Hatali");
                }
            }
        }
    }
}
