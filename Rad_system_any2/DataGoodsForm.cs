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
using Npgsql;


namespace Rad_system_any2
{
    public partial class DataGoodsForm : Form
    {
        NpgsqlConnection con;

        public DataGoodsForm(NpgsqlConnection con)
        {
            InitializeComponent();
            this.con = con;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //NpgsqlCommand cmd = new NpgsqlCommand($"insert into Product values(default, {textBox1.Text}, {textBox2.Text})", con);
                NpgsqlCommand cmd = new NpgsqlCommand($"insert into Product(id_product, name_product, ed_product) values(default, :name_product , :ed_product )", con);


                cmd.Parameters.AddWithValue("name_product", textBox1.Text);
                cmd.Parameters.AddWithValue("ed_product", textBox2.Text);

                cmd.ExecuteNonQuery();
                Close();
            }
            catch (Exception ex) {
                Debug.WriteLine("Any exception");
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
