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
using System.IO;

namespace Vipusknaya2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        /*
         в этом проекте добавляется :
         1)txt файл, зписывающий в себе данные  про разные КМ с разными начальными данными
         2)последовательные изменения виживания
         3)последовательные изменения циклов (по строкам и по столбцам в разные стороны)
         4)настойки
             */



        int h, w;//размеры поля
            //это понадобится для построения графика:
        int[] n;//массив, который накапливает в себе количества М на поле в течении всего времени                
        double[] concentration;//массив, который накапливает в себе концентрацию М на поле (в%) в течении всего времени
        Graphics g, g1;
        int z = 10;//размер одного М = z*z
        bool[,] a;
        int abc;//счетчик итераций
        string[] s2;
        string[] s1;
        int m;
        string path;
        //переменные для виживания:
        int q1 = 1, q2 = 3, q3 = 4, f = 0, r;
                
        //переменные для циклов:
        int c=1;
        int rmb, xyz = 0;
        


        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Add("График количества М на поле");
            comboBox1.Items.Add("График концентрации (в%) М на поле");
            //comboBox1.Items.Add("График средней продолжительности жизни КМ от начальной концентрации ");
            //считываем файл:
            path = System.IO.Path.GetFullPath("History.txt");
            s1 = File.ReadAllLines(path);

            //пример записи в файле:
            //0 2 50 50 0 2500 100 1 0 0     ,где
            //первое слово в строке означает порядковый номер попытки
            //второе слово означает количество "жизненных циклов"
            //третье и четвертое - означают размеры
            //дальше каждые 3(5) слов означают соответственно: № Итерации, Количество М, Концентрация(, условие смерти и условие рождения)

            s2 = s1[s1.Length - 1].Split(' ');
            m = Convert.ToInt32(s2[0]);
            toolTip1.SetToolTip(this.button6,"Сбросить все настройки на исходные");
            toolTip1.SetToolTip(this.button5, "Подтвердить");

        }

        private void button1_Click(object sender, EventArgs e)
        {//по нажатии запускается игра
            //но если игра уже запущена и эта кнопка еще раз нажата, то можно ее приостановить
            if (button1.Text == "Начать")
            {
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
                if (w * h < int.Parse(textBox1.Text))//на случай если заданое количество М больше чем поле
                {
                    n[abc] = w * h;
                    textBox1.Text = (w*h).ToString();
                }
                else
                    n[abc] = int.Parse(textBox1.Text);
                rmb = 0;
                concentration = new double[10000];
                //концентрация будет высщитоватся в процентах, поэтому умножаем на 100:
                concentration[abc] = Convert.ToDouble(n[abc] * 100 / (h * w));//расчитываем начальную концентрацию
                a = new bool[w, h];
                for (int i = 0; i < w; i++)
                    for (int j = 0; j < h; j++)
                        a[i, j] = false;
                listBox1.Items.Add("Итерация № 0 :");
                listBox1.Items.Add("n = " + n[0]);
                listBox1.Items.Add("Концентрация = " + concentration[0] + " %");
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
                    }
                    else
                    {
                        i--;
                    }
                }
                timer1.Interval = 50;
                timer1.Enabled = true;
                button1.Text = "Преостановить";
            }
            else
            {
                timer1.Enabled = !timer1.Enabled;
                if (button1.Text == "Преостановить")
                    button1.Text = "Продолжить";
                else
                    button1.Text = "Преостановить";
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //в пустой клетке, рядом с которой ровно q1 живые клетки, зарождается жизнь;
            //если у живой клетки есть от q2 до q3 живых соседа, то эта клетка продолжает жить; 
            //в противном случае, если соседей меньше q2 или больше q3, клетка умирает («от одиночества» или «от перенаселённости»)

            //в этом проэкте будет практиковать последовательные изменения виживания
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
            if (radioButton2.Checked)
            {
                //f Є [1 ; 10]
                if (f <= 9)
                {
                    f++;
                    f = id_limit(f, false);
                }
                else
                {
                    f = 1;
                    f = id_limit(f, false);
                }
            }
            if (radioButton3.Checked)
            {
                //r Є [1 ; 10]
                if (r <= 9)
                {
                    r++;
                    r = id_limit(r, true);
                }
                else
                {
                    r = 1;
                    r = id_limit(r, true);
                }
            }

            //циклы:
            if (checkBox1.Checked == true)//если настройки позволяют разносторонние циклы
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
            int schet=0;
            for (int i = 0; i < w; i++)
                for (int j = 0; j < h; j++)
                    if(a[i,j]==true)
                    {
                        schet++;
                    }
            n[abc] = schet;
            concentration[abc] = Convert.ToDouble(n[abc] * 100 / (h * w));
            if (n[abc] < 1)
            {
                timer1.Enabled = false;
                Record();
                button1.Text = "Начать";
            }
            if (n[abc] == h * w)
            {
                timer1.Enabled = false;
                Record();
                button1.Text = "Начать";
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
                Record();
                button1.Text = "Начать";
                MessageBox.Show("Так как КМ за последние 20 итераций не изменялась, то игра считается законченой", "", MessageBoxButtons.OK);
                pictureBox1.Refresh();
            }
            listBox1.Items.Add("Итерация № " + abc + " :");
            listBox1.Items.Add("n = " + n[abc]);
            listBox1.Items.Add("Концентрация = " + concentration[abc] + " %");
            if (radioButton1.Checked)
            {
                if (q2 == q3)
                {
                    listBox1.Items.Add("Умирает, если соседей = " + q2);
                }
                listBox1.Items.Add("Умирает, если соседей >" + Convert.ToInt32(q2) + " и <" + Convert.ToInt32(q3));
            }
            if (radioButton2.Checked)
            {
                listBox1.Items.Add("живет, если соседи:");
                if (f == 1)
                    listBox1.Items.Add("1 сверху и 1 снизу");
                if (f == 2)
                    listBox1.Items.Add("1 сверху и 1 справа");
                if (f == 3)
                    listBox1.Items.Add("2 соседа сверху и 1 слева");
                if (f == 4)
                    listBox1.Items.Add("1 слева сверху и 1 справа снизу");
                if (f == 5)
                    listBox1.Items.Add("1 снизу и 1 слева");
                if (f == 6)
                    listBox1.Items.Add("1 справа сверху и 1 слева снизу");
                if (f == 7)
                    listBox1.Items.Add("1 справа сверху и 1 слева сверху");
                if (f == 8)
                    listBox1.Items.Add("1 справа снизу и 1 слева снизу");
                if (f == 9)
                    listBox1.Items.Add("1 слева и 1 справа сверху");
                if (f == 10)
                    listBox1.Items.Add("1 слева и 1 справа");
            }
            if (radioButton4.Checked)
                listBox1.Items.Add("Рождается, если соседей = " + q1);
            if(radioButton3.Checked)
            {
                listBox1.Items.Add("Рождаются, если соседи расположены:");
                if (r == 1)
                    listBox1.Items.Add("1 сверху и 1 снизу");
                if (r == 2)
                    listBox1.Items.Add("1 сверху и 1 справа");
                if (r == 3)
                    listBox1.Items.Add("2 соседа сверху и 1 слева");
                if (r == 4)
                    listBox1.Items.Add("1 слева сверху и 1 справа снизу");
                if (r == 5)
                    listBox1.Items.Add("1 снизу и 1 слева");
                if (r == 6)
                    listBox1.Items.Add("1 справа сверху и 1 слева снизу");
                if (r == 7)
                    listBox1.Items.Add("1 справа сверху и 1 слева сверху");
                if (r == 8)
                    listBox1.Items.Add("1 справа снизу и 1 слева снизу");
                if (r == 9)
                    listBox1.Items.Add("1 слева и 1 справа сверху");
                if (r == 10)
                    listBox1.Items.Add("1 слева и 1 справа");
            }
        }

        //графики:
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
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
            //вертикальная ось количества/концентрации М:
            x1 = 0;
            y1 = pictureBox2.Location.Y;
            g1.DrawLine(Pens.Black, x0, y0, x1, y1);

            if (abc > 0)//ограничение на случай нажатия на кнопку перед жизнью колонии М
            {
                if (comboBox1.Text == "График количества М на поле")
                {
                    //находим максимальное значение М из всех (для красоты написания графика)
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
                        g1.DrawEllipse(Pens.Black, x1 - 5, y1 - 5, 10, 10);
                    }

                }
                else if (comboBox1.Text == "График концентрации (в%) М на поле")
                {
                    x1 = 0;
                    y1 = pictureBox2.Height;
                    for (int i = 0; i < abc + 1; i++)
                    {
                        g1.DrawLine(Pens.Blue, x1, y1, i * pictureBox2.Width / abc, Convert.ToInt32(pictureBox2.Height - 5 - pictureBox2.Height / (100 / concentration[i])));
                        x1 = i * pictureBox2.Width / abc;
                        y1 = Convert.ToInt32(pictureBox2.Height - 5 - pictureBox2.Height / (100 / concentration[i]));
                        g1.DrawEllipse(Pens.Black, x1 - 5, y1 - 5, 10, 10);
                    }
                }
            }
        }               
        //настройки:
        private void button4_Click(object sender, EventArgs e)
        {//"Настройки"
            panel1.Visible = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {//"Ок"
            panel1.Visible = false;
            pictureBox1.Refresh();
            for (int i = 0; i < w; i++)
                for (int j = 0; j < h; j++)
                    if (a[i, j])
                    {
                        g.FillRectangle(Brushes.Gold, i * z, j * z, z, z);
                        g.DrawRectangle(Pens.Red, i * z, j * z, z, z);
                    }

        }

        private void button6_Click(object sender, EventArgs e)
        {//"Сбросить"
            //при нажатии устанавливаются стандартные настройки
            radioButton1.Checked = true;
            panel7.Enabled = true;
            panel5.Enabled = false;
            label10.Text = "от 2 до 6";
            trackBar4.Value = 6;
            trackBar5.Value = 2;
            label8.Text = "от 2 до 5";
            trackBar2.Value = 2;
            trackBar1.Value = 5;
            checkBox1.Checked = true;
            trackBar3.Value = 10;
            label9.Text = "10 х 10";
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {//"Количество соседей"
            panel7.Enabled = true;
            panel5.Enabled = false;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {//"Расположение соседей"
            panel7.Enabled = false;
            panel5.Enabled = true;
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            if (trackBar2.Value+1 > trackBar1.Value&&trackBar2.Value+1<9)
                trackBar1.Value = trackBar2.Value+1;
            label8.Text = "от " + trackBar2.Value + " до " + trackBar1.Value;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            if (trackBar2.Value+1 > trackBar1.Value&&trackBar1.Value-1>0)
                trackBar2.Value = trackBar1.Value-1;
            label8.Text = "от " + trackBar2.Value + " до " + trackBar1.Value;
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {//"Размер микроорганизма на поле"
            label9.Text = trackBar3.Value + " x " + trackBar3.Value;
            z = trackBar3.Value;
        }

        private void trackBar4_Scroll(object sender, EventArgs e)
        {
            if(trackBar5.Value + 1 > trackBar4.Value && trackBar4.Value - 1 > 0)
                trackBar5.Value = trackBar4.Value - 1;
            label10.Text = "от " + trackBar5.Value + " до " + trackBar4.Value;
        }

        private void trackBar5_Scroll(object sender, EventArgs e)
        {
            if (trackBar5.Value + 1 > trackBar4.Value && trackBar5.Value + 1 < 9)
                trackBar4.Value = trackBar5.Value + 1;
            label10.Text = "от " + trackBar5.Value + " до " + trackBar4.Value;
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            panel3.Enabled = radioButton4.Checked;
            panel9.Enabled = radioButton3.Checked;
        }

        private void button2_Click(object sender, EventArgs e)
        {//по нажатию этой кнопки переход к расположению КМ
            pictureBox1.Visible = true;
            pictureBox2.Visible = false;
            pictureBox1.Refresh();
            for (int i = 0; i < w; i++)
                for (int j = 0; j < h; j++)
                    if (a[i, j])
                    {
                        g.FillRectangle(Brushes.Gold, i * z, j * z, z, z);
                        g.DrawRectangle(Pens.Red, i * z, j * z, z, z);
                    }
        }

        //вспомогательные подпрограммы:
        void body_of_the_cycle(int i,int j)
        {
            //чтобы не вызывать функцию несколько лишних раз - приравниваем ее к переменной k
            int k = num_of_neigh(i, j);
            if (radioButton1.Checked)//если виживание зависит от количества соседей:
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
                    }
                }
                //если же здесь (в этой координате) ничего нет и количество соседей = q1  
                //и если настройки позволяют,то здесь появляется новый М                
                birth(k,i,j,r);
            }
            if (radioButton2.Checked)//если виживание зависит от расположения соседий:
            {
                if (a[i, j])//если здесь М
                {
                    if (location_of_neigh(f, i, j,false) == true)//если условия виживания положительные,
                    {
                        g.FillRectangle(Brushes.Gold, i * z, j * z, z, z);//то М остается
                        g.DrawRectangle(Pens.Red, i * z, j * z, z, z);
                    }
                    else//в инном случае М умирает
                    {
                        a[i, j] = false;
                        g.FillRectangle(Brushes.White, i * z, j * z, z + 1, z + 1);
                    }
                }
                birth(k, i, j,r);
            }
        }

        void Record()
        {
            //по окончанию в txt файл записуем данные о 
            string[] s_n = new string[10000];
            string[] s_conc = new string[10000];
            int j = -1;//счетчик всех итераций в данной игре
            //считываем данные с listBox
            for (int i = 0; i < listBox1.Items.Count - 1; i++)
            {
                s2 = (listBox1.Items[i].ToString()).Split(' ');
                if (s2[0] == "Итерация")
                {
                    j++;
                }
                if (s2[0] == "n")
                {
                    s_n[j] = s2[2];
                }
                if (s2[0] == "Концентрация")
                {
                    s_conc[j] = s2[2];
                }
            }
            //открываем поток
            var sw = new StreamWriter(path, true);
            m++;//счетчик всех записей
            sw.Write(m + " " + (j + 1).ToString() + " " + w + " " + h + " ");
            for (int i = 0; i < j + 1; i++)
            {
                sw.Write(i + " " + s_n[i] + " " + s_conc[i] + " ");
            }
            sw.WriteLine();
            sw.Close();
        }

        bool location_of_neigh(int id, int i, int j,bool p)//p=true ->проверяем на рождение
        {                                                 //p=false->проверяем на виживание
            //в зависимости от f проверяет наличие (или отсутствие) соседей в определенных точках
            bool bl = false;
            int num = 0;
            if (id == 1 && ((checkBox2.Checked && p == false) || (checkBox31.Checked && p)))//если 1 сверху и 1 снизу
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
            
            if (id == 2 && ((checkBox3.Checked && p == false) || (checkBox30.Checked && p)))//если 1 сверху и 1 справа
            {
                try
                {
                    if (a[i, j - 1])//сверху
                        num++;
                }
                catch { }
                try
                {
                    if (a[i+1, j])//справа
                        num++;
                }
                catch { }
                if (num == 2)
                    bl = true;
                else
                    bl = false;
            }
            
            if (id == 3 && ((checkBox4.Checked && p == false) || (checkBox29.Checked && p)))//если 2 соседа сверху (подряд) и 1 слева 
            {
                try
                {
                    if (a[i, j - 1])//сверху на 1
                        num++;
                }
                catch { }
                try
                {
                    if (a[i, j-2])//сверху на 2
                        num++;
                }
                catch { }
                try
                {
                    if (a[i-1, j])//слева
                        num++;
                }
                catch { }
                if (num == 3)
                    bl = true;
                else
                    bl = false;
            }
            
            if (id == 4 && ((checkBox5.Checked && p == false) || (checkBox28.Checked && p)))//если 1 слева сверху и 1 справа снизу
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
            
            if (id == 5 && ((checkBox6.Checked && p == false) || (checkBox27.Checked && p)))//если 1 снизу и 1 слева
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
            
            if (id == 6 && ((checkBox7.Checked && p == false) || (checkBox26.Checked && p)))//если 1 справа сверху и 1 слева снизу
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
            
            if (id == 7 && ((checkBox8.Checked && p == false) || (checkBox25.Checked && p)))//если 1 справа сверху и 1 слева сверху
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
            
            if (id == 8 && ((checkBox9.Checked && p == false) || (checkBox24.Checked && p)))//если 1 справа снизу и 1 слева снизу
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
            
            if (id == 9 && ((checkBox10.Checked && p == false) || (checkBox23.Checked && p)))//если 1 справа сверху и 1 слева 
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
            
            if (id == 10 && ((checkBox11.Checked && p == false) || (checkBox22.Checked && p)))//если 1 справа и 1 слева 
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

        int id_limit(int id, bool p)
        {//переменная 'id' - порядковый номер комбинации
            repeat:
            if (id == 1 && ((checkBox2.Checked == false && p == false) || (checkBox31.Checked == false && p == true)))
                id++;
            if (id == 2 && ((checkBox3.Checked == false && p == false) || (checkBox30.Checked == false && p == true)))
                id++;
            if (id == 3 && ((checkBox4.Checked == false && p == false) || (checkBox29.Checked == false && p == true)))
                id++;
            if (id == 4 && ((checkBox3.Checked == false && p == false) || (checkBox28.Checked == false && p == true)))
                id++;
            if (id == 5 && ((checkBox6.Checked == false && p == false) || (checkBox27.Checked == false && p == true)))
                id++;
            if (id == 6 && ((checkBox7.Checked == false && p == false) || (checkBox26.Checked == false && p == true)))
                id++;
            if (id == 7 && ((checkBox8.Checked == false && p == false) || (checkBox25.Checked == false && p == true)))
                id++;
            if (id == 8 && ((checkBox9.Checked == false && p == false) || (checkBox24.Checked == false && p == true)))
                id++;
            if (id == 9 && ((checkBox10.Checked == false && p == false) || (checkBox23.Checked == false && p == true)))
                id++;
            if (id == 10 && ((checkBox11.Checked == false && p == false) || (checkBox22.Checked == false && p == true)))
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

        void birth(int k,int i,int j,int r)
        {
            if (radioButton4.Checked)//виживание от количества соседей
            {
                if (k == q1)
                {
                    a[i, j] = true;
                    g.FillRectangle(Brushes.Gold, i * z, j * z, z, z);
                    g.DrawRectangle(Pens.Red, i * z, j * z, z, z);
                }
            }
            else if (radioButton3.Checked)//если условие виживания зависит от расположения соседей
            {
                if(location_of_neigh(r, i, j,true))
                {
                    a[i, j] = true;
                    g.FillRectangle(Brushes.Gold, i * z, j * z, z, z);
                    g.DrawRectangle(Pens.Red, i * z, j * z, z, z);
                }
            }
        }
    }
}
