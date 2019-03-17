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
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
        }
        string s;
        string[] s1;
        private void Settings_Load(object sender, EventArgs e)
        {
            s = this.Text;
            s1 = s.Split(' ');
            radioButton1.Checked = bool.Parse(s1[0]);//a1
            panel7.Enabled= bool.Parse(s1[0]);
            radioButton2.Checked = !bool.Parse(s1[0]);
            panel5.Enabled = !bool.Parse(s1[0]);
            radioButton4.Checked = bool.Parse(s1[1]);//b1
            panel3.Enabled = bool.Parse(s1[1]);
            radioButton3.Checked = !bool.Parse(s1[1]);
            panel9.Enabled = !bool.Parse(s1[1]);
            trackBar5.Value = int.Parse(s1[2]);//vmin
            trackBar4.Value = int.Parse(s1[3]);//vmax
            trackBar2.Value = int.Parse(s1[4]);//bmin
            trackBar1.Value = int.Parse(s1[5]);//bmax
            trackBar3.Value = int.Parse(s1[6]);//size_micr
            checkBox1.Checked = bool.Parse(s1[7]);//diff_cycles
            checkBox2.Checked = bool.Parse(s1[8]);//b[0]
            checkBox3.Checked = bool.Parse(s1[9]);//b[1]
            checkBox4.Checked = bool.Parse(s1[10]);//b[2]
            checkBox5.Checked = bool.Parse(s1[11]);//b[3]
            checkBox6.Checked = bool.Parse(s1[12]);//b[4]
            checkBox7.Checked = bool.Parse(s1[13]);//b[5]
            checkBox8.Checked = bool.Parse(s1[14]);//b[6]
            checkBox9.Checked = bool.Parse(s1[15]);//b[7]
            checkBox10.Checked = bool.Parse(s1[16]);//b[8]
            checkBox11.Checked = bool.Parse(s1[17]);//b[9]
            checkBox31.Checked = bool.Parse(s1[18]);//v[0]
            checkBox30.Checked = bool.Parse(s1[19]);//v[1]
            checkBox29.Checked = bool.Parse(s1[20]);//v[2]
            checkBox28.Checked = bool.Parse(s1[21]);//v[3]
            checkBox27.Checked = bool.Parse(s1[22]);//v[4]
            checkBox26.Checked = bool.Parse(s1[23]);//v[5]
            checkBox25.Checked = bool.Parse(s1[24]);//v[6]
            checkBox24.Checked = bool.Parse(s1[25]);//v[7]
            checkBox23.Checked = bool.Parse(s1[26]);//v[8]
            checkBox22.Checked = bool.Parse(s1[27]);//v[9]
            label10.Text = "від " + trackBar5.Value + " до " + trackBar4.Value;
            label8.Text = "від " + trackBar2.Value + " до " + trackBar1.Value;
            label9.Text = trackBar3.Value + " x " + trackBar3.Value;
            this.Text = "Налаштування";
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            panel7.Enabled = true;
            panel5.Enabled = false;
        }
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            panel7.Enabled = false;
            panel5.Enabled = true;
        }
        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            panel3.Enabled = radioButton4.Checked;
            panel9.Enabled = radioButton3.Checked;
        }
        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            panel9.Enabled = radioButton3.Checked;
            panel3.Enabled = !radioButton3.Checked;
        }
        private void trackBar5_Scroll(object sender, EventArgs e)
        {
            if (trackBar5.Value + 1 > trackBar4.Value && trackBar5.Value + 1 < 9)
                trackBar4.Value = trackBar5.Value + 1;
            label10.Text = "від " + trackBar5.Value + " до " + trackBar4.Value;
        }
        private void trackBar4_Scroll(object sender, EventArgs e)
        {
            if (trackBar5.Value + 1 > trackBar4.Value && trackBar4.Value - 1 > 0)
                trackBar5.Value = trackBar4.Value - 1;
            label10.Text = "від " + trackBar5.Value + " до " + trackBar4.Value;
        }
        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            if (trackBar2.Value + 1 > trackBar1.Value && trackBar2.Value + 1 < 9)
                trackBar1.Value = trackBar2.Value + 1;
            label8.Text = "від " + trackBar2.Value + " до " + trackBar1.Value;
        }
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            if (trackBar2.Value + 1 > trackBar1.Value && trackBar1.Value - 1 > 0)
                trackBar2.Value = trackBar1.Value - 1;
            label8.Text = "від " + trackBar2.Value + " до " + trackBar1.Value;
        }
        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            label9.Text = trackBar3.Value + " x " + trackBar3.Value;
            //z = trackBar3.Value;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            radioButton1.Checked = bool.Parse(s1[0]);//a1
            panel7.Enabled = bool.Parse(s1[0]);
            radioButton2.Checked = !bool.Parse(s1[0]);
            panel5.Enabled = !bool.Parse(s1[0]);
            radioButton4.Checked = bool.Parse(s1[1]);//b1
            panel3.Enabled = bool.Parse(s1[1]);
            radioButton3.Checked = !bool.Parse(s1[1]);
            panel9.Enabled = !bool.Parse(s1[1]);
            trackBar5.Value = int.Parse(s1[2]);//vmin
            trackBar4.Value = int.Parse(s1[3]);//vmax
            trackBar2.Value = int.Parse(s1[4]);//bmin
            trackBar1.Value = int.Parse(s1[5]);//bmax
            trackBar3.Value = int.Parse(s1[6]);//size_micr
            checkBox1.Checked = bool.Parse(s1[7]);//diff_cycles
            checkBox2.Checked = bool.Parse(s1[8]);//b[0]
            checkBox3.Checked = bool.Parse(s1[9]);//b[1]
            checkBox4.Checked = bool.Parse(s1[10]);//b[2]
            checkBox5.Checked = bool.Parse(s1[11]);//b[3]
            checkBox6.Checked = bool.Parse(s1[12]);//b[4]
            checkBox7.Checked = bool.Parse(s1[13]);//b[5]
            checkBox8.Checked = bool.Parse(s1[14]);//b[6]
            checkBox9.Checked = bool.Parse(s1[15]);//b[7]
            checkBox10.Checked = bool.Parse(s1[16]);//b[8]
            checkBox11.Checked = bool.Parse(s1[17]);//b[9]
            checkBox31.Checked = bool.Parse(s1[18]);//v[0]
            checkBox30.Checked = bool.Parse(s1[19]);//v[1]
            checkBox29.Checked = bool.Parse(s1[20]);//v[2]
            checkBox28.Checked = bool.Parse(s1[21]);//v[3]
            checkBox27.Checked = bool.Parse(s1[22]);//v[4]
            checkBox26.Checked = bool.Parse(s1[23]);//v[5]
            checkBox25.Checked = bool.Parse(s1[24]);//v[6]
            checkBox24.Checked = bool.Parse(s1[25]);//v[7]
            checkBox23.Checked = bool.Parse(s1[26]);//v[8]
            checkBox22.Checked = bool.Parse(s1[27]);//v[9]
            label10.Text = "від " + trackBar5.Value + " до " + trackBar4.Value;
            label8.Text = "від " + trackBar2.Value + " до " + trackBar1.Value;
            label9.Text = trackBar3.Value + " x " + trackBar3.Value;
            this.Text = "Налаштування";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public bool a
        {
            get
            {
                return radioButton1.Checked;
            }
        }
        public bool b
        {
            get
            {
                return radioButton4.Checked;
            }
        }
        public int vmin
        {
            get
            {
                return trackBar5.Value;
            }
        }
        public int vmax
        {
            get
            {
                return trackBar4.Value;
            }
        }
        public int bmin
        {
            get
            {
                return trackBar2.Value;
            }
        }
        public int bmax
        {
            get
            {
                return trackBar1.Value;
            }
        }
        public int size_micr
        {
            get
            {
                return trackBar3.Value;
            }
        }
        public bool diff_cycles
        {
            get
            {
                return checkBox1.Checked;
            }
        }
        public bool b0
        {
            get
            {
                return checkBox2.Checked;
            }
        }
        public bool b1
        {
            get
            {
                return checkBox3.Checked;
            }
        }
        public bool b2
        {
            get
            {
                return checkBox4.Checked;
            }
        }
        public bool b3
        {
            get
            {
                return checkBox5.Checked;
            }
        }
        public bool b4
        {
            get
            {
                return checkBox6.Checked;
            }
        }
        public bool b5
        {
            get
            {
                return checkBox7.Checked;
            }
        }
        public bool b6
        {
            get
            {
                return checkBox8.Checked;
            }
        }
        public bool b7
        {
            get
            {
                return checkBox9.Checked;
            }
        }
        public bool b8
        {
            get
            {
                return checkBox10.Checked;
            }
        }
        public bool b9
        {
            get
            {
                return checkBox11.Checked;
            }
        }
        public bool v0
        {
            get
            {
                return checkBox31.Checked;
            }
        }
        public bool v1
        {
            get
            {
                return checkBox30.Checked;
            }
        }
        public bool v2
        {
            get
            {
                return checkBox29.Checked;
            }
        }
        public bool v3
        {
            get
            {
                return checkBox28.Checked;
            }
        }
        public bool v4
        {
            get
            {
                return checkBox27.Checked;
            }
        }
        public bool v5
        {
            get
            {
                return checkBox26.Checked;
            }
        }
        public bool v6
        {
            get
            {
                return checkBox25.Checked;
            }
        }
        public bool v7
        {
            get
            {
                return checkBox24.Checked;
            }
        }
        public bool v8
        {
            get
            {
                return checkBox23.Checked;
            }
        }
        public bool v9
        {
            get
            {
                return checkBox22.Checked;
            }
        }
    }
}
