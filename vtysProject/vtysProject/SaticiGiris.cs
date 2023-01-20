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
    public partial class SaticiGiris : Form
    {
        public SaticiGiris()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-M9SLCJM; Initial Catalog = dbmarket; Integrated Security =True");
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string adi = textBox1.Text;
                string sifree = textBox2.Text;

                baglanti.Open();
                SqlCommand komut = new SqlCommand();
                komut.Connection = baglanti;
                komut.CommandText = "SELECT * FROM satici WHERE kullaniciAdi ='" + adi + "' AND sifre ='" + sifree + "'";
                komut.ExecuteNonQuery();
                SqlDataReader dr = komut.ExecuteReader();
                if (dr.Read())
                {
                    Perakende prk = new Perakende();
                    prk.Show();
                    Hide();
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Hatali Giriş!!!");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {//ekle

            String st1 = textBox1.Text; // kullaniciAdi
            String st2 = textBox2.Text; // sifre


            if (st1 == "" || st2 == "")
            {
                MessageBox.Show("Lütfen Boş Bıraktığınız Yerleri Tekrardan Giriş Yapınız!!!");
            }
            else
            {

                baglanti.Open();
                SqlCommand komut = new SqlCommand("INSERT INTO satici (kullaniciAdi, sifre) VALUES('" + st1 + "','" + st2 + "')", baglanti);
                komut.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Kayıt Başarılı!!!");

            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            AnaSayfa ana = new AnaSayfa();
            ana.Show();
            Hide();
        }
    }
}
