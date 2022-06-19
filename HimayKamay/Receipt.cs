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
using Microsoft.Reporting.WinForms;
namespace HimayKamay
{
    public partial class Receipt : Form
    {
        MySqlConnection con;
        MySqlCommand cmd;
        MySqlCommand cmd1;
        MySqlDataReader read;
        public Receipt()
        {
            InitializeComponent();
            con = new MySqlConnection();
            con.ConnectionString = "server=localhost;user id=root;password=;database=filipinodish";
        }

        private void Receipt_Load(object sender, EventArgs e)
        {

            this.reportViewer1.RefreshReport();
        }
        public void receipt()
        {
            ReportDataSource print;
            try
            {
                this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + @"\Report\Report1.rdlc";
                this.reportViewer1.LocalReport.DataSources.Clear();
                DataSet1 ds = new DataSet1();
                MySqlDataAdapter da = new MySqlDataAdapter();
                con.Open();
                da.SelectCommand = new MySqlCommand("SELECT p.Item,p.productid,p.price,p.quantity,p.total,c.changee,c.cash,c.transno,c.year,c.time from tblcart as  p INNER JOIN tblsales as c on c.transno = p.transno where p.transno like '" + Sales.stud + "' ", con);
                da.Fill(ds.Tables["dtcart"]);
                con.Close();

                print = new ReportDataSource("DataSet1", ds.Tables["dtcart"]);
                reportViewer1.LocalReport.DataSources.Add(print);
                reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
            }
            catch(Exception ex)
            {
                con.Close();
            }
        }
    }
}
