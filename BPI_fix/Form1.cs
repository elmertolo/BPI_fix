using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BPI_fix
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public string activeB = "";
        private void btnBPI_Click(object sender, EventArgs e)
        {
            activeB = "BPI";
            BPI bpi = new BPI();
            bpi.Show();

            this.Hide();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnFBPI_Click(object sender, EventArgs e)
        {
            activeB = "FBPI";
            FBPI fbpi = new FBPI();
            fbpi.Show();
            this.Hide();
        }
    }
}
