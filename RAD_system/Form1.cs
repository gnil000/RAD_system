using Microsoft.Office.Interop.Excel;
namespace RAD_system
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string result = "";

            result += "ФИО: " + textBox1.Text+"\n\n"+"Адрес: "+ textBox2.Text+"\n\n"+"Телефон: "+textBox3.Text+"\n\n";

            if (radioButton1.Checked)
                result += "Пол: мужской\n\n";
            else if (radioButton2.Checked)
                result += "Пол: женский\n\n";

            result += "Спорт:\n";
            foreach(var i in checkedListBox1.CheckedItems)
                result += "\t"+i.ToString()+"\n";

            richTextBox1.Text = result;

            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.ShowDialog();
                string filename = ofd.FileName;
                Microsoft.Office.Interop.Excel.Application excelObject = new Microsoft.Office.Interop.Excel.Application();
                excelObject.Visible = false;
                Workbook wb = excelObject.Workbooks.Open(filename, 0, false, 5, "", "", false, XlPlatform.xlWindows, "", true, false, 0, true, false, false);
                Worksheet wsh = (Worksheet)wb.Sheets[1];
                wsh.Cells[1, 1] = label1.Text;
                wsh.Cells[1, 2] = textBox1.Text;
                wsh.Cells[2, 1] = label2.Text;
                wsh.Cells[2, 2] = textBox2.Text;
                wsh.Cells[3, 1] = label3.Text;
                wsh.Cells[3, 2] = textBox3.Text;
                wsh.Cells[4,1] = "Пол";
                if (radioButton1.Checked)
                    wsh.Cells[4,2] =  "Мужской";
                else if (radioButton2.Checked)
                    wsh.Cells[4,2] = "Женский";
                wsh.Cells[5, 1] = "Спорт";
                int i2 = 6;
                foreach (var i in checkedListBox1.CheckedItems) 
                    wsh.Cells[i2++, 2]= i.ToString();
                wb.Save();
                wb.Close();
            }


        }
    }
}