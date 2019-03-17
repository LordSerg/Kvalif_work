using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vipusknaya7
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        /*
         В этом проэкте:
             1) рассматривать виживание М по следующему алгоритму:
            а)первым циклом проверяем каждый М на виживание и записывать в второстепенный массив расположение М
            б)вторым циклом проставить все М
            *то есть вместо одного цикла теперь их два
             */
        int size;
        bool[,] a, a1;
        int h, w, num;
        int s_min, s_max, b_value;
        bool b = true, b1 = false;
        Graphics g;

        private void Form1_Load(object sender, EventArgs e)
        {
            check_values();
        }

        private void button1_Click(object sender, EventArgs e)
        {//кнопка "Run"/"Stop"
            if (button1.Text == "Run")
            {
                button1.Text = "Stop";
                timer1.Enabled = true;
            }
            else
            {
                button1.Text = "Run";
                timer1.Enabled = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {//кнопка "One step"
            timer1.Enabled = false;
            button1.Text = "Run";
            num = 0;
            for (int i = 0; i < h; i++)
                for (int j = 0; j < w; j++)
                {
                    if (a[i, j] == true)
                        num++;
                }
            if (num > 0)
                play();
        }

        private void button3_Click(object sender, EventArgs e)
        {//кнопка "Settings"
            panel4.Visible = !panel4.Visible;
            panel3.Visible = !panel3.Visible;
        }

        private void button7_Click(object sender, EventArgs e)
        {//кнопка "OK"
            panel4.Visible = true;
            panel3.Visible = false;
            check_values();
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {//рaсставляем М
            g = pictureBox1.CreateGraphics();
            int i1 = -1, j1 = -1;
            for (int i = 0; i < w; i++)
            {
                if (((double)(e.X - pictureBox1.Location.X) / size <= i) && ((double)(e.X - pictureBox1.Location.X) / 5 >= i - 1))
                {
                    i1 = i;
                    break;
                }
                else if (((double)(e.X - pictureBox1.Location.X) / size > w))
                {
                    i1 = -1;
                }
            }
            for (int j = 0; j < h; j++)
            {
                if (((double)(e.Y - pictureBox1.Location.Y) / size < j) && ((double)(e.Y - pictureBox1.Location.Y) / 5 > j - 1))
                {
                    j1 = j;
                    break;
                }
                else if (((double)(e.Y - pictureBox1.Location.Y) / size > h))
                {
                    j1 = -1;
                }
            }
            if (i1 >= 0 && j1 >= 0)
            {
                a[i1, j1] = !a[i1, j1];
                if (a[i1, j1] == true)
                    g.FillRectangle(Brushes.Black, (i1) * size, (j1) * size, size, size);
                else
                    g.FillRectangle(Brushes.White, (i1) * size, (j1) * size, size, size);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            num = 0;
            for (int i = 0; i < h; i++)
                for (int j = 0; j < w; j++)
                {
                    if (a[i, j] == true)
                        num++;
                }
            if (num > 0)
            {
                play();
                timer1.Enabled = true;
            }
            else
            {
                timer1.Enabled = false;
                button1.Text = "Run";
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            b = true;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            b = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            pictureBox1.Refresh();
            listBox1.Items.Clear();
            for (int i = 0; i < h; i++)
                for (int j = 0; j < w; j++)
                {
                    a[i, j] = false;
                    a1[i, j] = false;
                }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            size = trackBar1.Value;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Random r = new Random();
            int i1, j1;
            g = pictureBox1.CreateGraphics();
            for(int i=0;i<(h*w)/2;i++)
            {
                i1 = r.Next(h);
                j1 = r.Next(w);
                a[i1, j1] = !a[i1, j1];
                if (a[i1, j1] == true)
                    g.FillRectangle(Brushes.Black, (i1) * size, (j1) * size, size, size);
                else
                    g.FillRectangle(Brushes.White, (i1) * size, (j1) * size, size, size);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            g = pictureBox1.CreateGraphics();
            bool bl = true;
            for (int i = 0; i < h; i++, bl = !bl)
                    for (int j = Convert.ToInt32(bl); j < w; j += 2)
                    {
                        a[i, j] = true;
                        g.FillRectangle(Brushes.Black, (i) * size, (j) * size, size, size);
                    }
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            b1 = true;
            label6.Text = "Size of microorganism = " + trackBar1.Value;
        }

        void play()
        {//главная подпрограмма
            int non;//Number Of Neighbors
            //сначала
            for (int i = 0; i < h; i++)
            {
                for (int j = 0; j < w; j++)
                {
                    non = num_neigh(i,j);
                    if (a[i, j] == true)
                    {//есть микроорганизм
                        a1[i, j] = try_surv(i,j,non);
                    }
                    else
                    {//нет  микроорганизма
                        a1[i, j] = try_born(i,j,non);
                    }
                }
            }
            for (int i = 0; i < h; i++)
            {
                for (int j = 0; j < w; j++)
                {
                    //расставляем микроорганизмы и заполняем основной массив
                    a[i, j] = a1[i, j];
                    if(a[i,j]==true)
                    {
                        g.FillRectangle(Brushes.Black, (i) * size, (j) * size, size, size);
                    }
                    else
                        g.FillRectangle(Brushes.White, (i) * size, (j) * size, size, size);
                }
            }
        }
        
        bool try_surv(int i2,int j2,int n)
        {//проверяем, выживет ли М
            bool is_surv = true;
            if (n >= s_min && n <= s_max)
            {
                is_surv = true;
            }
            else
            {
                is_surv = false;
            }
            //listBox1.Items.Add(n);
            return is_surv;
        }

        bool try_born(int i2,int j2,int n)
        {//проверяем, может ли здесь родится М
            bool is_born=true;
            if (n ==b_value)
            {
                is_born = true;
            }
            else
            {
                is_born = false;
            }
            return is_born;
        }

        void check_values()
        {//заносим все параметры в переменные
            h =Convert.ToInt32(textBox1.Text);
            w =Convert.ToInt32(textBox2.Text);
            s_min= Convert.ToInt32(textBox3.Text);
            s_max = Convert.ToInt32(textBox4.Text);
            b_value = Convert.ToInt32(textBox5.Text);
            size = trackBar1.Value;
            label6.Text = "Size of microorganism = "+size;
            if (b == true)
            {//если размер поля был изменен
                a = new bool[h, w];
                a1 = new bool[h, w];
                b = false;
                pictureBox1.Refresh();
                listBox1.Items.Clear();
            }
            if (b1 == true)
            {//если размер М был изменен
                pictureBox1.Refresh();
                for (int i = 0; i < h; i++)
                    for (int j = 0; j < w; j++)
                        if (a[i, j] == true)
                            g.FillRectangle(Brushes.Black, (i) * size, (j) * size, size, size);
                        else
                            g.FillRectangle(Brushes.White, (i) * size, (j) * size, size, size);
            }
        }

        int num_neigh(int i2, int j2)
        {//проверяем количество соседей
            int num = 0;
            try
            {
                if (a[i2, j2 + 1])//сосед снизу
                    num++;
            }
            catch { }
            try
            {
                if (a[i2, j2 - 1])//сверху
                    num++;
            }
            catch { }
            try
            {
                if (a[i2 + 1, j2 + 1])//диагональ вниз направо 
                    num++;
            }
            catch { }
            try
            {
                if (a[i2 + 1, j2 - 1])//диагональ вверх направо 
                    num++;
            }
            catch { }
            try
            {
                if (a[i2 - 1, j2 + 1])//диагональ вниз налево 
                    num++;
            }
            catch { }
            try
            {
                if (a[i2 - 1, j2 - 1])//диагональ ввверх налево 
                    num++;
            }
            catch { }
            try
            {
                if (a[i2 + 1, j2])//сосед справа
                    num++;
            }
            catch { }
            try
            {
                if (a[i2 - 1, j2])//слева
                    num++;
            }
            catch { }
            return num;
        }
    }
}
