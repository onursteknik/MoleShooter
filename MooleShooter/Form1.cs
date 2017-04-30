using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MooleShooter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Doldur();
        }
        Random rnd = new Random();

        void Doldur()
        {
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Interval = 1000;
            panel1.Controls.Clear();
            Button btn = new Button();
            btn.Size = new Size(50,50);
            btn.BackgroundImage = Properties.Resources.Yellow_Bird;
            btn.BackgroundImageLayout = ImageLayout.Zoom;
            btn.BackColor = Color.Transparent;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.FlatAppearance.MouseOverBackColor = Color.White;
            btn.FlatAppearance.MouseDownBackColor = Color.White;
            btn.Location = new Point(rnd.Next(0,565),rnd.Next(0,260));
            btn.Click += Btn_Click;
            panel1.Controls.Add(btn);
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            timer2.Start();
            skor += 1;
            lblSkor.Text = skor.ToString();
            oran = (skor*100)/(skor + int.Parse(lblIska.Text));
            lblOran.Text = "%" + oran;
            Button btn = (Button)sender;
            PictureBox pb = new PictureBox();
            pb.Image = Properties.Resources.blood_PNG6104;
            pb.SizeMode = PictureBoxSizeMode.StretchImage;
            pb.Size = new Size(80, 80);
            Point pbp = btn.Location;      // Aşağıdaki 4 satırda kan resminin yerini belirliyoruz. Dengeli olması açısından kuş resminin konumundan biraz yukarı sola doğru alıyoruz ki ortalı düzgün gözüksün.
            pbp.X -= 15;
            pbp.Y -= 15;
            pb.Location = new Point(pbp.X, pbp.Y);   
            panel1.Controls.Add(pb);
            lblSkor.Tag = pb;  //Tag özelliğini gizli bir cep olarak düşünebilirsiniz. Her türlü (1) adet objeyi içine alabilir.Ben içine picturebox ı attım.Kuşu vurabilirsek timer2 çalışacak. Timer2 kuş resmini kaldıracak ve timer1 i çalıştıracak. Timer1 in hemen devreye girmesi için aşağıda timer1 in interval ini 10 yapacağız. Kuşu vurduğumuz an kan gözükecek ve timer2 nin interval i olan 500ms sonra form hemen eski haline geri gelecek. Yani biz kuşu vurduysak 510 ms sonra form eski haline dönüyor.
            panel1.Controls.Remove(btn);
            
        }

        //Cursor u değiştirmek için gerekli - windows api kullanıyoruz 
        //Detaylı öğreneceğim diyorsanız Türkçe kaynak adresi burada :)
        // http://www.csharpnedir.com/articles/read/?id=83
        //Daha basitçe anlatım ise burada
        // https://www.teknologweb.com/c-ile-windows-api-kullanimi
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern IntPtr LoadCursorFromFile(string fileName);

        
        private void Form1_Load(object sender, EventArgs e)
        {
            IntPtr handle = LoadCursorFromFile("sniper.cur"); //Buradaki 3 satırda formun cursor'unu sniper yaptık.
            Cursor cc = new Cursor(handle);
            this.Cursor = cc;
        }
        int skor = 0;
        int iska = 0;
        double oran = 0;
        private void panel1_Click(object sender, EventArgs e)
        {
            //Kuşa değil de panele tıklarsa ıskalamış sayılacak.Iska sayısını artırıp gösterecek gerekli kodları yazdık.
            iska += 1;
            lblIska.Text = iska.ToString();
            int skr = int.Parse(lblSkor.Text);
            oran = (skr * 100) / (skr + iska);
            lblOran.Text = "%" + oran;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            PictureBox pb = (PictureBox)lblSkor.Tag;
            panel1.Controls.Remove(pb);
            timer2.Stop();
            timer1.Start();
            timer1.Interval = 10;
        }
    }
}
