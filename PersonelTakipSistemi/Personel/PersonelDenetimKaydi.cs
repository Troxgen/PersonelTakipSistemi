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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace PersonelTakipSistemi.Personel
{
    public partial class PersonelDenetimKaydi : MetroFramework.Forms.MetroForm
    {
        public PersonelDenetimKaydi()
        {
            InitializeComponent();
        }
        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Veritabani/eh.mdb");

        private void LoadDataToListView()
        {
            baglanti.Open();
            string query = "SELECT Kimlik, YetkiliID, DenetimKaydi, Tarih FROM DenetimKaydi";
            OleDbCommand command = new OleDbCommand(query, baglanti);
            OleDbDataReader reader = command.ExecuteReader();

            metroListView1.Items.Clear(); // ListView temizlenir

            while (reader.Read())
            {
                // Satır oluştur ve sütunları ekle
                ListViewItem item = new ListViewItem(reader["Kimlik"].ToString()); // İlk sütun
                item.SubItems.Add(reader["YetkiliID"].ToString()); // İkinci sütun
                item.SubItems.Add(reader["DenetimKaydi"].ToString()); // Üçüncü sütun
                item.SubItems.Add(Convert.ToDateTime(reader["Tarih"]).ToString("yyyy-MM-dd")); // Dördüncü sütun

                metroListView1.Items.Add(item); // ListView'e satırı ekle
            }

            reader.Close();
            baglanti.Close();
        }

        private void SetupListView()
        {
            // ListView Ayarları
            metroListView1.View = View.Details; // Detay görünüm
            metroListView1.FullRowSelect = true; // Tüm satırı seçebilme
            metroListView1.GridLines = true; // Izgara çizgileri

            // Sütunlar Ekle
            metroListView1.Columns.Add("Kimlik", 200); // Sütun genişlikleri belirlenebilir
            metroListView1.Columns.Add("Yetkili ID", 200);
            metroListView1.Columns.Add("Denetim Kaydı", 430);
            metroListView1.Columns.Add("Tarih", 200);
        }

        private void PersonelDenetimKaydi_Load(object sender, EventArgs e)
        {
            SetupListView();
            LoadDataToListView();
        }
    }
}
