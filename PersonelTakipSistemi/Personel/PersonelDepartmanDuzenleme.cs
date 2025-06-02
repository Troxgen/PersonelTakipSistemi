using PersonelTakipSistemi.Class;
using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace PersonelTakipSistemi.Personel
{

    public partial class PersonelDepartmanDuzenleme : MetroFramework.Forms.MetroForm
    {

        public PersonelDepartmanDuzenleme()
        {
            InitializeComponent();
        }
        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Veritabani/eh.mdb");
        Log log = new Log();
        Form1 Form1 = new Form1();


        public void Listele(DataGridView data, string kelime)
        {
            baglanti.Open();
            OleDbDataAdapter da = new OleDbDataAdapter("SELECT  * from " + kelime, baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);
            data.DataSource = dt;

            baglanti.Close();

        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            try
            {
                baglanti.Open();
                OleDbCommand komut = new OleDbCommand("INSERT INTO Departman (DepartmanAdi, Aciklama) VALUES (@1, @2)", baglanti);
                komut.Parameters.AddWithValue("@1", DepartmanAdiText.Text);
                komut.Parameters.AddWithValue("@2", AciklamaText.Text);

                int sonuc = komut.ExecuteNonQuery();
                if (sonuc > 0)
                {
                    MessageBox.Show("Departman başarıyla eklendi!");
                    log.DenetimKaydi(Form1.RolID.ToString(), "İdli Departman başarıyla eklendi!", baglanti);
                    MessageBox.Show("İşlem Başarı İle Sonuçlandı !");


                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
            finally
            {
                baglanti.Close();
            }

            Listele(metroGrid1, "Departman");
        }

        private void metroGrid1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (metroGrid1.SelectedRows.Count > 0)
                {
                    string secilenDepartmanAdi = metroGrid1.SelectedRows[0].Cells["DepartmanAdi"].Value.ToString();

                    baglanti.Open();
                    OleDbCommand komut = new OleDbCommand("DELETE FROM Departman WHERE DepartmanAdi = @1", baglanti);
                    komut.Parameters.AddWithValue("@1", secilenDepartmanAdi);

                    int sonuc = komut.ExecuteNonQuery();
                    if (sonuc > 0)
                    {
                        MessageBox.Show("Departman başarıyla silindi!");
                        MessageBox.Show("İşlem Başarı İle Sonuçlandı !");

                    }

                }
                else
                {
                    MessageBox.Show("Lütfen silmek istediğiniz satırı seçin.");

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
            finally
            {
                baglanti.Close();
                log.DenetimKaydi(Form1.RolID.ToString(), "İdli Kullanıcı Departman başarıyla silindi!", baglanti);

            }

            Listele(metroGrid1, "Departman");
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            try
            {
                baglanti.Open();
                OleDbCommand komut = new OleDbCommand("Update Departman Set DepartmanAdi=@1,Aciklama=@2 wehere id = @3)", baglanti);
                komut.Parameters.AddWithValue("@1", DepartmanAdiText.Text);
                komut.Parameters.AddWithValue("@2", AciklamaText.Text);
                komut.Parameters.AddWithValue("@3", metroGrid2.CurrentRow.Cells[0].Value.ToString());
                int sonuc = komut.ExecuteNonQuery();
                if (sonuc > 0)
                {
                    MessageBox.Show("Departman başarıyla eklendi!");

                }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
            finally
            {
                if (baglanti.State == System.Data.ConnectionState.Open)
                {
                    baglanti.Close();
                    log.DenetimKaydi(Form1.RolID.ToString(), "İdli Departman başarıyla güncellendi!", baglanti);

                }
            }

            Listele(metroGrid1, "Departman");
        }

        private void PersonelDepartmanDuzenleme_Load(object sender, EventArgs e)
        {
            Listele(metroGrid1, "Departman");
            Listele(metroGrid2, "Departman");

        }
    }
}