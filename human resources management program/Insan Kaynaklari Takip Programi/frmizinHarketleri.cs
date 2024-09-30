using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Insan_Kaynaklari_Takip_Programi
{
    public partial class frmizinHarketleri : Form
    {
        public frmizinHarketleri()
        {
            InitializeComponent();
        }
        izin Izin = new izin();
        private void btnizinTurleri_Click(object sender, EventArgs e)
        {
            frmizinturleri frm = new frmizinturleri();
            frm.ShowDialog();
        }

        private void frmizinHarketleri_Load(object sender, EventArgs e)
        {
            Veritabani.Listele_Ara(dataGridView1,"select izinHarketID,PersonelID,KullaniciID,tur.izinTuru,izinBaslangic,izinBitis," +
                "islem,Aciklama,Tarih,Saat from izinHarketleri i,izinTurleri tur where i.izinTurID=tur.izinTurID");
            Personeller.ComboyaKayitGetir(comboizinTuru);
        }

        private void txtPersonelID_TextChanged(object sender, EventArgs e)
        {
            Primler.PeronelAdSoyadGetir(txtPersonelID,txtAdiSoyadi);
            if (txtPersonelID.Text=="")
            {
                txtAdiSoyadi.Text = "";   
            }
        }
        public void Temizle()
        {

            dateTimeBaslangic.Value = DateTime.Now;
            dateTimeBitis.Value = DateTime.Now;
            Personeller.ComboyaKayitGetir(comboizinTuru);
            foreach (Control item in Controls)
            {
                item.Text = "";
            }
        }
        private void btnEkle_Click(object sender, EventArgs e)
        {

            izin i = new izin();
            i.PERSONELID = int.Parse(txtPersonelID.Text);
            i.KullaniciID = Kullanicilar.kid;
            i.IzinTurID = (int)comboizinTuru.SelectedValue;
            i.IzinBaslangic = dateTimeBaslangic.Value;
            i.IzinBitis = dateTimeBitis.Value;
            i.Islem = i.PERSONELID + "-" + txtAdiSoyadi.Text + "için" + comboizinTuru.Text + "oluşturuldu";
            i.Aciklama = txtAciklama.Text;
            i.Tarih = DateTime.Now;
            i.Saat = DateTime.Now;
            string sql = "insert into izinHarketleri values('"+i.PERSONELID+"','"+i.KullaniciID+"','"+i.IzinTurID+"',@izinBaslangic,@izinBitis," +
                "'"+i.Islem+"','"+i.Aciklama+"',@Tarih,@Saat)";
            SqlCommand komut = new SqlCommand();
            komut.Parameters.Add("@izinBaslangic",SqlDbType.Date).Value = i.IzinBaslangic;
            komut.Parameters.Add("@izinBitis", SqlDbType.Date).Value = i.IzinBitis;
            komut.Parameters.Add("@Tarih", SqlDbType.Date).Value = i.Tarih;
            komut.Parameters.Add("@Saat", SqlDbType.DateTime).Value = i.Saat;
            try
            {
                Veritabani.ESG(komut,sql);
                Temizle();
                MessageBox.Show("İzin kaydı oluşturuldu.","İzin",MessageBoxButtons.OK,MessageBoxIcon.Information);
                Veritabani.Listele_Ara(dataGridView1, "select izinHarketID,PersonelID,KullaniciID,tur.izinTuru,izinBaslangic,izinBitis," +
                "islem,Aciklama,Tarih,Saat from izinHarketleri i,izinTurleri tur where i.izinTurID=tur.izinTurID");
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message,"Uyarı");
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            txtizinHarketID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txtPersonelID.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();

            comboizinTuru.Text =  dataGridView1.CurrentRow.Cells["izinTuru"].Value.ToString();
            dateTimeBaslangic.Text =dataGridView1.CurrentRow.Cells["izinBaslangic"].Value.ToString();
            dateTimeBitis.Text = dataGridView1.CurrentRow.Cells["izinBitis"].Value.ToString();
            txtAciklama.Text = dataGridView1.CurrentRow.Cells["Aciklama"].Value.ToString();

        }
    }
}
