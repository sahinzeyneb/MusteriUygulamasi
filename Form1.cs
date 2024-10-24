using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ŞirketUygulaması
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Entities db = new Entities();  // veritabanı bağlantısı için bir Entities nesnesi oluşturma
        private void RemoveCustomer(int id) 
        {
            
            var musteri = db.tblMusteriler.Find(id); //veri tabanında belirtilen id ye sahip müşteri kaydını bulur
            if (musteri != null)
            {
                musteri.durum = false; // müşteri kayıt durumunu false yaparak tamamen silmez 
                db.SaveChanges(); //değişiklikleri veri tabanına kaydeder
                MessageBox.Show("Kayıt başarıyla kaldırıldı.");
            }
            else
            {
                MessageBox.Show("Kayıt bulunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            listele(); // Kaydı listeyi güncelle
        }
        public void listele() 
        {
            dataGridView1.DataSource = db.tblMusteriler.ToList(); //tüm kayıtlar
            txtID.Text = "";
            txtAd.Text = "";
            txtSoyad.Text = "";
            txtEposta.Text = "";
            txtTel.Text = "";
            txtAdres.Text = "";



        }
        
        private void Form1_Load(object sender, EventArgs e) //form açıldığında çalışan metot
        {
            listele();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            { //tüm alanlar dolu mu ?
                if (txtAd.Text == "" || txtSoyad.Text == "" || txtEposta.Text == "" || txtTel.Text == "" || txtAdres.Text == "")

                    MessageBox.Show("Lütfen tüm alanları giriniz", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else 
                {
                    tblMusteriler x = new tblMusteriler();
                    x.ad = txtAd.Text;
                    x.soyad = txtSoyad.Text;
                    x.eposta = txtEposta.Text;
                    x.telefon = txtTel.Text;
                    x.adres = txtAdres.Text;
                    x.durum = true;

                    db.tblMusteriler.Add(x); //yeni kişiyi ekler
                    db.SaveChanges(); // veritabanına kaydeder
                    MessageBox.Show("Kayıt Başarılı");
                }
                listele();

            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e) // bir hücreye tıklandığında
        { //tüm alanları doldurur
            txtID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txtAd.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtSoyad.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            txtEposta.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            txtTel.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            txtAdres.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
        }

        private void btnSil_Click(object sender, EventArgs e)
        { //ID boş mu ?
            if (txtID.Text == "")
                MessageBox.Show("Lütfen silmek istediğiniz kişiyi seçiniz", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                int id = int.Parse(txtID.Text); //id alır int türüne dönüştürür
                var x = db.tblMusteriler.Find(id); //belirtilen id ye sahip müşteriyi bulur
                x.durum = false; //durum pasif
                db.SaveChanges(); //veritabanına kaydeder
                MessageBox.Show("Müşteri silindi");
            }
            listele();
        }

        private void btnGüncelle_Click(object sender, EventArgs e)
        { //dolu mu?
            if (txtAd.Text == "" || txtSoyad.Text == "" || txtEposta.Text == "" || txtTel.Text == "" || txtAdres.Text == "")
                MessageBox.Show("Lütfen güncellenecek kişiyi seçiniz", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                int id = int.Parse(txtID.Text); //id alır int türüne dönüştürür
                var x = db.tblMusteriler.Find(id); //belirtilen id ye sahip müşteriyi bulur
                x.ad = txtAd.Text; // alanı günceller
                x.soyad = txtSoyad.Text;
                x.eposta = txtEposta.Text;
                x.telefon = txtTel.Text;
                x.adres = txtAdres.Text;
                db.SaveChanges();
                MessageBox.Show("Güncelleme yapıldı");
            }

            listele();
        }

        private void btnCıkıs_Click(object sender, EventArgs e)
        {
            Application.Exit(); 
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
       
            if (dataGridView1.Columns[e.ColumnIndex].Name == "durum") //durum sütununu bul 
            {
                bool durum = Convert.ToBoolean(e.Value); //değeri bool a çevir
                if (!durum) 
                {
                    dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightGray; // pasif müşterileri gri 
                }
                else 
                {
                    dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightSkyBlue; // aktif müşteri rengi mavi
                }
            }
        }
    }
}
