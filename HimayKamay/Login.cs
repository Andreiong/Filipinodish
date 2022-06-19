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
    public partial class Login : Form
    {
        MySqlConnection con;
        MySqlCommand cmd;
        MySqlDataReader read;

        int i;
        public Login()
        {
            InitializeComponent();
            con = new MySqlConnection();
            con.ConnectionString = "server=localhost;user id=root;password=;database=filipinodish";
            this.KeyPreview = true;
            TextBox1.Focus();
        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            int count;
            try
            {
                con.Open();
                cmd = new MySqlCommand("Select * from tblaccount where Username = '" + TextBox1.Text + "' And Password = '" + TextBox2.Text + "' ", con);
                count = Convert.ToInt32(cmd.ExecuteScalar());
                if (count > 0)
                {
                    Windows frmwindows = new Windows();
                    frmwindows.Show();
                    this.Hide();
                }
                else if (i == 2)
                {
                    MessageBox.Show("Sorry you're out of tries");
                    this.Close();
                }
                else
                {
                    i = i + 1;
                    MessageBox.Show("Wrong Username or Password!");
                }
                con.Close();
                TextBox1.Clear();
                TextBox2.Clear();
            }
            catch (Exception ex)
            { 
            }
        }

        private void Button1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) {
                this.Close();
            }
            
        else if (e.KeyCode == Keys.Enter)
            {
                if (TextBox1.Text == String.Empty)
                {
                    Label5.Text = ("Username is empty");
                        }
               
              else if (TextBox2.Text == String.Empty)
                {
                    Label6.Text = ("Password is empty");
                }
                int count;
                try
                {
                    con.Open();
                    cmd = new MySqlCommand("Select * from tblaccount where Username = '" + TextBox1.Text + "' And Password = '" + TextBox2.Text + "' ", con);
                    count = Convert.ToInt32(cmd.ExecuteScalar());
                    if (count > 0)
                    {
                        Windows frmwindows = new Windows();
                        frmwindows.Show();
                        this.Hide();
                    }
                    else if (i == 2)
                    {
                        MessageBox.Show("Sorry you're out of tries");
                        this.Close();
                    }
                    else
                    {
                        i = i + 1;
                        MessageBox.Show("Wrong Username or Password!");
                    }
                    con.Close();
                    TextBox1.Clear();
                    TextBox2.Clear();
                    Label5.Text = ("");
                    Label6.Text = ("");
                }
                catch(Exception ex)
                {
                }
                
            }
           
        }

        private void Label4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
