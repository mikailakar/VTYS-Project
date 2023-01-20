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
    public partial class pesinSatis : Form
    {
        public pesinSatis()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-M9SLCJM; Initial Catalog = dbmarket; Integrated Security =True");

        private void pesinSatis_Load(object sender, EventArgs e)
        {
            listele();
        }

        private void listele()
        {
            baglanti.Open();
            SqlDataAdapter da = new SqlDataAdapter("Select *from stok", baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView2.DataSource = dt;
            SqlDataAdapter da2 = new SqlDataAdapter("Select *from pesinSatis", baglanti);
            DataTable dt2 = new DataTable();
            da2.Fill(dt2);
            dataGridView1.DataSource = dt2;
            baglanti.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String st1 = textBox4.Text; //barkod kodu
            String st2 = textBox5.Text; //miktar


            baglanti.Open();
            SqlCommand komut2 = new SqlCommand();
            komut2.Connection = baglanti;
            komut2.CommandText = "Select Min(miktar) from stok WHERE urunBarkod='" + st1 + "'";
            object mikar = komut2.ExecuteScalar();
            if (mikar.ToString() == string.Empty) mikar = "0";
            int miktar = int.Parse(mikar.ToString());

            int kalan = miktar - int.Parse(st2);


            SqlCommand komut = new SqlCommand("UPDATE stok SET miktar = '" + kalan + "' WHERE urunBarkod = '" + st1 + "' ", baglanti);
            komut.ExecuteNonQuery();


            String st3 = textBox3.Text; //urun kodu
            String st4 = textBox7.Text; //alınan tarih

            String st6 = textBox6.Text; //toplam tutar

            SqlCommand komut3 = new SqlCommand();
            komut3.Connection = baglanti;
            komut3.CommandText = "Select Max(satisNo) from pesinSatis";
            object sonsatis = komut3.ExecuteScalar();
            if (sonsatis.ToString() == string.Empty) sonsatis = "0";
            int ssatis = int.Parse(sonsatis.ToString()) + 1;


            if (st3 == "" || st4 == "" || st2 == "" || st6 == "")
            {
                MessageBox.Show("Lütfen Boş Bıraktığınız Yerleri Tekrardan Giriş Yapınız!!!");
            }
            else
            {

                SqlCommand komut4 = new SqlCommand("INSERT INTO pesinSatis (satisNo, urunKodu, alinanTarih, miktar, toplamTutar, urunBarkod) VALUES('" + ssatis + "','" + st3 + "','" + st4 + "','" + st2 + "','" + st6 + "','" + st1 + "')", baglanti);
                komut4.ExecuteNonQuery();
            }
            baglanti.Close();



            listele();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            String st1 = textBox2.Text; // satisNo
            baglanti.Open();

            SqlCommand komut13 = new SqlCommand();
            komut13.Connection = baglanti;
            komut13.CommandText = "Select Min(urunBarkod) from pesinSatis WHERE satisNo='" + st1 + "'";
            object bkk = komut13.ExecuteScalar();
            if (bkk.ToString() == string.Empty) bkk = "0";
            int bark = int.Parse(bkk.ToString());

            SqlCommand komut14 = new SqlCommand();
            komut14.Connection = baglanti;
            komut14.CommandText = "Select Min(miktar) from pesinSatis WHERE satisNo='" + st1 + "'";
            object emikt = komut14.ExecuteScalar();
            if (emikt.ToString() == string.Empty) emikt = "0";
            int emik = int.Parse(emikt.ToString());

            SqlCommand komut12 = new SqlCommand();
            komut12.Connection = baglanti;
            komut12.CommandText = "Select Min(miktar) from stok WHERE urunBarkod='" + bark + "'";
            object mikk = komut12.ExecuteScalar();
            if (mikk.ToString() == string.Empty) mikk = "0";
            int miktt = int.Parse(mikk.ToString());

            int ymik = miktt + emik;

            SqlCommand komut15 = new SqlCommand("UPDATE stok SET miktar = '" + ymik + "' WHERE urunBarkod = '" + bark + "' ", baglanti);
            komut15.ExecuteNonQuery();


            SqlCommand komut11 = new SqlCommand("DELETE FROM pesinSatis WHERE satisNo =  ('" + st1 + "')", baglanti);
            komut11.ExecuteNonQuery();

            baglanti.Close();
            listele();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Perakende per = new Perakende();
            per.Show();
            Hide();
        }
    }
}
