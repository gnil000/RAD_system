using Npgsql;
using System.Diagnostics;

namespace Rad_system_any2
{
    public partial class UpdateForm1 : Form
    {

        NpgsqlConnection con;
        int id;

        public UpdateForm1(NpgsqlConnection con, int id)
        {
            InitializeComponent();
            this.con = con;
            this.id = id;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                NpgsqlCommand cmd = new NpgsqlCommand($"update product set name_product=:name_product, ed_product=:ed_product where id_product=:id_product", con);
            
                cmd.Parameters.AddWithValue("name_product", textBox1.Text);
                cmd.Parameters.AddWithValue("ed_product", textBox2.Text);
                cmd.Parameters.AddWithValue("id_product", id);

                cmd.ExecuteNonQuery();
                Close();
            }
            catch (Exception ex)
            {
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
