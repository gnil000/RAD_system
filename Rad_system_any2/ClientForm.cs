using Microsoft.Office.Interop.Excel;
using Npgsql;
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
    public partial class ClientForm : Form
    {

        DataSet ds;
        System.Data.DataTable dt;
        NpgsqlConnection npgsqlConnection;


        public ClientForm()
        {
            InitializeComponent();

            npgsqlConnection = new NpgsqlConnection("Server=localhost;Port=5432;User Id=postgres;Password=admin;Database=Rad_system_Lera");

            npgsqlConnection.Open();
            ConnectToDataBase();
            dataGridView1.Columns[0].HeaderText = "Код клиента";
            dataGridView1.Columns[1].HeaderText = "ФИО";
            dataGridView1.Columns[2].HeaderText = "Адресс";
            dataGridView1.Columns[3].HeaderText = "Телефон";
        }

        private void ClientForm_Load(object sender, EventArgs e)
        {
        }


        private void ClientForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            npgsqlConnection.Close();
        }



        void ConnectToDataBase()
        {
            ds = new DataSet();
            dt = new System.Data.DataTable();
            string sql = "select * from client";

            NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(sql, npgsqlConnection);

            ds.Reset();
            dataAdapter.Fill(ds);
            dt = ds.Tables[0];
            dataGridView1.DataSource = dt;
        }



        private void Delete(int id)
        {
            NpgsqlCommand cmd = new NpgsqlCommand("delete from client where id_client = :id", npgsqlConnection);
            cmd.Parameters.AddWithValue("id", id);
            cmd.ExecuteNonQuery();
        }


        private void добавитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddClientForm f1 = new AddClientForm(npgsqlConnection);
            f1.ShowDialog();
            ConnectToDataBase();
        }

        private void редактироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int id = (int)dataGridView1.CurrentRow.Cells["id_client"].Value;
            //string name = (string)dataGridView1.CurrentRow.Cells["name_product"].Value;
            //double price = Convert.ToDouble(dataGridView1.CurrentRow.Cells["price"].Value);
            UpdateClientForm f = new UpdateClientForm(npgsqlConnection, id);
            f.ShowDialog();
            ConnectToDataBase();
            //Update();
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int id = (int)dataGridView1.CurrentRow.Cells["id_client"].Value;
            Delete(id);
            //Update();
            ConnectToDataBase();

        }

        private void сформироватьНакладнуюToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int id = (int)dataGridView1.CurrentRow.Cells["id_client"].Value;


            string sql = $"select product.name_product, quantitiy_otgruzka, price_otgruzka from futurainfo inner join product on futurainfo.id_product = product.id_product where (futurainfo.id_futura in (select futura.id_futura from futura where futura.id_client = {id}))";

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
                wsh.Cells[1, 3] = "Сумма (в рублях)";

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
