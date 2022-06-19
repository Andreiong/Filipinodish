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
    public partial class Transaction : Form
    {
        MySqlConnection con;
        MySqlCommand cmd;
        MySqlCommand cmd1;
        MySqlDataReader read;

        int i;
        public Transaction()
        {
            InitializeComponent();
            con = new MySqlConnection();
            con.ConnectionString = "server=localhost;user id=root;password=;database=filipinodish";
            this.KeyPreview = true;
        }

        public void transaction()
        {
            try
            {
                DataGridView2.Rows.Clear();

                con.Open();
                cmd = new MySqlCommand("select c.id, c.transno, p.Item, c.price, c.quantity, p.category, p.Image from tblcart as c inner join tblproduct as p on p.ID = c.productid order by transno desc", con);
                read = cmd.ExecuteReader();
                while (read.Read())
                {
                    i = i + 1;
                    DataGridView2.Rows.Add(i, read["id"].ToString(), read["Item"].ToString(), read["transno"].ToString(), read["price"].ToString(), read["Quantity"].ToString(), read["category"].ToString(), read["Image"]);

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

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Transaction_Load(object sender, EventArgs e)
        {
            transaction();
        }

        private void Transaction_Activated(object sender, EventArgs e)
        {
            transaction();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text == String.Empty)
            {
                transaction();
            }
            else
            {
                search();
            }
        }
        public void search() {
            DataGridView2.Rows.Clear();

            con.Open();
            cmd = new MySqlCommand("select c.id, c.transno, p.Item, c.price, c.quantity, p.category, p.Image from tblcart as c inner join tblproduct as p on p.ID = c.productid where transno like '"+ textBox1.Text +"'", con);
            read = cmd.ExecuteReader();
            while (read.Read())
            {
                i = i + 1;
                DataGridView2.Rows.Add(i, read["id"].ToString(), read["Item"].ToString(), read["transno"].ToString(), read["price"].ToString(), read["Quantity"].ToString(), read["category"].ToString(), read["Image"]);

            }

            for (int j = 0; j <= DataGridView2.Rows.Count - 1; j++)
            {
                DataGridViewRow r = DataGridView2.Rows[j];
                r.Height = 100;
            }
            var imagecolumn = (DataGridViewImageColumn)DataGridView2.Columns["Column4"];
            imagecolumn.ImageLayout = DataGridViewImageCellLayout.Stretch;
            con.Close();
        }
    }
}
