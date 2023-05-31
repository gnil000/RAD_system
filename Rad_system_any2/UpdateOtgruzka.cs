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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace Rad_system_any2
{
    public partial class UpdateOtgruzka : Form
    {
        NpgsqlConnection con;
        int id;
        float price, quantity;
        public UpdateOtgruzka(NpgsqlConnection con, int id, float price, float quantity)
        {
            InitializeComponent();
            this.con = con;
            this.id = id;
            this.price = price;
            this.quantity = quantity;   
        }

        private void button1_Click(object sender, EventArgs e)
        {

            //try
            //{
                NpgsqlCommand cmd = new NpgsqlCommand($"update futurainfo set quantitiy_otgruzka=:quantitiy_otgruzka, price_otgruzka=:price_otgruzka where id_fut_info=:id_fut_info", con);
            float price_otgruzka = (price / quantity) * Convert.ToInt64(textBox1.Text);

            cmd.Parameters.AddWithValue("price_otgruzka",price_otgruzka);
            cmd.Parameters.AddWithValue("quantitiy_otgruzka", Convert.ToInt64(textBox1.Text));
                cmd.Parameters.AddWithValue("id_fut_info", id);

                cmd.ExecuteNonQuery();
                Close();
            //}
            //catch (Exception ex)
            //{
            //    Debug.WriteLine("Any exception");
            //}


        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
