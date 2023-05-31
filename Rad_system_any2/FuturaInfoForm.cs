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
    public partial class FuturaInfoForm : Form
    {
        DataSet ds;
        DataTable dt;
        NpgsqlConnection npgsqlConnection;

        public FuturaInfoForm()
        {
            InitializeComponent();

            npgsqlConnection = new NpgsqlConnection("Server=localhost;Port=5432;User Id=postgres;Password=admin;Database=Rad_system_Lera");

            npgsqlConnection.Open();
            ConnectToDataBase();
            dataGridView1.Columns[0].HeaderText = "Код отчёта";
            dataGridView1.Columns[1].HeaderText = "Код накладной";
            dataGridView1.Columns[2].HeaderText = "Код продукта";
            dataGridView1.Columns[3].HeaderText = "Количество";
            dataGridView1.Columns[4].HeaderText = "Сумма";
            dataGridView1.Columns[5].HeaderText = "Количество отгруженного товара";
            dataGridView1.Columns[6].HeaderText = "Сумма отгруженного товара";

        }


        private void FuturaInfoForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            npgsqlConnection.Close();
        }

        void ConnectToDataBase()
        {
            ds = new DataSet();
            dt = new DataTable();
            string sql = "select * from futurainfo";

            NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(sql, npgsqlConnection);

            ds.Reset();
            dataAdapter.Fill(ds);
            dt = ds.Tables[0];
            dataGridView1.DataSource = dt;
        }



        private void Delete(int id)
        {
            NpgsqlCommand cmd = new NpgsqlCommand("delete from futurainfo where id_fut_info = :id", npgsqlConnection);
            cmd.Parameters.AddWithValue("id", id);
            cmd.ExecuteNonQuery();
        }


        private void добавитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //AddFuturaInfoForm f1 = new AddFuturaInfoForm(npgsqlConnection);
            //f1.ShowDialog();
            //ConnectToDataBase();
        }

        private void редактироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //int id = (int)dataGridView1.CurrentRow.Cells["id_product"].Value;
            //string name = (string)dataGridView1.CurrentRow.Cells["name_product"].Value;
            //double price = Convert.ToDouble(dataGridView1.CurrentRow.Cells["price"].Value);
            //ChangeGoodForm f = new ChangeForm1(npgsqlConnection, id, name, price);
            //f.ShowDialog();
            //Update();
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int id = (int)dataGridView1.CurrentRow.Cells["id_fut_info"].Value;
            Delete(id);
            //Update();
            ConnectToDataBase();

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
