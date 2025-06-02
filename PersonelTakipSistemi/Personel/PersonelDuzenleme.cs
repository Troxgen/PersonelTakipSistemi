using System;
using System.Data;
using System.Data.OleDb;

using System.Windows.Forms;

namespace PersonelTakipSistemi.Personel
{
    public partial class PersonelDuzenleme : MetroFramework.Forms.MetroForm
    {
        // Constructor: Formun başlatılmasını sağlar.
        public PersonelDuzenleme()
        {
            InitializeComponent();
        }

        // OleDbConnection ile Access veritabanına bağlanır.
        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Veritabani/eh.mdb");

        // Kontrol sınıfı, doğrulama işlemleri için tanımlanmıştır.
        Kontrol kontrol = new Kontrol();

        // Doldur Metodu: Departman verilerini metroComboBox1 kontrolüne doldurur.
        public void Doldur()
        {
            baglanti.Open();  // Veritabanına bağlantı açılır.
            metroComboBox1.Items.Clear();  // ComboBox içerisindeki öğeler temizlenir.
            OleDbCommand komut = new OleDbCommand("SELECT * FROM Departman", baglanti);  // Departman tablosundan veri çeker.
            OleDbDataReader dr = komut.ExecuteReader();
            while (dr.Read())  // Okunan verileri ComboBox'a ekler.
            {
                metroComboBox1.Items.Add(dr["DepartmanAdi"]);
            }
            baglanti.Close();  // Bağlantı kapatılır.
        }

        // Temizle Metodu: Personel bilgilerinin tüm alanlarını sıfırlar.
        public void Temizle()
        {
            tckimlikTextBox.Clear();  // TC Kimlik alanı temizlenir.
            sicilnoTextBox.Clear();  // Sicil No alanı temizlenir.
            adresTextBox.Clear();  // Adres alanı temizlenir.
            soyadTextBox.Clear();  // Soyad alanı temizlenir.
            emailTextBox.Clear();  // Email alanı temizlenir.
            dogumtarihiTextBox = null;  // Doğum tarihi alanı null'a atanır.
            dogumyeriTextBox.Clear();  // Doğum yeri alanı temizlenir.
            adresTextBox.Clear();  // Adres alanı temizlenir.
            ceptelefonuTextBox.Clear();  // Cep telefonu alanı temizlenir.
            evtelefonuTextBox.Clear();  // Ev telefonu alanı temizlenir.
            askerlikdurumuTextBox.Text = null;  // Askerlik durumu alanı null'a atanır.
            isebaslamatarihiTextBox.Text = null;  // İşe başlama tarihi alanı null'a atanır.
            ibanTextBox.Clear();  // IBAN alanı temizlenir.
            maasTextBox.Clear();  // Maaş alanı temizlenir.
            yolyardimiTextBox.Text = null;  // Yol yardımı alanı null'a atanır.
            yemekyardimiTextBox.Text = null;  // Yemek yardımı alanı null'a atanır.
        }

        // Listele Metodu: Belirtilen DataGridView'e personel verilerini doldurur.
        public void Listele(DataGridView data, string kelime)
        {
            baglanti.Open();  // Veritabanına bağlantı açılır.
            OleDbDataAdapter da = new OleDbDataAdapter("SELECT * from " + kelime, baglanti);  // Veritabanından veri çekilir.
            DataTable dt = new DataTable();
            da.Fill(dt);  // Çekilen veriler DataTable'a doldurulur.
            data.DataSource = dt;  // DataGridView'e veri atanır.
            baglanti.Close();  // Bağlantı kapatılır.
        }

