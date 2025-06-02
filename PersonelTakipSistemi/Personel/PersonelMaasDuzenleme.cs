using System;
using System.Data.OleDb;
using System.Data;
using System.Windows.Forms;
using PersonelTakipSistemi.Class;
using MetroFramework.Controls;

namespace PersonelTakipSistemi.Personel
{
    public partial class PersonelMaasDuzenleme : MetroFramework.Forms.MetroForm
    {
        public PersonelMaasDuzenleme()
        {
            InitializeComponent();
        }
        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Veritabani/eh.mdb");
        public void Listele(DataGridView data, string kelime)
        {
            baglanti.Open();
            OleDbDataAdapter da = new OleDbDataAdapter("SELECT * from " + kelime, baglanti); 
            DataTable dt = new DataTable();
            da.Fill(dt);
            data.DataSource = dt;  
            baglanti.Close(); 
        }
        double deger1;
        double deger2;

        public void MesaiBilgileriniHesaplaVeGoster(string tcKimlik, Label mesaiSaatLabel, Label mesaiUcretLabel)
{
    try
    {
        if (baglanti.State != ConnectionState.Open)
            baglanti.Open();

        // Doğru sorgu
        string query = @"
            SELECT 
                Mesai.MesaiBaslangicSuresi, 
                Mesai.MesaiBitisSuresi, 
                Personel.Maas 
            FROM 
                Mesai 
            INNER JOIN 
                Personel ON Mesai.TcKimlik = Personel.TcKimlik
            WHERE 
                Mesai.TcKimlik = @TcKimlik";

        OleDbCommand komut = new OleDbCommand(query, baglanti);
        komut.Parameters.Add("@TcKimlik", OleDbType.VarChar).Value = tcKimlik;

        OleDbDataReader reader = komut.ExecuteReader();

        double toplamMesaiUcreti = 0;
        double toplamMesaiSaat = 0;

        while (reader.Read())
        {
            DateTime baslangic = Convert.ToDateTime(reader["MesaiBaslangicSuresi"]);
            DateTime bitis = Convert.ToDateTime(reader["MesaiBitisSuresi"]);
            double maas = Convert.ToDouble(reader["Maas"]);

            // Saatlik ücret ve mesai katsayısı hesaplama
            double saatlikUcret = maas / 30 / 8; // Aylık maaşı gün ve saate bölerek saatlik ücreti buluyoruz
            double katsayi = 1.5;

            // Mesai süresi ve ücreti hesaplama
            TimeSpan mesaiSuresi = bitis - baslangic;
            double mesaiSaat = (double)mesaiSuresi.TotalHours;
            double mesaiUcreti = mesaiSaat * saatlikUcret * katsayi;

            toplamMesaiSaat += mesaiSaat;
            toplamMesaiUcreti += mesaiUcreti;
        }

        reader.Close();

        // Hesaplanan değerleri etiketlere yazdırma
        mesaiSaatLabel.Text = $"{toplamMesaiSaat:N2} Saat";
        mesaiUcretLabel.Text = $"{toplamMesaiUcreti:N2} TL";
                deger1 = toplamMesaiUcreti;
    }
    catch (Exception ex)
    {
        MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
    finally
    {
        if (baglanti.State == ConnectionState.Open)
            baglanti.Close();
    }
}

        private void PersonelMaasDuzenleme_Load(object sender, EventArgs e)
        {
            Listele(metroGrid1, "Personel");
            Listele(metroGrid2, "Maas");

        }
        Log Log = new Log();
        
        private void metroButton2_Click_1(object sender, EventArgs e)
        {
            DateTime bugun = DateTime.Now;
            int gun = bugun.Day;
            int ay = bugun.Month;
            int yil = bugun.Year;
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand("INSERT INTO OdenenMaas (OdenenMaas,	TcKimlik,	Ad,	Soyad,OdenmeTarihi) values (@1,@2,@3,@4,@5)", baglanti);
            komut.Parameters.AddWithValue("@1", metroGrid1.Rows[metroGrid1.CurrentRow.Index].Cells[15].Value.ToString());
            komut.Parameters.AddWithValue("@2", metroGrid1.Rows[metroGrid1.CurrentRow.Index].Cells[1].Value.ToString());
            komut.Parameters.AddWithValue("@3", metroGrid1.Rows[metroGrid1.CurrentRow.Index].Cells[4].Value.ToString());
            komut.Parameters.AddWithValue("@4", metroGrid1.Rows[metroGrid1.CurrentRow.Index].Cells[5].Value.ToString());
            komut.Parameters.AddWithValue("@5", $"Bugün: {gun}.{ay}.{yil}");
            komut.ExecuteNonQuery();
            baglanti.Close();
            string mesage = metroGrid1.Rows[metroGrid1.CurrentRow.Index].Cells[1].Value.ToString() + " Tc kimlik numaralı," + metroGrid1.Rows[metroGrid1.CurrentRow.Index].Cells[4].Value.ToString() + " " + metroGrid1.Rows[metroGrid1.CurrentRow.Index].Cells[5].Value.ToString() + "Adlı kişiye " + metroGrid1.Rows[metroGrid1.CurrentRow.Index].Cells[15].Value.ToString() + "₺ maaş yatırıldı";
            Log.DenetimKaydi(Form1.Id.ToString(), mesage,baglanti);
            MessageBox.Show("Başarıyla Ödendi !");

        }

        private void metroGrid1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            PersonelTcLabel.Text = metroGrid1.Rows[metroGrid1.CurrentRow.Index].Cells[1].Value.ToString();
            AdLabel.Text = metroGrid1.Rows[metroGrid1.CurrentRow.Index].Cells[3].Value.ToString();
            SoyadLabel.Text = metroGrid1.Rows[metroGrid1.CurrentRow.Index].Cells[4].Value.ToString();
            PersonelMaasLabel.Text = metroGrid1.Rows[metroGrid1.CurrentRow.Index].Cells[15].Value.ToString() + "₺";
            PersonelIbanaLabel.Text = metroGrid1.Rows[metroGrid1.CurrentRow.Index].Cells[14].Value.ToString();
            string personelId = metroGrid1.Rows[metroGrid1.CurrentRow.Index].Cells[0].Value.ToString();
            MesaiBilgileriniHesaplaVeGoster(metroGrid1.Rows[metroGrid1.CurrentRow.Index].Cells[1].Value.ToString(), metroLabel5, MesaiLabel);
            baglanti.Open();

            // Veritabanından brüt maaşı al
            string query = "SELECT Maas FROM Personel WHERE PersonelID = @PersonelID";
            OleDbCommand komut = new OleDbCommand(query, baglanti);
            komut.Parameters.AddWithValue("@PersonelID", personelId);

            // Veriyi okuyarak değişkene ata
            double brutMaas = 0;
            OleDbDataReader reader = komut.ExecuteReader();
            if (reader.Read())
            {
                brutMaas = Convert.ToDouble(reader["Maas"]);
            }

            reader.Close();
            baglanti.Close();

            double tplm = deger1 + brutMaas;
            double sgkOrani = 0.14;
            double issizlikSigortasiOrani = 0.01;
            double damgaVergisiOrani = 0.00759;
            double gelirVergisiOrani = 0.15;

            double sgkKesintisi = brutMaas * sgkOrani;
            double issizlikKesintisi = brutMaas * issizlikSigortasiOrani;
            double gelirVergisiMatrahi = brutMaas - (sgkKesintisi + issizlikKesintisi);
            double gelirVergisi = gelirVergisiMatrahi * gelirVergisiOrani;
            double damgaVergisi = brutMaas * damgaVergisiOrani;
            double toplamKesinti = sgkKesintisi + issizlikKesintisi + gelirVergisi + damgaVergisi;
            double netMaas = brutMaas - toplamKesinti;

            double isverenSgkOrani = 0.205;
            double isverenIssizlikOrani = 0.02;
            double isverenSgkPayi = brutMaas * isverenSgkOrani;
            double isverenIssizlikPayi = brutMaas * isverenIssizlikOrani;
            double toplamIsverenMaliyeti = brutMaas + isverenSgkPayi + isverenIssizlikPayi;

            // Doğru etiket güncellemeleri
            SgkisciLabel.Text = $"{sgkKesintisi:N2} TL";
            GelirVergiLabel.Text = $"{gelirVergisi:N2} TL";
            İsizlikSigortaLabel.Text = $"{issizlikKesintisi:N2} TL";
            DamgaVergiLabel.Text = $"{damgaVergisi:N2} TL";
            ToplamKesintiLabel.Text = $"{toplamKesinti:N2} TL";
            PersonelMaasLabel.Text = $"{brutMaas:N2} TL";
            ToplamMaliyetLabel.Text = $"{toplamIsverenMaliyeti:N2} TL";
            SgkisverenPayiLabel.Text = $"{isverenIssizlikPayi:N2} TL";
            issizlikSigortasiIsverenMaliyetiLabel.Text = $"{isverenSgkPayi:N2} TL";
            metroLabel8.Text = $"{tplm:N2} TL";
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand("UPDATE Personel SET Maas = @Maas WHERE PersonelID = @ID",baglanti);
            komut.Parameters.AddWithValue("@Maas", Convert.ToDouble(GuncelleMaasText.Text));
            komut.Parameters.AddWithValue("@ID", metroGrid2.Rows[metroGrid2.CurrentRow.Index].Cells[1].Value.ToString());
            baglanti.Close();
            string msg = metroGrid2.Rows[metroGrid2.CurrentRow.Index].Cells[1].Value.ToString() + "Tc Kimlik numaralı personelin maaşını ="+GuncelleMaasText.Text+" ₺ olarak güncellendi";
            Log.DenetimKaydi(Form1.Id, msg, baglanti);
            MessageBox.Show("Başarıyla güncellendi");

        }

        private void metroGrid2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            GuncelleAdLabe.Text = metroGrid1.Rows[metroGrid1.CurrentRow.Index].Cells[3].Value.ToString();
            GuncelleSoyadLabel.Text = metroGrid1.Rows[metroGrid1.CurrentRow.Index].Cells[4].Value.ToString();
            GuncelleMaasText.Text = metroGrid1.Rows[metroGrid1.CurrentRow.Index].Cells[15].Value.ToString() + "₺";

        }
    }
}
