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
        int[] n;
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
            Graphics g1 = this.CreateGraphics();
            g1.DrawLine(Pens.Black, 3, 0, 3, this.Height);
            g1.DrawLine(Pens.Black, 0, this.Height - 45, this.Width, this.Height - 45);
            if (tabControl1.SelectedIndex == 0)
            {
                int max = 0;
                for (int i = 0; i < s1.Length; i++)
                {
                    if (max < n[i])
                        max = n[i];
                }
                max += 5;
                int x1 = 5, y1 = tabPage1.Height - (n[0]) * tabPage1.Height / max;
                for (int i = 0; i < s1.Length - 1; i++)
                {
                    g.DrawLine(Pens.Red, x1, y1, (i * tabPage1.Width) / (s1.Length - 1), tabPage1.Height - (n[i] * tabPage1.Height) / max);
                    x1 = 5 + (i * tabPage1.Width / (s1.Length - 1));
                    y1 = tabPage1.Height - (n[i]) * tabPage1.Height / max;
                    g.DrawEllipse(Pens.Black, x1 - 2, y1 - 2, 4, 4);
                }
                g.DrawLine(Pens.Red, x1, y1, ((s1.Length - 1) * tabPage1.Width) / (s1.Length - 1), tabPage1.Height - (n[(s1.Length - 1)] * tabPage1.Height) / max);
                x1 = -5 + ((s1.Length - 1) * tabPage1.Width / (s1.Length - 1));
                y1 = tabPage1.Height - (n[(s1.Length - 1)]) * tabPage1.Height / max;
                g.DrawEllipse(Pens.Black, x1 - 2, y1 - 2, 4, 4);
            }
        }
    }
}
