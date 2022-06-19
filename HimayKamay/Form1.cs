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
using System.Globalization;

namespace HimayKamay
{
    public partial class Form1 : Form
    {
         MySqlConnection con;
         MySqlCommand cmd;
         MySqlDataReader read;

        public Button btncategory = new Button();
        private PictureBox pic = new PictureBox();
        public Label item = new Label();
        public Label price = new Label();
        public string _filter = "";

        public static string Food;
        public static string transno;
        public static string user;
        public static string pid;
        public Form1()
        {
            InitializeComponent();
            con = new MySqlConnection();
            con.ConnectionString = "server=localhost;user id=root;password=;database=filipinodish";
            Loadmenu();
            Loadcategory();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Loadmenu();
            Loadcategory();
            Button1.Enabled = false;
            Button2.Enabled = false;
            Button5.Enabled = false;
            Button6.Enabled = false; 
            Label17.Visible = false;
            Label18.Visible = false;
        }
        public void Loadmenu() 
        {
            FlowLayoutPanel2.AutoScroll = true;
            FlowLayoutPanel2.Controls.Clear();
            con.Open();
            cmd = new MySqlCommand("Select image,id,item,price from tblproduct where category like '" + _filter + "%'order by Item", con);
            read = cmd.ExecuteReader();
            while (read.Read())
            {
                long len = read.GetBytes(0, 0, null, 0, 0);
                byte[] array = new byte[System.Convert.ToInt32(len) + 1];
                read.GetBytes(0, 0, array, 0, System.Convert.ToInt32(len));
                pic = new PictureBox();
                pic.Height = 150;
                pic.Width = 150;
                pic.BackgroundImageLayout = ImageLayout.Stretch;
                pic.Cursor = Cursors.Hand;
                MemoryStream ms = new MemoryStream(array);
                System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(ms);
                pic.BackgroundImage = bitmap;
                pic.Tag = read["id"].ToString();

                item = new Label();
                item.BackColor = Color.FromArgb(0, 151, 230);
                item.ForeColor = Color.White;
                item.Text = read["Item"].ToString();
                item.Dock = DockStyle.Bottom;
                item.TextAlign = ContentAlignment.MiddleCenter;
                item.Font = new Font("Calibri", 11, FontStyle.Regular);
                item.Cursor = Cursors.Hand;
                item.Tag = read["id"].ToString();

                price = new Label();
                price.BackColor = Color.FromArgb(230, 126, 34);
                price.ForeColor = Color.White;
                price.Text = read["price"].ToString();
                price.Dock = DockStyle.Top;
                price.TextAlign = ContentAlignment.MiddleCenter;
                price.Font = new Font("Calibri", 12, FontStyle.Regular);
                price.Cursor = Cursors.Hand;
                price.AutoSize = true;
                price.Tag = read["id"].ToString();

                pic.Controls.Add(item);
                pic.Controls.Add(price);
                FlowLayoutPanel2.Controls.Add(pic);
                pic.Click += new EventHandler(Select_click);
                item.Click += new EventHandler(Select_click);
                price.Click += new EventHandler(Select_click);

            }
            read.Close();
            con.Close();
        }
        public void Loadcategory()
        {
            FlowLayoutPanel1.Controls.Clear();
            con.Open();
            cmd = new MySqlCommand("select * from tblcategory order by category asc", con);
            read = cmd.ExecuteReader();
            while (read.Read())
            {
                FlowLayoutPanel1.AutoScroll = true;
                btncategory = new Button();
                btncategory.Width = 138;
                btncategory.Height = 40;
                btncategory.FlatStyle = FlatStyle.Popup;
                btncategory.BackColor = Color.FromArgb(0, 151, 230);
                btncategory.ForeColor = Color.White;
                btncategory.Cursor = Cursors.Hand;
                btncategory.TextAlign = ContentAlignment.MiddleCenter;
                btncategory.Text = read["category"].ToString();

                FlowLayoutPanel1.Controls.Add(btncategory);

                btncategory.Click += new EventHandler(Filter_click);
            }
            read.Close();
            con.Close();
        }
        public void Loadcart()
        {
            double _total = new double();
            DataGridView2.Rows.Clear();
            con.Open();
            cmd = new MySqlCommand("select c.id, p.Item, c.price, c.quantity, c.total from tblcart as c inner join tblproduct as p on p.ID = c.productid where c.transno like '" + Label4.Text + "'", con);
            read = cmd.ExecuteReader();
            while (read.Read())
            {
                _total = _total + double.Parse(read["total"].ToString());
                DataGridView2.Rows.Add(read["id"].ToString(), read["Item"].ToString(), read["price"].ToString(), read["quantity"].ToString(), read["total"].ToString());
            }
            read.Close();
            con.Close();
            TextBox2.Text = _total.ToString("₱ #,##0.00");
            Label8.Text = _total.ToString("₱ #,##0.00");
            Label6.Text = _total.ToString("#,##0.00");
            TextBox5.Text = Label6.Text;

        }
        public void Select_click(object sender, EventArgs e)
        {
            while (Label4.Text == String.Empty)
                {
                MessageBox.Show("Click New Order First");
                return;
            }
            double price = new double();
            string id = ((PictureBox)sender).Tag.ToString();
            con.Open();
            cmd = new MySqlCommand("Select * from tblproduct where id like '" + id + "'",con);
            read = cmd.ExecuteReader();
            read.Read();
            if (read.HasRows)
            {
                price = Convert.ToDouble(read["price"].ToString());
                Food = read["Item"].ToString();
                textBox6.Text = read["id"].ToString();
                TextBox4.Text = Food;
                pid = textBox6.Text;

            }
            read.Close();
            con.Close();

            Qty frmquantity = new Qty();
            frmquantity.Addtocart(id,price);
            frmquantity.ShowDialog();
            Loadcart();

            Sales frmsales = new Sales();
            frmsales.Addtocart(id);
            

            con.Open();
            cmd = new MySqlCommand("Select price from tblproduct where id like '" + id + "'", con);
            read = cmd.ExecuteReader();
            TextBox1.Text = price.ToString("#,##0.00");
            con.Close();

            con.Open();
            cmd = new MySqlCommand("Select Item from tblproduct where id like '" + id + "'", con);
            read = cmd.ExecuteReader();
            con.Close();

           
                   
    }
        public void Filter_click(object sender, EventArgs e)
        {
            if (Label4.Text == String.Empty)
            {
                MessageBox.Show("Click New Order First");
                return;
            }
            _filter = ((Button)sender).Text.ToString();
            Loadmenu();
        }

