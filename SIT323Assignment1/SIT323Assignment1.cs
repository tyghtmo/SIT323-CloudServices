using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace SIT323Assignment1
{
    public partial class SIT323Assignment1Form : Form
    {

        private ErrorForm errorForm { get; set; }
        public static List<string> CompleteErrorList = new List<string>();

        public SIT323Assignment1Form()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void OpenTANFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenTANFile();

        }

        private void OpenTANFile()
        {
            //Clear any residual errors
            CompleteErrorList.Clear();

            DialogResult result;
            result = openFileDialog1.ShowDialog();
            if(result == DialogResult.OK)
            {
                //TODO .tan and .csv initialisation and parsing
                TaskAllocations.TryParse(openFileDialog1.FileName, out TaskAllocations aTaskAllocation);
                if (aTaskAllocation.AllocationErrorList != null) CompleteErrorList.AddRange(aTaskAllocation.AllocationErrorList);

                //TODO get rid of this test
                //aTaskAllocation.ConfigPath = "Test3.csv";

                if (aTaskAllocation.ConfigPath != null)
                {
                    string directoryPath = Path.GetDirectoryName(openFileDialog1.FileName);
                    string configFullPath = directoryPath + "\\" + aTaskAllocation.ConfigPath;

                    //Configuration aConfiguration = new Configuration(configFullPath);
                    //aConfiguration.Validate();
                    Configuration.TryParse(configFullPath, out Configuration aConfiguration);
                    if (aConfiguration.ConfigurationErrorList != null) CompleteErrorList.AddRange(aConfiguration.ConfigurationErrorList);
                }
            }
        }

        private void errorListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ErrorForm errorForm = new ErrorForm();
            errorForm.ShowDialog();
        }
    }
}
