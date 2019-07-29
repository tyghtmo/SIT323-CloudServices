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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void openTANFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openTANFile();
        }

        private void openTANFile()
        {
            DialogResult result;

            result = openFileDialog1.ShowDialog();
            if(result == DialogResult.OK)
            {
                //TODO .tan opening
                TaskAllocations.TryParse(openFileDialog1.FileName, out TaskAllocations aTaskAllocation);
            }
        }
    }
}
