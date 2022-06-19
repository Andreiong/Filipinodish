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
    public partial class Productlist : Form
    {
        MySqlCommand cmd;
        MySqlConnection con;
        MySqlDataReader read;
        public Productlist()
        {
            InitializeComponent();
            con = new MySqlConnection();
            con.ConnectionString = "server=localhost;user id=root;password=;database=filipinodish";
            loadcategory();
        }
        public void loadcategory()
        {
            cboCategory.Items.Clear();
            con.Open();
            cmd = new MySqlCommand("Select * from tblcategory", con);
            read = cmd.ExecuteReader();
            while (read.Read())
            {
                cboCategory.Items.Add(read["category"].ToString()) ;
            }
            con.Close();
            read.Close();
        }
        private void Button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog1.Filter = "Image files (*.png) | *.png|(*.jpg)|*.jpg|(*.gif)|*.gif";
            OpenFileDialog1.ShowDialog();
            PictureBox1.BackgroundImage = Image.FromFile(OpenFileDialog1.FileName);
;
        }
        

        private void Btnadd_Click(object sender, EventArgs e)
        {
            try
            {
                if (OpenFileDialog1.FileName == "OpenFileDialog1") 
                {
                    MessageBox.Show("Insert Image!");
                    return;
            }
                if (txtItem.Text == String.Empty || txtPrice.Text == String.Empty) {
                    MessageBox.Show("Insert Item");
                return;
           }
                if (cboCategory.Text == String.Empty) {
                    MessageBox.Show("Choose Category");
                    return;
                }
                DialogResult dialogResult = MessageBox.Show("Add this Item?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (dialogResult == DialogResult.Yes)
                {
                    MemoryStream mstream = new MemoryStream();
                    PictureBox1.BackgroundImage.Save(mstream, System.Drawing.Imaging.ImageFormat.Jpeg);
                    byte[] arrImage = mstream.GetBuffer();

                    con.Open();
                cmd = new MySqlCommand("Insert into tblproduct(Item,Price,Category,Image)values(@Item,@Price,@Category,@Image) ", con);

                    cmd.Parameters.AddWithValue("@Item", txtItem.Text);
                    cmd.Parameters.AddWithValue("@Price", double.Parse(txtPrice.Text));
                    cmd.Parameters.AddWithValue("@Category", cboCategory.Text);
                    cmd.Parameters.AddWithValue("@Image", arrImage);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Record Saved", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information) ;
                    
                }
                txtItem.Clear();
                txtID.Text = "(Auto)";
                txtPrice.Clear();
                cboCategory.Text = "";
                Btnadd.Enabled = true;
                btnUpdate.Enabled = false;
                txtItem.Focus();
                PictureBox1.BackgroundImage = null;
            }
            catch (Exception ex)
            { 
            }
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            Category frmcategory = new Category();
            frmcategory.ShowDialog();
            this.Refresh();
        }

        private void ToolStripButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Productlist_Activated(object sender, EventArgs e)
        {
            loadcategory();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try 
            {
                if (txtItem.Text == String.Empty || txtPrice.Text == String.Empty) {
                    MessageBox.Show("Insert Item");
                    return;
            }
                if (cboCategory.Text == String.Empty) {
                    MessageBox.Show("Choose Category");
                    return; 
                }
                DialogResult dialogResult = MessageBox.Show("Update this Item?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (dialogResult == DialogResult.Yes)
                {
                    MemoryStream mstream = new MemoryStream();
                    PictureBox1.BackgroundImage.Save(mstream, System.Drawing.Imaging.ImageFormat.Jpeg);
                    byte[] arrImage = mstream.GetBuffer();

                    con.Open();
                    cmd = new MySqlCommand("Update tblproduct set Item=@Item,Price=@Price,Category=@Category,Image=@Image Where id=@id", con);
                   cmd.Parameters.AddWithValue("@Item", txtItem.Text);
                  cmd.Parameters.AddWithValue("@Price", double.Parse(txtPrice.Text));
                   cmd.Parameters.AddWithValue("@Category", cboCategory.Text);
                    cmd.Parameters.AddWithValue("@Image", arrImage);
                    cmd.Parameters.AddWithValue("@ID", txtID.Text);
                    cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Item Updated");
                }
                txtItem.Clear();
                txtID.Text = "(Auto)";
                txtPrice.Clear();
                cboCategory.Text = "";
                Btnadd.Enabled = true;
                btnUpdate.Enabled = false;
                txtItem.Focus();
                PictureBox1.BackgroundImage = null;
            }
            catch(Exception ex)
            {
                con.Close();
            }
        }
    }
    }

