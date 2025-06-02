using MetroFramework.Controls;
using PersonelTakipSistemi.Class;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PersonelTakipSistemi.Personel
{
    public partial class PersonelDevamsizlikDuzenleme : MetroFramework.Forms.MetroForm
    {
        public PersonelDevamsizlikDuzenleme()
        {
            InitializeComponent();
        }
        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Veritabani/eh.mdb");
        Log Log = new Log();
        public void Listele(DataGridView data, string kelime)
        {
            baglanti.Open();
            OleDbDataAdapter da = new OleDbDataAdapter("SELECT  * from " + kelime, baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);
            data.DataSource = dt;

            baglanti.Close();

        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand("insert into Devamsizlik (DevamasizlikTuru,DevamsizlikTarihi,DevamsizlikBitisTarihi,PersonelID)values(@1,@2,@3,@4)", baglanti);
            komut.Parameters.AddWithValue("@1", DevamsizlikComboBox.Text);
            komut.Parameters.AddWithValue("@2", BaslangicTarihiComboBox.Text);
            komut.Parameters.AddWithValue("@3", BitisTarihiComboBox.Text);
            komut.Parameters.AddWithValue("@4", metroGrid1.Rows[metroGrid1.CurrentRow.Index].Cells[0].Value.ToString());
            komut.ExecuteReader();
            baglanti.Close();
            string msg = metroGrid1.Rows[metroGrid1.CurrentRow.Index].Cells[1].Value.ToString()+ " Tc kimlik numaralı personele devamsizlik işlemi yapıldı.";
            Log.DenetimKaydi(Form1.Id, msg, baglanti);
            MessageBox.Show("İşlem Başarı İle Sonuçlandı !");
        }

        private void PersonelDevamsizlikDuzenleme_Load(object sender, EventArgs e)
        {
            Listele(metroGrid1, "Personel");
            Listele(metroGrid2, "Devamsizlik");
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand("Update * from ", baglanti);
            komut.Parameters.AddWithValue("@1", DevamsizlikComboBox.Text);
            komut.Parameters.AddWithValue("@2", BaslangicTarihiComboBox.Text);
            komut.Parameters.AddWithValue("@3", BitisTarihiComboBox.Text);
            baglanti.Close();
            string msg = metroGrid2.Rows[metroGrid2.CurrentRow.Index].Cells[1].Value.ToString() + "Tc kimlik numaralı personele devamsizlik işlemi yapıldı.";
            Log.DenetimKaydi(Form1.Id, msg, baglanti);
            MessageBox.Show("İşlem Başarı İle Sonuçlandı !");

        }

        private void metroGrid1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            metroLabel4.Text = metroGrid1.CurrentRow.Cells[3].Value.ToString();
            metroLabel3.Text = metroGrid1.CurrentRow.Cells[4].Value.ToString();
        }
    }
}
