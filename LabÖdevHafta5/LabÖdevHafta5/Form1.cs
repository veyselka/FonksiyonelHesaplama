using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LabÖdevHafta5
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string[] sayilar;
        int max;
        double PI = 3.14159265359;
        string sonuç = "";

        Stack stack=new Stack();

        private void Form1_Load(object sender, EventArgs e)
        {

        }


        public double Mutlak_Hesaplama(double Mutlak)
        {
            
            if(Mutlak<0) {
                Mutlak *= -1;
            }
            else
            {
                Mutlak = Mutlak;
            }

            return Mutlak;
        }

        public double Karekök_Hesaplama(double sayı)
        {
            double x = sayı;
            double epsilon = 0.00001;
            while (true)
            {
                x = 0.5 * (x + sayı / x);
                if (Mutlak_Hesaplama(sayı - x * x) < epsilon)
                {
                    return x;
                }
            }
        }

        double Sin(double angle)
        {
            angle %= 2 * PI;
            double sin = 0;
            for (int i = 0; i < 84; i++)
            {
                sin += Us(-1, i) * (Us(angle, 2 * i + 1) / Fac(2 * i + 1));
            }
            return sin;
        }

        double Cos(double angle)
        {
            angle %= 2 * PI;
            double cos = 0;
            for (int i = 0; i < 84; i++)
            {
                cos += Us(-1, i) * (Us(angle, 2 * i) / Fac(2 * i));
            }
            return cos;
        }

        double Fac(int sayı)
        {
            if (sayı == 0)
            {
                return 1;
            }
            double result = 1;
            for (int i = 1; i <= sayı; i++)
            {
                result *= i;
            }
            return result;
        }

        double Us(double sayı, int üs)
        {
            double sonuç = 1;
            for (int i = 0; i < üs; i++)
            {
                sonuç *= sayı;
            }
            return sonuç;
        }









        private void button1_Click(object sender, EventArgs e)
        {
            double sonuc=Mutlak_Hesaplama(Convert.ToInt32(textBox1.Text));
            textBox1.Text=Convert.ToString(sonuc);
            stack.Push(sonuc);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            double sonuc = Karekök_Hesaplama(Convert.ToDouble(textBox1.Text));
            textBox1.Text = Convert.ToString(sonuc);
            stack.Push(sonuc);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            sayilar = textBox1.Text.Split(',');
            max = int.MinValue;
            foreach (string i in sayilar)
            {
                if (int.TryParse(i, out int num))
                {
                    if (num > max)
                    {
                        max = num;
                    }
                }
            }
            textBox1.Text = max.ToString();
            stack.Push(max);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            double sayi = Convert.ToDouble(textBox1.Text);
            double sonuc = Math.Sin(sayi);
            sonuç = textBox1.Text;
            textBox1.Text = sonuc.ToString();
            stack.Push(sonuc);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            double sayi = Convert.ToDouble(textBox1.Text);
            double sonuc = Cos(sayi);
            sonuç = textBox1.Text;
            textBox1.Text = sonuc.ToString();
            stack.Push(sonuc);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (stack.Count == 0)
            {
                MessageBox.Show("Başka Eleman Yok !", "Uyarı", MessageBoxButtons.OK);
            }
            else
            {
                textBox1.Text = stack.Pop().ToString();

            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Form form = new Form();
            form.Text = "Çizim";
            form.Size = new Size(600, 400);
            form.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            PictureBox pictureBox = new PictureBox();
            pictureBox.Dock = DockStyle.Fill;
            pictureBox.BackColor = Color.White;
            form.Controls.Add(pictureBox);

            int startX = Convert.ToInt32(textBox2.Text);
            int endX = Convert.ToInt32(textBox3.Text);

            form.Show();

            Bitmap bitmap = new Bitmap(pictureBox.Width, pictureBox.Height);
            Graphics graphics = Graphics.FromImage(bitmap);
            Pen Sinpen = new Pen(Color.Blue);
            Pen Cospen = new Pen(Color.Red);

            float scale = 50;
            if (radioButton1.Checked)
            {
                for (int i = 0; i < pictureBox.Width; i++)
                {
                    double xValue = startX + (endX - startX) * (float)i / pictureBox.Width;
                    float y = (float)(Sin(xValue * PI / 180) * scale + pictureBox.Height / 2);
                    graphics.DrawRectangle(Sinpen, i, y, 1, 1);
                }
            }
            if (radioButton2.Checked)
            {
                for (int i = 0; i < pictureBox.Width; i++)
                {
                    double xValue = startX + (endX - startX) * (float)i / pictureBox.Width;
                    float y = (float)(Cos(xValue * PI / 180) * scale + pictureBox.Height / 2);
                    graphics.DrawRectangle(Cospen, i, y, 1, 1);
                }
            }

            pictureBox.Image = bitmap;
        }

        
    }
    
}
