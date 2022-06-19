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
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();
            this.KeyPreview = true;
        }

        private void About_Load(object sender, EventArgs e)
        {

        }

        private void About_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();

            }
        }
    }
}
