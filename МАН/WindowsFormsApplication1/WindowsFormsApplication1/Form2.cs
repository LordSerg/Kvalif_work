using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        //переменные, отвечающие за настройки:
        int vmin, vmax, bmin, bmax;
        bool[,] a, a0;//массивы КМ (один для просмотра, второй - для непосредственной расстановки)
        int size;//размер М на поле
        int h, w;//размеры поля
                 //это понадобится для построения графика:
        int[] n;//массив, который накапливает в себе количества М на поле в течении всего времени                
        double[] concentration;//массив, который накапливает в себе концентрацию М на поле (в%) в течении всего времени
        double[] configuration;
        Graphics g;
        int abc,abc0;//счетчик итераций
        //переменные для виживания:
        int q1, q2, q3;
        //q1 Є [bmin ; bmax]
        //q2 Є [vmin ; (vmin+vmax)/2] 
        //q3 Є [(vmin+vmax)/2 ; vmax]
        int num_of_same_m = 0;//переменная для графиков
        bool rnd;//случайные ли изменения выживания?
        int start_num;
        int t1, t2;

        private void Form2_Load(object sender, EventArgs e)
        {
            w = 50;
            h = 50;
            vmin = 2;
            vmax = 6;
            bmin = 2;
            bmax = 5;
            rnd = false;
            q1 = bmin;
            q2 = vmin;
            q3 = (vmin + vmax) / 2;
            start_num = 250;
            if (pictureBox1.Width > pictureBox1.Height)
                size = pictureBox1.Width / w;
            else
                size = pictureBox1.Height / h;
            t1 = 100;
            t2 = 100;
            timer1.Interval = t1;
            timer2.Interval = t2;
        }

        private void button1_Click(object sender, EventArgs e)
        {//settings
            Form3 f = new Form3();
            f.Show();
            //w = ;
            //h = ;
            //start_num = ;
            //vmin = ;
            //vmax = ;
            //bmin = ;
            //bmax = ;
            //rnd = ;
            //t1 = ;
            //t2 = ;
            
            timer1.Interval = t1;
            timer2.Interval = t2;
            q1 = bmin;
            q2 = vmin;
            q3 = (vmin + vmax) / 2;
            if (pictureBox1.Width > pictureBox1.Height)
                size = pictureBox1.Width / w;
            else
                size = pictureBox1.Height / h;
        }

        private void button2_Click(object sender, EventArgs e)
        {//start
            if (button2.Text == "Старт"&&timer1.Enabled==false)
            {
                g = pictureBox1.CreateGraphics();
                button3.Visible = true;
                button4.Visible = true;
                button5.Visible = true;
                if (pictureBox1.Width > pictureBox1.Height)
                    size = (pictureBox1.Width / w)-1;
                else
                    size = (pictureBox1.Height / h)-1;
                a = new bool[h, w];
                a0 = new bool[h, w];
                n = new int[10000];
                configuration = new double[10000];
                n[0] = start_num;
                abc = 0;
                abc0 = 0;
                Random r = new Random();
                int x, y;
                for(int i=0;i<n[0];i++)
                {
                    x = r.Next(w);
                    y = r.Next(h);
                    if (!a[x, y])
                    {
                        a[x, y] = true;
                        a0[x, y] = true;
                        g.FillRectangle(Brushes.Gold, x * size, y * size, size, size);
                        g.DrawRectangle(Pens.Red, x * size, y * size, size, size);
                    }
                }
                timer1.Interval = t1;
                timer2.Interval = t2;
                timer1.Enabled = true;
                timer2.Enabled = false;
                if (t1 != t2)
                    timer2.Enabled = true;
                button2.Text = "Стоп";
            }
            else if(button2.Text == "Старт" && timer1.Enabled==true)
            {
                timer1.Enabled = true;
                timer2.Enabled = false;
                if (t1 != t2)
                    timer2.Enabled = true;
            }
            else
            {
                timer1.Enabled = false;
                timer2.Enabled = false;
                button2.Text = "Старт";
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {//one step
            main_function();
            main_function_cond();
        }

        private void button4_Click(object sender, EventArgs e)
        {//refresh
            for (int i = 0; i < h; i++)
                for (int j = 0; j < w; j++)
                {
                    a[i, j] = false;
                    a0[i, j] = false;
                }
            timer1.Enabled = false;
            timer2.Enabled = false;
            button2.Text = "Старт";
            button3.Enabled = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {//graphics

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (t1 == t2)
                main_function_cond();
            main_function();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            main_function_cond();
        }

        void main_function()
        {//в этой функции проводятся изменения КМ
         //1.смотрим, где М умрет и где появится
            abc++;
            for (int i = 0; i < h; i++)
                for (int j = 0; j < w; j++)
                {
                    body_of_the_cycle(i, j);
                }
            //2.ставим получившееся + следим за статистикой
            for (int i = 0; i < h; i++)
                for (int j = 0; j < w; j++)
                {
                    if (a[i, j] == a0[i, j])
                        configuration[abc]++;
                    a[i, j] = a0[i, j];
                    if (a[i, j])
                    {
                        g.FillRectangle(Brushes.Gold, i * size, j * size, size, size);
                        g.DrawRectangle(Pens.Red, i * size, j * size, size, size);
                        n[abc]++;
                    }
                    else
                    {
                        g.FillRectangle(Brushes.White, i * size, j * size, size + 1, size + 1);
                    }
                }
            configuration[abc] = (configuration[abc] * 100) / (h * w);

        }

        void main_function_cond()
        {//в этой функции изменяются только условия
            abc0++;
            if (!rnd)
            {//последовательные изменения 
                //q1 Є [bmin ; bmax]
                if (q1 <= bmax - 1)
                    q1++;
                else
                    q1 = bmin;
                //q2 Є [ vmin ; (vmin+vmax)/2 )
                if (q2 < ((vmin + vmax) / 2) - 1)
                    q2++;
                else
                    q2 = vmin;
                //q3 Є [(vmin+vmax)/2 +1; vmax]
                if (q3 < vmax)
                    q3++;
                else
                    q3 = ((vmin + vmax) / 2) + 1;
            }
            else
            {//случайные изменения

            }
        }

        void body_of_the_cycle(int i, int j)
        {
            //чтобы не вызывать функцию несколько лишних раз - приравниваем ее к переменной k
            int k = num_of_neigh(i, j);
            if (a[i, j])//если в этой координате есть М, то проверяем количество соседей
            {
                if (k > q2 - 1 && k < q3 + 1)//
                {

                }
                else
                {
                    a0[i, j] = false;
                }
            }
            else
            {
                //если же здесь (в этой координате) ничего нет и количество соседей = q1  
                //и если настройки позволяют,то здесь появляется новый М
                if (k == q1)
                {
                    a0[i, j] = true;
                    //g.FillRectangle(Brushes.Gold, i * size, j * size, size, size);
                    //g.DrawRectangle(Pens.Red, i * size, j * size, size, size);
                }
            }
        }

        int num_of_neigh(int i, int j)
        {//подсчет количества соседей у координаты (i;j)
            int num = 0;
            try
            {
                if (a[i, j + 1])//сосед снизу
                    num++;
            }
            catch { }
            try
            {
                if (a[i, j - 1])//сверху
                    num++;
            }
            catch { }
            try
            {
                if (a[i + 1, j + 1])//диагональ вниз направо 
                    num++;
            }
            catch { }
            try
            {
                if (a[i + 1, j - 1])//диагональ вверх направо 
                    num++;
            }
            catch { }
            try
            {
                if (a[i - 1, j + 1])//диагональ вниз налево 
                    num++;
            }
            catch { }
            try
            {
                if (a[i - 1, j - 1])//диагональ ввверх налево 
                    num++;
            }
            catch { }
            try
            {
                if (a[i + 1, j])//сосед справа
                    num++;
            }
            catch { }
            try
            {
                if (a[i - 1, j])//слева
                    num++;
            }
            catch { }
            return num;
        }
    }
}
