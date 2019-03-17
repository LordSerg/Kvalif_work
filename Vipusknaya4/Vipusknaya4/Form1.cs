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
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using OpenTK.Platform.Windows;



namespace Vipusknaya4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //в этом проэкте будет опробована часть "плавающяя сетка"*
        //* - это когда у каждой ячейки разное кол-во соседей, которое изменяется в небольшом диапазоне
        //+ временные поля

        Brush col1;
        Pen col2;
        int size;
        Graphics p;
        bool[,] a;
        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //toolTip1.SetToolTip(tabControl2, "Квадратная сеть, где: \r\nу углового 'М' - 3 соседа; \r\nу бокового - 5 соседей; \r\nу центрального - 8 соседей.");//обозначение \r\n - перенос строки
            tabControl1.SelectedIndex = 0;//переходим на основную
            col1 =  Brushes.Gold;
            col2 = Pens.Red;
            timer1.Interval = 1;
            timer1.Enabled = true;
            size = trackBar1.Value;
            p = pictureBox1.CreateGraphics();
            
        }

        //квадрат:
        private void button3_Click(object sender, EventArgs e)
        {//выбор цвета №1
            Form2 f = new Form2();
            f.ShowDialog();
            if(f.data2==true)
            {
                col1 = f.data;
            }            
        }
        private void button2_Click(object sender, EventArgs e)
        {//выбор цвета №2
            Form2 f = new Form2();
            f.ShowDialog();
            if (f.data2 == true)
            {
                col2 = new Pen(f.data);
            }
        }

        //шестиугольник:
        private void button5_Click(object sender, EventArgs e)
        {//выбор цвета №1
            Form2 f = new Form2();
            f.ShowDialog();
            if (f.data2 == true)
            {
                col1 = f.data;
            }
        }
        private void button6_Click(object sender, EventArgs e)
        {//выбор цвета №2
            Form2 f = new Form2();
            f.ShowDialog();
            if (f.data2 == true)
            {
                col2 = new Pen(f.data);
            }
        }

        

        //N-угольник:

        //OK:
        private void button4_Click(object sender, EventArgs e)
        {
            //tabControl1.SelectedIndex = 0;
            
        }

        //регулировка размера ячеек:
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            label9.Text = trackBar1.Value + "x" + trackBar1.Value;
        }

        private void tabControl2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        //class time
        //{//создать новый таймер / создать новые пределы (j_max, j_min, i_max, i_min)            
        //}

        private void button8_Click(object sender, EventArgs e)
        {//start
            int x, y;
            x = Convert.ToInt32(textBox1.Text);
            y = Convert.ToInt32(textBox2.Text);
            a = new bool[x, y];
            GL.ClearColor(Color.Black);
            GL.Clear(ClearBufferMask.ColorBufferBit);
            glControl1.SwapBuffers();
            timer1.Enabled = true;
            timer1.Interval = 1;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {//главный таймер
            if(tabControl1.SelectedIndex==0)
            {
                //строим изображение
                timer2.Enabled = false;









            }
            if(tabControl1.SelectedIndex==2)
            {
                timer2.Interval = 1;
                timer2.Enabled = true;
                //настройки
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            //if(tabControl2.SelectedIndex==0)
            //{//рисуем квадраты:
            //pictureBox1.Refresh();
            for (int i = 0; i < 2; i++)
                for (int j = 0; j < 2; j++)
                {
                    p.FillRectangle(col1, i * size, j * size, size, size);
                    p.DrawRectangle(col2, i * size, j * size, size, size);
                }
            //}
            //if(tabControl2.SelectedIndex==1)
            //{//рисуем 6-угольники:

            //}
            //if(tabControl2.SelectedIndex==2)
            //{//рисуем n-угольники:

            //}
        }
    }
}
