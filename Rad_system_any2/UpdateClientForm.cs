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
    public partial class UpdateClientForm : Form
    {
        NpgsqlConnection con;
        int id;
        public UpdateClientForm(NpgsqlConnection con, int id)
        {
            InitializeComponent();
            this.id = id;
            this.con = con;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            try
            {
                NpgsqlCommand cmd = new NpgsqlCommand($"update client set name_client=:name_client, adress=:adress, phone=:phone where id_client=:id_client", con);

                cmd.Parameters.AddWithValue("name_client", textBox1.Text);
                cmd.Parameters.AddWithValue("adress", textBox2.Text);
                cmd.Parameters.AddWithValue("phone", textBox3.Text);
                cmd.Parameters.AddWithValue("id_client", id);

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
