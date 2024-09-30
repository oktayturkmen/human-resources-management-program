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
    public partial class frmDepartmanlar : Form
    {
        public frmDepartmanlar()
        {
            InitializeComponent();
        }

        private void frmDepartmanlar_Load(object sender, EventArgs e)
        {
            Departmanlar.DepartmanGetir(listView1);
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            Departmanlar d = new Departmanlar();
            d.Departman = txtDepartman.Text;
            d.Aciklama = txtAciklama.Text;

            string sorgu = "INSERT INTO Departmanlar (departman, aciklama) VALUES (@departman, @aciklama)";
            SqlCommand komut = new SqlCommand(sorgu);

            komut.Parameters.AddWithValue("@departman", d.Departman);
            komut.Parameters.AddWithValue("@aciklama", d.Aciklama);

            Veritabani.ESG(komut, sorgu);

            MessageBox.Show("İşlem Başarılı.");
            Departmanlar.DepartmanGetir(listView1);
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            Departmanlar d = new Departmanlar();
            d.DepartmanID = int.Parse(txtDepartmanID.Text);
            d.Departman = txtDepartman.Text;
            d.Aciklama = txtAciklama.Text;

            string sorgu = "update departmanlar set departman='"+d.Departman+"',aciklama='"+d.Aciklama+"'where departmanID='"+d.DepartmanID+"'";
            SqlCommand komut = new SqlCommand();
            Veritabani.ESG(komut, sorgu);

            MessageBox.Show("işlem başarlı.");
            Departmanlar.DepartmanGetir(listView1);
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count>0)
            {

                Departmanlar d = new Departmanlar();
                d.DepartmanID = int.Parse(listView1.SelectedItems[0].SubItems[0].Text);
        
                string sorgu = "delete from departmanlar where departmanID='" + d.DepartmanID + "'";
                SqlCommand komut = new SqlCommand();
                Veritabani.ESG(komut, sorgu);

                MessageBox.Show("işlem Başarlı.");
                Departmanlar.DepartmanGetir(listView1);
            }
            else
            {
                MessageBox.Show("Önce kayıt seçilmelidir.", "Uyarı", MessageBoxButtons.OK,MessageBoxIcon.Information);

            }
        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            txtDepartmanID.Text = listView1.SelectedItems[0].SubItems[0].Text;
            txtDepartman.Text= listView1.SelectedItems[0].SubItems[1].Text;
            txtAciklama.Text= listView1.SelectedItems[0].SubItems[2].Text;
        }
    }
}