        private void Gettransno()
        {
            try
            {
                string sdate = DateTime.Now.ToString("yyyyMMdd");
                string transnum;
                int count;
                con.Open();
                cmd = new MySqlCommand("SELECT * FROM tblcart where transno like '" + sdate + "%' order by id desc", con);
                read = cmd.ExecuteReader();
                read.Read();
                if (read.HasRows)
                {   
                    transnum = read[0].ToString();
                    count = int.Parse(transnum);
                    Label4.Text = sdate + (count + 1);
                  
                }
                else
                {
                    transnum = sdate + "0001";
                    Label4.Text = transnum;
                }
                transno = Label4.Text;
                con.Close();
                read.Close();

            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message,"warning", MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }

            
        }
        private void Search()
        {
            MySqlDataAdapter read1;
            FlowLayoutPanel2.AutoScroll = true;
            FlowLayoutPanel2.Controls.Clear();
            con.Open();
            cmd = new MySqlCommand("Select image,id,item,price from tblproduct where Item like '" + TextBox3.Text + "%' order by Item", con);
            read = cmd.ExecuteReader();
            while (read.Read())
            {
                long len = read.GetBytes(0, 0, null, 0, 0);
                byte[] array = new byte[System.Convert.ToInt32(len) + 1];
                read.GetBytes(0, 0, array, 0, System.Convert.ToInt32(len));
                pic = new PictureBox();
                pic.Height = 150;
                pic.Width = 150;
                pic.BackgroundImageLayout = ImageLayout.Stretch;
                pic.Cursor = Cursors.Hand;
                MemoryStream ms = new MemoryStream(array);
                System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(ms);
                pic.BackgroundImage = bitmap;
                pic.Tag = read["id"].ToString();


                item = new Label();
                item.BackColor = Color.FromArgb(0, 151, 230);
                item.ForeColor = Color.White;
                item.Text = read["Item"].ToString();
                item.Dock = DockStyle.Bottom;
                item.TextAlign = ContentAlignment.MiddleCenter;
                item.Font = new Font("Calibri", 11, FontStyle.Regular);
                item.Cursor = Cursors.Hand;
                item.Tag = read["id"].ToString();

                price = new Label();
                price.BackColor = Color.FromArgb(230, 126, 34);
                price.ForeColor = Color.White;
                price.Text = read["price"].ToString();
                price.Dock = DockStyle.Top;
                price.TextAlign = ContentAlignment.MiddleCenter;
                price.Font = new Font("Calibri", 12, FontStyle.Regular);
                price.Cursor = Cursors.Hand;
                price.AutoSize = true;
                price.Tag = read["id"].ToString();


                pic.Controls.Add(item);
                pic.Controls.Add(price);
                FlowLayoutPanel2.Controls.Add(pic);
                pic.Click += new EventHandler(Select_click);
                item.Click += new EventHandler(Select_click);
                price.Click += new EventHandler(Select_click);
            }
            read.Close();
            con.Close();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            Gettransno();
            Button1.Enabled = true;
            Button2.Enabled = true;
            Button5.Enabled = true;
            Button6.Enabled = true;
            Label17.Visible = true;
            Label18.Visible = true;
            Label11.Text = ("");
            Label12.Text = ("");
            Label13.Text = ("");
            Label15.Text = ("");
            Label14.Text = ("");
            Label6.Text = ("Customer ID :");
            Label9.Text = ("Date : ");
            Label10.Text = (DateTime.Now.ToString("MM/dd/yyyy"));
            Label17.Text = ("Status");
            Label19.Text = ("Trans. No.");
            TextBox3.Enabled = true;
            Loadcart();
        }
       

