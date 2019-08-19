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
    public partial class ErrorForm : Form
    {
        public ErrorForm()
        {
            InitializeComponent();
        }

        private void ErrorForm_Load(object sender, EventArgs e)
        {
            List<string> errors = SIT323Assignment1Form.CompleteErrorList;

            StringBuilder strBuild = new StringBuilder();
            int errorCounter = 1;
            foreach(string str in errors)
            {
                strBuild.Append("Error ");
                strBuild.Append(errorCounter);
                strBuild.Append(": ");
                strBuild.Append(str);
                strBuild.Append("\n");

                errorCounter++;
            }
            string errorText = strBuild.ToString();
            label1.Text = errorText;
        }

    }
}
