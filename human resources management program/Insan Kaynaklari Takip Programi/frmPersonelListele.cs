﻿using System;
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
    public partial class frmPersonelListele : Form
    {
        public frmPersonelListele()
        {
            InitializeComponent();
        }

        private void frmPersonelListele_Load(object sender, EventArgs e)
        {
            Personeller.ComboyaKayitGetir(comboDepartman);
            YeniListele();

        }

        private void YeniListele()
        {
            Veritabani.Listele_Ara(dataGridView1, "select p.PersonelID, p.Adi,p.Soyadi,p.Telefon, p.Adres, p.Email," +
                "d.Departman,p.Durumu,p.Maasi,p.GirisTarihi,p.Aciklama from Personeller p,Departmanlar d" +
                " where p.DepartmanID=d.DepartmanID");
            lblToplamKayit.Text = "Toplam" + (dataGridView1.Rows.Count - 1) + "kayıt listelendi.";
            decimal toplammaas = 0;
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                toplammaas += decimal.Parse(dataGridView1.Rows[i].Cells["Maasi"].Value.ToString());
            }
            lblToplamMaas.Text = "Toplam maaş tutarı=" + toplammaas.ToString("0.00") + " TL";

        }

        void Temizle()
        {
            dateTimePickerGirisTarihi.Value = DateTime.Now;
            comboDepartman.Text = "";
            foreach (Control item in Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }
            }
        }
        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            Personeller p = new Personeller();
            p.PERSONELID = int.Parse(txtPersonelID.Text);
            p.Adi = txtAdi.Text;
            p.Soyadi = txtSoyadi.Text;
            p.Telefon = txtTelefon.Text;
            p.Adres = txtAdres.Text;
            p.Email = txtEmail.Text;
            p.DepartmanID = (int)comboDepartman.SelectedValue;
            p.Maasi = decimal.Parse(txtMaasi.Text);
            p.GirisTarihi = dateTimePickerGirisTarihi.Value;
            p.Aciklama = txtAciklama.Text;
            string sorgu = "update personeller set adi='" + p.Adi + "',soyadi='" + p.Soyadi + "',telefon='" + p.Telefon + "', " +
                "adres='" + p.Adres + "',email='" + p.Email + "',departmanID='" + p.DepartmanID + "',maasi=@Maasi,giristarihi=@GirisTarihi,aciklama='" + p.Aciklama + "'  " +
                "where personelID='" + p.PERSONELID + "' ";
            SqlCommand komut = new SqlCommand();
            komut.Parameters.Add("@Maasi", SqlDbType.Decimal).Value = p.Maasi;
            komut.Parameters.Add("@GirisTarihi", SqlDbType.Date).Value = p.GirisTarihi;
            Veritabani.ESG(komut, sorgu);
            Temizle();
            MessageBox.Show("İşlem başarılı.", "Güncelleme", MessageBoxButtons.OK, MessageBoxIcon.Information);
            YeniListele();
        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            Personeller p = new Personeller();
            p.PERSONELID = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
            //string sorgu = "delete from personeller where PersonelID='" + p.PERSONELID + "'";
            //SqlCommand komut = new SqlCommand();
            //Veritabani.ESG(komut, sorgu);
            string sorgu2 = "update personeller set durumu='Pasif' where personelID='" + p.PERSONELID + "'";
            SqlCommand komut2 = new SqlCommand();
            Veritabani.ESG(komut2, sorgu2);
            Temizle();
            MessageBox.Show("İşlem başarılı.", "Silme", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            YeniListele();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txtPersonelID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txtAdi.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtSoyadi.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            txtTelefon.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            txtAdres.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            txtEmail.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            comboDepartman.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            txtMaasi.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
            dateTimePickerGirisTarihi.Value = DateTime.Parse(dataGridView1.CurrentRow.Cells[9].Value.ToString());
            txtAciklama.Text = dataGridView1.CurrentRow.Cells[10].Value.ToString();

        }

        private void txtPersonelIDAra_TextChanged(object sender, EventArgs e)
        {
            Veritabani.Listele_Ara(dataGridView1, "select p.PersonelID, p.Adi,p.Soyadi,p.Telefon, p.Adres, p.Email," +
                "d.Departman,p.Durumu,p.Maasi,p.GirisTarihi,p.Aciklama from Personeller p,Departmanlar d" +
                " where p.DepartmanID=d.DepartmanID and PersonelID like '%" + txtPersonelIDAra.Text + "%'");
        }

        private void txtPersonelAdiAra_TextChanged(object sender, EventArgs e)
        {
            Veritabani.Listele_Ara(dataGridView1, "select p.PersonelID, p.Adi,p.Soyadi,p.Telefon, p.Adres, p.Email," +
                 "d.Departman,p.Durumu,p.Maasi,p.GirisTarihi,p.Aciklama from Personeller p,Departmanlar d" +
                 " where p.DepartmanID=d.DepartmanID and Adi like '%" + txtPersonelAdiAra.Text + "%'");
        }

        private void txtPersonelSoyadAra_TextChanged(object sender, EventArgs e)
        {
            Veritabani.Listele_Ara(dataGridView1, "select p.PersonelID, p.Adi,p.Soyadi,p.Telefon, p.Adres, p.Email," +
                "d.Departman,p.Durumu,p.Maasi,p.GirisTarihi,p.Aciklama from Personeller p,Departmanlar d" +
                " where p.DepartmanID=d.DepartmanID and Soyadi like '%" + txtPersonelSoyadAra.Text + "%'");
        }

        private void txtPersonelTelefonAra_TextChanged(object sender, EventArgs e)
        {
            Veritabani.Listele_Ara(dataGridView1, "select p.PersonelID, p.Adi,p.Soyadi,p.Telefon, p.Adres, p.Email," +
                "d.Departman,p.Durumu,p.Maasi,p.GirisTarihi,p.Aciklama from Personeller p,Departmanlar d" +
                " where p.DepartmanID=d.DepartmanID and Telefon like '%" + txtPersonelTelefonAra.Text + "%'");
        }

        private void dateTimePickerG_Tarihi_ValueChanged(object sender, EventArgs e)
        {
            Personeller.TariheGoreAra(dateTimePickerG_Tarihi, dataGridView1);
        }
    }
}
