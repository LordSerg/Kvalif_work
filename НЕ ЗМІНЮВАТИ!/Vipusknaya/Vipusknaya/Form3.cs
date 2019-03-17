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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            string s;
            dataGridView1.ColumnCount =4;
            dataGridView1.RowCount =5;
            dataGridView1.Columns[0].HeaderCell.Value = "№";
            dataGridView1.Columns[1].HeaderCell.Value = "Зміст";
            dataGridView1.Columns[2].HeaderCell.Value = "Срок виконання";
            dataGridView1.Columns[3].HeaderCell.Value = "Чим звітую";
            s = "Перша версія, де КМ розвивається без умов виживання";
            dataGridView1.Rows[0].Cells[0].Value = "1";
            dataGridView1.Rows[0].Cells[1].Value = s;
            dataGridView1.Rows[0].Cells[2].Value = "до 20.06.18";
            s = "1)На полі розставляється Х початкових мікроорганізмів(М); 2)З кожним тактом народжуються нові М, якщо задовільняє кількість сусідів; 3) з кожним тактом додаються нові поля у матриці суміжності мікроорганізмів.";
            dataGridView1.Rows[0].Cells[3].Value = s;
            dataGridView1.Rows[1].Cells[0].Value = "2";
            dataGridView1.Rows[1].Cells[1].Value = "Умови Виживання";
            dataGridView1.Rows[1].Cells[2].Value = "до 30.06.18";
            s = "Умови виживання будуть задаватись юзером та виводитись на екран у вигляді графіку синусоїди: період зміни від значень.";
            dataGridView1.Rows[1].Cells[3].Value = s;
            dataGridView1.Rows[3].Cells[0].Value = "3";
            dataGridView1.Rows[3].Cells[1].Value = "Умови Вимирання";
            dataGridView1.Rows[3].Cells[2].Value = "до 15.07.18";
            s = "Якщо від М декілька періодів не народжується наступних поколінь, то М помирає (або за інших умов)";
            dataGridView1.Rows[3].Cells[3].Value = s;
            dataGridView1.Rows[2].Cells[0].Value = "4";
            dataGridView1.Rows[2].Cells[1].Value = "Адаптація";
            dataGridView1.Rows[2].Cells[2].Value = "до 01.08.18";
            s = "У критичний момент, коли у КМ приріст населення малий (але не = 0), то кожен М зазнає адаптації.";
            dataGridView1.Rows[2].Cells[3].Value = s;
            //dataGridView1.Rows[4].Cells[0].Value = "5";
            //dataGridView1.Rows[4].Cells[1].Value = "???";
            //dataGridView1.Rows[4].Cells[2].Value = "до 10.08.18";
            //dataGridView1.Rows[4].Cells[3].Value = "";
            dataGridView1.Rows[4].Cells[0].Value = "5";
            dataGridView1.Rows[4].Cells[1].Value = "3D простір";
            dataGridView1.Rows[4].Cells[2].Value = "до 30.08.18";
            s = "Переведення всього у 3D простір";
            dataGridView1.Rows[4].Cells[3].Value = "Переведення всього у 3D простір";
            for(int i=0;i< 4;i++)
            {
                dataGridView1.Columns[i].Width =dataGridView1.Width/4;
            }
            for (int j = 0; j < 5; j++)
            {
                dataGridView1.Rows[j].Height =dataGridView1.Height/5;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.Show();
            this.Hide();
        }
    }
}
