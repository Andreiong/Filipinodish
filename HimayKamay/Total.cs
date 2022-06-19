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
    public partial class Total : Form
    {
        MySqlConnection con;
        MySqlCommand cmd;
        MySqlDataReader read;
        int i;
        public Total()
        {
            InitializeComponent();
            con = new MySqlConnection();
            con.ConnectionString = "server=localhost;user id=root;password=;database=filipinodish";
        }
        private void Income() {
            DataGridView2.Rows.Clear();

            con.Open();
            cmd = new MySqlCommand("select c.prodid, c.transno, c.price, c.cash, p.category, c.user from tblsales as c inner join tblproduct as p on p.ID = c.prodid order by transno desc", con);
        read = cmd.ExecuteReader();
        while(read.Read()){
                i = i + 1;
                DataGridView2.Rows.Add(i, read["prodid"].ToString(), read["transno"].ToString(), read["price"].ToString(), read["cash"].ToString(), read["category"].ToString(), read["user"].ToString());
            }
         for (int j = 0; j <= DataGridView2.Rows.Count - 1; j++) { 
                DataGridViewRow r = DataGridView2.Rows[j];
                 r.Height = 100;   
            }
              
                read.Close();
                con.Close();
        }
        private void sum() 
        {
            try
            {
                double sum;
                string total;
                string sdate = DateTime.Now.ToString("yyyy-MM-dd");
                con.Open();
                cmd = new MySqlCommand("select ifnull(sum(total),0) from tblcart where productdate between quantity and productdate", con);
                total = cmd.ExecuteScalar().ToString();
                sum = double.Parse(total);
                Label4.Text = sum.ToString("₱ #,##0.00");
                con.Close();
                    }
            catch (Exception ex)
            {
                con.Close();
            }
        }
        private void count()
        {
            try
            {
                int count;
                con.Open();
                cmd = new MySqlCommand("Select ifnull(count(ID),0) From tblproduct", con);
                count = Convert.ToInt32(cmd.ExecuteScalar());
                Label5.Text = count.ToString();
                con.Close();
            }
            catch (Exception ex) {
                con.Close();
            }
        }
        private void Label4_Click(object sender, EventArgs e)
        {

        }
        private void Total_Load(object sender, EventArgs e)
        {
            count();
            sum();
            Income();
        }

        private void Total_Activated(object sender, EventArgs e)
        {
            count();
            sum();
            Income();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

            if (textBox1.Text == string.Empty)
            {
                Income();
            }
            else {
                search();
            }
        }
        public void search()
        {
            DataGridView2.Rows.Clear();

            con.Open();
            cmd = new MySqlCommand("select c.prodid, c.transno, c.price, c.cash, p.category, c.user from tblsales as c inner join tblproduct as p on p.ID = c.prodid where transno like '"+ textBox1.Text + "'", con);
            read = cmd.ExecuteReader();
            while (read.Read())
            {
                i = i + 1;
                DataGridView2.Rows.Add(i, read["prodid"].ToString(), read["transno"].ToString(), read["price"].ToString(), read["cash"].ToString(), read["category"].ToString(), read["user"].ToString());
            }
            for (int j = 0; j <= DataGridView2.Rows.Count - 1; j++)
            {
                DataGridViewRow r = DataGridView2.Rows[j];
                r.Height = 100;
            }

            read.Close();
            con.Close();
        }
    }
}
