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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        P p = new P();
        private void button4_Click(object sender, EventArgs e)
        {//изменение звена
            if (button4.BackgroundImage == WindowsFormsApplication1.Properties.Resources.zveno1)
                button4.BackgroundImage = WindowsFormsApplication1.Properties.Resources.zveno2;
            else
                button4.BackgroundImage = WindowsFormsApplication1.Properties.Resources.zveno1;
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            p.Change(!p.s1, false, false, false, groupBox1, groupBox2, groupBox3, groupBox4, checkBox2);
            p.s1 = !p.s1;
            p.s2 = false;
            p.s3 = false;
            p.s4 = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            p.Change(false,!p.s2, false, false, groupBox1, groupBox2, groupBox3, groupBox4, checkBox2);
            p.s2 = !p.s2;
            p.s1 = false;
            p.s3 = false;
            p.s4 = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            p.Change(false, false, !p.s3, false, groupBox1, groupBox2, groupBox3, groupBox4, checkBox2);
            p.s3 = !p.s3;
            p.s1 = false;
            p.s2 = false;
            p.s4 = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            p.Change(false, false, false, !p.s4, groupBox1, groupBox2, groupBox3, groupBox4, checkBox2);
            p.s4 = !p.s4;
            p.s1 = false;
            p.s2 = false;
            p.s3 = false;
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            p = new P();
        }
        
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox2.Checked)
            {

            }
            else
            {

            }
        }
        
        class P
        {
            public bool s1,s2,s3,s4;
            public P()
            {
                s1 = false;
                s2 = false;
                s3 = false;
                s4 = false;
            }
            public void Change(bool b1, bool b2, bool b3, bool b4,GroupBox g1, GroupBox g2, GroupBox g3, GroupBox g4,CheckBox ch)
            {
                if ((b1==false)&&(b2 == false) &&(b3 == false) &&(b4 == false))
                {
                    g1.Size = new Size(661,39);
                    g2.Size = new Size(661, 39);
                    g3.Size = new Size(661, 39);
                    g4.Size = new Size(640, 39);
                    g1.Location = new Point(12, 12);
                    g2.Location = new Point(12, 57);
                    g3.Location = new Point(12, 102);
                    g4.Location = new Point(33, 147);
                    ch.Location = new Point(12,168);
                }
                else
                {
                    if (b1)
                    {
                        g1.Size = new Size(661, 150);
                        g2.Size = new Size(661, 39);
                        g3.Size = new Size(661, 39);
                        g4.Size = new Size(640, 39);
                        g1.Location = new Point(12, 12);
                        g2.Location = new Point(12, 168);
                        g3.Location = new Point(12, 213);
                        g4.Location = new Point(33, 258);
                        ch.Location = new Point(12,279);
                    }
                    else if (b2)
                    {
                        g1.Size = new Size(661, 39);
                        g2.Size = new Size(661, 282);
                        g3.Size = new Size(661, 39);
                        g4.Size = new Size(640, 39);
                        g1.Location = new Point(12, 12);
                        g2.Location = new Point(12, 57);
                        g3.Location = new Point(12, 345);
                        g4.Location = new Point(33, 390);
                        ch.Location = new Point(12,411);
                    }
                    else if (b3)
                    {
                        g1.Size = new Size(661, 39);
                        g2.Size = new Size(661, 39);
                        g3.Size = new Size(661, 150);
                        g4.Size = new Size(640, 39);
                        g1.Location = new Point(12, 12);
                        g2.Location = new Point(12, 57);
                        g3.Location = new Point(12, 102);
                        g4.Location = new Point(33, 258);
                        ch.Location = new Point(12,279);
                    }
                    else if (b4)
                    {
                        g1.Size = new Size(661, 39);
                        g2.Size = new Size(661, 39);
                        g3.Size = new Size(661, 39);
                        g4.Size = new Size(640, 150);
                        g1.Location = new Point(12, 12);
                        g2.Location = new Point(12, 57);
                        g3.Location = new Point(12, 102);
                        g4.Location = new Point(33, 147);
                        ch.Location = new Point(12,168);
                    }
                }
            }
        }
    }
}
