using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.IO;
namespace HimayKamay
{
    public partial class Unused : Form
    {
        MySqlConnection con;
        MySqlCommand cmd;
        MySqlCommand cmd1;
        MySqlDataReader read;

        int i;
        public Unused()
        {
            InitializeComponent();
            con = new MySqlConnection();
            con.ConnectionString = "server=localhost;user id=root;password=;database=filipinodish";
        }
        public void records()
        {
            try
            {
                DataGridView2.Rows.Clear();

                con.Open();
                cmd = new MySqlCommand("select * from inactive order by id desc", con);
                read = cmd.ExecuteReader();
                while (read.Read())
                {
                    i = i + 1;
                    DataGridView2.Rows.Add(i, read["id"].ToString(), read["Item"].ToString(), read["Price"].ToString(), read["Category"].ToString(), read["Image"]);

                }
                for (int j = 0; j <= DataGridView2.Rows.Count - 1; j++)
                {
                    DataGridViewRow r = DataGridView2.Rows[j];
                    r.Height = 100;


                }
                var imagecolumn = (DataGridViewImageColumn)DataGridView2.Columns["Column4"];
                imagecolumn.ImageLayout = DataGridViewImageCellLayout.Stretch;

                read.Close();
                con.Close();
            }
            catch (Exception ex)
            {
            }
        }

        private void Unused_Load(object sender, EventArgs e)
        {
            records();
        }

        private void DataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colname = DataGridView2.Columns[e.ColumnIndex].Name;
            if (colname == "ColAdd")
            {
                DialogResult dialogResult = MessageBox.Show("Add this Item?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes) ;
                con.Open();
                cmd = new MySqlCommand("Insert into tblproduct (ID,Item,Price,Category,Image) Select * from inactive where ID like '" + DataGridView2.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", con);
                cmd.ExecuteNonQuery();
                cmd1 = new MySqlCommand(" Delete from inactive where ID like '" + DataGridView2.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", con);
                cmd1.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Item Added" , "Added", MessageBoxButtons.OK, MessageBoxIcon.Information);
                records();

            }
            else if (colname == "ColDelete")
            {
                DialogResult dialogResult = MessageBox.Show("Delete this record?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes) 
                {
                    con.Open();
                    cmd1 = new MySqlCommand(" Delete from inactive where ID like '" + DataGridView2.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", con);
                    cmd1.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Item Deleted", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    records();
                }
                else
                {
                    return;
                }
            }
        }

        private void Unused_Activated(object sender, EventArgs e)
        {
            records();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text == string.Empty)
            {
                records();
            }
            else {
                search();
            }
        }
        private void search()
        {

            DataGridView2.Rows.Clear();

            con.Open();
            cmd = new MySqlCommand("select * from inactive where Item like '"+ textBox1.Text +"'", con);
            read = cmd.ExecuteReader();
            while (read.Read())
            {
                i = i + 1;
                DataGridView2.Rows.Add(i, read["id"].ToString(), read["Item"].ToString(), read["Price"].ToString(), read["Category"].ToString(), read["Image"]);

            }
            for (int j = 0; j <= DataGridView2.Rows.Count - 1; j++)
            {
                DataGridViewRow r = DataGridView2.Rows[j];
                r.Height = 100;


            }
            var imagecolumn = (DataGridViewImageColumn)DataGridView2.Columns["Column4"];
            imagecolumn.ImageLayout = DataGridViewImageCellLayout.Stretch;

            read.Close();
            con.Close();
        }
    }
    }

