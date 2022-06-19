using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HimayKamay
{
    public partial class Windows : Form
    {
        public Windows()
        {
            InitializeComponent();

            string sdate = DateTime.Now.ToString("dd/MM/yyyy");
            string stime = DateTime.Now.ToString("hh:mm tt");
            Label3.Text = sdate;
            Label4.Text = stime;
        }

        public void Button3_Click(object sender, EventArgs e)
        {
            Product frmproduct = new Product();
            switchpanel(frmproduct);
            Label2.Text = ("I T E M  L I S T");
        }
        private void switchpanel(Form panel)
        {
            Panel4.Controls.Clear();
            panel.TopLevel = false;
            Panel4.Controls.Add(panel);
            panel.Show();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            Form1 frm1 = new Form1();
            DialogResult dialogResult = MessageBox.Show("Do you want to Log-out?", "Confirmation", MessageBoxButtons.YesNo);
            if(dialogResult == DialogResult.Yes)
            {
                frm1.Loadcategory();
                this.Close();
           }
        }

        private void ToolStripButton1_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Do you want to Log-out?", "Confirmation", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Transaction frmtransac = new Transaction();
            switchpanel(frmtransac);
            Label2.Text = ("T R A N S A C T I O N");
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            Unused frmunused = new Unused();
            switchpanel(frmunused);
            Label2.Text = ("U N U S E D  I T E M");
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            Aboutus frmabtus = new Aboutus();
            switchpanel(frmabtus);
            Label2.Text = ("A B O U T  U S");
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Total frmtotal = new Total();
            switchpanel(frmtotal);
            Label2.Text = ("I N C O M E");
        }
    }
}
