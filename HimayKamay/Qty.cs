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
namespace HimayKamay
{
    public partial class Qty : Form
    {
        MySqlCommand cmd;
        MySqlConnection con;
        MySqlDataReader read;

        string id;
        double price;
        int i;
        public Qty()
        {
            InitializeComponent();
            con = new MySqlConnection();
            con.ConnectionString = "server=localhost;user id=root;password=;database=filipinodish";
            this.KeyPreview = true;
        }

  
        private void Qty_KeyDown(object sender, KeyEventArgs e)
        {
            Form1 frm1 = new Form1();
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
            else if (e.KeyCode == Keys.Enter)
            {

                string sdate = DateTime.Now.ToString("yyyy-MM-dd");

                con.Open();
                cmd = new MySqlCommand("insert into tblcart (transno,Item,productid,price,productdate,quantity)values(@transno,@Item,@productid,@price,@productdate,@quantity)", con);
                cmd.Parameters.AddWithValue("@transno", Form1.transno);
                cmd.Parameters.AddWithValue("@Item", Form1.Food);
                cmd.Parameters.AddWithValue("@productid", id);
                cmd.Parameters.AddWithValue("@price", price);
                cmd.Parameters.AddWithValue("@productdate", sdate);
                cmd.Parameters.AddWithValue("@quantity", double.Parse(TextBox1.Text));
                cmd.ExecuteNonQuery();
                con.Close();


                con.Open();
                cmd = new MySqlCommand("Update tblcart set total = price * quantity", con);
                cmd.Parameters.AddWithValue("@transno", frm1.Label4.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                this.Close();
            }
        }
        public void Addtocart(string id, double price)
        {
            this.id = id;
            this.price = price;
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            Form1 frm1 = new Form1();
            string sdate = DateTime.Now.ToString("yyyy-MM-dd");

            con.Open();
            cmd = new MySqlCommand("insert into tblcart (transno,Item,productid,price,productdate,quantity)values(@transno,@Item,@productid,@price,@productdate,@quantity)", con);
            cmd.Parameters.AddWithValue("@transno", Form1.transno);
            cmd.Parameters.AddWithValue("@Item", Form1.Food);
            cmd.Parameters.AddWithValue("@productid", id);
            cmd.Parameters.AddWithValue("@price", price);
            cmd.Parameters.AddWithValue("@productdate", sdate);
            cmd.Parameters.AddWithValue("@quantity", double.Parse(TextBox1.Text));
            cmd.ExecuteNonQuery();
            con.Close();


            con.Open();
            cmd = new MySqlCommand("Update tblcart set total = price * quantity", con);
            cmd.Parameters.AddWithValue("@transno", frm1.Label4.Text);
            cmd.ExecuteNonQuery();
            con.Close();
            this.Close();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }
    }
}
