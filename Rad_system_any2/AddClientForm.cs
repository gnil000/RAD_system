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
    public partial class AddClientForm : Form
    {

        NpgsqlConnection con;

        public AddClientForm(NpgsqlConnection con)
        {
            InitializeComponent();
            this.con = con; 
        }

        private void AddClientForm_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                NpgsqlCommand cmd = new NpgsqlCommand($"insert into Client(id_client, name_client, adress, phone) values(default, :name_client , :adress , :phone )", con);


                cmd.Parameters.AddWithValue("name_client", textBox1.Text);
                cmd.Parameters.AddWithValue("adress", textBox2.Text);
                cmd.Parameters.AddWithValue("phone", textBox3.Text);


                cmd.ExecuteNonQuery();
                Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Any exception");
            }



        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
