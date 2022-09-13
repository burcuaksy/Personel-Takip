using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
namespace stajprojesi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //Veri tabanı dosya yolu ve provider nesnesinin belirlenmesi
        OleDbConnection baglantim = new OleDbConnection("Provider=Microsoft.Ace.Oledb.12.0;Data Source=personel.accdb");

        //Formlar arası değişkenlerde kullanılacak değişkenler
        public static string tcno, adi, soyadi, yetki;

        private void button1_Click(object sender, EventArgs e)
        {
            if (hak != 0)
            {
                baglantim.Open(); //baglantımızı açıyoruz
                OleDbCommand selectsorgu = new OleDbCommand("select * from kullanicilar", baglantim);//kullanıcılar tablosundaki bütün verileri çektik
                OleDbDataReader kayitokuma = selectsorgu.ExecuteReader();//sorgunun yürütülmesini sağladık ve bellge kayitokuma ekledik
                while (kayitokuma.Read())
                {
                    if (radioButton1.Checked == true)
                    {
                        if (kayitokuma["kullaniciadi"].ToString() == textBox1.Text && kayitokuma["parola"].ToString() == textBox2.Text && kayitokuma["yetki"].ToString() == "Yönetici")
                        {
                            durum = true;
                            tcno = kayitokuma.GetValue(0).ToString();
                            adi = kayitokuma.GetValue(1).ToString();
                            soyadi = kayitokuma.GetValue(2).ToString();
                            yetki = kayitokuma.GetValue(3).ToString();
                            this.Hide();//başarılı bir giriş yapıldığı için form1 i gizliyoruz
                            Form2 frm2 = new Form2();//2.formun açılmasını sağlıyoruz
                            frm2.Show();
                            break; //while komutundan çıkıs yaptık
                        }
                    }
                    if (radioButton2.Checked == true)
                    {
                        if (kayitokuma["kullaniciadi"].ToString() == textBox1.Text && kayitokuma["parola"].ToString() == textBox2.Text && kayitokuma["yetki"].ToString() == "Kullanıcı")
                        {
                            durum = true;
                            tcno = kayitokuma.GetValue(0).ToString();
                            adi = kayitokuma.GetValue(1).ToString();
                            soyadi = kayitokuma.GetValue(2).ToString();
                            yetki = kayitokuma.GetValue(3).ToString();
                            this.Hide();//başarılı bir giriş yapıldığı için form1 i gizliyoruz
                            Form3 frm3 = new Form3();//3.formun açılmasını sağlıyoruz
                            frm3.Show();
                            break; //while komutundan çıkıs yaptık
                        }
                    }
                }
                if (durum == false)
                    hak--;
                baglantim.Close();
            }
            label5.Text = Convert.ToString(hak);
            if (hak == 0)
            {
                button1.Enabled = false;
                MessageBox.Show("Giriş hakkı kalmadı!", "KGM Personel Takip Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        //yerel yani yalnızca bu formda geçerli olacak değişkenler
        int hak = 3; bool durum = false;

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "Kullanıcı Girişi...";
            this.AcceptButton = button1; this.CancelButton = button2;
            label5.Text = Convert.ToString(hak);
            radioButton1.Checked = true;
            this.StartPosition = FormStartPosition.CenterScreen;  //form1 ortada açılacak
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow; //simge durumuna getirme pasifleştirildi

        }
    }

}
