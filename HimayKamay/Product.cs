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
    public partial class Product : Form
    {
        MySqlConnection con;
        MySqlCommand cmd;
        MySqlCommand cmd1;
        MySqlDataReader read;

        int i;
        public Product()
        {
            InitializeComponent();
            con = new MySqlConnection();
            con.ConnectionString = "server=localhost;user id=root;password=;database=filipinodish";
        }

        public void Record()
        {
            try
            {
                DataGridView2.Rows.Clear();

                con.Open();
                cmd = new MySqlCommand("select * from tblproduct order by id desc", con);
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

        private void Product_Load(object sender, EventArgs e)
        {
            Record();
        }

        private void DataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colname = DataGridView2.Columns[e.ColumnIndex].Name;
            if (colname == "ColEdit")
            {
                Productlist frmproductlicst = new Productlist();
                con.Open();
                cmd = new MySqlCommand("select image from tblproduct where id like '" + DataGridView2.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", con);
                read = cmd.ExecuteReader();
                while (read.Read())
                {
                    long len = read.GetBytes(0, 0, null, 0, 0);
                    byte[] array = new byte[System.Convert.ToInt32(len) + 1];
                    read.GetBytes(0, 0, array, 0, System.Convert.ToInt32(len));
                    MemoryStream ms = new MemoryStream(array);
                    System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(ms);
                    frmproductlicst.PictureBox1.BackgroundImage = bitmap;
                    frmproductlicst.txtID.Text = DataGridView2.Rows[e.RowIndex].Cells[1].Value.ToString();
                    frmproductlicst.txtItem.Text = DataGridView2.Rows[e.RowIndex].Cells[2].Value.ToString();
                    frmproductlicst.txtPrice.Text = DataGridView2.Rows[e.RowIndex].Cells[3].Value.ToString();
                    frmproductlicst.cboCategory.Text = DataGridView2.Rows[e.RowIndex].Cells[4].Value.ToString();
                }
                read.Close();
                con.Close();
                frmproductlicst.loadcategory();
                frmproductlicst.Btnadd.Enabled = false;
                frmproductlicst.btnUpdate.Enabled = true;
                frmproductlicst.ShowDialog();
            }
            else if (colname == "ColDelete")
            {
                DialogResult dialogResult = MessageBox.Show("Delete this record?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    con.Open();
                    cmd = new MySqlCommand("Insert into inactive (ID,Item,Price,Category,Image) Select * from tblproduct where ID like '" + DataGridView2.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", con);
                    cmd.ExecuteNonQuery();
                    cmd1 = new MySqlCommand(" Delete from tblproduct where ID like '" + DataGridView2.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", con);
                    cmd1.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Item Deleted", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Record();
                }
                else
                {
                    return;
                }
            }
            Record();
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            Productlist frmproductlist = new Productlist();
            frmproductlist.Btnadd.Enabled = true;
            frmproductlist.btnUpdate.Enabled = false;
            frmproductlist.loadcategory();
            frmproductlist.ShowDialog();
            Record();
        }

        private void Product_Activated(object sender, EventArgs e)
        {
            Record();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            search();
        }
        private void search()
        {
            if (textBox1.Text == string.Empty)
            {
                Record();
            }
            else {
                    DataGridView2.Rows.Clear();

                    con.Open();
                    cmd = new MySqlCommand("select * from tblproduct where Item like '" + textBox1.Text + "'", con);
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

}