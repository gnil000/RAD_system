using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Rad_system_any2
{
    public partial class appForm : Form
    {
        public appForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            var thread = new Thread(()=> new Form1().ShowDialog());
            thread.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
           ClientForm form2 = new ClientForm();
            //var thread = new Thread(()=> new ClientForm().ShowDialog());
            form2.ShowDialog();
            //thread.Start();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //FuturaForm form3 = new FuturaForm();
            var thread = new Thread(()=> new FuturaForm().ShowDialog());
            //form3.ShowDialog();
            thread.Start();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //FuturaInfoForm form4 = new FuturaInfoForm();
            //form4.ShowDialog();
            var thread = new Thread(()=> new FuturaInfoForm().ShowDialog());
            thread.Start();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //var thread = new Thread(() => new MaterialReportForm().ShowDialog());
            //thread.Start();

            MaterialReportForm form5 = new MaterialReportForm();
            form5.ShowDialog();

        }
    }
}
