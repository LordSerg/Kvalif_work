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
            //else if(tabControl1.SelectedIndex==1)
            //{
            //    chart2.Series[0].Points.Clear();
            //    chart2.ChartAreas[0].AxisX.Minimum = 0;
            //    chart2.ChartAreas[0].AxisY.Maximum = 100;
            //    for (int i = 0; i < n1.Length - 1; i++)
            //    {
            //        chart2.Series[0].Points.AddXY(i, n1[i]);
            //    }
            //}
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
        }
    }
}
