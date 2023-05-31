
using Npgsql;
using System.Data;

namespace Rad_system_any2
{
    public partial class Form1 : Form
    {
        DataSet ds;
        DataTable dt;
        NpgsqlConnection npgsqlConnection;

        public Form1()
        {
            InitializeComponent();

            npgsqlConnection = new NpgsqlConnection("Server=localhost;Port=5432;User Id=postgres;Password=admin;Database=Rad_system_Lera");

            npgsqlConnection.Open();
            ConnectToDataBase();
            dataGridView1.Columns[0].HeaderText = "Код товара";
            dataGridView1.Columns[1].HeaderText = "Наименование товара";
            dataGridView1.Columns[2].HeaderText = "Единица измерения";

        }

        void ConnectToDataBase() {
            ds = new DataSet();
            dt = new DataTable();
            string sql = "select * from Product";

            NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(sql, npgsqlConnection);

            ds.Reset();
            dataAdapter.Fill(ds);
            dt = ds.Tables[0];
            dataGridView1.DataSource = dt;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            npgsqlConnection.Close();
        }

        private void Delete(int id) {
            NpgsqlCommand cmd = new NpgsqlCommand("delete from product where id_product = :id", npgsqlConnection);
            cmd.Parameters.AddWithValue("id", id);
            cmd.ExecuteNonQuery();
        }


        private void добавитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGoodsForm f1 = new DataGoodsForm(npgsqlConnection);
            f1.ShowDialog();
            ConnectToDataBase();
        }

        private void редактироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int id = (int)dataGridView1.CurrentRow.Cells["id_product"].Value;
            //string name = (string)dataGridView1.CurrentRow.Cells["name_product"].Value;
           // string ed = (string)dataGridView1.CurrentRow.Cells["ed_product"].Value;
            UpdateForm1 f = new UpdateForm1(npgsqlConnection, id);
            f.ShowDialog();
            //Update();
            ConnectToDataBase();
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int id = (int)dataGridView1.CurrentRow.Cells["id_product"].Value;
            Delete(id);
            //Update();
            ConnectToDataBase();

        }
    }
}