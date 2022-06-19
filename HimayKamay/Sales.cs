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
    public partial class Sales : Form
    {
        MySqlCommand cmd;
        MySqlConnection con;

       public static string stud;
        string pid;
        double total;
        double change;
        public Sales()
        {
            InitializeComponent();
            con = new MySqlConnection();
            con.ConnectionString = "server=localhost;user id=root;password=;database=filipinodish";
            this.KeyPreview = true;
            Button2.Enabled = false;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                double total = double.Parse(Label4.Text);
                double change = double.Parse(TextBox2.Text) - total;
                if (change < 0)
                {
                    MessageBox.Show("Insuffiecient Money");
                    return;

                }
                else
                {
                    Payment();

                }
            }
            catch(Exception ex)
            { 
            }
        }
        private void Sales_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
            else if(e.KeyCode == Keys.Enter)
            {
                Button1_Click(sender, e);

            }
          
        }
      
        private void Payment()
        {
            string stime = DateTime.Now.ToString("hh:mm tt");
            string sdate = DateTime.Now.ToString("yyyy-MM-dd");
            con.Open();
            cmd = new MySqlCommand("Insert into tblsales(transno,prodid,Price,cash,changee,year,time,User)values(@transno,@prodid,@Price,@cash,@changee,@year,@time,@user)", con);
            cmd.Parameters.AddWithValue("@transno", Form1.transno);
            cmd.Parameters.AddWithValue("@prodid", Form1.pid);
              cmd.Parameters.AddWithValue("@Price", double.Parse(Label4.Text)); 
             cmd.Parameters.AddWithValue("@cash", double.Parse(TextBox2.Text));
               cmd.Parameters.AddWithValue("@changee", double.Parse(Label5.Text));
               cmd.Parameters.AddWithValue("@year", sdate);
              cmd.Parameters.AddWithValue("@time", stime);
              cmd.Parameters.AddWithValue("@user", Form1.user);
              cmd.ExecuteNonQuery();

            MessageBox.Show("Payment Accepted");
            con.Close();
            Button1.Enabled = false;
            Button2.Enabled = true;
            TextBox2.Text = ("");


        }
        public void Addtocart(string id)
        {
            this.pid = id;
        }


        private void TextBox2_TextChanged(object sender, EventArgs e)
        {

            try
            {
                double total = double.Parse(Label4.Text);
                double cash = double.Parse(TextBox2.Text);

                double change = cash - total;

                if (change < 0)
                {
                    Label5.Text = ("0.00");
                    return;
                }
                else
                {
                    Label5.Text = change.ToString("#,##0.00");
                }
            }
            catch(Exception ex)
            {
                Label5.Text = change.ToString("#,##0.00");
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Receipt frmreceipt = new Receipt();
            frmreceipt.receipt();
            frmreceipt.ShowDialog();
            
        }

        private void Sales_Load(object sender, EventArgs e)
        {
            stud = Stud.Text;
        }

        private void TextBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }
    }
}
