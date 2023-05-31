using Microsoft.Office.Interop.Excel;
using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Rad_system_any2
{
    public partial class MaterialReportForm : Form
    {

        DataSet ds, ds2;
        System.Data.DataTable dt, dt2, dt5;
        NpgsqlConnection npgsqlConnection;


        public MaterialReportForm()
        {
            InitializeComponent();

            npgsqlConnection = new NpgsqlConnection("Server=localhost;Port=5432;User Id=postgres;Password=admin;Database=Rad_system_Lera");

            npgsqlConnection.Open();
            ConnectToDataBase();
            dataGridView1.Columns[0].HeaderText = "Код клиента";
            dataGridView1.Columns[1].HeaderText = "ФИО";
            dataGridView1.Columns[2].HeaderText = "Адресс";
            dataGridView1.Columns[3].HeaderText = "Телефон";


            ds2 = new DataSet();
            dt2 = new System.Data.DataTable();

            dt2 = dt.Clone();
            dt2.Clear();
            dataGridView2.DataSource = dt2;
            dataGridView2.Columns[0].HeaderText = "Код клиента";
            dataGridView2.Columns[1].HeaderText = "ФИО";
            dataGridView2.Columns[2].HeaderText = "Адресс";
            dataGridView2.Columns[3].HeaderText = "Телефон";


        }

        void ConnectToDataBase()
        {
            ds = new DataSet();
            dt = new System.Data.DataTable();
            string sql = "select * from Client";

            NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(sql, npgsqlConnection);

            ds.Reset();
            dataAdapter.Fill(ds);
            dt = ds.Tables[0];
            dataGridView1.DataSource = dt;
        }


        private void MaterialReportForm_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            object[] bb = new object[dataGridView1.CurrentRow.Cells.Count];
            for (int i = 0; i < bb.Length; i++)
            {
                bb[i] = dataGridView1.CurrentRow.Cells[i].Value;
            }

            dt2.Rows.Add(bb);
            dataGridView2.DataSource = dt2;

        }


        private void Cell_Click(object sender, EventArgs e)
        {
            object[] bb = new object[dataGridView1.CurrentRow.Cells.Count];
            for (int i = 0; i < bb.Length; i++)
            {
                bb[i] = dataGridView1.CurrentRow.Cells[i].Value;
            }

            dt2.Rows.Add(bb);
            dataGridView2.DataSource = dt2;

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void CellDel_Click(object sender, EventArgs e)
        {
            dataGridView2.Rows.Remove(dataGridView2.CurrentRow);
        }


        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e) //сформировать 
        {
            var date1 = dateTimePicker1.Value;
            var date2 = dateTimePicker2.Value;

            var index = dataGridView2.Rows.Count;

            int[] keyV = new int[index-1];

            string sqlAdd;
            for (int i = 0; i < index; i++)
            {
                if (Convert.ToInt32(dataGridView2.Rows[i].Cells[0].Value) != 0)
                {
                    keyV[i] = Convert.ToInt32(dataGridView2.Rows[i].Cells[0].Value);
                }
            }

            sqlAdd = String.Join(", ", keyV);

            string sql = $"select product.name_product, sum(quantitiy_otgruzka), sum(price_otgruzka), sum(quantity-quantitiy_otgruzka) as qua_qua_otgr, sum(price-price_otgruzka) as price_price_otgr, sum(price) from futurainfo inner join product on futurainfo.id_product = product.id_product where (futurainfo.id_futura in (select futura.id_futura from futura where futura.id_client in ({sqlAdd}) and futura.predoplata = true and futura.data_fut between '{date1}' and '{date2}')) group by product.name_product";

            NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(sql, npgsqlConnection);


            DataSet ds3 = new DataSet();
            var dt3 = new System.Data.DataTable();

            ds3.Reset();
            dataAdapter.Fill(ds3);
            dt3 = ds3.Tables[0];

            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.ShowDialog();
                string filename = ofd.FileName;
                Microsoft.Office.Interop.Excel.Application excelObject = new Microsoft.Office.Interop.Excel.Application();
                excelObject.Visible = false;
                Workbook wb = excelObject.Workbooks.Open(filename, 0, false, 5, "", "", false, XlPlatform.xlWindows, "", true, false, 0, true, false, false);
                Worksheet wsh = (Worksheet)wb.Sheets[1];

                wsh.Cells[1, 1] = "Название товара";
                wsh.Cells[1, 2] = "Количество отгруженного";
                wsh.Cells[1, 3] = "Сумма отгруженного";
                wsh.Cells[1, 4] = "Подлежащие отгрузке";
                wsh.Cells[1, 5] = "Сумма подлежащих к отгрузке";
                wsh.Cells[1, 7] = "Период выборки для отчёта:";
                wsh.Cells[1, 6] = "Общая сумма";
                wsh.Cells[2,7] =  $"{date1} - {date2}";

                    for (int i = 0; i < dt3.Rows.Count; i++)
                    {

                        for (int j = 0; j < dt3.Columns.Count; j++)
                            wsh.Cells[i + 2, j + 1] = dt3.Rows[i].ItemArray[j];

                    }
                excelObject.Columns.AutoFit();
                wb.Save();
                wb.Close();
            }
        }
    }
}
