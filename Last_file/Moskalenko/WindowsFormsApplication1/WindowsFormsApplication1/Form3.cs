using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApplication1
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        //переменные, отвечающие за настройки:
        bool a1;//true: виживание по количеству
                //false: виживание по расположению
        bool[] v;
        int vmin;
        int vmax;
        bool b1;//true: рождение по количеству
                //false: рождение по расположению
        bool[] b;
        int bmin;
        int bmax;
        int size_micr;
        bool diff_cycles;
        //переменные лдя КМ
        int h, w;//размеры поля
                 //это понадобится для построения графика:
        int[] n1, n2,n;//массив, который накапливает в себе количества М на поле в течении всего времени                
        double[] concentration;//массив, который накапливает в себе концентрацию М на поле (в%) в течении всего времени
        double[] configuration;
        Graphics g;
        int z = 10;//размер одного М = z*z
        bool[,] a,a_g;
        int abc, abc1,abc2;//счетчик итераций
        //переменные для выживания:
        int q1 = 1, q2 = 3, q3 = 4, q11 = 1, q21 = 3, q31 = 4, f1 = 0, f2 = 0, r1, r2;
        //переменные для циклов:
        int c = 1, c1 = 1;
        //переменные для циклов
        bool T1 = true, T2 = true;
        int T1_interval, T2_interval;

        private void button7_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            timer2.Enabled = false;
            for (int i = 0; i < w; i++)
                for (int j = 0; j < h; j++)
                    a[i, j] = false;
            pictureBox1.Refresh();
            button1.Text = "Розпочати";
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            timer2.Enabled = false;
            for (int i = 0; i < w; i++)
                for (int j = 0; j < h; j++)
                    a[i, j] = false;
            pictureBox1.Refresh();
            button1.Text = "Розпочати";
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            timer2.Enabled = false;
            for (int i = 0; i < w; i++)
                for (int j = 0; j < h; j++)
                    a[i, j] = false;
            pictureBox1.Refresh();
            button1.Text = "Розпочати";
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            a1 = true;
            b1 = true;
            vmin = 2;
            vmax = 6;
            bmin = 2;
            bmax = 5;
            size_micr = 10;
            diff_cycles = true;
            v = new bool[10];
            b = new bool[10];
            for (int i = 0; i < 10; i++)
            {
                v[i] = true;
                b[i] = true;
            }
            z = size_micr;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text == "Розпочати")
            {
                //xyz = 0;
                button4.Visible = true;
                button3.Visible = true;
                button2.Visible = true;
                button5.Visible = true;
                label2.Visible = true;
                listBox1.Visible = true;
                listBox2.Visible = true;
                //T1 = true;
                //T2 = false;
                listBox1.Items.Clear();
                listBox2.Items.Clear();
                abc = 0;
                abc1 = 0;
                abc2 = 0;
                q1 = 1;
                q2 = 3;
                q3 = 4;
                f1 = 0;
                f2 = 0;
                r1 = 0;
                r2 = 0;
                c = 1;
                pictureBox1.Refresh();
                int x, y;
                h = int.Parse(textBox2.Text);
                w = int.Parse(textBox3.Text);
                n = new int[10000];
                n1 = new int[10000];
                n2 = new int[10000];
                //if (w * h < int.Parse(textBox1.Text))//на случай если заданое количество М больше чем поле
                //{
                //    n[abc2] = w * h;
                //    n1[abc1] = w * h;
                //    n2[abc] = w * h;
                //    textBox1.Text = (w * h).ToString();
                //}
                //else
                //{
                //    n[abc2] = int.Parse(textBox1.Text);
                //    n1[abc1] = int.Parse(textBox1.Text);
                //    n2[abc] = int.Parse(textBox1.Text);
                //}
                n[abc2] = (w * h)/2;
                n1[abc1] = (w * h)/2;
                n2[abc] = (w * h)/2;
                concentration = new double[10000];
                configuration = new double[10000];
                //концентрация будет высщитоватся в процентах, поэтому умножаем на 100:
                concentration[abc2] = Convert.ToDouble(n[abc2] * 100 / (h * w));//расчитываем начальную концентрацию
                a = new bool[w, h];
                a_g = new bool[w, h];
                for (int i = 0; i < w; i++)
                    for (int j = 0; j < h; j++)
                    {
                        a[i, j] = false;
                        a_g[i, j] = false;
                    }
                g = pictureBox1.CreateGraphics();
                Random rnd = new Random();
                //"расселяем" первые М по полю:
                bool bl = true;
                for (int i = 0; i < h; i++, bl = !bl)
                    for (int j = Convert.ToInt32(bl); j < w; j += 2)
                    {
                        a[i, j] = true;
                        g.FillRectangle(Brushes.Gold, i * z, j * z, z, z);
                        g.DrawRectangle(Pens.Red, i * z, j * z, z, z);
                        //g.FillRectangle(Brushes.Black, (i) * z, (j) * z, z, z);
                    }
                
                T1_interval = Convert.ToInt32(textBox4.Text);
                T2_interval = Convert.ToInt32(textBox5.Text);
                timer1.Interval = T1_interval;
                timer2.Interval = T2_interval;
                timer1.Enabled = T1;
                timer2.Enabled = T2;
                button1.Text = "Призупинити";
                //listBox1.Items.Add("abc = "+abc);
                //listBox1.Items.Add("n[abc] = " + n[abc]);
                //listBox1.Items.Add("c[abc] = " + concentration[abc]);
                //listBox1.Items.Add("Умирает, если >" + Convert.ToInt32(q2) + " и <" + Convert.ToInt32(q3));
                //listBox1.Items.Add("q1 = " + q1);
            }
            else
            {
                if (button1.Text == "Призупинити")
                {
                    timer1.Enabled = false;
                    timer2.Enabled = false;
                    button1.Text = "Продовжити";
                }
                else
                {
                    timer1.Enabled = T1;
                    timer2.Enabled = T2;
                    button1.Text = "Призупинити";
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //графіки
            string path = @"Text.txt";
            //string str="";
            if (File.Exists(path))
            {
                System.IO.StreamWriter file = new System.IO.StreamWriter(path, false);//очищаем
                file.WriteLine("");
                file.Close();
                file = new StreamWriter(path);                
                for (int i = 0; i < abc2; i++)
                {
                    file.Write((n[i]) + " ");
                }
                file.Write(n[abc2]);
                //
                file.WriteLine("");
                for (int i = 0; i < abc2; i++)
                {
                    file.Write((concentration[i]) + " ");
                }
                file.Write(concentration[abc2]);
                //
                file.WriteLine("");
                for (int i = 0; i < abc2-1; i++)
                {
                    file.Write((configuration[i]) + " ");
                }
                file.Write(configuration[abc2-1]);
                file.Close();
            }
            Graphic f = new Graphic();
            f.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            T2 = !T2;
            //timer2.Enabled = T2;
            if (T2)
            {
                button3.Text = "Часове поле T2: увімкнуте";
                button3.BackColor = Color.FromArgb(192, 255, 192);
            }
            else
            {
                button3.Text = "Часове поле T2: вимкнено";
                button3.BackColor = Color.FromArgb(255, 192, 192);
            }
        }
        private void button5_Click(object sender, EventArgs e)
        {
            T1 = !T1;
            //timer1.Enabled = T1;
            if (T1)
            {
                button2.Text = "Часове поле T1: увімкнуте";
                button2.BackColor = Color.FromArgb(192, 255, 192);
            }
            else
            {
                button2.Text = "Часове поле T1: вимкнено";
                button2.BackColor = Color.FromArgb(255, 192, 192);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Interval = Convert.ToInt32(textBox4.Text);
            timer2.Interval = Convert.ToInt32(textBox5.Text);
            if (T1)
            {
                abc1++;
                //q11 Є [trackBar2 ; trackBar1]
                if (q11 <= bmax - 1)
                    q11++;
                else
                    q11 = bmin;
                //q2 Є [ trackBar5 ; (trackBar5+trackBar4)/2 )
                if (q21 < ((vmin + vmax) / 2) - 1)
                    q21++;
                else
                    q21 = vmin;
                //q3 Є [(trackBar5+trackBar4)/2 +1; trackBar4]
                if (q31 < vmax)
                    q31++;
                else
                    q31 = ((vmin + vmax) / 2) + 1;
                z = size_micr;
                if (a1 == false)
                {
                    //f Є [1 ; 10]
                    if (f1 <= 9)
                    {
                        f1++;
                        f1 = id_limit(f1, true);
                    }
                    else
                    {
                        f1 = 1;
                        f1 = id_limit(f1, true);
                    }
                }
                if (b1 == false)
                {
                    //r Є [1 ; 10]
                    if (r1 <= 9)
                    {
                        r1++;
                        r1 = id_limit(r1, false);
                    }
                    else
                    {
                        r1 = 1;
                        r1 = id_limit(r1, false);
                    }
                }
                //циклы:
                if (c1 == 1)//по строкам слева на право и сверху вниз
                    for (int j = 0; j < h; j++)
                        for (int i = 0; i < w / 2; i++)
                            body_of_the_cycle(i, j, q11, q21, q31, a, r1, f1);
                if (c1 == 2)//по столбцам с сверху вниз и справа на лево
                    for (int i = w / 2 - 1; i >= 0; i--)
                        for (int j = 0; j < h; j++)
                            body_of_the_cycle(i, j, q11, q21, q31, a, r1, f1);
                if (c1 == 3)//по строкам справа на лево и снизу вверх
                    for (int j = h - 1; j >= 0; j--)
                        for (int i = w / 2 - 1; i >= 0; i--)
                            body_of_the_cycle(i, j, q11, q21, q31, a, r1, f1);
                if (c1 == 4)//по столбцам снизу вверх и слева на право
                    for (int i = 0; i < w / 2; i++)
                        for (int j = h - 1; j >= 0; j--)
                            body_of_the_cycle(i, j, q11, q21, q31, a, r1, f1);
                if (c1 == 5)//по столбцам сверху вниз и слева на право
                    for (int i = 0; i < w / 2; i++)
                        for (int j = 0; j < h; j++)
                            body_of_the_cycle(i, j, q11, q21, q31, a, r1, f1);
                if (c1 == 6)//по строкам слева на право и снизу вверх 
                    for (int j = h - 1; j >= 0; j--)
                        for (int i = 0; i < w / 2; i++)
                            body_of_the_cycle(i, j, q11, q21, q31, a, r1, f1);
                if (c1 == 7)//по столбцам снизу вверх и справа на лево
                    for (int i = w / 2 - 1; i >= 0; i--)
                        for (int j = h - 1; j >= 0; j--)
                            body_of_the_cycle(i, j, q11, q21, q31, a, r1, f1);
                if (c1 == 8)//по строкам справа на лево и сверху вниз
                    for (int j = 0; j < h; j++)
                        for (int i = w / 2 - 1; i >= 0; i--)
                            body_of_the_cycle(i, j, q11, q21, q31, a, r1, f1);
                if (c1 <= 7)
                {
                    c1++;
                }
                else
                    c1 = 1;
                //подсчет М на поле
                bool count = false;
                int schet = 0;
                for (int i = 0; i < w; i++)
                    for (int j = 0; j < h; j++)
                        if (a[i, j] == true)
                        {
                            count = true;
                            schet++;
                        }
                n1[abc1] = schet;
                //concentration[abc] = Convert.ToDouble(n[abc] * 100 / (h * w));
                if (!count)
                {
                    timer1.Enabled = false;
                    timer2.Enabled = false;
                    button1.Text = "Розпочати";
                }
                //if (n[abc1] == h * w)
                //{
                //    timer1.Enabled = false;
                //    button1.Text = "Начать";
                //}
                listBox1.Items.Add("Ітерація № " + abc1 + " :");
                listBox1.Items.Add("Кіль-ть = " + n1[abc1]);
                //listBox1.Items.Add("Концентрація = " + concentration[abc] + " %");
                if (a1 == true)
                {
                    if (q21 == q31)
                    {
                        listBox1.Items.Add("Зникає, якщо сусідів = " + q21);
                    }
                    listBox1.Items.Add("Зникає, якщо сусідів >" + Convert.ToInt32(q21) + " і <" + Convert.ToInt32(q31));
                }
                if (a1 == false)
                {
                    listBox1.Items.Add("виживає, якщо сусіди:");
                    if (f1 == 1)
                        listBox1.Items.Add("1 зверху і 1 знизу");
                    if (f1 == 2)
                        listBox1.Items.Add("1 зверху і 1 справа");
                    if (f1 == 3)
                        listBox1.Items.Add("2 сусіда зверху і 1 зліва");
                    if (f1 == 4)
                        listBox1.Items.Add("1 зліва зверху і 1 справа знизу");
                    if (f1 == 5)
                        listBox1.Items.Add("1 знизу і 1 зліва");
                    if (f1 == 6)
                        listBox1.Items.Add("1 справа зверху і 1 зліва знизу");
                    if (f1 == 7)
                        listBox1.Items.Add("1 справа зверху і 1 зліва зверху");
                    if (f1 == 8)
                        listBox1.Items.Add("1 справа знизу і 1 зліва знизу");
                    if (f1 == 9)
                        listBox1.Items.Add("1 зліва і 1 справа зверху");
                    if (f1 == 10)
                        listBox1.Items.Add("1 зліва і 1 справа");
                }
                if (b1 == true)
                    listBox1.Items.Add("Народжується, якщо сусідів = " + q11);
                if (b1 == false)
                {
                    listBox1.Items.Add("Народжується, якщо сусіди:");
                    if (r1 == 1)
                        listBox1.Items.Add("1 зверху і 1 знизу");
                    if (r1 == 2)
                        listBox1.Items.Add("1 зверху і 1 справа");
                    if (r1 == 3)
                        listBox1.Items.Add("2 сусіда зверху і 1 зліва");
                    if (r1 == 4)
                        listBox1.Items.Add("1 зліва зверху і 1 справа знизу");
                    if (r1 == 5)
                        listBox1.Items.Add("1 знизу і 1 зліва");
                    if (r1 == 6)
                        listBox1.Items.Add("1 справа зверху і 1 зліва знизу");
                    if (r1 == 7)
                        listBox1.Items.Add("1 справа зверху і 1 зліва зверху");
                    if (r1 == 8)
                        listBox1.Items.Add("1 справа знизу і 1 зліва знизу");
                    if (r1 == 9)
                        listBox1.Items.Add("1 зліва і 1 справа зверху");
                    if (r1 == 10)
                        listBox1.Items.Add("1 зліва і 1 справа");
                }

                abc2++;
                schet = 0;
                for (int i = 0; i < w; i++)
                    for (int j = 0; j < h; j++)
                        if (a[i, j] == true)
                        {
                            schet++;
                        }
                n[abc2] = schet;
                concentration[abc2] = Convert.ToDouble(n[abc2] * 100 / (h * w));
                int num_of_same_m = compare(a_g, a, h, w);
                for (int i = 0; i < w; i++)
                    for (int j = 0; j < h; j++)
                    {
                        a_g[i, j] = a[i, j];
                    }
                configuration[abc2 - 1] = ((100 * num_of_same_m) / (w * h));
                //listBox1.Items.Add("Співпадаэ " + configuration[abc - 1].ToString() + " %");
                num_of_same_m = 0;
            }
            if (w % 2 == 0)
                g.DrawLine(Pens.Black, (w / 2) * z, 0, (w / 2) * z, h * z);
            else
                g.DrawLine(Pens.Black, ((w-1) / 2) * z, 0, ((w-1) / 2) * z, h * z);
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            timer1.Interval = Convert.ToInt32(textBox4.Text);
            timer2.Interval = Convert.ToInt32(textBox5.Text);
            if (T2)
            {
                abc++;
                //q1 Є [trackBar2 ; trackBar1]
                if (q1 <= bmax - 1)
                    q1++;
                else
                    q1 = bmin;
                //q2 Є [ trackBar5 ; (trackBar5+trackBar4)/2 )
                if (q2 < ((vmin + vmax) / 2) - 1)
                    q2++;
                else
                    q2 = vmin;
                //q3 Є [(trackBar5+trackBar4)/2 +1; trackBar4]
                if (q3 < vmax)
                    q3++;
                else
                    q3 = ((vmin + vmax) / 2) + 1;
                z = size_micr;
                if (a1 == false)
                {
                    //f Є [1 ; 10]
                    if (f2 <= 9)
                    {
                        f2++;
                        f2 = id_limit(f2, true);
                    }
                    else
                    {
                        f2 = 1;
                        f2 = id_limit(f2, true);
                    }
                }
                if (b1 == false)
                {
                    //r Є [1 ; 10]
                    if (r2 <= 9)
                    {
                        r2++;
                        r2 = id_limit(r2, false);
                    }
                    else
                    {
                        r2 = 1;
                        r2 = id_limit(r2, false);
                    }
                }

                //циклы:
                if (c == 1)//по строкам слева на право и сверху вниз
                    for (int j = 0; j < h; j++)
                        for (int i = w / 2; i < w; i++)
                            body_of_the_cycle(i, j, q1, q2, q3, a, r2, f2);
                if (c == 2)//по столбцам с сверху вниз и справа на лево
                    for (int i = w - 1; i >= w / 2; i--)
                        for (int j = 0; j < h; j++)
                            body_of_the_cycle(i, j, q1, q2, q3, a, r2, f2);
                if (c == 3)//по строкам справа на лево и снизу вверх
                    for (int j = h - 1; j >= 0; j--)
                        for (int i = w - 1; i >= w / 2; i--)
                            body_of_the_cycle(i, j, q1, q2, q3, a, r2, f2);
                if (c == 4)//по столбцам снизу вверх и слева на право
                    for (int i = w / 2; i < w; i++)
                        for (int j = h - 1; j >= 0; j--)
                            body_of_the_cycle(i, j, q1, q2, q3, a, r2, f2);
                if (c == 5)//по столбцам сверху вниз и слева на право
                    for (int i = w / 2; i < w; i++)
                        for (int j = 0; j < h; j++)
                            body_of_the_cycle(i, j, q1, q2, q3, a, r2, f2);
                if (c == 6)//по строкам слева на право и снизу вверх 
                    for (int j = h - 1; j >= 0; j--)
                        for (int i = w / 2; i < w; i++)
                            body_of_the_cycle(i, j, q1, q2, q3, a, r2, f2);
                if (c == 7)//по столбцам снизу вверх и справа на лево
                    for (int i = w - 1; i >= w / 2; i--)
                        for (int j = h - 1; j >= 0; j--)
                            body_of_the_cycle(i, j, q1, q2, q3, a, r2, f2);
                if (c == 8)//по строкам справа на лево и сверху вниз
                    for (int j = 0; j < h; j++)
                        for (int i = w - 1; i >= w / 2; i--)
                            body_of_the_cycle(i, j, q1, q2, q3, a, r2, f2);
                if (c <= 7)
                {
                    c++;
                }
                else
                    c = 1;

                //подсчет М на поле
                int schet = 0;
                for (int i = 0; i < w; i++)
                    for (int j = 0; j < h; j++)
                        if (a[i, j] == true)
                        {
                            schet++;
                        }
                n2[abc] = schet;
                //concentration[abc] = Convert.ToDouble(n2[abc] * 100 / (h * w));
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
                listBox2.Items.Add("Ітерація № " + abc + " :");
                listBox2.Items.Add("Кіль-ть = " + n2[abc]);
                //listBox2.Items.Add("Концентрація = " + concentration[abc] + " %");
                if (a1 == true)
                {
                    if (q2 == q3)
                    {
                        listBox2.Items.Add("Зникає, якщо сусідів = " + q2);
                    }
                    listBox2.Items.Add("Зникає, якщо сусідів >" + Convert.ToInt32(q2) + " і <" + Convert.ToInt32(q3));
                }
                if (a1 == false)
                {
                    listBox2.Items.Add("виживає, якщо сусіди:");
                    if (f2 == 1)
                        listBox2.Items.Add("1 зверху і 1 знизу");
                    if (f2 == 2)
                        listBox2.Items.Add("1 зверху і 1 справа");
                    if (f2 == 3)
                        listBox2.Items.Add("2 сусіда зверху і 1 зліва");
                    if (f2 == 4)
                        listBox2.Items.Add("1 зліва зверху і 1 справа знизу");
                    if (f2 == 5)
                        listBox2.Items.Add("1 знизу і 1 зліва");
                    if (f2 == 6)
                        listBox2.Items.Add("1 справа зверху і 1 зліва знизу");
                    if (f2 == 7)
                        listBox2.Items.Add("1 справа зверху і 1 зліва зверху");
                    if (f2 == 8)
                        listBox2.Items.Add("1 справа знизу і 1 зліва знизу");
                    if (f2 == 9)
                        listBox2.Items.Add("1 зліва і 1 справа зверху");
                    if (f2 == 10)
                        listBox2.Items.Add("1 зліва і 1 справа");
                }
                if (b1 == true)
                    listBox2.Items.Add("Народжується, якщо сусідів = " + q1);
                if (b1 == false)
                {
                    listBox2.Items.Add("Народжується, якщо сусіди росташовані:");
                    if (r2 == 1)
                        listBox2.Items.Add("1 зверху і 1 знизу");
                    if (r2 == 2)
                        listBox2.Items.Add("1 зверху і 1 справа");
                    if (r2 == 3)
                        listBox2.Items.Add("2 сусіда зверху і 1 зліва");
                    if (r2 == 4)
                        listBox2.Items.Add("1 зліва зверху і 1 справа знизу");
                    if (r2 == 5)
                        listBox2.Items.Add("1 знизу і 1 зліва");
                    if (r2 == 6)
                        listBox2.Items.Add("1 справа зверху і 1 зліва знизу");
                    if (r2 == 7)
                        listBox2.Items.Add("1 справа зверху і 1 зліва зверху");
                    if (r2 == 8)
                        listBox2.Items.Add("1 справа знизу і 1 зліва знизу");
                    if (r2 == 9)
                        listBox2.Items.Add("1 зліва і 1 справа зверху");
                    if (r2 == 10)
                        listBox2.Items.Add("1 зліва і 1 справа");
                }
            }
            if (w % 2 == 0)
                g.DrawLine(Pens.Black, (w / 2) * z, 0, (w / 2) * z, h * z);
            else
                g.DrawLine(Pens.Black, ((w - 1) / 2) * z, 0, ((w - 1) / 2) * z, h * z);
        }

        void body_of_the_cycle(int i, int j, int q1, int q2, int q3, bool[,] xyz, int r, int f)
        {
            int k = num_of_neigh(i, j, xyz);
            if (a1 == true)//если виживание зависит от количества соседей:
            {
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
                //если же здесь (в этой координате) ничего нет и количество соседей = q1  
                //и если настройки позволяют,то здесь появляется новый М                
                birth(k, i, j, r);
            }
            if (a1 == false)//если виживание зависит от расположения соседий:
            {
                if (xyz[i, j])//если здесь М
                {
                    if (location_of_neigh(f, i, j, false) == true)//если условия виживания положительные,
                    {
                        g.FillRectangle(Brushes.Gold, i * z, j * z, z, z);//то М остается
                        g.DrawRectangle(Pens.Red, i * z, j * z, z, z);
                    }
                    else//в инном случае М умирает
                    {
                        xyz[i, j] = false;
                        g.FillRectangle(Brushes.White, i * z, j * z, z + 1, z + 1);
                    }
                }
                birth(k, i, j, r);
            }

        }

        int num_of_neigh(int i, int j, bool[,] xyz)
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

        private void button4_Click(object sender, EventArgs e)
        {
            //налаштування 
            string sss = button1.Text;
            if (button1.Text != "Розпочати")
            {
                timer1.Enabled = false;
                timer2.Enabled = false;
                button1.Text = "Продовжити";
            }
            Settings s = new Settings();
            s.Text = a1 + " " + b1 + " " + vmin + " " + vmax + " " + bmin + " " + bmax + " " + size_micr + " " + diff_cycles
                + " " + b[0] + " " + b[1] + " " + b[2] + " " + b[3] + " " + b[4] + " " + b[5] + " "
                + b[6] + " " + b[7] + " " + b[8] + " " + b[9] + " " + v[0] + " " + v[1] + " "
                + v[2] + " " + v[3] + " " + v[4] + " " + v[5] + " " + v[6] + " " + v[7] + " "
                + v[8] + " " + v[9] + " ";
            s.ShowDialog();
            a1 = s.a;
            b1 = s.b;
            vmin = s.vmin;
            vmax = s.vmax;
            bmin = s.bmin;
            bmax = s.bmax;
            size_micr = s.size_micr;
            z = size_micr;
            diff_cycles = s.diff_cycles;
            b[0] = s.b0;
            b[1] = s.b1;
            b[2] = s.b2;
            b[3] = s.b3;
            b[4] = s.b4;
            b[5] = s.b5;
            b[6] = s.b6;
            b[7] = s.b7;
            b[8] = s.b8;
            b[9] = s.b9;
            v[0] = s.v0;
            v[1] = s.v1;
            v[2] = s.v2;
            v[3] = s.v3;
            v[4] = s.v4;
            v[5] = s.v5;
            v[6] = s.v6;
            v[7] = s.v7;
            v[8] = s.v8;
            v[9] = s.v9;
            pictureBox1.Refresh();
            for (int i = 0; i < w; i++)
                for (int j = 0; j < h; j++)
                {
                    if (a[i, j] == true)
                    {
                        g.FillRectangle(Brushes.Gold, i * z, j * z, z, z);
                        g.DrawRectangle(Pens.Red, i * z, j * z, z, z);
                    }
                }
            if (button1.Text != "Розпочати")
            {
                if (sss != "Призупинити")
                {
                    button1.Text = "Продовжити";
                    timer1.Enabled = false;
                    timer2.Enabled = false;
                }
                else
                {
                    button1.Text = "Призупинити";
                    timer1.Enabled = T1;
                    timer2.Enabled = T2;
                }
            }
        }

        int id_limit(int id, bool p)
        {//переменная 'id' - порядковый номер комбинации
        repeat:
            if (id == 1 && ((v[0] == false && p == false) || (b[0] == false && p == true)))
                id++;
            if (id == 2 && ((v[1] == false && p == false) || (b[1] == false && p == true)))
                id++;
            if (id == 3 && ((v[2] == false && p == false) || (b[2] == false && p == true)))
                id++;
            if (id == 4 && ((v[3] == false && p == false) || (b[3] == false && p == true)))
                id++;
            if (id == 5 && ((v[4] == false && p == false) || (b[4] == false && p == true)))
                id++;
            if (id == 6 && ((v[5] == false && p == false) || (b[5] == false && p == true)))
                id++;
            if (id == 7 && ((v[6] == false && p == false) || (b[6] == false && p == true)))
                id++;
            if (id == 8 && ((v[7] == false && p == false) || (b[7] == false && p == true)))
                id++;
            if (id == 9 && ((v[8] == false && p == false) || (b[8] == false && p == true)))
                id++;
            if (id == 10 && ((v[9] == false && p == false) || (b[9] == false && p == true)))
            {
                id = 1;
                goto repeat;
            }
            return id;
        }

        void birth(int k, int i, int j, int r)
        {
            if (b1 == true)//виживание от количества соседей
            {
                if (k == q1)
                {
                    a[i, j] = true;
                    g.FillRectangle(Brushes.Gold, i * z, j * z, z, z);
                    g.DrawRectangle(Pens.Red, i * z, j * z, z, z);
                }
            }
            else if (b1 == false)//если условие виживания зависит от расположения соседей
            {
                if (location_of_neigh(r, i, j, true))
                {
                    a[i, j] = true;
                    g.FillRectangle(Brushes.Gold, i * z, j * z, z, z);
                    g.DrawRectangle(Pens.Red, i * z, j * z, z, z);
                }
            }
        }

        bool location_of_neigh(int id, int i, int j, bool p)//p=true ->проверяем на рождение
        {                                                 //p=false->проверяем на виживание
            //в зависимости от f проверяет наличие (или отсутствие) соседей в определенных точках
            bool bl = false;
            int num = 0;

            if (id == 1 && ((v[0] && p == false) || (b[0] && p)))//если 1 сверху и 1 снизу
            {
                try
                {
                    if (a[i, j - 1])//сверху
                        num++;
                }
                catch { }
                try
                {
                    if (a[i, j + 1])//снизу
                        num++;
                }
                catch { }
                if (num == 2)
                    bl = true;
                else
                    bl = false;
            }

            if (id == 2 && ((v[1] && p == false) || (b[1] && p)))//если 1 сверху и 1 справа
            {
                try
                {
                    if (a[i, j - 1])//сверху
                        num++;
                }
                catch { }
                try
                {
                    if (a[i + 1, j])//справа
                        num++;
                }
                catch { }
                if (num == 2)
                    bl = true;
                else
                    bl = false;
            }

            if (id == 3 && ((v[2] && p == false) || (b[2] && p)))//если 2 соседа сверху (подряд) и 1 слева 
            {
                try
                {
                    if (a[i, j - 1])//сверху на 1
                        num++;
                }
                catch { }
                try
                {
                    if (a[i, j - 2])//сверху на 2
                        num++;
                }
                catch { }
                try
                {
                    if (a[i - 1, j])//слева
                        num++;
                }
                catch { }
                if (num == 3)
                    bl = true;
                else
                    bl = false;
            }

            if (id == 4 && ((v[3] && p == false) || (b[3] && p)))//если 1 слева сверху и 1 справа снизу
            {
                try
                {
                    if (a[i - 1, j - 1])//слева сверху
                        num++;
                }
                catch { }
                try
                {
                    if (a[i + 1, j + 1])//справа снизу
                        num++;
                }
                catch { }
                if (num == 2)
                    bl = true;
                else
                    bl = false;
            }

            if (id == 5 && ((v[4] && p == false) || (b[4] && p)))//если 1 снизу и 1 слева
            {
                try
                {
                    if (a[i, j + 1])//снизу
                        num++;
                }
                catch { }
                try
                {
                    if (a[i - 1, j])//слева
                        num++;
                }
                catch { }
                if (num == 2)
                    bl = true;
                else
                    bl = false;
            }

            if (id == 6 && ((v[5] && p == false) || (b[5] && p)))//если 1 справа сверху и 1 слева снизу
            {
                try
                {
                    if (a[i + 1, j - 1])//справа сверху
                        num++;
                }
                catch { }
                try
                {
                    if (a[i - 1, j + 1])//слева снизу
                        num++;
                }
                catch { }
                if (num == 2)
                    bl = true;
                else
                    bl = false;
            }

            if (id == 7 && ((v[6] && p == false) || (b[6] && p)))//если 1 справа сверху и 1 слева сверху
            {
                try
                {
                    if (a[i - 1, j - 1])//слева сверху
                        num++;
                }
                catch { }
                try
                {
                    if (a[i + 1, j - 1])//справа сверху
                        num++;
                }
                catch { }
                if (num == 2)
                    bl = true;
                else
                    bl = false;
            }

            if (id == 8 && ((v[7] && p == false) || (b[7] && p)))//если 1 справа снизу и 1 слева снизу
            {
                try
                {
                    if (a[i - 1, j + 1])//слева снизу
                        num++;
                }
                catch { }
                try
                {
                    if (a[i + 1, j + 1])//справа снизу
                        num++;
                }
                catch { }
                if (num == 2)
                    bl = true;
                else
                    bl = false;
            }

            if (id == 9 && ((v[8] && p == false) || (b[8] && p)))//если 1 справа сверху и 1 слева 
            {
                try
                {
                    if (a[i + 1, j - 1])//справа сверху
                        num++;
                }
                catch { }
                try
                {
                    if (a[i - 1, j])//слева
                        num++;
                }
                catch { }
                if (num == 2)
                    bl = true;
                else
                    bl = false;
            }

            if (id == 10 && ((v[9] && p == false) || (b[9] && p)))//если 1 справа и 1 слева 
            {
                try
                {
                    if (a[i + 1, j])//справа
                        num++;
                }
                catch { }
                try
                {
                    if (a[i - 1, j])//слева 
                        num++;
                }
                catch { }
                if (num == 2)
                    bl = true;
                else
                    bl = false;
            }

            return bl;
            //если bl=true, то М виживает
            //если bl=false, то М умирает
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Mainmenu m = new Mainmenu();
            m.Show();
            this.Close();
        }

        int compare(bool[,] notmain, bool[,] main, int h, int w)
        {
            int xyz1 = 0;
            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < h; j++)
                {
                    if (main[i, j] == notmain[i, j])
                        xyz1++;
                }
            }
            return xyz1;
        }
    }
}
