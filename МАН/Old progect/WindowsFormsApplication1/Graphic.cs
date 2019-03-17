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
    public partial class Graphic : Form
    {
        public Graphic()
        {
            InitializeComponent();
        }

        Graphics g;
        int[] n,n1,n2;
        int cond_b, cond_v;//condition - условие
        string[] s1;

        private void Graphic_Load(object sender, EventArgs e)
        {
            string path = System.IO.Path.GetFullPath(@"Text.txt");
            int count = System.IO.File.ReadAllLines(path).Length;
            string[] s = System.IO.File.ReadAllLines(path);
            StreamReader file = new StreamReader(System.IO.Path.GetFullPath(path));
            s1 = s[0].Split(' ');
            n = new int[s1.Length];
            for (int i = 0; i < s1.Length; i++)
            {
                n[i] = Convert.ToInt32(s1[i]);
            }
            //s1 = s[1].Split(' ');
            //n1 = new int[s1.Length];
            //for (int i = 0; i < s1.Length; i++)
            //{
            //    n1[i] = Convert.ToInt32(s1[i]);
            //}
            s1 = s[2].Split(' ');
            n2 = new int[s1.Length];
            for (int i = 0; i < s1.Length; i++)
            {
                n2[i] = Convert.ToInt32(s1[i]);
            }
            file.Close();
            g = tabPage1.CreateGraphics();
            draw_g();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            draw_g();
        }

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            draw_g();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            draw_g();
        }

        private void Graphic_SizeChanged(object sender, EventArgs e)
        {
            this.Refresh();
            g = tabPage1.CreateGraphics();
            draw_g();
        }

        void draw_g()
        {
            if (tabControl1.SelectedIndex == 0)
            {
                chart1.Series[0].Points.Clear();
                chart1.ChartAreas[0].AxisX.Minimum = 0;
                for (int i = 0; i < n.Length - 1; i++)
                {
                    chart1.Series[0].Points.AddXY(i, n[i]);
                }
            }
            else if(tabControl1.SelectedIndex==1)
            {
                chart3.Series[0].Points.Clear();
                chart3.ChartAreas[0].AxisX.Minimum = 0;
                chart3.ChartAreas[0].AxisY.Maximum = 100;
                for (int i = 0; i < n2.Length - 1; i++)
                {
                    chart3.Series[0].Points.AddXY(i, n2[i]);
                }
            }
            else if (tabControl1.SelectedIndex == 2)
            {//построение дисперсии к первому графику
                chart2.Series[0].Points.Clear();
                chart2.ChartAreas[0].AxisX.Minimum = 0;
                int[] n3;
                n3 = new int[n.Length];
                n3 = dispers(n.Length,n);
                for (int i = 0; i < n2.Length - 1; i++)
                {
                    chart2.Series[0].Points.AddXY(i, n3[i]);
                }
            }
            else if (tabControl1.SelectedIndex == 3)
            {
                chart4.Series[0].Points.Clear();
                chart4.ChartAreas[0].AxisX.Minimum = 0;
                int[] n3,dd;
                n3 = new int[n.Length];
                dd = new int[n3.Length];
                dd = dispers(n3.Length, dispers(n.Length, n));
                for (int i = 0; i < dd.Length - 1; i++)
                {
                    chart4.Series[0].Points.AddXY(i, dd[i]);
                }
            }
            else if(tabControl1.SelectedIndex==4)
            {//отношение корня из дисперсии к среднему арифм.
                chart5.Series[0].Points.Clear();
                chart5.ChartAreas[0].AxisX.Minimum = 0;
                int[] n3 = new int[n.Length];
                int mid = 0;
                for (int i = 0; i < n.Length; i++)
                    mid += n[i];
                mid /= n.Length;
                for (int i = 0; i < n.Length; i++)
                    n3[i] = Convert.ToInt32(Math.Sqrt(dispers(n.Length,n)[i]) / mid);
                for (int i = 0; i < n.Length - 1; i++)
                {
                    chart5.Series[0].Points.AddXY(i, n3[i]);
                }
            }
        }

        int[] dispers(int length, int[] a)
        {
            int[] d;
            int mid = 0;
            for (int j = 0; j < length; j++)
                mid += a[j];
            mid /= length;
            d = new int[length];
            for(int i=0;i<length;i++)
            {
                int k = 0;
                for (int j = 0; j < i; j++)
                    k += (a[j] - mid) * (a[j] - mid);
                d[i] = k / (i + 1);
            }
            return d;
        }
    }
}
