using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace vtysProject
{
    public partial class borcYonetimi : Form
    {
        public borcYonetimi()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-M9SLCJM; Initial Catalog = dbmarket; Integrated Security =True");
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

            String st1 = textBox1.Text; // musteriKodu
            DataTable tablo = new DataTable();
            baglanti.Open();
            SqlDataAdapter adKod = new SqlDataAdapter("SELECT * FROM musteri WHERE musteriKodu like '%" + st1 + "%' ", baglanti);
            adKod.Fill(tablo);
            dataGridView1.DataSource = tablo;
            baglanti.Close();

            listele2();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            listele3();
        }

        private void listele3()
        {
            baglanti.Open();
            SqlDataAdapter da = new SqlDataAdapter("Select *from musteri", baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglanti.Close();
        }

        private void listele2()
        {
            baglanti.Open();
            String st1 = textBox1.Text; // musteriKodu

            SqlDataAdapter da2 = new SqlDataAdapter("Select *from cariSatis where musteriKodu='" + st1 + "'", baglanti);
            DataTable dt2 = new DataTable();
            da2.Fill(dt2);
            dataGridView2.DataSource = dt2;
            baglanti.Close();
        }
        private void listele()
        {
            baglanti.Open();
            SqlDataAdapter da = new SqlDataAdapter("Select *from odemeDetayi", baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView3.DataSource = dt;
            baglanti.Close();
        }

        private void borcYonetimi_Load(object sender, EventArgs e)
        {
            listele3();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String st1 = textBox1.Text; //no
            String st2 = textBox2.Text; //tutar
            String st3 = textBox3.Text; //tarih
            baglanti.Open();

            SqlCommand komut1 = new SqlCommand();
            komut1.Connection = baglanti;
            komut1.CommandText = "Select Min(musteriBorcu) from musteri where musteriKodu='" + st1 + "'";
            object ebor = komut1.ExecuteScalar();
            if (ebor.ToString() == string.Empty) ebor = "0";
            float ebr = float.Parse(ebor.ToString()) - float.Parse(st2.Replace(".", ","));

            SqlCommand komut2 = new SqlCommand("UPDATE musteri SET musteriBorcu = '" + ebr.ToString().Replace(",", ".") + "' WHERE musteriKodu = '" + st1 + "' ", baglanti);
            komut2.ExecuteNonQuery();


            SqlCommand komut4 = new SqlCommand();
            komut4.Connection = baglanti;
            komut4.CommandText = "Select Min(musteriIsmi) from musteri WHERE musteriKodu='" + st1 + "'";
            object bkk = komut4.ExecuteScalar();
            if (bkk.ToString() == string.Empty) bkk = "0";
            string isim = bkk.ToString();
            SqlCommand komut3 = new SqlCommand();
            komut3.Connection = baglanti;
            komut3.CommandText = "Select Min(musteriSoyisim) from musteri WHERE musteriKodu='" + st1 + "'";
            object sysm = komut3.ExecuteScalar();
            if (sysm.ToString() == string.Empty) sysm = "0";
            string soyisim = sysm.ToString();

            SqlCommand komut5 = new SqlCommand("INSERT INTO odemeDetayi (musteriKodu, musteriIsmi, musteriSoyisim, odenenTutar, tarih) VALUES('" + st1 + "','" + isim + "','" + soyisim + "','" + st2 + "','" + st3 + "')", baglanti);
            komut5.ExecuteNonQuery();


            SqlCommand komut6 = new SqlCommand();
            komut6.Connection = baglanti;
            komut6.CommandText = "Select Min(odenenborc) from musteri where musteriKodu='" + st1 + "'";
            object obor = komut6.ExecuteScalar();
            if (obor.ToString() == string.Empty) obor = "0";
            float oborc = float.Parse(obor.ToString()) + float.Parse(st2.Replace(".", ","));

            SqlCommand komut7 = new SqlCommand("UPDATE musteri SET odenenBorc = '" + oborc.ToString().Replace(",", ".") + "' WHERE musteriKodu = '" + st1 + "' ", baglanti);
            komut7.ExecuteNonQuery();


            baglanti.Close();
            listele3();
            listele();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AnaSayfa ana = new AnaSayfa();
            ana.Show();
            Hide();

        }
    }
}