        private void Button5_Click(object sender, EventArgs e)
        {
            Label18.Text = "Dine In";
            user = Label18.Text;
        }

        private void Button6_Click(object sender, EventArgs e)
        {
        Label18.Text = "Take Out";
          user = Label18.Text;

        }

        private void TextBox3_TextChanged(object sender, EventArgs e)
        {
            Search();
        }

        private void TextBox3_Click(object sender, EventArgs e)
        {
            while (Label4.Text == String.Empty){
            TextBox3.Enabled = false;
            MessageBox.Show("Click Order New First");
            return;
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Clear Order?", "Confirmation", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                con.Open();
                cmd = new MySqlCommand("delete from tblcart where transno like '" + Label4.Text + "'",con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Item Removed");
                con.Close();
                Loadcart();
            }
        }

        private void DataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colname = DataGridView2.Columns[e.ColumnIndex].Name;
            if (colname == "ColRemove")
            {
                con.Open();
                cmd = new MySqlCommand("delete from tblcart where id like '" + DataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString() + "'", con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Item has been removed");
                con.Close();
                Loadcart();
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (Label18.Text == String.Empty) {
                MessageBox.Show("Choose Status First");
                return;
            }
            else if (TextBox5.Text == String.Empty){ 
            MessageBox.Show("Order First");
                return;
            }
            Sales frmsales = new Sales();
            frmsales.Stud.Text = Label4.Text;
            frmsales.Label4.Text = Label6.Text;
            frmsales.ShowDialog();
            Gettransno();
            Loadcart();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            Login frmlogin = new Login();
            frmlogin.Show();
        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {
            Loadcategory();
        }

        private void PictureBox2_Click_1(object sender, EventArgs e)
        {
            Loadmenu();
        }
    }
    }

