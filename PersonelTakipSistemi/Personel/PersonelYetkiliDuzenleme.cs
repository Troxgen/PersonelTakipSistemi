using PersonelTakipSistemi.Class;
using System;
using System.Data.OleDb;
using System.Windows.Forms;

namespace PersonelTakipSistemi.Personel
{
    public partial class PersonelYetkiliDuzenleme : MetroFramework.Forms.MetroForm
    {
        public PersonelYetkiliDuzenleme()
        {
            InitializeComponent();
        }
        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Veritabani/eh.mdb");
        Log Log = new Log();
        public void Doldur()
        {
            baglanti.Open();
            metroComboBox1.Items.Clear();
            OleDbCommand komut = new OleDbCommand("SELECT * FROM RollYetkileri", baglanti);
            OleDbDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                metroComboBox1.Items.Add(dr["YetkiAdi"]);
            }
            baglanti.Close();
       
        }
        private void PersonelYetkiliDuzenleme_Load(object sender, EventArgs e)
        {
            Doldur();
        }

        private void metroButton2_Click_1(object sender, EventArgs e)
        {
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand("UPDATE  * From RollYetkileri YetkiAdi = @1,DepartmanDuzenleme=@2,PersonelDuzenleme=@3,DevamsizlikDuzenleme=@4,MaasDuzenleme=@5,MesaiDuzenleme=@6,DenetimKaydiGorme=@7,AyarlariDuzenleme=@8 wehere RolID = @9", baglanti);
            komut.Parameters.AddWithValue("@1", metroComboBox1.Text);
            komut.Parameters.AddWithValue("@2", GuncelleDepartmanDuzenleme.Checked);
            komut.Parameters.AddWithValue("@3", GuncellePersonelDuzenleme.Checked);
            komut.Parameters.AddWithValue("@4", GuncelleDevamsizlikDuzenleme.Checked);
            komut.Parameters.AddWithValue("@5", GuncelleMaasDuzenleme.Checked);
            komut.Parameters.AddWithValue("@6", GuncelleMesaiDuzenleme.Checked);
            komut.Parameters.AddWithValue("@7", GuncelleDenetimDuzenleme.Checked);
            komut.Parameters.AddWithValue("@8", GuncelleAyarlarDuzenleme.Checked);
            komut.Parameters.AddWithValue("@9", GuncelleYetkiDuzenleme.Checked);

            komut.ExecuteNonQuery();
            baglanti.Close();
            string mesage = metroComboBox1.Text+" Adlı Roll başarıla güncellendi";
            Log.DenetimKaydi(Form1.Id.ToString(), mesage, baglanti);
            MessageBox.Show("Başarıyla Güncellendi Edildi! ");
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand("INSERT into  * From (YetkiAdi,DepartmanDuzenleme,PersonelDuzenleme,DevamsizlikDuzenleme,MaasDuzenleme,MesaiDuzenleme,DenetimKaydiGorme,AyarlariDuzenleme)values(@1,@2,@3,@4,@5,@6,@7,@8,@9) ", baglanti);
            komut.Parameters.AddWithValue("@1", RolAdiTextBox.Text);
            komut.Parameters.AddWithValue("@2", DepartmanDuzenleme.Checked);
            komut.Parameters.AddWithValue("@3", PersonelDuzenleme.Checked);
            komut.Parameters.AddWithValue("@4", DevamsizlikDuzenleme.Checked);
            komut.Parameters.AddWithValue("@5", MaasDuzenleme.Checked);
            komut.Parameters.AddWithValue("@6", MesaiDuzenleme.Checked);
            komut.Parameters.AddWithValue("@7", DenetimDuzenleme.Checked);
            komut.Parameters.AddWithValue("@8", AyarlarDuzenleme.Checked);
            komut.Parameters.AddWithValue("@9", YetkiDuzenleme.Checked);
            komut.ExecuteNonQuery();
            baglanti.Close();
            string mesage = RolAdiTextBox.Text + " Adlı Roll başarıla eklendi";
            Log.DenetimKaydi(Form1.Id.ToString(), mesage, baglanti);
            MessageBox.Show("Başarıyla Kayıt Edildi! ");

        }


        private void metroComboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand("SELECT * FROM RollYetkileri WHERE YetkiAdi = @YetkiAdi", baglanti);
            komut.Parameters.AddWithValue("@YetkiAdi", metroComboBox1.SelectedItem.ToString());

            OleDbDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                GuncelleDepartmanDuzenleme.Checked = Convert.ToBoolean(dr["DepartmanDuzenleme"]);
                GuncellePersonelDuzenleme.Checked = Convert.ToBoolean(dr["PersonelDuzenleme"]);
                GuncelleDevamsizlikDuzenleme.Checked = Convert.ToBoolean(dr["DevamsizlikDuzenleme"]);
                GuncelleMaasDuzenleme.Checked = Convert.ToBoolean(dr["MaasDuzenleme"]);
                GuncelleMesaiDuzenleme.Checked = Convert.ToBoolean(dr["MesaiDuzenleme"]);
                GuncelleDenetimDuzenleme.Checked = Convert.ToBoolean(dr["DenetimKaydiGorme"]);
                GuncelleAyarlarDuzenleme.Checked = Convert.ToBoolean(dr["AyarlariDuzenleme"]);
                GuncelleYetkiDuzenleme.Checked = Convert.ToBoolean(dr["YetkiDuzenleme"]);
            }
            baglanti.Close();
        }

        private void metroButton3_Click(object sender, EventArgs e)
        {
            OleDbCommand komut = new OleDbCommand("DELETE FROM RollYetkileri WHERE YetkiAdi = @YetkiAdi", baglanti);
            komut.Parameters.AddWithValue("@YetkiAdi", metroComboBox1.Text); // rolId değişkenini tanımlayın veya kullanın

            baglanti.Open();
            int rowsAffected = komut.ExecuteNonQuery();
            baglanti.Close();
            string mesage = metroComboBox1.Text + " Adlı Roll başarıla silindi";
            Log.DenetimKaydi(Form1.Id.ToString(), mesage, baglanti);
            MessageBox.Show(metroComboBox1.Text + " Adlı Roll başarıla silindi");

        }
    }
}
