using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Rad_system_any2
{
    public partial class FuturaForm : Form
    {
        DataSet ds, ds2;
        DataTable dt, dt2;
        NpgsqlConnection npgsqlConnection;


        public FuturaForm()
        {
            InitializeComponent();
            npgsqlConnection = new NpgsqlConnection("Server=localhost;Port=5432;User Id=postgres;Password=admin;Database=Rad_system_Lera");

            npgsqlConnection.Open();
            ConnectToDataBase();
            
            dataGridView1.Columns[0].HeaderText = "Код накладной";
            dataGridView1.Columns[1].HeaderText = "Дата создания";
            dataGridView1.Columns[2].HeaderText = "ФИО клиента";
            dataGridView1.Columns[3].HeaderText = "Сумма";
            dataGridView1.Columns[4].HeaderText = "Предопалата";


            WatchFuturaInfo();
            dataGridView2.Columns[0].HeaderText = "Код отчёта";
            dataGridView2.Columns[1].HeaderText = "Код накладной";
            dataGridView2.Columns[2].HeaderText = "Название продукта";
            dataGridView2.Columns[3].HeaderText = "Количество";
            dataGridView2.Columns[4].HeaderText = "Сумма";
            dataGridView2.Columns[5].HeaderText = "Количество отгруженного товара";
            dataGridView2.Columns[6].HeaderText = "Сумма отгруженного товара";
        }

        private void PickFuturaInfo(object sender, EventArgs e)
        {
            int id = (int)dataGridView1.CurrentRow.Cells["id_futura"].Value;


           // string sql = $"select * from futurainfo where id_futura = {id}";
            string sql = $"select id_fut_info, id_futura, product.name_product, quantity, price, quantitiy_otgruzka, price_otgruzka from futurainfo inner join product on futurainfo.id_product = product.id_product where id_futura = {id}";


            NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(sql, npgsqlConnection);

            ds2.Reset();
            dataAdapter.Fill(ds2);
            dt2 = ds2.Tables[0];
            dataGridView2.DataSource = dt2;

        }

        void Pick() {
            int id = (int)dataGridView1.CurrentRow.Cells["id_futura"].Value;


            // string sql = $"select * from futurainfo where id_futura = {id}";
            string sql = $"select id_fut_info, id_futura, product.name_product, quantity, price, quantitiy_otgruzka, price_otgruzka from futurainfo inner join product on futurainfo.id_product = product.id_product where id_futura = {id}";


            NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(sql, npgsqlConnection);

            ds2.Reset();
            dataAdapter.Fill(ds2);
            dt2 = ds2.Tables[0];
            dataGridView2.DataSource = dt2;
        }

        void WatchFuturaInfo()
        {
            ds2 = new DataSet();
            dt2 = new DataTable();
            //string sql = "select * from futurainfo";
            string sql = $"select id_fut_info, id_futura, product.name_product, quantity, price, quantitiy_otgruzka, price_otgruzka from futurainfo inner join product on futurainfo.id_product = product.id_product";


            NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(sql, npgsqlConnection);

            ds2.Reset();
            dataAdapter.Fill(ds2);
            dt2 = ds2.Tables[0];
            dataGridView2.DataSource = dt2;

        }


        private void FuturaForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            npgsqlConnection.Close();
        }


        void ConnectToDataBase()
        {
            ds = new DataSet();
            dt = new DataTable();
            string sql = "select id_futura, data_fut, client.name_client, total_sum, predoplata from futura inner join client on futura.id_client = client.id_client";

            NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(sql, npgsqlConnection);

            ds.Reset();
            dataAdapter.Fill(ds);
            dt = ds.Tables[0];
            dataGridView1.DataSource = dt;
        }



        private void Delete(int id)
        {
            NpgsqlCommand cmd = new NpgsqlCommand("delete from futura where id_futura = :id", npgsqlConnection);
            cmd.Parameters.AddWithValue("id", id);
            cmd.ExecuteNonQuery();
        }


        private void добавитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddFuturaForm f1 = new AddFuturaForm(npgsqlConnection);
            f1.ShowDialog();
            ConnectToDataBase();
        }

        private void добавитьВоВторуюТаблицуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int id = (int)dataGridView1.CurrentRow.Cells["id_futura"].Value;
            AddFuturaInfoForm f1 = new AddFuturaInfoForm(npgsqlConnection, id);
            f1.ShowDialog();
            ConnectToDataBase();
        }

        private void изменитьОтгрузкуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int id = (int)dataGridView2.CurrentRow.Cells["id_fut_info"].Value;
            float price = (float)dataGridView2.CurrentRow.Cells["price"].Value;
            float quantity = (float)dataGridView2.CurrentRow.Cells["quantity"].Value;


            UpdateOtgruzka f = new UpdateOtgruzka(npgsqlConnection, id, price, quantity);
            f.ShowDialog();
            ConnectToDataBase();
            Pick();
        }

        private void редактироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int id = (int)dataGridView1.CurrentRow.Cells["id_futura"].Value;


            //string name = (string)dataGridView1.CurrentRow.Cells["name_product"].Value;
            //double price = Convert.ToDouble(dataGridView1.CurrentRow.Cells["price"].Value);
            UpdatePredoplata f = new UpdatePredoplata(npgsqlConnection, id);
            f.ShowDialog();
            ConnectToDataBase();
            Pick();
            //Update();
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int id = (int)dataGridView1.CurrentRow.Cells["id_futura"].Value;
            Delete(id);
            //Update();
            ConnectToDataBase();

        }



    }
}
