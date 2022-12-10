using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chatter.Client
{
    public partial class Connect_Form : Form
    {
        public bool failed = true;
        public Connect_Form()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Program._Client.Connect(TBAddress.Text, int.Parse(TBPort.Text));
                failed = false;
                this.Close();
            }
            catch
            {
                MessageBox.Show("Incorrect Address or Port");
            }
        }
    }
}
