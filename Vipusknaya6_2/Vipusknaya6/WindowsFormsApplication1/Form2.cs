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
    public partial class Form2 : Form
    {
        public Form2()
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

        //переменные для КМ:
        int h, w;//размеры поля
                 //это понадобится для построения графика:
        int[] n;//массив, который накапливает в себе количества М на поле в течении всего времени                
        double[] concentration;//массив, который накапливает в себе концентрацию М на поле (в%) в течении всего времени
        double[] configuration;
        Graphics g;
        int z;//размер одного М = z*z
        bool[,] a;
        int abc;//счетчик итераций
        //переменные для виживания:
        int q1 = 1, q2 = 3, q3 = 4, f = 0, r;
        //переменные для циклов:
        int c = 1;
        int rmb, xyz = 0;
        //переменные для графиков:
        bool[,] a_g;
        int num_of_same_m=0;

        private void Form2_Load(object sender, EventArgs e)
        {
            a1 = true;
            b1 = true;
            vmin =2;
            vmax =6;
            bmin =2;
            bmax =5;
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
            //по нажатии запускается игра
            //но если игра уже запущена и эта кнопка еще раз нажата, то можно ее приостановить
            if (button1.Text == "Розпочати")
            {
                xyz = 0;
                button4.Visible = true;
                button2.Visible = true;
                listBox1.Visible = true;
                label2.Visible = true;
                listBox1.Items.Clear();
                abc = 0;
                q1 = 1;
                q2 = 3;
                q3 = 4;
                f = 0;
                r = 0;
                c = 1;
                pictureBox1.Refresh();
                int x, y;
                h = int.Parse(textBox2.Text);
                w = int.Parse(textBox3.Text);
                n = new int[10000];
                //birth_g = new int[9][];
                //surv_g = new int[9][];
                //for (int i = 0; i < 9; i++)
                //{
                //    birth_g[i] = new int[100000];
                //    surv_g[i] = new int[100000];
                //}
                if (w * h < int.Parse(textBox1.Text))//на случай если заданое количество М больше чем поле
                {
                    n[abc] = w * h;
                    textBox1.Text = (w * h).ToString();
                }
                else
                    n[abc] = int.Parse(textBox1.Text);
                rmb = 0;
                concentration = new double[10000];
                configuration = new double[10000];
                //концентрация будет высщитоватся в процентах, поэтому умножаем на 100:
                concentration[abc] = Convert.ToDouble(n[abc] * 100 / (h * w));//расчитываем начальную концентрацию

                a = new bool[w, h];
                a_g = new bool[w, h];
                for (int i = 0; i < w; i++)
                    for (int j = 0; j < h; j++)
                    {
                        a_g[i, j] = false;
                        a[i, j] = false;
                    }
                listBox1.Items.Add("Ітерація № 0 :");
                listBox1.Items.Add("Кіль-ть = " + n[0]);
                listBox1.Items.Add("Концентрація = " + concentration[0] + " %");
                g = pictureBox1.CreateGraphics();
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
                        a_g[x, y] = false;
                    }
                    else
                    {
                        i--;
                    }
                }
                timer1.Interval = 50;
                timer1.Enabled = true;
                button1.Text = "Призупинити";
            }
            else
            {
                timer1.Enabled = !timer1.Enabled;
                if (button1.Text == "Призупинити")
                    button1.Text = "Продовжити";
                else
                    button1.Text = "Призупинити";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {//графіки

            string path = @"Text.txt";
            //string str="";
            if (!File.Exists(path))
                File.Create("Text");
            System.IO.StreamWriter file = new System.IO.StreamWriter(path, false);//очищаем
            file.WriteLine("");
            file.Close();
            file = new StreamWriter(path);
            //записи о количестве М
            for (int i = 0; i < abc; i++)
            {
                file.Write(n[i] + " ");
            }
            file.Write(n[abc]);
            //записи о концентрации М
            file.WriteLine("");
            for (int i = 0; i < abc; i++)
            {
                file.Write(concentration[i] + " ");
            }
            file.Write(concentration[abc]);
            //
            file.WriteLine("");
            for (int i = 0; i < abc-1; i++)
            {
                file.Write(configuration[i] + " ");
            }
            file.Write(configuration[abc-1]);
            file.Close();

            Graphic f = new Graphic();
            f.Show();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            //налаштування 
            bool x123 = timer1.Enabled;
            if (button1.Text != "Розпочати")
            {
                timer1.Enabled = false;
                button1.Text = "Продовжити";
            }
            Settings s = new Settings();
            s.Text = a1+" "+b1+" "+vmin+" "+vmax+" "+bmin+" "+bmax+" "+size_micr+" "+diff_cycles
                +" "+b[0]+" " + b[1] + " " + b[2] + " " + b[3] + " " + b[4] + " " + b[5] + " "
                + b[6] + " " + b[7] + " " + b[8] + " " + b[9] + " " + v[0] + " " + v[1] + " " 
                + v[2] + " " + v[3] + " "+ v[4] + " " + v[5] + " " + v[6] + " " + v[7] + " "
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
                timer1.Enabled = x123;
                if (x123 != true)
                    button1.Text = "Продовжити";
                else
                    button1.Text = "Призупинити";
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //s_g = 0;
            //b_g = 0;
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
            if (a1 == false)
            {
                //f Є [1 ; 10]
                if (f <= 9)
                {
                    f++;
                    f = id_limit(f, true);
                }
                else
                {
                    f = 1;
                    f = id_limit(f, true);
                }
            }
            if (b1 == false)
            {
                //r Є [1 ; 10]
                if (r <= 9)
                {
                    r++;
                    r = id_limit(r, false);
                }
                else
                {
                    r = 1;
                    r = id_limit(r, false);
                }
            }

            //циклы:
            if (diff_cycles == true)//если настройки позволяют разносторонние циклы
            {
                if (c == 1)//по строкам слева на право и сверху вниз
                    for (int j = 0; j < h; j++)
                        for (int i = 0; i < w; i++)
                            body_of_the_cycle(i, j);
                if (c == 2)//по столбцам с сверху вниз и справа на лево
                    for (int i = w - 1; i >= 0; i--)
                        for (int j = 0; j < h; j++)
                            body_of_the_cycle(i, j);
                if (c == 3)//по строкам справа на лево и снизу вверх
                    for (int j = h - 1; j >= 0; j--)
                        for (int i = w - 1; i >= 0; i--)
                            body_of_the_cycle(i, j);
                if (c == 4)//по столбцам снизу вверх и слева на право
                    for (int i = 0; i < w; i++)
                        for (int j = h - 1; j >= 0; j--)
                            body_of_the_cycle(i, j);
                if (c == 5)//по столбцам сверху вниз и слева на право
                    for (int i = 0; i < w; i++)
                        for (int j = 0; j < h; j++)
                            body_of_the_cycle(i, j);
                if (c == 6)//по строкам слева на право и снизу вверх 
                    for (int j = h - 1; j >= 0; j--)
                        for (int i = 0; i < w; i++)
                            body_of_the_cycle(i, j);
                if (c == 7)//по столбцам снизу вверх и справа на лево
                    for (int i = w - 1; i >= 0; i--)
                        for (int j = h - 1; j >= 0; j--)
                            body_of_the_cycle(i, j);
                if (c == 8)//по строкам справа на лево и сверху вниз
                    for (int j = 0; j < h; j++)
                        for (int i = w - 1; i >= 0; i--)
                            body_of_the_cycle(i, j);
                if (c <= 7)
                {
                    c++;
                }
                else
                    c = 1;
            }
            else//если же настройки не позволяют разносторонние циклы, то 
            {//применяем стандартный цикл
                for (int i = 0; i < w; i++)
                    for (int j = 0; j < h; j++)
                        body_of_the_cycle(i, j);
            }
            //подсчет М на поле
            int schet = 0;
            for (int i = 0; i < w; i++)
                for (int j = 0; j < h; j++)
                    if (a[i, j] == true)
                    {
                        schet++;
                    }
            n[abc] = schet;
            concentration[abc] = Convert.ToDouble(n[abc] * 100 / (h * w));
            if (n[abc] < 1)
            {
                timer1.Enabled = false;
                button1.Text = "Розпочати";
            }
            if (n[abc] == h * w)
            {
                timer1.Enabled = false;
                button1.Text = "Розпочати";
            }
            //на случай, если останутся только неподвижные КМ
            if (abc > 0)
            {
                if (n[abc] == rmb)
                {
                    xyz++;
                }
                else
                {
                    xyz = 0;
                }
            }
            rmb = n[abc];
            if (xyz >= 20)
            {
                for (int i = 0; i < w; i++)
                    for (int j = 0; j < h; j++)
                        a[i, j] = false;
                timer1.Enabled = false;
                //Record();
                button1.Text = "Розпочати";
                MessageBox.Show("Оскільки КМ за останні 20 ітерацій не змінилась, то гра вважається закінченою", "", MessageBoxButtons.OK);
                pictureBox1.Refresh();
            }
            //выводим все данные на экран
            listBox1.Items.Add("Ітерація № " + abc + " :");
            listBox1.Items.Add("Кіль-ть = " + n[abc]);
            listBox1.Items.Add("Концентрація = " + concentration[abc] + " %");
            if (a1 == true)
            {
                if (q2 == q3)
                {
                    listBox1.Items.Add("Зникає, якщо сусідів = " + q2);
                }
                listBox1.Items.Add("Зникає, якщо сусідів >" + Convert.ToInt32(q2) + " і <" + Convert.ToInt32(q3));
                //surv_g[][]
            }
            if (a1 == false)
            {
                listBox1.Items.Add("виживає, якщо сусіди:");
                if (f == 1)
                    listBox1.Items.Add("1 зверху і 1 знизу");
                if (f == 2)
                    listBox1.Items.Add("1 зверху і 1 справа");
                if (f == 3)
                    listBox1.Items.Add("2 сусіда зверху і 1 зліва");
                if (f == 4)
                    listBox1.Items.Add("1 зліва зверху і 1 справа знизу");
                if (f == 5)
                    listBox1.Items.Add("1 знизу і 1 зліва");
                if (f == 6)
                    listBox1.Items.Add("1 справа зверху і 1 зліва знизу");
                if (f == 7)
                    listBox1.Items.Add("1 справа зверху і 1 зліва зверху");
                if (f == 8)
                    listBox1.Items.Add("1 справа знизу і 1 зліва знизу");
                if (f == 9)
                    listBox1.Items.Add("1 зліва і 1 справа зверху");
                if (f == 10)
                    listBox1.Items.Add("1 зліва і 1 справа");
            }
            if (b1 == true)
            {
                listBox1.Items.Add("Народжується, якщо сусідів = " + q1);
                //birth_g[q1][abc] =;
            }
            if (b1 == false)
            {
                listBox1.Items.Add("Народжується, якщо сусіди росташовані:");
                if (r == 1)
                    listBox1.Items.Add("1 зверху і 1 знизу");
                if (r == 2)
                    listBox1.Items.Add("1 зверху і 1 справа");
                if (r == 3)
                    listBox1.Items.Add("2 сусіда зверху і 1 зліва");
                if (r == 4)
                    listBox1.Items.Add("1 зліва зверху і 1 справа знизу");
                if (r == 5)
                    listBox1.Items.Add("1 знизу і 1 зліва");
                if (r == 6)
                    listBox1.Items.Add("1 справа зверху і 1 зліва знизу");
                if (r == 7)
                    listBox1.Items.Add("1 справа зверху і 1 зліва зверху");
                if (r == 8)
                    listBox1.Items.Add("1 справа знизу і 1 зліва знизу");
                if (r == 9)
                    listBox1.Items.Add("1 зліва і 1 справа зверху");
                if (r == 10)
                    listBox1.Items.Add("1 зліва і 1 справа");
            }
            //перевіряємо конфігурацію
            num_of_same_m = compare(a_g, a, h, w);
            for (int i = 0; i < w; i++)
                for (int j = 0; j < h; j++)
                {
                    a_g[i, j] = a[i, j];
                }
            configuration[abc - 1] = ((100 * num_of_same_m) / (w * h));
            listBox1.Items.Add("Співпадаэ " + configuration[abc-1].ToString() + " %");
            num_of_same_m = 0;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            for (int i = 0; i < w; i++)
                for (int j = 0; j < h; j++)
                    a[i, j] = false;
            pictureBox1.Refresh();
            button1.Text = "Розпочати";
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            for (int i = 0; i < w; i++)
                for (int j = 0; j < h; j++)
                    a[i, j] = false;
            pictureBox1.Refresh();
            button1.Text = "Розпочати";
        }

        void body_of_the_cycle(int i, int j)
        {
            //чтобы не вызывать функцию несколько лишних раз - приравниваем ее к переменной k
            int k = num_of_neigh(i, j);
            if (a1==true)//если виживание зависит от количества соседей:
            {
                if (a[i, j])//если в этой координате есть М, то проверяем количество соседей
                {
                    if (k > q2 - 1 && k < q3 + 1)//
                    {
                        g.FillRectangle(Brushes.Gold, i * z, j * z, z, z);
                        g.DrawRectangle(Pens.Red, i * z, j * z, z, z);
                    }
                    else
                    {
                        a[i, j] = false;
                        g.FillRectangle(Brushes.White, i * z, j * z, z + 1, z + 1);
                        //s_g--;
                    }
                }
                //если же здесь (в этой координате) ничего нет и количество соседей = q1  
                //и если настройки позволяют,то здесь появляется новый М                
                birth(k, i, j, r);
            }
            if (a1==false)//если виживание зависит от расположения соседий:
            {
                if (a[i, j])//если здесь М
                {
                    if (location_of_neigh(f, i, j, false) == true)//если условия виживания положительные,
                    {
                        g.FillRectangle(Brushes.Gold, i * z, j * z, z, z);//то М остается
                        g.DrawRectangle(Pens.Red, i * z, j * z, z, z);
                    }
                    else//в инном случае М умирает
                    {
                        a[i, j] = false;
                        g.FillRectangle(Brushes.White, i * z, j * z, z + 1, z + 1);
                        //s_g
                    }
                }
                birth(k, i, j, r);
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

        void birth(int k, int i, int j, int r)
        {
            if (b1==true)//виживание от количества соседей
            {
                if (k == q1)
                {
                    a[i, j] = true;
                    g.FillRectangle(Brushes.Gold, i * z, j * z, z, z);
                    g.DrawRectangle(Pens.Red, i * z, j * z, z, z);
                    //b_g++;
                }
            }
            else if (b1==false)//если условие виживания зависит от расположения соседей
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

        private void button3_Click(object sender, EventArgs e)
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
