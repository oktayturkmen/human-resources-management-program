﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Insan_Kaynaklari_Takip_Programi
{
    public partial class frmMesailer : Form
    {
        public frmMesailer()
        {
            InitializeComponent();
        }

        private void frmMesailer_Load(object sender, EventArgs e)
        {
            int yil = DateTime.Now.Year;
            for (int i = yil; i >= yil - 5; i--)
            {
                comboYil.Items.Add(i);

            }
            Veritabani.Listele_Ara(dataGridView1, "select *from Mesailer");
        }

        private void txtPersonelID_TextChanged(object sender, EventArgs e)
        {
            Primler.PeronelAdSoyadGetir(txtPersonelID,txtAdiSoyadi);
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMesaiID.Text = dataGridView1.CurrentRow.Cells["MesaiID"].Value.ToString();

            txtPersonelID.Text = dataGridView1.CurrentRow.Cells["PersonelID"].Value.ToString(); 
            txtMesaiSaatUcreti.Text = dataGridView1.CurrentRow.Cells["MesaiSaatUcreti"].Value.ToString();
            txtAciklama.Text = dataGridView1.CurrentRow.Cells["Aciklama"].Value.ToString();


            string baslangic =dataGridView1.CurrentRow.Cells["BaslangicSaati"].Value.ToString(); 
            string bitis= dataGridView1.CurrentRow.Cells["BitisSaati"].Value.ToString();
            string donem= dataGridView1.CurrentRow.Cells["Donem"].Value.ToString();

            dateTimeBaslangic.Text = baslangic.Substring(0, 10);
            maskedtxtBaslangic.Text = baslangic.Substring(11); 
            dateTimeBitis.Text= bitis.Substring(0, 10); 
            maskedtxtBitis.Text = bitis.Substring(11);
            int say= donem.IndexOf("/");
            comboAy.Text = donem.Substring(0, say); 
            comboYil.Text = donem.Substring(say + 1);


        }

        private void btnPersonelMesaileri_Click(object sender, EventArgs e)
        {
            frmPersonelMesaileri frm = new frmPersonelMesaileri();
            frm.ShowDialog();
        }

        private void btnTumMesaileriOde_Click(object sender, EventArgs e)
        {

        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            foreach (Control item in Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }
                if (item is ComboBox)
                {
                    item.Text = "";
                }
                if (item is MaskedTextBox)
                {
                    item.Text = "";
                }
            }
            dateTimeBaslangic.Value = DateTime.Now;
            dateTimeBitis.Value = DateTime.Now;
        }
        private void txtMesaiSaatUcreti_TextChanged(object sender, EventArgs e)
        {
            try
            {

                string baslangic = dateTimeBaslangic.Text + " " + maskedtxtBaslangic.Text;
                string bitis = dateTimeBitis.Text + " " + maskedtxtBitis.Text;
                TimeSpan saatfarki = DateTime.Parse(bitis) - DateTime.Parse(baslangic);
                double MSaatUcreti = double.Parse(txtMesaiSaatUcreti.Text);
                double tutar = saatfarki.TotalHours * MSaatUcreti;
                txtTutar.Text = tutar.ToString("0.00");
            }
            catch (Exception)
            {

                
            }
        }
    }
}
