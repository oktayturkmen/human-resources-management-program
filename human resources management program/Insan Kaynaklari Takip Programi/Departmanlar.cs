using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Insan_Kaynaklari_Takip_Programi
{
    internal class Departmanlar
    {
        private int _DepartmanID;
        private string _Departman;
        private string _Aciklama;

        public int DepartmanID { get => _DepartmanID; set => _DepartmanID = value; }
        public string Departman { get => _Departman; set => _Departman = value; }
        public string Aciklama { get => _Aciklama; set => _Aciklama = value; }

        


        public static SqlDataReader DepartmanGetir(ListView lst)
        {
            lst.Items.Clear();

            // Bağlantı dizesini oluştur
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=İnsan_Kaynaklari_Yonetim;Integrated Security=True";

            // SqlConnection nesnesini oluştur ve bağlantı dizesini ata
            using (SqlConnection baglanti = new SqlConnection(connectionString))
            {
                baglanti.Open(); // Bağlantıyı aç

                // SqlCommand nesnesini oluştur ve bağlantıyı ve sorguyu ata
                SqlCommand komut = new SqlCommand("SELECT * FROM Departmanlar", baglanti);

                // SqlDataReader nesnesini oluştur ve sorguyu çalıştır
                SqlDataReader dr = komut.ExecuteReader();

                // Verileri ListView'e ekle
                while (dr.Read())
                {
                    ListViewItem ekle = new ListViewItem();
                    ekle.Text = dr[0].ToString();
                    ekle.SubItems.Add(dr[1].ToString());
                    ekle.SubItems.Add(dr[2].ToString());
                    lst.Items.Add(ekle);
                }

                return dr; // SqlDataReader nesnesini geri dön
            }
        }

    }
}
