using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Vipusknaya2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        int h, w;//размеры поля
        int[] n;//массив, который накапливает в себе количества М на поле в течении всего времени
                //это понадобится для построения графика
        Graphics g, g1;
        int z = 10;
        int[,] a;
        int abc;//счетчик итераций

        private void button1_Click(object sender, EventArgs e)
        {//по нажатии запускается
            listBox1.Items.Clear();
            abc = 0;
            pictureBox1.Refresh();
            int x, y;
            n = new int[1000];//
            n[abc] = int.Parse(textBox1.Text);
            h = int.Parse(textBox2.Text);
            w = int.Parse(textBox3.Text);
            a = new int[w,h];
            for (int i = 0; i < w; i++)
                for (int j = 0; j < h; j++)
                    a[i, j] = 0;
            listBox1.Items.Add("Итерация № 0:");
            listBox1.Items.Add("n = " + n[0]);
            g =pictureBox1.CreateGraphics();
            Random r = new Random();
            for (int i = 0; i < n[abc]; i++)
            {
                x = r.Next(w);
                y = r.Next(h);
                if (a[x, y] != 1)
                {
                    a[x, y] = 1;
                    
                    g.FillRectangle(Brushes.Gold, x*z, y*z, z, z);
                    g.DrawRectangle(Pens.Red, x*z, y*z, z, z);
                }
                else
                {
                    i--;
                }
            }
            timer1.Interval = 50;
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Random r = new Random();
            //если виживание зависит от количества соседей:
            if (radioButton1.Checked == true)
            {
                //в пустой клетке, рядом с которой ровно q1 живые клетки, зарождается жизнь;
                //если у живой клетки есть от q2 до q3 живых соседа, то эта клетка продолжает жить; 
                //в противном случае, если соседей меньше q2 или больше q3, клетка умирает («от одиночества» или «от перенаселённости»)

                abc++;
                int q1, q2, q3;
                int d = 0;//счетчик умерших
                q1 = r.Next(2, 5);
                q2 = r.Next(2, 4);
                q3 = q2 + r.Next(1, 3);
                for (int i = 0; i < w; i++)
                {
                    for (int j = 0; j < h; j++)
                    {//чтобы не вызывать функцию несколько лишних раз - приравниваем ее к переменной k
                        int k = num_of_neigh(i, j);
                        if (a[i, j] == 1)
                        {
                            if (k > q2 - 1 && k < q3 + 1)
                            {
                                g.FillRectangle(Brushes.Gold, i * z, j * z, z, z);
                                g.DrawRectangle(Pens.Red, i * z, j * z, z, z);
                            }
                            else
                            {
                                d++;
                                a[i, j] = 0;
                                g.FillRectangle(Brushes.White, i * z, j * z, z + 1, z + 1);
                            }
                        }
                        else if (k == q1)
                        {
                            d--;
                            a[i, j] = 1;
                            g.FillRectangle(Brushes.Gold, i * z, j * z, z, z);
                            g.DrawRectangle(Pens.Red, i * z, j * z, z, z);
                        }
                    }
                }
                n[abc] = n[abc - 1] - d;
                if (n[abc] < 1)
                {
                    timer1.Enabled = false;
                }
                if (n[abc] == h * w)
                {
                    timer1.Enabled = false;
                }
                listBox1.Items.Add("Итерация №" + abc + ":");
                listBox1.Items.Add("n = " + n[abc]);
                listBox1.Items.Add("Умерает, если соседей >" + Convert.ToInt32(q2 - 1) + " и <" + Convert.ToInt32(q3 + 1));
                listBox1.Items.Add("Рождается, если соседей = " + q1);
            }
            else//если виживание зависит от расположения соседий:
            {
                abc++;
                int d = 0;//счетчик умерших
                int q1 = r.Next(2, 5);
                /*
                for (int i = 0; i < w; i++)
                {
                    for (int j = 0; j < h; j++)
                    {
                        if (a[i, j] == 1)//если здесь М
                        {
                            if (location_of_neigh(r.Next(1, 5), i, j) == true)//если условия виживания положительные,
                            {
                                g.FillRectangle(Brushes.Gold, i * z, j * z, z, z);//то М остается
                                g.DrawRectangle(Pens.Red, i * z, j * z, z, z);
                            }
                            else//в инном случае М умирает
                            {
                                d++;
                                a[i, j] = 0;
                                g.FillRectangle(Brushes.White, i * z, j * z, z + 1, z + 1);
                            }
                        }
                        else if (num_of_neigh(i, j) == q1)
                        {
                            d--;
                            a[i, j] = 1;
                            g.FillRectangle(Brushes.Gold, i * z, j * z, z, z);
                            g.DrawRectangle(Pens.Red, i * z, j * z, z, z);
                        }
                    }
                }*/
                //так как условия смерти у кажой клетки разное, то все М умирают за 2-3 цикла
                //поэтому акждый цикл будет ити по своиму условию смерти:
                int f = r.Next(1, 5);
                
                for (int i = 0; i < w; i++)
                {
                    for (int j = 0; j < h; j++)
                    {
                        if (a[i, j] == 1)//если здесь М
                        {
                            if (location_of_neigh(f, i, j) == true)//если условия виживания положительные,
                            {
                                g.FillRectangle(Brushes.Gold, i * z, j * z, z, z);//то М остается
                                g.DrawRectangle(Pens.Red, i * z, j * z, z, z);
                            }
                            else//в инном случае М умирает
                            {
                                d++;
                                a[i, j] = 0;
                                g.FillRectangle(Brushes.White, i * z, j * z, z + 1, z + 1);
                            }
                        }
                        else if (num_of_neigh(i, j) == q1)
                        {
                            d--;
                            a[i, j] = 1;
                            g.FillRectangle(Brushes.Gold, i * z, j * z, z, z);
                            g.DrawRectangle(Pens.Red, i * z, j * z, z, z);
                        }
                    }
                }
                n[abc] = n[abc - 1] - d;
                if (n[abc] < 1)
                {
                    timer1.Enabled = false;
                }
                if (n[abc] == h * w)
                {
                    timer1.Enabled = false;
                }
                listBox1.Items.Add("Итерация №" + abc + ":");
                listBox1.Items.Add("n = " + n[abc]);
                listBox1.Items.Add("живет, если соседи:");
                if (f == 1)
                    listBox1.Items.Add("1 сверху и 1 справа");
                if (f == 2)
                    listBox1.Items.Add("2 соседа сверху и 1 слева");
                if (f == 3)
                    listBox1.Items.Add("1 слева сверху и 1 справа снизу");
                if (f == 4)
                    listBox1.Items.Add("1 сверху и 1 снизу");
                listBox1.Items.Add("Рождается, если соседей = " + q1);
                
            }

        }

        bool location_of_neigh(int q, int i, int j)
        {
            bool bl = false;
            int num = 0;
            if (q == 1)//если 1 сверху и 1 справа
            {
                try
                {
                    if (a[i, j - 1] == 1)//сверху
                        num++;
                }
                catch { }
                try
                {
                    if (a[i+1, j] == 1)//справа
                        num++;
                }
                catch { }
                if (num == 2)
                    bl = true;
                else
                    bl = false;
            }
            if (q == 2)//если 2 соседа сверху (подряд) и 1 слева 
            {
                try
                {
                    if (a[i, j - 1] == 1)//сверху на 1
                        num++;
                }
                catch { }
                try
                {
                    if (a[i, j-2] == 1)//сверху на 2
                        num++;
                }
                catch { }
                try
                {
                    if (a[i-1, j] == 1)//слева
                        num++;
                }
                catch { }
                if (num == 3)
                    bl = true;
                else
                    bl = false;
            }
            if (q == 3)//если 1 слева сверху и 1 справа снизу
            {
                try
                {
                    if (a[i - 1, j - 1] == 1)//слева сверху
                        num++;
                }
                catch { }
                try
                {
                    if (a[i + 1, j + 1] == 1)//справа снизу
                        num++;
                }
                catch { }
                if (num == 2)
                    bl = true;
                else
                    bl = false;
            }
            if (q == 4)//если 1 сверху и 1 снизу
            {
                try
                {
                    if (a[i, j - 1] == 1)//сверху
                        num++;
                }
                catch { }
                try
                {
                    if (a[i, j + 1] == 1)//снизу
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

        int num_of_neigh(int i, int j)
        {//подсчет количества соседей у координаты (i;j)
            int num = 0;
            try
            {
                if (a[i, j + 1] == 1)//сосед снизу
                    num++;
            }
            catch { }
            try
            {
                if (a[i, j - 1] == 1)//сверху
                    num++;
            }
            catch { }
            try
            {
                if (a[i + 1, j + 1] == 1)//диагональ вниз направо 
                    num++;
            }
            catch { }
            try
            {
                if (a[i + 1, j - 1] == 1)//диагональ вверх направо 
                    num++;
            }
            catch { }
            try
            {
                if (a[i - 1, j + 1] == 1)//диагональ вниз налево 
                    num++;
            }
            catch { }
            try
            {
                if (a[i - 1, j - 1] == 1)//диагональ ввверх налево 
                    num++;
            }
            catch { }
            try
            {
                if (a[i + 1, j] == 1)//сосед справа
                    num++;
            }
            catch { }
            try
            {
                if (a[i - 1, j] == 1)//слева
                    num++;
            }
            catch { }
            return num;
        }

       
        private void button3_Click(object sender, EventArgs e)
        {//по нажатию строится график 
            pictureBox1.Visible = false;
            pictureBox2.Visible = true;
            pictureBox2.Refresh();
            //рисуем график
            int x0, y0, x1, y1, max = 0;
            g1 = pictureBox2.CreateGraphics();
            x0 = 0;
            y0 = pictureBox2.Height - 5;
            x1 = pictureBox2.Location.X + pictureBox2.Width;
            y1 = y0;
            //горизонтальная ось времени:
            g1.DrawLine(Pens.Black, x0, y0, x1, y1);
            //вертикальная ось количества М:
            x1 = 0;
            y1 = pictureBox2.Location.Y;
            g1.DrawLine(Pens.Black, x0, y0, x1, y1);
            //находим максимальное значение М из всех
            if (abc > 0)//ограничение на случай нажатия на кнопку перед жизнью колонии М
            {
                for (int i = 0; i < abc + 1; i++)
                {
                    if (max < n[i])
                        max = n[i];
                }
                x1 = 0;
                y1 = pictureBox2.Height;
                for (int i = 0; i < abc + 1; i++)
                {
                    g1.DrawLine(Pens.Red, x1, y1, i * pictureBox2.Width / abc, pictureBox2.Height - 5 - (n[i]) * pictureBox2.Height / max);
                    x1 = i * pictureBox2.Width / abc;
                    y1 = pictureBox2.Height - 5 - (n[i]) * pictureBox2.Height / max;
                    g1.DrawEllipse(Pens.Black, x1 - 5, y1 - 5, 10, 10);//
                }
            }            
        }

        private void button2_Click(object sender, EventArgs e)
        {//по нажатию этой кнопки переход к расположению М
            pictureBox1.Visible = true;
            pictureBox2.Visible = false;
        }
    }
}
