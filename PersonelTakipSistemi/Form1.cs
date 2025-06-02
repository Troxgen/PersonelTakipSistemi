using PersonelTakipSistemi.Personel;
using System;
using System.Data.OleDb;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace PersonelTakipSistemi
{
    public partial class Form1 : MetroFramework.Forms.MetroForm
    {
        public Form1()
        {
            InitializeComponent();
        }

        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Veritabani/eh.mdb");
        public static string Id { get; set; }
        public static string RolID { get; set; }

        private void Form1_Load(object sender, EventArgs e)
        {
            metroTextBox2.Multiline = false;
            metroTextBox2.Padding = new Padding(25, 0, 0, 0);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (metroTextBox2.PasswordChar == '\0')
            {
                metroTextBox2.PasswordChar = '•';
                pictureBox1.Image = Properties.Resources.KapaliGoz;
            }
            else
            {
                metroTextBox2.PasswordChar = '\0';
                pictureBox1.Image = Properties.Resources.AcikGoz;
            }
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            try
            {
                string kullaniciAdi = metroTextBox1.Text.Trim();
                string sifre = metroTextBox2.Text.Trim();

                // Komut oluştur
                OleDbCommand komut = new OleDbCommand("SELECT * FROM Personel WHERE TcKimlik = @kullaniciAdi AND Sifre = @sifre", baglanti);
                komut.Parameters.AddWithValue("@kullaniciAdi", kullaniciAdi);
                komut.Parameters.AddWithValue("@sifre", sifre);

                // Bağlantıyı aç
                baglanti.Open();

                // Komutu çalıştır ve sonuçları oku
                OleDbDataReader reader = komut.ExecuteReader();

                if (reader.Read())
                {
                    Id = reader["PersonelID"].ToString();
                    RolID = reader["RolID"].ToString();
                    this.Hide();

                    PersonelAnaEkran mainForm = new PersonelAnaEkran();
                    mainForm.Show();
                }
                else
                {
                    MessageBox.Show("Kullanıcı adı veya şifre yanlış.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                baglanti.Close(); // Bağlantıyı kapat
            }
        }



    }
}