        // Personel ekleme butonu
        private void metroButton1_Click(object sender, EventArgs e)
        {
          
            // Askerlik durumu alanının boş olup olmadığını kontrol eder. 
            string SonDeger = askerlikdurumuTextBox == null || string.IsNullOrWhiteSpace(askerlikdurumuTextBox.Text) ? " " : askerlikdurumuTextBox.Text;

            try
            {
                baglanti.Open();  // Veritabanına bağlantı açılır.
                OleDbCommand komut = new OleDbCommand("INSERT INTO Personel (TcKimlik, SicilNo, Ad, Soyad, Email, DogumYeri, DogumTarihi, Cinsiyet, Adres, CepTelefonu, EvTelefonu, AskerlikDurumu, IseBaslamaTarihi, IbanNo, Maas, YolYardimi, YemekYardimi,Departman) VALUES (@TcKimlik, @SicilNo, @Ad, @Soyad, @Email, @DogumYeri, @DogumTarihi, @Cinsiyet, @Adres, @CepTelefonu, @EvTelefonu, @AskerlikDurumu, @IseBaslamaTarihi, @IbanNo, @Maas, @YolYardimi, @YemekYardimi,@Departman)", baglanti);
                // Parametreler eklenir.
                komut.Parameters.AddWithValue("@TcKimlik", tckimlikTextBox.Text);
                komut.Parameters.AddWithValue("@SicilNo", sicilnoTextBox.Text);
                komut.Parameters.AddWithValue("@Ad", adTextBox.Text);
                komut.Parameters.AddWithValue("@Soyad", soyadTextBox.Text);
                komut.Parameters.AddWithValue("@Email", emailTextBox.Text);
                komut.Parameters.AddWithValue("@DogumYeri", dogumyeriTextBox.Text);
                komut.Parameters.AddWithValue("@DogumTarihi", dogumtarihiTextBox.Text);
                komut.Parameters.AddWithValue("@Cinsiyet", cinsiyetTextBox.Text);
                komut.Parameters.AddWithValue("@Adres", adresTextBox.Text);
                komut.Parameters.AddWithValue("@CepTelefonu", ceptelefonuTextBox.Text);
                komut.Parameters.AddWithValue("@EvTelefonu", evtelefonuTextBox.Text);
                komut.Parameters.AddWithValue("@AskerlikDurumu", SonDeger);
                komut.Parameters.AddWithValue("@IseBaslamaTarihi", isebaslamatarihiTextBox.Text);
                komut.Parameters.AddWithValue("@IbanNo", ibanTextBox.Text);
                komut.Parameters.AddWithValue("@Maas", Convert.ToDouble(maasTextBox.Text));
                komut.Parameters.AddWithValue("@YolYardimi", yolyardimiTextBox.Text);
                komut.Parameters.AddWithValue("@YemekYardimi", yemekyardimiTextBox.Text);
                komut.Parameters.AddWithValue("@Departman", metroComboBox1.Text);

                komut.ExecuteNonQuery();  // Komut veritabanında çalıştırılır.
                MessageBox.Show("Kayıt Başarılı!");  // Başarılı kayıt mesajı verilir.
                Temizle();  // Tüm alanlar sıfırlanır.
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}");  // Hata durumunda mesaj gösterilir.
            }
            finally
            {
                if (baglanti.State == System.Data.ConnectionState.Open)
                    baglanti.Close();  // Bağlantı kapatılır.
                Listele(metroGrid2, "Personel");  // Personel verileri güncellenir.
            }
        }

