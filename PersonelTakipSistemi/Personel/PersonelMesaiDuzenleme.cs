using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;
using PersonelTakipSistemi.Class;

namespace PersonelTakipSistemi.Personel
{
    public partial class PersonelMesaiDuzenleme : MetroFramework.Forms.MetroForm
    {
        public PersonelMesaiDuzenleme()
        {
            InitializeComponent();
        }

        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Veritabani/eh.mdb");
        Log Log = new Log();

        public void Listele(DataGridView data, string tabloAdi)
        {
            try
            {
                baglanti.Open();
                OleDbDataAdapter da = new OleDbDataAdapter($"SELECT * FROM {tabloAdi}", baglanti);
                DataTable dt = new DataTable();
                da.Fill(dt);
                data.DataSource = dt;
                baglanti.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        
        }

        private void PersonelMesaiDuzenleme_Load(object sender, EventArgs e)
        {
            // DateTimePicker Formatını Ayarla
       
            Listele(metroGrid2, "Personel");
            Listele(metroGrid3, "Personel");
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            try
            {
                baglanti.Open();
                string query = "INSERT INTO Mesai (TcKimlik, Ad, Soyad, MesaiBaslangicSuresi, MesaiBitisSuresi) VALUES (@TcKimlik, @Ad, @Soyad, @Baslangic, @Bitis)";
                OleDbCommand komut = new OleDbCommand(query, baglanti);
                komut.Parameters.AddWithValue("@TcKimlik", metroGrid3.CurrentRow.Cells["TcKimlik"].Value.ToString());
                komut.Parameters.AddWithValue("@Ad", metroGrid3.CurrentRow.Cells["Ad"].Value.ToString());
                komut.Parameters.AddWithValue("@Soyad", metroGrid3.CurrentRow.Cells["Soyad"].Value.ToString());
                komut.Parameters.AddWithValue("@Baslangic", dateTimePicker1.Value);
                komut.Parameters.AddWithValue("@Bitis", dateTimePicker2.Value);
                komut.ExecuteNonQuery();
                baglanti.Close();
                Log.DenetimKaydi(Form1.Id, "Mesai bilgisi eklendi", baglanti);
                Listele(metroGrid3, "Personel");
                MessageBox.Show("Mesai bilgisi başarıyla eklendi!");

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            try
            {
                baglanti.Open();
                string query = @"
                    UPDATE Mesai
                    SET 
                        MesaiBitisSuresi = @Bitis, 
                        Ad = @Ad, 
                        Soyad = @Soyad, 
                        TcKimlik = @TcKimlik
                    WHERE MesaiID = @MesaiID";

                OleDbCommand komut = new OleDbCommand(query, baglanti);

                komut.Parameters.AddWithValue("@Bitis", dateTimePicker1.Value);
                komut.Parameters.AddWithValue("@Ad", metroGrid2.CurrentRow.Cells["Ad"].Value.ToString());
                komut.Parameters.AddWithValue("@Soyad", metroGrid2.CurrentRow.Cells["Soyad"].Value.ToString());
                komut.Parameters.AddWithValue("@TcKimlik", metroGrid2.CurrentRow.Cells["TcKimlik"].Value.ToString());
                komut.Parameters.AddWithValue("@MesaiID", metroGrid2.CurrentRow.Cells["MesaiID"].Value.ToString());

                komut.ExecuteNonQuery();
                baglanti.Close();

                Log.DenetimKaydi(Form1.Id, "Mesai bilgisi güncellendi", baglanti);

                Listele(metroGrid2, "Mesai");
                MessageBox.Show("Mesai bilgisi başarıyla güncellendi!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }

        private void metroGrid3_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (metroGrid3.CurrentRow != null)
            {
                AdLabel.Text = metroGrid3.CurrentRow.Cells["Ad"].Value.ToString();
                SoyadLabel.Text = metroGrid3.CurrentRow.Cells["Soyad"].Value.ToString();
            }
            else
            {
                MessageBox.Show("Satır seçimi yapılmadı!");
            }
        }

        private void metroGrid2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (metroGrid2.CurrentRow != null && metroGrid2.CurrentRow.Cells["MesaiBaslangicSuresi"].Value != null)
            {
                GuncelleLabel.Text = metroGrid2.CurrentRow.Cells["Ad"].Value.ToString();
                GuncelleSoyadLabel.Text = metroGrid2.CurrentRow.Cells["Soyad"].Value.ToString();

                if (DateTime.TryParse(metroGrid2.CurrentRow.Cells["MesaiBaslangicSuresi"].Value.ToString(), out DateTime parsedDate))
                {
                    dateTimePicker1.Value = parsedDate;
                }
                else
                {
                    MessageBox.Show("Geçersiz tarih formatı!");
                }
            }
            else
            {
                MessageBox.Show("Seçili satır veya hücre bulunamadı!");
            }
        }
    }
}
