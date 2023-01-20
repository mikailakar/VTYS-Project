using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace vtysProject
{
    public partial class CariSatis : Form
    {
        public CariSatis()
        {
            InitializeComponent();
        }

        //orm kullanımı
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-M9SLCJM; Initial Catalog = dbmarket; Integrated Security =True");
  
        private void button1_Click(object sender, EventArgs e)
        {
            String st1 = textBox4.Text; //barkod kodu
            String st2 = textBox5.Text; //miktar
            

            baglanti.Open();
            SqlCommand komut2 = new SqlCommand();
            komut2.Connection = baglanti;
            komut2.CommandText = "Select Min(miktar) from stok WHERE urunBarkod='"+ st1 +"'";
            object mikar = komut2.ExecuteScalar();
            if (mikar.ToString() == string.Empty) mikar = "0";
            int miktar = int.Parse(mikar.ToString());

            int kalan = miktar - int.Parse(st2);

            //orm kullanımı
            SqlCommand komut = new SqlCommand("UPDATE stok SET miktar = '"+ kalan +"' WHERE urunBarkod = '"+ st1 +"' ", baglanti);
            komut.ExecuteNonQuery();

            String st7 = textBox1.Text; //Müşteri Kodu

            String st3 = textBox3.Text; //urun kodu
            String st4 = textBox7.Text; //alınan tarih
            
            String st6 = textBox6.Text; //toplam tutar

            SqlCommand komut3 = new SqlCommand();
            komut3.Connection = baglanti;
            komut3.CommandText = "Select Max(satisNo) from cariSatis";
            object sonsatis = komut3.ExecuteScalar();
            if (sonsatis.ToString() == string.Empty) sonsatis = "0";
            int ssatis = int.Parse(sonsatis.ToString())+1;


            if (st7 == "" || st3 == "" || st4 == "" || st2 == "" || st6 == "")
            {
                MessageBox.Show("Lütfen Boş Bıraktığınız Yerleri Tekrardan Giriş Yapınız!!!");
            }
            else
            {
                SqlCommand komut5 = new SqlCommand();
                komut5.Connection = baglanti;
                komut5.CommandText = "Select Min(musteriBorcu) from musteri where musteriKodu='"+ st7 +"'";
                object borc = komut5.ExecuteScalar();
                if (borc.ToString() == string.Empty) borc = "0";
                float borc2 = float.Parse(borc.ToString()) + float.Parse(st6.Replace(".", ","));

                SqlCommand komut6 = new SqlCommand("UPDATE musteri SET musteriBorcu = '" + borc2.ToString().Replace(",", ".") + "' WHERE musteriKodu = '" + st7 + "' ", baglanti);
                komut6.ExecuteNonQuery();


                SqlCommand komut4 = new SqlCommand("INSERT INTO cariSatis (satisNo, musteriKodu, urunKodu, alinanTarih, miktar, toplamTutar, urunBarkod) VALUES('" + ssatis + "','" + st7 + "','" + st3 + "','" + st4 + "','" + st2 + "','" + st6 + "','" + st1 + "')", baglanti);
                komut4.ExecuteNonQuery();


                SqlCommand komut7 = new SqlCommand();
                komut7.Connection = baglanti;
                komut7.CommandText = "Select Min(satinAlim) from musteri where musteriKodu='" + st7 + "'";
                object sal = komut7.ExecuteScalar();
                if (sal.ToString() == string.Empty) sal = "0";
                int salim = int.Parse(sal.ToString()) + 1;

                SqlCommand komut8 = new SqlCommand("UPDATE musteri SET satinAlim = '" + salim + "' WHERE musteriKodu = '" + st7 + "' ", baglanti);
                komut8.ExecuteNonQuery();

            }
            baglanti.Close();



            listele();
            listele2();
            listele3();
        }



        private void CariSatis_Load(object sender, EventArgs e)
        {
            listele();
            listele2();
            listele3();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
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
            baglanti.Close();
        }

        private void listele2()
        {
            baglanti.Open();
            SqlDataAdapter da = new SqlDataAdapter("Select *from cariSatis", baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglanti.Close();
        }




        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            listele3();
        }

        private void listele3()
        {
            baglanti.Open();
            SqlDataAdapter da = new SqlDataAdapter("Select *from musteri", baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView3.DataSource = dt;
            baglanti.Close();
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString(); //musterikodu
            textBox3.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString(); // urunkodu
            textBox7.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString(); // alınantarih
            textBox5.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString(); // miktar
            textBox6.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString(); // toplam tutar
        }

        private void button2_Click(object sender, EventArgs e)
        {//sil
            String st1 = textBox2.Text; // satisNo
            baglanti.Open();


            SqlCommand komut17 = new SqlCommand();
            komut17.Connection = baglanti;
            komut17.CommandText = "Select Min(musteriKodu) from cariSatis where satisNo='"+ st1 +"'";
            object mkd = komut17.ExecuteScalar();
            if (mkd.ToString() == string.Empty) mkd = "0";
            int mkod = int.Parse(mkd.ToString());

            SqlCommand komut18 = new SqlCommand();
            komut18.Connection = baglanti;
            komut18.CommandText = "Select Min(toplamTutar) from cariSatis where satisNo='" + st1 + "'";
            object ttut = komut18.ExecuteScalar();
            if (ttut.ToString() == string.Empty) ttut = "0";
            float totut = float.Parse(ttut.ToString());

            SqlCommand komut16 = new SqlCommand();
            komut16.Connection = baglanti;
            komut16.CommandText = "Select Min(musteriBorcu) from musteri where musteriKodu='" + mkod + "'";
            object borc = komut16.ExecuteScalar();
            if (borc.ToString() == string.Empty) borc = "0";
            float borc2 = float.Parse(borc.ToString()) - totut;

            SqlCommand komut6 = new SqlCommand("UPDATE musteri SET musteriBorcu = '" + borc2.ToString().Replace(",", ".") + "' WHERE musteriKodu = '" + mkod + "' ", baglanti);
            komut6.ExecuteNonQuery();



            SqlCommand komut13 = new SqlCommand();
            komut13.Connection = baglanti;
            komut13.CommandText = "Select Min(urunBarkod) from cariSatis WHERE satisNo='" + st1 +"'";
            object bkk = komut13.ExecuteScalar();
            if (bkk.ToString() == string.Empty) bkk = "0";
            int bark = int.Parse(bkk.ToString());

            SqlCommand komut14 = new SqlCommand();
            komut14.Connection = baglanti;
            komut14.CommandText = "Select Min(miktar) from cariSatis WHERE satisNo='" + st1 + "'";
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


            SqlCommand komut11 = new SqlCommand("DELETE FROM cariSatis WHERE satisNo =  ('" + st1 + "')", baglanti);
            komut11.ExecuteNonQuery();


            SqlCommand komut7 = new SqlCommand();
            komut7.Connection = baglanti;
            komut7.CommandText = "Select Min(satinAlim) from musteri where musteriKodu='" + mkod + "'";
            object sal = komut7.ExecuteScalar();
            if (sal.ToString() == string.Empty) sal = "0";
            int salim = int.Parse(sal.ToString()) - 1;

            SqlCommand komut8 = new SqlCommand("UPDATE musteri SET satinAlim = '" + salim + "' WHERE musteriKodu = '" + mkod + "' ", baglanti);
            komut8.ExecuteNonQuery();



            baglanti.Close();
            listele();
            listele2();
            listele3();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Perakende per = new Perakende();
            per.Show();
            Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        { // pdf şeklinde indirme
            
            if(dataGridView3.Rows.Count > 0)
            {
                SaveFileDialog save = new SaveFileDialog();
                save.Filter = "PDF (*.pdf)|*.pdf";
                save.FileName = "Toplu_borc_durum_listesi.pdf";
                bool ErrorMessage = false;
                if(save.ShowDialog() == DialogResult.OK)
                {
                    if(File.Exists(save.FileName))
                    {
                        try
                        {
                            File.Delete(save.FileName);
                        } 
                        catch (Exception ex)
                        {
                            ErrorMessage = true;
                            MessageBox.Show("Hatayı alınca mantıklı bi şey yaz" + ex.Message);
                        }
                    }
                    if (!ErrorMessage)
                    {
                        try
                        {
                            PdfPTable pdfPTable = new PdfPTable(dataGridView3.Columns.Count);
                            pdfPTable.DefaultCell.Padding = 2;
                            pdfPTable.WidthPercentage = 100;
                            pdfPTable.HorizontalAlignment = Element.ALIGN_LEFT;

                            foreach(DataGridViewColumn col in dataGridView3.Columns)
                            {
                                PdfPCell pCell = new PdfPCell(new Phrase(col.HeaderText));
                                pdfPTable.AddCell(pCell);

                            }
                            foreach(DataGridViewRow row in dataGridView3.Rows)
                            {
                                foreach(DataGridViewCell dcell in row.Cells)
                                {
                                    pdfPTable.AddCell(dcell.Value.ToString());
                                }
                            }
                            using(FileStream fileStream = new FileStream(save.FileName + ".pdf", FileMode.Create))
                            {
                                Document pdfDoc = new Document(PageSize.A2, 10f, 10f, 10f, 0f);// sayfa boyutu.
                                PdfWriter.GetInstance(pdfDoc, fileStream);
                                pdfDoc.Open();
                                pdfDoc.Add(pdfPTable);
                                pdfDoc.Close();
                                fileStream.Close();
                            }
                            MessageBox.Show("Başarılı şekilde indirildi!!!", "Bilgi");
                        }
                        catch(Exception ex)
                        {
                            MessageBox.Show("Hata alındı" + ex.Message);
                        }
                    }
                }
            } 
            else
            {
                MessageBox.Show("Bulunamadı", "Bilgi");
            }

        }
    }
}
