using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vipusknaya5
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        int h, w;//размеры поля
                 //это понадобится для построения графика:
        int[] n;//массив, который накапливает в себе количества М на поле в течении всего времени                
        //double[] concentration;//массив, который накапливает в себе концентрацию М на поле (в%) в течении всего времени
        Graphics g;
        int z = 10;//размер одного М = z*z
        bool[,] a;
        int abc,abc1;//счетчик итераций
        //переменные для выживания:
        int q1 = 1, q2 = 3, q3 = 4, q11 = 1, q21 = 3, q31 = 4;
        //переменные для циклов:
        int c = 1, c1 = 1;
        //переменные для циклов
        bool T1=true, T2=true;

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Add("");
        }
        
        private void trackBar4_Scroll(object sender, EventArgs e)
        {
            if (trackBar5.Value + 1 > trackBar4.Value && trackBar4.Value - 1 > 0)
                trackBar5.Value = trackBar4.Value - 1;
            label10.Text = "от " + trackBar5.Value + " до " + trackBar4.Value;
        }
        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            if (trackBar2.Value + 1 > trackBar1.Value && trackBar2.Value + 1 < 9)
                trackBar1.Value = trackBar2.Value + 1;
            label8.Text = "от " + trackBar2.Value + " до " + trackBar1.Value;
        }
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            if (trackBar2.Value + 1 > trackBar1.Value && trackBar1.Value - 1 > 0)
                trackBar2.Value = trackBar1.Value - 1;
            label8.Text = "от " + trackBar2.Value + " до " + trackBar1.Value;
        }
        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            label9.Text = trackBar3.Value + " x " + trackBar3.Value;
            z = trackBar3.Value;
        }
        private void trackBar5_Scroll(object sender, EventArgs e)
        {
            if (trackBar5.Value + 1 > trackBar4.Value && trackBar5.Value + 1 < 9)
                trackBar4.Value = trackBar5.Value + 1;
            label10.Text = "от " + trackBar5.Value + " до " + trackBar4.Value;
        }

        int T1_interval, T2_interval;

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text == "Начать")
            {
                //T1 = true;
                //T2 = false;
                listBox1.Items.Clear();
                abc = 0;
                q1 = 1;
                q2 = 3;
                q3 = 4;
                c = 1;
                tabPage1.Refresh();
                int x, y;
                h = int.Parse(textBox2.Text);
                w = int.Parse(textBox3.Text);
                n = new int[10000];
                if (w * h < int.Parse(textBox1.Text))//на случай если заданое количество М больше чем поле
                {
                    n[abc] = w * h;
                    textBox1.Text = (w * h).ToString();
                }
                else
                    n[abc] = int.Parse(textBox1.Text);
                //concentration = new double[10000];
                //концентрация будет высщитоватся в процентах, поэтому умножаем на 100:
                //concentration[abc] = Convert.ToDouble(n[abc] * 100 / (h * w));//расчитываем начальную концентрацию
                a = new bool[w, h];
                //a2 = new bool[w, h];
                for (int i = 0; i < w; i++)
                    for (int j = 0; j < h; j++)
                    {
                        a[i, j] = false;
                        //a2[i, j] = false;
                    }
                g = tabPage1.CreateGraphics();
                Random rnd = new Random();
                //"расселяем" первые М по полю:
                for (int i = 0; i < n[abc]; i++)
                {
                    x = rnd.Next(w);
                    y = rnd.Next(h);
                    if (a[x, y] == false)
                    {
                        a[x, y] = true;
                        g.FillRectangle(Brushes.Gold, x * z, y * z, z, z);
                        g.DrawRectangle(Pens.Red, x * z, y * z, z, z);
                    }
                    else
                    {
                        i--;
                    }
                }
                //for (int i = 0; i < n[abc] / 2; i++)
                //{
                //    x = rnd.Next(w / 2,w);
                //    y = rnd.Next(h);
                //    if (a2[x, y] == false)
                //    {
                //        a2[x, y] = true;
                //        g.FillRectangle(Brushes.Gold, x * z, y * z, z, z);
                //        g.DrawRectangle(Pens.Red, x * z, y * z, z, z);
                //    }
                //    else
                //    {
                //        i--;
                //    }
                //}
                T1_interval = Convert.ToInt32(textBox4.Text);
                T2_interval = Convert.ToInt32(textBox5.Text);
                timer1.Interval = T1_interval;
                timer2.Interval = T2_interval;
                timer1.Enabled = T1;
                timer2.Enabled = T2;
                button1.Text = "Преостановить";
                //listBox1.Items.Add("abc = "+abc);
                //listBox1.Items.Add("n[abc] = " + n[abc]);
                //listBox1.Items.Add("c[abc] = " + concentration[abc]);
                //listBox1.Items.Add("Умирает, если >" + Convert.ToInt32(q2) + " и <" + Convert.ToInt32(q3));
                //listBox1.Items.Add("q1 = " + q1);
            }
            else
            {
                //timer1.Interval = Convert.ToInt32(textBox4.Text);
                //timer2.Interval = Convert.ToInt32(textBox5.Text);
                if (button1.Text == "Преостановить")
                {
                    timer1.Enabled = false;
                    timer2.Enabled = false;
                    button1.Text = "Продолжить";
                }
                else
                {
                    timer1.Enabled = T1;
                    timer2.Enabled = T2;
                    button1.Text = "Преостановить";
                }
               
            }
        }
       
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Interval = Convert.ToInt32(textBox4.Text);
            timer2.Interval = Convert.ToInt32(textBox5.Text);
            if (T1)
            {
                abc1++;
                //q1 Є [trackBar2 ; trackBar1]
                if (q11 <= trackBar1.Value - 1)
                    q11++;
                else
                    q11 = trackBar2.Value;
                //q2 Є [ trackBar5 ; (trackBar5+trackBar4)/2 )
                if (q21 < ((trackBar5.Value + trackBar4.Value) / 2) - 1)
                    q21++;
                else
                    q21 = trackBar5.Value;
                //q3 Є [(trackBar5+trackBar4)/2 +1; trackBar4]
                if (q31 < trackBar4.Value)
                    q31++;
                else
                    q31 = ((trackBar5.Value + trackBar4.Value) / 2) + 1;
                z = trackBar3.Value;
                //циклы:
                if (c1 == 1)//по строкам слева на право и сверху вниз
                    for (int j = 0; j < h; j++)
                        for (int i = 0; i < w / 2; i++)
                            body_of_the_cycle(i, j, q11, q21, q31,a);
                if (c1 == 2)//по столбцам с сверху вниз и справа на лево
                    for (int i = w/2 - 1; i >= 0; i--)
                        for (int j = 0; j < h; j++)
                            body_of_the_cycle(i, j, q11, q21, q31, a);
                if (c1 == 3)//по строкам справа на лево и снизу вверх
                    for (int j = h - 1; j >= 0; j--)
                        for (int i = w/2 - 1; i >= 0; i--)
                            body_of_the_cycle(i, j, q11, q21, q31, a);
                if (c1 == 4)//по столбцам снизу вверх и слева на право
                    for (int i = 0; i < w/2; i++)
                        for (int j = h - 1; j >= 0; j--)
                            body_of_the_cycle(i, j, q11, q21, q31, a);
                if (c1 == 5)//по столбцам сверху вниз и слева на право
                    for (int i = 0; i < w/2; i++)
                        for (int j = 0; j < h; j++)
                            body_of_the_cycle(i, j, q11, q21, q31, a);
                if (c1 == 6)//по строкам слева на право и снизу вверх 
                    for (int j = h - 1; j >= 0; j--)
                        for (int i = 0; i < w/2; i++)
                            body_of_the_cycle(i, j, q11, q21, q31, a);
                if (c1 == 7)//по столбцам снизу вверх и справа на лево
                    for (int i = w/2 - 1; i >= 0; i--)
                        for (int j = h - 1; j >= 0; j--)
                            body_of_the_cycle(i, j, q11, q21, q31, a);
                if (c1 == 8)//по строкам справа на лево и сверху вниз
                    for (int j = 0; j < h; j++)
                        for (int i = w / 2 - 1; i >= 0; i--)
                            body_of_the_cycle(i, j, q11, q21, q31, a);
                if (c1 <= 7)
                {
                    c1++;
                }
                else
                    c1 = 1;
                //подсчет М на поле
                bool count = false;
                for (int i = 0; i < w; i++)
                    for (int j = 0; j < h; j++)
                        if (a[i, j] == true)
                        {
                            count = true;
                        }
                //n[abc] = schet;
                //concentration[abc] = Convert.ToDouble(n[abc] * 100 / (h * w));
                if (!count)
                {
                    timer1.Enabled = false;
                    timer2.Enabled = false;
                    button1.Text = "Начать";
                }
                //if (n[abc1] == h * w)
                //{
                //    timer1.Enabled = false;
                //    button1.Text = "Начать";
                //}
                //listBox1.Items.Add("abc = " + abc);
                //listBox1.Items.Add("n[abc] = " + n[abc]);
                //listBox1.Items.Add("c[abc] = " + concentration[abc]);
                //listBox1.Items.Add("Умирает, если >" + Convert.ToInt32(q2) + " и <" + Convert.ToInt32(q3));
                //listBox1.Items.Add("q1 = " + q1);


                //построение графиков:
                if (tabControl1.SelectedIndex==1)
                {//если выделен график
                    drar_g();
                }
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            timer1.Interval = Convert.ToInt32(textBox4.Text);
            timer2.Interval = Convert.ToInt32(textBox5.Text);
            if (T2)
            {
                abc++;
                //q1 Є [trackBar2 ; trackBar1]
                if (q1 <= trackBar1.Value - 1)
                    q1++;
                else
                    q1 = trackBar2.Value;
                //q2 Є [ trackBar5 ; (trackBar5+trackBar4)/2 )
                if (q2 < ((trackBar5.Value + trackBar4.Value) / 2) - 1)
                    q2++;
                else
                    q2 = trackBar5.Value;
                //q3 Є [(trackBar5+trackBar4)/2 +1; trackBar4]
                if (q3 < trackBar4.Value)
                    q3++;
                else
                    q3 = ((trackBar5.Value + trackBar4.Value) / 2) + 1;
                z = trackBar3.Value;
                //циклы:
                if (c == 1)//по строкам слева на право и сверху вниз
                    for (int j = 0; j < h; j++)
                        for (int i = w / 2; i < w; i++)
                            body_of_the_cycle(i, j, q1, q2, q3, a);
                if (c == 2)//по столбцам с сверху вниз и справа на лево
                    for (int i = w - 1; i >= w / 2; i--)
                        for (int j = 0; j < h; j++)
                            body_of_the_cycle(i, j, q1, q2, q3, a);
                if (c == 3)//по строкам справа на лево и снизу вверх
                    for (int j = h - 1; j >= 0; j--)
                        for (int i = w - 1; i >= w / 2; i--)
                            body_of_the_cycle(i, j, q1, q2, q3, a);
                if (c == 4)//по столбцам снизу вверх и слева на право
                    for (int i = w / 2; i < w; i++)
                        for (int j = h - 1; j >= 0; j--)
                            body_of_the_cycle(i, j, q1, q2, q3, a);
                if (c == 5)//по столбцам сверху вниз и слева на право
                    for (int i = w / 2; i < w; i++)
                        for (int j = 0; j < h; j++)
                            body_of_the_cycle(i, j, q1, q2, q3, a);
                if (c == 6)//по строкам слева на право и снизу вверх 
                    for (int j = h - 1; j >= 0; j--)
                        for (int i = w / 2; i < w; i++)
                            body_of_the_cycle(i, j, q1, q2, q3, a);
                if (c == 7)//по столбцам снизу вверх и справа на лево
                    for (int i = w - 1; i >= w / 2; i--)
                        for (int j = h - 1; j >= 0; j--)
                            body_of_the_cycle(i, j, q1, q2, q3, a);
                if (c == 8)//по строкам справа на лево и сверху вниз
                    for (int j = 0; j < h; j++)
                        for (int i = w - 1; i >= w / 2; i--)
                            body_of_the_cycle(i, j, q1, q2, q3,a);
                if (c <= 7)
                {
                    c++;
                }
                else
                    c = 1;
                //подсчет М на поле
                //bool schet = false;
                //for (int i = w/2; i < w; i++)
                //    for (int j = 0; j < h; j++)
                //        if (a2[i, j] == true)
                //        {
                //            schet = true;
                //        }
                //n[abc] = schet;
                //concentration[abc] = Convert.ToDouble(n[abc] * 100 / (h * w));
                //if (!schet)
                //{
                //    timer2.Enabled = false;
                //    timer1.Enabled = false;
                //    button1.Text = "Начать";
                //}
                //if (n[abc] == h * w)
                //{
                //    timer1.Enabled = false;
                //    button1.Text = "Начать";
                //}
                //listBox1.Items.Add("abc = " + abc);
                //listBox1.Items.Add("n[abc] = " + n[abc]);
                //listBox1.Items.Add("c[abc] = " + concentration[abc]);
                //listBox1.Items.Add("Умирает, если >" + Convert.ToInt32(q2) + " и <" + Convert.ToInt32(q3));
                //listBox1.Items.Add("q1 = " + q1);
                //построение графиков:
                if (tabControl1.SelectedIndex == 1)
                {//если выделен график
                    drar_g();
                }
            }

        }

        void drar_g()
        {
            //if (timer1.Enabled == false)
            //{
            //    if (timer1.Interval < timer2.Interval)
            //    {
            //        //таймер1 - интенсивнее
            //        grafik = new Grafik();
            //        if (comboBox1.SelectedIndex == 1)
            //            grafik.grafik = new grafik1();
            //    }
            //    else
            //    {
            //        //таймер2 - интенсивнее (или равен)
            //        grafik = new Grafik();
            //    }
            //}
        }

        void body_of_the_cycle(int i,int j,int q1,int q2,int q3,bool[,] xyz)
        {
            int k = num_of_neigh(i, j,xyz);
            if (xyz[i, j])//если в этой координате есть М, то проверяем количество соседей
            {
                if (k > q2 - 1 && k < q3 + 1)//
                {
                    g.FillRectangle(Brushes.Gold, i * z, j * z, z, z);
                    g.DrawRectangle(Pens.Red, i * z, j * z, z, z);
                }
                else
                {
                    xyz[i, j] = false;
                    g.FillRectangle(Brushes.White, i * z, j * z, z + 1, z + 1);
                }
            }
            if(xyz[i,j]==false)
            {
                if (k == q1)
                {
                    xyz[i, j] = true;
                    g.FillRectangle(Brushes.Gold, i * z, j * z, z, z);
                    g.DrawRectangle(Pens.Red, i * z, j * z, z, z);
                }
            }

        }

        int num_of_neigh(int i, int j,bool [,]xyz)
        {//подсчет количества соседей у координаты (i;j)
            int num = 0;
            try
            {
                if (xyz[i, j + 1])//сосед снизу
                    num++;
            }
            catch { }
            try
            {
                if (xyz[i, j - 1])//сверху
                    num++;
            }
            catch { }
            try
            {
                if (xyz[i + 1, j + 1])//диагональ вниз направо 
                    num++;
            }
            catch { }
            try
            {
                if (xyz[i + 1, j - 1])//диагональ вверх направо 
                    num++;
            }
            catch { }
            try
            {
                if (xyz[i - 1, j + 1])//диагональ вниз налево 
                    num++;
            }
            catch { }
            try
            {
                if (xyz[i - 1, j - 1])//диагональ ввверх налево 
                    num++;
            }
            catch { }
            try
            {
                if (xyz[i + 1, j])//сосед справа
                    num++;
            }
            catch { }
            try
            {
                if (xyz[i - 1, j])//слева
                    num++;
            }
            catch { }
            return num;
        }
        //включить/выключить поля:
        private void button2_Click(object sender, EventArgs e)
        {//T1
            T1 = !T1;
            //timer1.Enabled = T1;
            if(T1)
            {
                button2.Text = "Временное поле T1: включено";
                button2.BackColor = Color.FromArgb(192, 255, 192);
            }
            else
            {
                button2.Text = "Временное поле T1: выключено";
                button2.BackColor = Color.FromArgb(255, 192, 192);
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {//T2
            T2 = !T2;
            //timer2.Enabled = T2;
            if (T2)
            {
                button3.Text = "Временное поле T2: включено";
                button3.BackColor = Color.FromArgb(192, 255, 192);
            }
            else
            {
                button3.Text = "Временное поле T2: выключено";
                button3.BackColor = Color.FromArgb(255, 192, 192);
            }
        }

        
    }
}
