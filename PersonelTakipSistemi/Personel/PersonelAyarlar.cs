using MetroFramework.Controls;
using PersonelTakipSistemi.Class;
using System;
using System.Data.OleDb;
using System.Windows.Forms;

namespace PersonelTakipSistemi.Personel
{
    public partial class PersonelAyarlar : MetroFramework.Forms.MetroForm
    {
        public PersonelAyarlar()
        {
            InitializeComponent();
        }
        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Veritabani/eh.mdb");
        Log log = new Log();
        public void EskiAyarlar()
        {
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand("SELECT * FROM Ayarlar", baglanti);
            OleDbDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                PazartesiCheckBox.Checked = Convert.ToBoolean(dr["Pazartesi"]);
                SaliCheckBox.Checked = Convert.ToBoolean(dr["Sali"]);
                CarsambaCheckBox.Checked = Convert.ToBoolean(dr["Carsamba"]);
                PersembeCheckBox.Checked = Convert.ToBoolean(dr["Persembe"]);
                CumaCheckBox.Checked = Convert.ToBoolean(dr["Cuma"]);
                CumartesiCheckBox.Checked = Convert.ToBoolean(dr["Cumartesi"]);
                PazarCheckBox.Checked = Convert.ToBoolean(dr["Pazar"]);
                GirisDateTime.Text = (dr["GirisSaati"]).ToString();
                CikisDateTime.Text = (dr["CikisSaati"]).ToString();

            }
            baglanti.Close();
        }
        private void PersonelAyarlar_Load(object sender, EventArgs e)
        {
            EskiAyarlar();
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand("UPDATE Ayarlar SET Pazartesi=@1, Sali=@2, Carsamba=@3, Persembe=@4, Cuma=@5, Cumartesi=@6, Pazar=@7, GirisSaati=@8, CikisSaati=@9, YillikIzinSuresi=@10 WHERE ID = 1", baglanti);
            komut.Parameters.AddWithValue("@1", PazartesiCheckBox.Checked);
            komut.Parameters.AddWithValue("@2", SaliCheckBox.Checked);
            komut.Parameters.AddWithValue("@3", CarsambaCheckBox.Checked);
            komut.Parameters.AddWithValue("@4", PersembeCheckBox.Checked);
            komut.Parameters.AddWithValue("@5", CumaCheckBox.Checked);
            komut.Parameters.AddWithValue("@6", CumartesiCheckBox.Checked);
            komut.Parameters.AddWithValue("@7", PazarCheckBox.Checked);
            komut.Parameters.AddWithValue("@8", GirisDateTime.Value);
            komut.Parameters.AddWithValue("@9", CikisDateTime.Value);
            komut.Parameters.AddWithValue("@10", numericUpDown1.Value.ToString());
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("İşlem Başarı İle Sonuçlandı !");

        }
    }
}
