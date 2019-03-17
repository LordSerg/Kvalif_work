using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using OpenTK.Platform.Windows;


namespace _3_d_KM
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        bool[,,] a,a1;
        int s,b;
        bool is_life=false;
        int h, w, l, n;
        int x, y, z, x0, y0, z0;
        int abc = 0;
        
        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            x = trackBar3.Value;
            if(x>x0)
            {
                GL.Begin(PrimitiveType.Patches);
                draw_M(a);
                GL.Rotate(1,1,0,0);
                GL.End();
                glControl1.SwapBuffers();
            }
            else
            {
                GL.Begin(PrimitiveType.Patches);
                draw_M(a);
                GL.Rotate(-1, 1, 0, 0);
                GL.End();
                glControl1.SwapBuffers();
            }
            x0 = x;
        }

        private void trackBar4_Scroll(object sender, EventArgs e)
        {
            y = trackBar4.Value;
            if (y > y0)
            {
                GL.Begin(PrimitiveType.Patches);
                draw_M(a);
                GL.Rotate(1, 0, 1, 0);
                GL.End();
                glControl1.SwapBuffers();
            }
            else
            {
                GL.Begin(PrimitiveType.Patches);
                draw_M(a);
                GL.Rotate(-1, 0, 1, 0);
                GL.End();
                glControl1.SwapBuffers();
            }
            y0 = y;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            label4.Text = "Survives if N = " + trackBar1.Value;
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            label5.Text = "Borns if N = " + trackBar2.Value;
        }

        private void trackBar5_Scroll(object sender, EventArgs e)
        {
            z = trackBar5.Value;
            if (z > z0)
            {
                GL.Begin(PrimitiveType.Patches);
                draw_M(a);
                GL.Rotate(1, 0, 0, 1);
                GL.End();
                glControl1.SwapBuffers();
            }
            else
            {
                GL.Begin(PrimitiveType.Patches);
                draw_M(a);
                GL.Rotate(-1, 0, 0, 1);
                GL.End();
                glControl1.SwapBuffers();
            }
            z0 = z;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            abc++;
            timer1.Interval = int.Parse(textBox4.Text);
            do_life();
            n = 0;
            for(int i=0;i<h;i++)
                for(int j=0;j<w;j++)
                    for(int u=0;u<l;u++)
                    {
                        if (a[i, j, u])
                            n++;
                    }
            label11.Text = "n" + abc + " = " + n;
            if (n == 0)
            {
                timer1.Enabled = false;
                is_life = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (is_life == false)
            {
                is_life = true;
                abc = 0;
                enabled(is_life);
                h = int.Parse(textBox1.Text);//высота
                w = int.Parse(textBox2.Text);//ширина
                l = int.Parse(textBox3.Text);//длинна
                n = int.Parse(textBox5.Text);//начальное кол-во М
                a = new bool[h, w, l];
                a1 = new bool[h, w, l];
                Random r = new Random();
                int n1, n2, n3;
                for (int i = 0; i < n; i++)//заполняем поле:
                {
                    n1 = r.Next(h);
                    n2 = r.Next(w);
                    n3 = r.Next(l);
                    if (a[n1, n2, n3] == false)
                    {
                        a[n1, n2, n3] = true;
                    }
                    else
                        i--;
                }
                label11.Text = "n" + abc + " = " + n;
                //a1 = a;
                for (int i = 0; i < h; i++)
                    for (int j = 0; j < w; j++)
                        for (int u = 0; u < l; u++)
                        {
                            a1[i, j, u] = a[i, j, u];
                        }
                //выводим поле на форму
                draw_M(a);
                glControl1.SwapBuffers();
                timer1.Interval = int.Parse(textBox4.Text);
                timer1.Enabled = true;
            }
            else if(is_life==true)
            {
                timer1.Enabled = !timer1.Enabled;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            for (int i = 0; i < h; i++)
                for (int j = 0; j < w; j++)
                    for (int u = 0; u < l; u++)
                    {
                        a[i, j, u] = false;
                        a1[i, j, u] = false;
                    }
            enabled(false);
            is_life = false;
            glControl1.SwapBuffers();
        }

        void draw_M(bool [,,]array)
        {//перерисовка М
            //glControl1.SwapBuffers();
            GL.ClearColor(Color.Black);
            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.Begin(PrimitiveType.Points);
            for (int i = 0; i < h; i++)
                for (int j = 0; j < w; j++)
                    for (int u = 0; u < l; u++)
                    {
                        if (array[i, j, u]==true)
                        {
                            GL.Color3(Color.Red);
                            GL.Vertex3((i - ((h - 1) / 2)) * 0.035, (j - ((w - 1) / 2)) * 0.035, (u - ((l - 1) / 2)) * 0.05);
                        }
                        else
                        {
                            //GL.Color3(Color.White);
                            //GL.Vertex3((i - ((h - 1) / 2)) * 0.01, (j - ((w - 1) / 2)) * 0.01, (u - ((l - 1) / 2)) * 0.01);
                        }
                    }
            GL.End();
        }

        /*void draw_M_2()
        {//перерисовка М линиями
            //glControl1.SwapBuffers();
            GL.ClearColor(Color.White);
            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.Begin(PrimitiveType.Lines);
            for (int i = 0; i < h; i++)
            {
                for (int j = 0; j < w; j++)
                {
                    for (int u = 0; u < l; u++)
                    {
                        if (a[i, j, u])
                        {
                            for (int i1 = 0; i1 < 2; i1++)
                                for (int j1 = 0; j1 < 2; j1++)
                                    for (int u1 = 0; u1 < 2; u1++)
                                    {
                                        if (!(i1 == 0 && j1 == 0 && u1 == 0))
                                        {
                                            try
                                            {
                                                if (a[i + i1, j + j1, u + u1])
                                                {
                                                    GL.Color3(Color.Red);
                                                    GL.Vertex3((i - ((h - 1) / 2)) * 0.1, (j - ((w - 1) / 2)) * 0.1, (u - ((l - 1) / 2)) * 0.1);
                                                    GL.Vertex3(((i + i1) - ((h - 1) / 2)) * 0.1, ((j + j1) - ((w - 1) / 2)) * 0.1, ((u + u1) - ((l - 1) / 2)) * 0.1);
                                                }
                                                else
                                                {

                                                }
                                            }
                                            catch { }
                                        }
                                    }
                        }
                        else
                        {
                            for (int i1 = 0; i1 < 2; i1++)
                                for (int j1 = 0; j1 < 2; j1++)
                                    for (int u1 = 0; u1 < 2; u1++)
                                    {
                                        if (!(i1 == 0 && j1 == 0 && u1 == 0))
                                        {
                                            try
                                            {
                                                if (!a[i + i1, j + j1, u + u1])
                                                {
                                                    GL.Color3(Color.Black);
                                                    GL.Vertex3((i - ((h - 1) / 2)) * 0.1, (j - ((w - 1) / 2)) * 0.1, (u - ((l - 1) / 2)) * 0.1);
                                                    GL.Vertex3(((i + i1) - ((h - 1) / 2)) * 0.1, ((j + j1) - ((w - 1) / 2)) * 0.1, ((u + u1) - ((l - 1) / 2)) * 0.1);
                                                }
                                                else
                                                {

                                                }
                                            }
                                            catch { }
                                        }
                                    }
                        }
                    }
                }
            }
            GL.End();
        }*/

        void enabled(bool zzz)
        {
            panel1.Enabled = !zzz;
            panel2.Enabled = zzz;
            panel3.Enabled = zzz;
            button2.Enabled = zzz;
            label9.Enabled = zzz;
            textBox4.Enabled = zzz;
        }

        void do_life()
        {//основная подпрограмма
            int num;
            for (int i = 0; i < h; i++)
                for (int j = 0; j < w; j++)
                    for (int u = 0; u < l; u++)
                    {
                        num = num_of_neigh(i, j, u, a);
                        if (a[i, j, u]==true)
                        {
                            a1[i, j, u] = _s(num);
                        }
                        else
                        {
                            a1[i, j, u] = _b(num);
                        }
                    }
            //a = a1;
            for (int i = 0; i < h; i++)
                for (int j = 0; j < w; j++)
                    for (int u = 0; u < l; u++)
                    {
                        a[i, j, u] = a1[i, j, u];
                    }
            draw_M(a1);
            glControl1.SwapBuffers();
        }

        bool _b(int nn)
        {//проверка на рождение
            if (nn ==trackBar2.Value)
                return true;
            else
                return false;
        }

        bool _s(int nn)
        {//проверка на выживание
            if ((nn >= trackBar1.Value - 2) &&( nn <= trackBar1.Value + 2))
                return true;
            else
                return false;
        }

        int num_of_neigh(int i,int j,int u,bool[,,]array)
        {
            n=0;
            for (int i1 = -1; i1 < 2; i1++)
                for (int j1 = -1; j1 < 2; j1++)
                    for (int u1 = -1; u1 < 2; u1++)
                    {
                        if (i1 != 0 && j1 != 0 && u1 != 0)
                        {
                            try
                            {
                                if (array[i + i1, j + j1, u + u1] == true)
                                    n++;
                            }
                            catch { }
                        }
                    }
            return n;
        }

    }
}
