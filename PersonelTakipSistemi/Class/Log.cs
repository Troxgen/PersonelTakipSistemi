using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonelTakipSistemi.Class
{
    internal class Log
    {
        public void DenetimKaydi(string YetkiliId, string Log,OleDbConnection Baglanti)
        {
            DateTime bugun = DateTime.Now;
            int gun = bugun.Day;
            int ay = bugun.Month;
            int yil = bugun.Year;
            Baglanti.Open();
            OleDbCommand komut = new OleDbCommand("INSERT INTO DenetimKaydi (YetkiliID,DenetimKaydi,Tarih) VALUES (@DepartmanAdi,@Aciklama,@Tarih)", Baglanti);
            komut.Parameters.AddWithValue("@YetkiliID", YetkiliId);
            komut.Parameters.AddWithValue("@Log", Log);
            komut.Parameters.AddWithValue("@Tarih", $"{gun}.{ay}.{yil}");

            komut.ExecuteNonQuery();
            Baglanti.Close();
        }
    }
}