        // ComboBox'daki seçime göre Askerlik durumu alanını gösterir ya da gizler.
        private void metroComboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cinsiyetTextBox.Text == "Erkek")
            {
                metroLabel12.Visible = true;  // Askerlik durumu alanı görünür.
                askerlikdurumuTextBox.Visible = true;  // Askerlik durumu alanı görünür.
            }
            else
            {
                metroLabel12.Visible = false;  // Askerlik durumu alanı görünmez.
                askerlikdurumuTextBox.Visible = false;  // Askerlik durumu alanı görünmez.
            }
        }

        // TC Kimlik doğrulama
        private void metroTextBox1_Leave(object sender, EventArgs e)
        {
            if (!kontrol.TcKimlikKontrol(tckimlikTextBox.Text.ToString()))
            {
                MessageBox.Show("Tc Kimlik Numaranız Hatalı ! !", "Doğrulama", MessageBoxButtons.OK, MessageBoxIcon.Error);  // Geçersiz TC Kimlik uyarısı gösterilir.
            }
        }

        // IBAN doğrulama
        private void metroTextBox14_Leave(object sender, EventArgs e)
        {
            //if (!kontrol.IbanKontrol(ibanTextBox.Text.ToString()))
            //{
            //    MessageBox.Show("Iban Numaranız Hatalı ! !", "Doğrulama", MessageBoxButtons.OK, MessageBoxIcon.Error);  // Geçersiz IBAN uyarısı gösterilir.
            //}
        }

        // Verileri güncelleme
        private void metroButton2_Click(object sender, EventArgs e)
        {
           
        }

        private void metroButton3_Click(object sender, EventArgs e)
        {
            try
            {
                OleDbCommand komut = new OleDbCommand("UPDATE Personel SET SicilNo=@SicilNo, Ad=@Ad, Soyad=@Soyad, Email=@Email, DogumYeri=@DogumYeri, DogumTarihi=@DogumTarihi, Cinsiyet=@Cinsiyet, Adres=@Adres, CepTelefonu=@CepTelefonu, EvTelefonu=@EvTelefonu, AskerlikDurumu=@AskerlikDurumu, IseBaslamaTarihi=@IseBaslamaTarihi, IbanNo=@IbanNo, Maas=@Maas, YolYardimi=@YolYardimi, YemekYardimi=@YemekYardimi WHERE TcKimlik=@TcKimlik", baglanti);
                komut.Parameters.AddWithValue("@TcKimlik", tckimlikTextBox.Text);
                komut.Parameters.AddWithValue("@SicilNo", sicilnoTextBox.Text);
                komut.Parameters.AddWithValue("@Ad", adTextBox.Text);
                komut.Parameters.AddWithValue("@Soyad", soyadTextBox.Text);
                komut.Parameters.AddWithValue("@Email", emailTextBox.Text);
                komut.Parameters.AddWithValue("@DogumYeri", dogumyeriTextBox.Text);
                komut.Parameters.AddWithValue("@DogumTarihi", dogumtarihiTextBox.Text);
                komut.Parameters.AddWithValue("@Cinsiyet", cinsiyetTextBox.Text);
                komut.Parameters.AddWithValue("@Adres", adresTextBox.Text);
                komut.Parameters.AddWithValue("@CepTelefonu", ceptelefonuTextBox.Text);
                komut.Parameters.AddWithValue("@EvTelefonu", evtelefonuTextBox.Text);
                komut.Parameters.AddWithValue("@AskerlikDurumu", askerlikdurumuTextBox.Text);
                komut.Parameters.AddWithValue("@IseBaslamaTarihi", isebaslamatarihiTextBox.Text);
                komut.Parameters.AddWithValue("@IbanNo", ibanTextBox.Text);
                komut.Parameters.AddWithValue("@Maas", Convert.ToDecimal(maasTextBox.Text));
                komut.Parameters.AddWithValue("@YolYardimi", yolyardimiTextBox.Text);
                komut.Parameters.AddWithValue("@YemekYardimi", yemekyardimiTextBox.Text);
                baglanti.Open();
                komut.ExecuteNonQuery();  // Güncelleme işlemi yapılır.
                MessageBox.Show("Kayıt Başarıyla Güncellendi!");  // Başarılı güncelleme mesajı gösterilir.
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);  // Hata mesajı gösterilir.
            }
            finally
            {
                baglanti.Close();  // Bağlantı kapatılır.
                Listele(metroGrid2, "Personel");  // Güncellenmiş veriler listeye eklenir.
            }
        }

      

        private void metroGrid2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            guncelleTckimlikTextBox.Text = metroGrid2.Rows[metroGrid2.CurrentRow.Index].Cells[0].Value.ToString();
            guncelleSicilnoTextbox.Text = metroGrid2.Rows[metroGrid2.CurrentRow.Index].Cells[1].Value.ToString();
            guncelleAdTextbox.Text = metroGrid2.Rows[metroGrid2.CurrentRow.Index].Cells[2].Value.ToString();
            guncelleSoyadTextbox.Text = metroGrid2.Rows[metroGrid2.CurrentRow.Index].Cells[3].Value.ToString();
            guncelleEmailTextbox.Text = metroGrid2.Rows[metroGrid2.CurrentRow.Index].Cells[4].Value.ToString();
            guncelleDogumYeriTextbox.Text = metroGrid2.Rows[metroGrid2.CurrentRow.Index].Cells[5].Value.ToString();
           // guncelleDogumTarihiTextbox.Value = metroGrid2.Rows[metroGrid2.CurrentRow.Index].Cells[6].Value.ToString();
            guncelleCinsiyetTextbox.Text = metroGrid2.Rows[metroGrid2.CurrentRow.Index].Cells[7].Value.ToString();
            guncelleAdresTextbox.Text = metroGrid2.Rows[metroGrid2.CurrentRow.Index].Cells[8].Value.ToString();
            guncelleCepTelefonuTextbox.Text = metroGrid2.Rows[metroGrid2.CurrentRow.Index].Cells[9].Value.ToString();
            guncelleEvTelefonuTextbox.Text = metroGrid2.Rows[metroGrid2.CurrentRow.Index].Cells[10].Value.ToString();
            guncelleAskerlikDurumuTextbox.Text = metroGrid2.Rows[metroGrid2.CurrentRow.Index].Cells[11].Value.ToString();
            guncelleIbanTextbox.Text = metroGrid2.Rows[metroGrid2.CurrentRow.Index].Cells[13].Value.ToString();
            guncelleMaasTextbox.Text = metroGrid2.Rows[metroGrid2.CurrentRow.Index].Cells[14].Value.ToString();
            guncelleYolYardimiTextbox.Text = metroGrid2.Rows[metroGrid2.CurrentRow.Index].Cells[15].Value.ToString();
            guncelleYemekYardimiiTextbox.Text = metroGrid2.Rows[metroGrid2.CurrentRow.Index].Cells[16].Value.ToString();
        }

        private void PersonelDuzenleme_Load(object sender, EventArgs e)
        {
            Doldur();
            Listele(metroGrid2, "Personel");
            DateTime today = DateTime.Now;

            // Tarihi istediğiniz formatta düzenle
            string formattedDate = today.ToString("MM/dd/yyyy"); // Ay, Gün, Yıl formatında

            // MetroLabel'e tarihi yazdır
            isebaslamatarihiTextBox.Text = formattedDate;

        }

        private void ibanTextBox_Click(object sender, EventArgs e)
        {

        }
    }
}
