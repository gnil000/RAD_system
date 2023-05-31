using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Rad_system_any2
{
    public partial class AddFuturaInfoForm : Form
    {
        DataSet ds;
        DataTable dt;
        NpgsqlConnection con;
        int id;

        public AddFuturaInfoForm(NpgsqlConnection con, int id)
        {
            InitializeComponent();
            this.con = con;
            this.id = id;

            ConnectToDataBase();
            dataGridView1.Columns[0].HeaderText = "Код товара";
            dataGridView1.Columns[1].HeaderText = "Наименование товара";
            dataGridView1.Columns[2].HeaderText = "Единица измерения";
        }


        void ConnectToDataBase()
        {
            ds = new DataSet();
            dt = new DataTable();
            string sql = "select * from Product";

            NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(sql, con);

            ds.Reset();
            dataAdapter.Fill(ds);
            dt = ds.Tables[0];
            dataGridView1.DataSource = dt;
        }
        private void button1_Click(object sender, EventArgs e)
        {

            try
            {
                //NpgsqlCommand cmd = new NpgsqlCommand($"insert into Product values(default, {textBox1.Text}, {textBox2.Text})", con);
                NpgsqlCommand cmd = new NpgsqlCommand($"insert into FuturaInfo(id_fut_info, id_futura, id_product, quantity, quantitiy_otgruzka, price) values(default, :id_futura, :id_product, :quantity, :quantitiy_otgruzka , :price)", con);


                cmd.Parameters.AddWithValue("id_futura", id);
                cmd.Parameters.AddWithValue("id_product", (int)dataGridView1.CurrentRow.Cells["id_product"].Value);
                cmd.Parameters.AddWithValue("quantity", Convert.ToInt64(textBox3.Text));
                cmd.Parameters.AddWithValue("price", Convert.ToInt64(textBox4.Text));
                cmd.Parameters.AddWithValue("quantitiy_otgruzka", Convert.ToInt64(textBox5.Text));

                cmd.ExecuteNonQuery();
                Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Any exception");
            }



        }
    }
}
