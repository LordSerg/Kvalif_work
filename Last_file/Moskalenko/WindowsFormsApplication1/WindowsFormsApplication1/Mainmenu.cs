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
    public partial class Mainmenu : Form
    {
        public Mainmenu()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 f = new Form2();
            f.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form3 f = new Form3();
            f.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form4 f = new Form4();
            f.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {

        }

        private void Mainmenu_Load(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(button1, "Колонія мікроорганізмів за звичайних змін умов");
            toolTip1.SetToolTip(button2, "Колонія мікроорганізмів, що розділена на дві частини,\nу яких час проходження одного кроку різні");
            toolTip1.SetToolTip(button3, "Колонія мікроорганізмів, у якої час проходження\nодного кроку та час зміни умов - різні");
            toolTip1.SetToolTip(button5, "Закрити програму");
        }
    }
}
