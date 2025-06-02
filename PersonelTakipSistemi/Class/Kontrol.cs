using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace PersonelTakipSistemi
{
   public  class Kontrol
    {
        public bool IbanKontrol(string iban)
        {
            if (string.IsNullOrWhiteSpace(iban) || iban.Length < 15 || iban.Length > 34)
                return false;

            iban = iban.ToUpper().Replace(" ", "");
            string rearranged = iban.Substring(4) + iban.Substring(0, 4);
            string numericIban = "";

            foreach (char c in rearranged)
            {
                numericIban += char.IsLetter(c) ? (c - 'A' + 10).ToString() : c.ToString();
            }

            BigInteger ibanNumber = BigInteger.Parse(numericIban);
            return ibanNumber % 97 == 1;
        }
        public bool TcKimlikKontrol(string tcKimlik)
        {
            if (string.IsNullOrWhiteSpace(tcKimlik) || tcKimlik.Length != 11 || !tcKimlik.All(char.IsDigit))
                return false;

            if (tcKimlik[0] == '0')
                return false;

            int toplam1 = 0;
            int toplam2 = 0;

            for (int i = 0; i < 9; i += 2)
            {
                toplam1 += int.Parse(tcKimlik[i].ToString());
            }

            for (int i = 1; i < 8; i += 2)
            {
                toplam2 += int.Parse(tcKimlik[i].ToString());
            }

            int onuncuHane = (toplam1 * 7 - toplam2) % 10;
            if (onuncuHane != int.Parse(tcKimlik[9].ToString()))
                return false;

            int toplam3 = 0;
            for (int i = 0; i < 10; i++)
            {
                toplam3 += int.Parse(tcKimlik[i].ToString());
            }

            int onbirinciHane = toplam3 % 10;
            if (onbirinciHane != int.Parse(tcKimlik[10].ToString()))
                return false;

            return true;
        }
    }
}
