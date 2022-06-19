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
    public partial class Category : Form
    {
        MySqlCommand cmd;
        MySqlConnection con;
        MySqlDataReader read;
        public Category()
        {
            InitializeComponent();
            con = new MySqlConnection();
            con.ConnectionString = "server=localhost;user id=root;password=;database=filipinodish";
        }

        private void Button3_Click(object sender, EventArgs e)
        {
          
                DialogResult dialogResult = MessageBox.Show("Save this Item?", "Save item", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if(dialogResult== DialogResult.Yes)
                {
                    con.Open();
                     cmd = new MySqlCommand("INSERT INTO tblcategory (category)values(@category)", con);
                    cmd.Parameters.AddWithValue("@category", txtCategory.Text);
                    cmd.ExecuteNonQuery();
                    con.Close();
                   MessageBox.Show("Category Saved");
                Productlist frmprodlst = new Productlist();
                frmprodlst.loadcategory();
                txtCategory.Clear();
                    txtCategory.Focus();
                    
                }

        
        }

        private void ToolStripButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
