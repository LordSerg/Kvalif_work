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
        int a, b;//початкові розміри поля
        int n;//початкова кількість М
        int t;//лічильник кількості М
        int min=0, max=3;//М може розмножуватися, якщоу нього сусідей не менше від 'min' і не більше від 'max'
        int[] x;//масив, до якого записуються координати 'x' мікроорганізму на полі
        int[] y;//масив, до якого записуються координати 'y' мікроорганізму на полі
        Random r;
        bool l=false;
        private void button1_Click(object sender, EventArgs e)
        {
            //if (l == false)
            //{
                l = true;
                if (button1.Text == "Почати")
                {
                    button1.Text = "Припинити";
                    timer1.Enabled = true;
                    timer1.Interval = 3000;
                    t = -1;
                    n = Convert.ToInt32(textBox1.Text);

                    a = Convert.ToInt32(textBox2.Text);
                    b = Convert.ToInt32(textBox3.Text);
                    dataGridView1.ColumnCount = a;
                    dataGridView1.RowCount = b;

                    for (int i = 0; i < a; i++)
                    {
                        for (int j = 0; j < b; j++)
                        {
                            dataGridView1.Columns[i].Width = trackBar1.Value * 10;
                            dataGridView1.Rows[j].Height = trackBar1.Value * 10;
                            dataGridView1.Rows[j].Cells[i].Style.BackColor = Color.Red;
                            dataGridView1.Rows[j].Cells[i].Value = 0;
                        }
                    }

                    x = new int[a * b];
                    y = new int[b * a];
                    r = new Random();
                    for (int i = 0; i < n; i++)
                    {
                        t++;
                        x[t] = r.Next(a);
                        y[t] = r.Next(b);
                        for (int i1 = 0; i1 < a; i1++)
                        {
                            if (i1 == x[t])
                                for (int j = 0; j < b; j++)
                                {
                                    if (j == y[t])
                                    {
                                        dataGridView1.Rows[j].Cells[i1].Value = t + 1;
                                        dataGridView1.Rows[j].Cells[i1].Style.BackColor = Color.Yellow;
                                    }
                                }
                        }
                    }
                    label4.Text = "x[" + t + "] = " + x[t] + " ;y[" + t + "] = " + y[t];

                    //

                }
                else
                {
                    button1.Text = "Почати";
                    timer1.Enabled = false;
                }
            //}
            //else
            //{
            //    MessageBox.Show("Ви хочете ");
            //}
}

        private void timer1_Tick(object sender, EventArgs e)
        {
            //1 рахуємо, скільки М на  полі
            int k=0;//k - кількість М
            for(int i=0;i<a;i++)
                for(int j=0;j<b;j++)
                {
                    if (Convert.ToInt32(dataGridView1.Rows[j].Cells[i].Value) > 0)
                        k++;
                }
            label5.Text = k.ToString();
            for (int i = 0; i < k; i++)
            {

                if (number_of_neighbors(x[i], y[i]) <= Convert.ToInt32(textBox4.Text))
                {//якщо кількість сусідів < 3, то до цього М1 додаемо сусіда М2 
                    t++;
                    next_generation(x[i], y[i],i);//до М, що знаходиться у координатах [x[i];y[i]] додається сусід
                    dataGridView1.Rows[y[t]].Cells[x[t]].Value = t + 1;
                    dataGridView1.Rows[y[t]].Cells[x[t]].Style.BackColor = Color.Yellow;
                }

            }
            for (int i = 0; i < a; i++)
                for (int j = 0; j < b; j++)
                {
                    dataGridView1.Columns[i].Width = trackBar1.Value * 10;
                    dataGridView1.Rows[j].Height = trackBar1.Value * 10;
                }
            label4.Text = "x[" + t + "] = " + x[t] + " ;y[" + t + "] = " + y[t];
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //timer1.Enabled = !timer1.Enabled;
            Form2 f2 = new Form2();
            f2.Show();
            this.Hide();
        }

        int number_of_neighbors(int w, int h)
        {
            int answer = 0;
            if ((h + 1) < (a - 1))
                if (Convert.ToInt32(dataGridView1.Rows[h + 1].Cells[w].Value) > 0)
                    answer++;
            if (h > 0)
                if (Convert.ToInt32(dataGridView1.Rows[h - 1].Cells[w].Value) > 0)
                    answer++;
            if ((w + 1) < (b - 1))
                if (Convert.ToInt32(dataGridView1.Rows[h].Cells[w + 1].Value) > 0)
                    answer++;
            if (w > 0)
                if (Convert.ToInt32(dataGridView1.Rows[h].Cells[w - 1].Value) > 0)
                    answer++;
            if ((h + 1) < (a - 1) && (w + 1) < (b - 1))
                if (Convert.ToInt32(dataGridView1.Rows[h + 1].Cells[w + 1].Value) > 0)
                    answer++;
            if ((h + 1) < (a - 1) && w > 0)
                if (Convert.ToInt32(dataGridView1.Rows[h + 1].Cells[w - 1].Value) > 0)
                    answer++;
            if (h > 0 && (w + 1) < (b - 1))
                if (Convert.ToInt32(dataGridView1.Rows[h - 1].Cells[w + 1].Value) > 0)
                    answer++;
            if (h > 0 && w > 0)
                if (Convert.ToInt32(dataGridView1.Rows[h - 1].Cells[w - 1].Value) > 0)
                    answer++;
            return answer;
        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }

        void next_generation(int w, int h,int i)
        { 
            //w=x[i] i h=y[i]
            //bool z = false;
            restart:
            r = new Random();
            try
            {
                
                int q = r.Next(1, 9);
                if (q == 1)///up
                {
                    y[t] = y[i] - 1;
                    x[t] = x[i];
                }
                if (q == 2)//down
                {
                    y[t] = y[i] + 1;
                    x[t] = x[i];
                }
                if (q == 3)//right
                {
                    y[t] = y[i];
                    x[t] = x[i] + 1;
                }
                if (q == 4)//left
                {
                    y[t] = y[i];
                    x[t] = x[i] - 1;
                }
                if (q == 5)//up+right
                {
                    y[t] = y[i] - 1;
                    x[t] = x[i] + 1;
                }
                if (q == 6)//up+left
                {
                    y[t] = y[i] - 1;
                    x[t] = x[i] - 1;
                }
                if (q == 7)//down+right
                {
                    y[t] = y[i] + 1;
                    x[t] = x[i] + 1;
                }
                if (q == 8)//down+left
                {
                    y[t] = y[i] + 1;
                    x[t] = x[i] - 1;
                }
                if (Convert.ToInt32(dataGridView1.Rows[y[t]].Cells[x[t]].Value) > 0)
                    goto restart;
            }
            catch (Exception)
            {
                goto restart;
            }
            
            
            /*
            while (z == false)
            {
                y[t] = h;
                x[t] = w;
                y[t] = (y[t - 1] + r.Next(-1, 2));
                x[t] = (x[t - 1] + r.Next(-1, 2));
                listBox1.Items.Add("x[" + t + "] = " + x[t] + " ;y[" + t + "] = " + y[t]);
                if (y[t] != y[t - 1] && x[t] != x[t - 1] && y[t] > -1 && x[t] > -1 && y[t] < b && x[t] < a)
                {
                    if(free_place()==true)
                    z = true;
                }

            }*/
            listBox1.Items.Add("x[" + t + "] = " + x[t] + " ;y[" + t + "] = " + y[t]);
        }
        bool free_place()
        {
            bool z = true;
            for(int i=0;i<t-1;i++)
            {
                if(x[i]==x[t]&&y[i]==y[t])
                {
                    z = false;
                    break;
                }
            }
            return z;
        }
    }
}
