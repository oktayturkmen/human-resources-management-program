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
    public partial class frmKullanici : Form
    {
        public frmKullanici()
        {
            InitializeComponent();
        }

        private void btnGiris_Click(object sender, EventArgs e)
        {
            Kullanicilar.KullaniciGirisi(txtKullaniciAdi.Text, txtSifre.Text);
            if(Kullanicilar.durum)
            {
                this.Close();
            }
            else
            {
                MessageBox.Show("kullanıcı adı veya şifre yalnış.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void lblCikis_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmSifremiUnutum frm = new frmSifremiUnutum();
            frm.ShowDialog();
        }

        private void btnKayitOl_Click(object sender, EventArgs e)
        {
            frmYeniKullanici frm = new frmYeniKullanici();
            frm.ShowDialog();
        }

        private void frmKullanici_Load(object sender, EventArgs e)
        {
            btnGiris.PerformClick();
        }
    }
}
