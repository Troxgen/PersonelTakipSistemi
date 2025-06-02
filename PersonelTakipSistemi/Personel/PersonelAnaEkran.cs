using System;
using System.Data.OleDb;
using System.Windows.Forms;

namespace PersonelTakipSistemi.Personel
{
    public partial class PersonelAnaEkran : MetroFramework.Forms.MetroForm
    {
        public PersonelAnaEkran()
        {
            InitializeComponent();
        }

        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Veritabani/eh.mdb");

        private void PersonelAnaEkran_Load(object sender, EventArgs e)
        {
            try
            {
                baglanti.Open();
                OleDbCommand command = new OleDbCommand("SELECT * FROM RollYetkileri WHERE RolID = @RolID", baglanti);
                command.Parameters.AddWithValue("@RolID", Form1.RolID);

                OleDbDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    DepartmanButton.Visible = Convert.ToBoolean($"{reader["DepartmanDuzenleme"]}");
                    PersonelButton.Visible = Convert.ToBoolean($"{reader["PersonelDuzenleme"]}");
                    DevammsizlkButton.Visible = Convert.ToBoolean($"{reader["DevamsizlikDuzenleme"]}");
                    MaasButton.Visible = Convert.ToBoolean($"{reader["MaasDuzenleme"]}");
                    MesaiButton.Visible = Convert.ToBoolean($"{reader["MesaiDuzenleme"]}");
                    DenetimButton.Visible = Convert.ToBoolean($"{reader["DenetimKaydiGorme"]}");
                    AyarlarButton.Visible = Convert.ToBoolean($"{reader["AyarlariDuzenleme"]}");
                }
                else
                {
                    MessageBox.Show("Yetki bilgisi bulunamadı. Lütfen sistem yöneticisine başvurun.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Hide();
                    Form1 form = new Form1();
                    form.ShowDialog();
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                baglanti.Close();
            }
        }

        private void DevammsizlkButton_Click(object sender, EventArgs e)
        {
            PersonelDevamsizlikDuzenleme pdd = new PersonelDevamsizlikDuzenleme();
            pdd.ShowDialog();
        }

        private void AyarlarButton_Click(object sender, EventArgs e)
        {
            PersonelAyarlar pdd = new PersonelAyarlar();
            pdd.ShowDialog();
        }

        private void MaasButton_Click(object sender, EventArgs e)
        {
            PersonelMaasDuzenleme pdd = new PersonelMaasDuzenleme();
            pdd.ShowDialog();
        }

        private void DepartmanButton_Click(object sender, EventArgs e)
        {
            PersonelDepartmanDuzenleme pdd = new PersonelDepartmanDuzenleme();
            pdd.ShowDialog();
        }

        private void DenetimButton_Click(object sender, EventArgs e)
        {
            PersonelDenetimKaydi pdd = new PersonelDenetimKaydi();
            pdd.ShowDialog();
        }

        private void MesaiButton_Click(object sender, EventArgs e)
        {
            PersonelMesaiDuzenleme pdd = new PersonelMesaiDuzenleme();
            pdd.ShowDialog();
        }

        private void PersonelButton_Click(object sender, EventArgs e)
        {
            PersonelDuzenleme pdd = new PersonelDuzenleme();
            pdd.ShowDialog();
        }
    }
}
