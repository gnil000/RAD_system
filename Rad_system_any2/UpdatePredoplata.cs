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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Rad_system_any2
{
    public partial class UpdatePredoplata : Form
    {
        NpgsqlConnection con;
        int id;
        public UpdatePredoplata(NpgsqlConnection con, int id)
        {
            InitializeComponent();
            this.con = con;
            this.id = id;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            try
            {
                NpgsqlCommand cmd = new NpgsqlCommand($"update futura set predoplata=:predoplata where id_futura=:id_futura", con);

                cmd.Parameters.AddWithValue("predoplata", checkBox1.Checked);
                cmd.Parameters.AddWithValue("id_futura", id);

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
