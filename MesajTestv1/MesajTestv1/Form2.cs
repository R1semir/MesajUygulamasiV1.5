using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace MesajTestv1
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-BJO2DGU\SQLEXPRESS;Initial Catalog=Test;Integrated Security=True");
        //Select MESAJID, (AD+ ' ' +SOYAD) AS GONDEREN,BASLIK,ICERIK From TBLMESAJLAR
        //inner join TBLKISILER on TBLMESAJLAR.GONDEREN = TBLKISILER.NUMARA Where ALICI = 
        void gelenkutusu()
        {
  SqlDataAdapter da1 = new SqlDataAdapter("select  MESAJID, (AD+ ' ' +SOYAD) AS Gönderen,BASLIK,ICERIK From TBLMESAJLAR inner join TBLKISILER on TBLMESAJLAR.GONDEREN= TBLKISILER.NUMARA WHERE ALICI=  " + numara,baglanti );
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);
            dataGridView1.DataSource = dt1;

        }
        // Select MESAJID, (AD+ ' ' +SOYAD) AS ALICI,BASLIK,ICERIK From TBLMESAJLAR inner join TBLKISILER on TBLMESAJLAR.ALICI = TBLKISILER.NUMARA Where GONDEREN = 
        void gidenkutusu()
        {
            SqlDataAdapter da3 = new SqlDataAdapter("select MESAJID, (AD+ ' ' + SOYAD) AS Kime,BASLIK,ICERIK From TBLMESAJLAR inner join TBLKISILER on TBLMESAJLAR.ALICI = TBLKISILER.NUMARA WHERE GONDEREN= "+numara,baglanti);
            DataTable dt3 = new DataTable();
            da3.Fill(dt3);
            dataGridView2.DataSource = dt3;
        }

      
       
      

        public string numara;
        private void Form2_Load(object sender, EventArgs e)
        {
            lblNumara.Text = numara;
            gelenkutusu();
            gidenkutusu();

            baglanti.Open();
            SqlCommand komutadsoyad = new SqlCommand("select AD,SOYAD from TBLKISILER Where NUMARA="+numara,baglanti);
            SqlDataReader dr = komutadsoyad.ExecuteReader();
            while (dr.Read())
            {
                lblAdSoyad.Text = dr[0] + " " + dr[1];
            }
            baglanti.Close();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komutgönder = new SqlCommand("insert into TBLMESAJLAR (GONDEREN,ALICI,BASLIK,ICERIK) values (@p1,@p2,@p3,@p4)",baglanti);
            komutgönder.Parameters.AddWithValue("@p1",numara);
            komutgönder.Parameters.AddWithValue("@p2", maskedTextBox1.Text);
            komutgönder.Parameters.AddWithValue("@p3", textBox1.Text);
            komutgönder.Parameters.AddWithValue("@p4", richTextBox1.Text);
            komutgönder.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Mesajınız İletildi");
            gidenkutusu();

        }
    }
}
