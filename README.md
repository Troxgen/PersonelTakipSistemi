Aşağıda C# ve Microsoft Access (MDB/ACCDB) veritabanı kullanan bir **Personel Takip Sistemi** için sade, teknik ve açıklayıcı bir `README.md` dosyası şablonu yer alıyor. Projenizin yapısına göre özelleştirebilirsiniz.

---

```markdown
# Personel Takip Sistemi

C# (Windows Forms) ve Microsoft Access veritabanı (MDB/ACCDB) kullanılarak geliştirilmiş temel personel takip sistemidir. Bu uygulama sayesinde personel bilgileri kolayca eklenebilir, güncellenebilir, silinebilir ve listelenebilir.

## 🚀 Özellikler

- Personel ekleme, güncelleme ve silme
- Personel listesi görüntüleme
- Access veritabanı üzerinden veri saklama (OLEDB ile bağlantı)
- Basit ve kullanıcı dostu arayüz (Windows Forms)
- Raporlama ve istatistik ekranı (isteğe bağlı)

## 🛠️ Kullanılan Teknolojiler

- C# (.NET Framework)
- Windows Forms
- Microsoft Access Veritabanı (MDB veya ACCDB)
- OLEDB Bağlantısı (`System.Data.OleDb`)

## 🗂️ Proje Yapısı

```

PersonelTakipSistemi/
│
├── bin/
├── Database/
│   └── personel.accdb
├── Forms/
│   ├── AnaForm.cs
│   ├── PersonelEkle.cs
│   ├── PersonelListele.cs
│   └── ...
├── Program.cs
├── App.config
└── README.md

````

## 🧩 Veritabanı Yapısı (`personel.accdb`)

**Tablo Adı: personeller**

| Alan Adı       | Veri Tipi      | Açıklama              |
|----------------|----------------|------------------------|
| id             | AutoNumber     | Birincil Anahtar       |
| ad             | Short Text     | Personel adı           |
| soyad          | Short Text     | Personel soyadı        |
| tc_no          | Short Text     | T.C. Kimlik No (11 hane)|
| telefon        | Short Text     | Telefon numarası       |
| adres          | Long Text      | Adres bilgisi          |
| pozisyon       | Short Text     | Görev/Pozisyon         |
| maas           | Currency       | Aylık maaş             |

## 🔌 Veritabanı Bağlantısı

```csharp
string baglantiYolu = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Database\eh.mdb;";
OleDbConnection baglanti = new OleDbConnection(baglantiYolu);
````

> Not: Projenizde .MDB uzantısı kullanıyorsanız `Microsoft.Jet.OLEDB.4.0` sağlayıcısı tercih edilmelidir.

## 📷 Ekran Görüntüleri

*Ana Sayfa | Personel Ekleme | Listeleme vs.*

> 📎 Projeye örnek ekran görüntüleri ekleyebilirsiniz.

## 📝 Kurulum ve Çalıştırma

1. Bu projeyi ZIP olarak indirin veya klonlayın:

   ```bash
   git clone https://github.com/kullaniciadi/PersonelTakipSistemi.git
   ```

2. Visual Studio ile açın.

3. `Veritabani/eh.mdb` dosyasının proje dizininde olduğundan emin olun.

4. Gerekli NuGet paketlerini kurun (normalde sistem kütüphaneleri yeterlidir).

5. Uygulamayı çalıştırın: `Ctrl + F5`

## ✅ Geliştirici Notları

* Kodlar basit ve anlaşılır şekilde yapılandırılmıştır.
* Geliştirmeye açıktır: Yetkilendirme, departman ekleme, Excel’e aktarma gibi özellikler eklenebilir.

## 🧑‍💻 Geliştirici

**Emirhan Hasırcı**
[GitHub Profiliniz](https://github.com/troxgen)

## 📄 Lisans

Bu proje açık kaynaklıdır ve MIT Lisansı ile lisanslanmıştır.

```

---

İsteğe bağlı olarak:

- Ekran görüntüleri klasörü ekleyebilirsin (`/screenshots/`).
- PDF dokümantasyon ya da örnek veritabanı dosyasını da depo içine dahil edebilirsin.

İstersen bu README’yi PDF formatında da oluşturabilirim.
```
