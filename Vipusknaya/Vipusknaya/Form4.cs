using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vipusknaya
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }
        int a, b;
        int n;
        int t;//лічильник кількості М
        int min = 0, max;//М може розмножуватися, якщоу нього сусідей не менше від 'min' і не більше від 'max'
        int[] x;//масив, до якого записуються координати 'x' мікроорганізму на полі
        int[] y;//масив, до якого записуються координати 'y' мікроорганізму на полі
        int[,] z;
        Random r;
        bool l = false;
        private void button1_Click(object sender, EventArgs e)
        {
            l = true;
            if (button1.Text == "Почати")
            {
                button1.Text = "Припинити";
                t = -1;
                n = Convert.ToInt32(textBox1.Text);//початкова кількість Мікроорганізмів
                a = Convert.ToInt32(textBox2.Text);//початкові розміри поля
                b = Convert.ToInt32(textBox3.Text);
                max = Convert.ToInt32(textBox4.Text);
                dataGridView1.ColumnCount = a;
                dataGridView1.RowCount = b;
                for (int i = 0; i < a; i++)//заповнюємо dataGridView пустими клітинками
                {
                    for (int j = 0; j < b; j++)
                    {
                        dataGridView1.Columns[i].Width = trackBar1.Value * 10;
                        dataGridView1.Rows[j].Height = trackBar1.Value * 10;
                        dataGridView1.Rows[j].Cells[i].Style.BackColor = Color.Red;
                        dataGridView1.Rows[j].Cells[i].Value = 0;//нуль означає, що на цих координатах нічого немає
                                                                 //а будь-яке інше число - означає присутність та порядковий номер М
                    }
                }
                x = new int[a * b];//на полі можуть бути "a*b" Мікроорганізмів
                y = new int[b * a];//тому масиви x та y мають саме такі розміри
                r = new Random();
                for (int i = 0; i < n; i++)//розставляємо мікроорганізми у довільному порядку
                {
                    t++;//лічильник мікроорганізмів
                    x[t] = r.Next(a);
                    y[t] = r.Next(b);
                    for (int i1 = 0; i1 < a; i1++)
                    {
                        if (i1 == x[t])
                            for (int j = 0; j < b; j++)
                            {
                                if (j == y[t])//на полі жовтим позначено мікроорганізм, а червоним - його відсутність
                                {
                                    dataGridView1.Rows[j].Cells[i1].Value = t + 1;
                                    dataGridView1.Rows[j].Cells[i1].Style.BackColor = Color.Yellow;
                                }
                            }
                    }
                }
                listBox1.Items.Add("x[" + (t + 1).ToString() + "] = " + x[t] + " ;y[" + (t + 1).ToString() + "] = " + y[t]);
                timer1.Enabled = true;//запускаємо таймер на автоматичне розповсюдження мікроорганізмів
                timer1.Interval = 3000;
            }
            else
            {
                button1.Text = "Почати";
                timer1.Enabled = false;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //1 рахуємо, скільки М на полі
            int k = 0;//k - лічильник
            for (int i = 0; i < a; i++)
                for (int j = 0; j < b; j++)
                {
                    if (Convert.ToInt32(dataGridView1.Rows[j].Cells[i].Value) > 0)
                        k++;
                }
            //або
            /*
            for (int i = 0; i < x.Length; i++)
                if (x[i] > 0)
                {
                    k++;
                }
             */
                 
             
            for (int i = 0; i < k; i++)
            {

                if (number_of_neighbors(x[i], y[i]) <= Convert.ToInt32(textBox4.Text))
                {//якщо кількість сусідів < заданої кількості, то до цього М1 додаемо сусіда М2 
                    next_generation(x[i], y[i]);//до М, що знаходиться у координатах [x[i];y[i]] додається сусід
                }

            }
            for (int i = 0; i < a; i++)
                for (int j = 0; j < b; j++)
                {
                    dataGridView1.Columns[i].Width = trackBar1.Value * 10;
                    dataGridView1.Rows[j].Height = trackBar1.Value * 10;
                }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //timer1.Enabled = !timer1.Enabled;
            Form1 f = new Form1();
            f.Show();
            this.Hide();
        }

        int number_of_neighbors(int w, int h)
        {
            int answer = 0;
            for (int i = h - 1; i <= h + 1; i++)
            {
                if (i >= 0 && i < b)
                    for (int j = w - 1; j <= w + 1; j++)
                        if (j >= 0 && j < a)
                            if (Convert.ToInt32(dataGridView1.Rows[i].Cells[j].Value) > 0)
                                answer++;
            }
            return answer;
        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }

        void next_generation(int x1, int y1)
        {
            r = new Random();//за допомогою маркера "restart" можна відслідити у яких точках
        restart:
            int q1 = r.Next(x1 - 1, x1 + 2);
            int q2 = r.Next(y1 - 1, y1 + 2);
            if (q1 >= 0 && q1 < a && q2 >= 0 && q2 < b && q1 != y1 && q2 != x1)//якщо коорд. хі коорд. у не виходить за рамки поля
            {//і якщо це не та сама координата з материнською М;але чомусь останнє не працює
                //тоді додаємо новий мікроорганізм до dataGridView 
                t++;
                x[t] = q1;
                y[t] = q2;
                dataGridView1.Rows[y[t]].Cells[x[t]].Value = t + 1;
                dataGridView1.Rows[y[t]].Cells[x[t]].Style.BackColor = Color.Yellow;
            }
            else
                goto restart;
            listBox1.Items.Add("x[" + (t + 1).ToString() + "] = " + x[t] + " ;y[" + (t + 1).ToString() + "] = " + y[t]);//записуємо координати нового М до listBox
        }
    }
}
