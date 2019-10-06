using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SIT323Assignment1
{
    public partial class EndpointForm : Form
    {
        public EndpointForm()
        {
            InitializeComponent();
        }

        private void EndpointForm_Load(object sender, EventArgs e)
        {
            label1.Text = "";

            foreach(string ip in SIT323Assignment1Form.endpointAdresses)
            {
                label1.Text += ip + "\n";
            }
        }
    }
}
