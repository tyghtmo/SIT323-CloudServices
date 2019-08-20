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
        private TaskAllocations aTaskAllocation = new TaskAllocations();
        private Configuration aConfiguration = new Configuration();
        string fileValiditys = "";

        public SIT323Assignment1Form()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label1.Text = "";
        }

        private void OpenTANFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenTANFile();

        }

        private void OpenTANFile()
        {
            //Clear any residual errors
            CompleteErrorList.Clear();
            label1.Text = "";
            fileValiditys = "";

            DialogResult result;
            result = openFileDialog1.ShowDialog();
            if(result == DialogResult.OK)
            {
                //Process TAN file
                CompleteErrorList.Add("START PROCESSING TAN FILE: " + openFileDialog1.SafeFileName);
                TaskAllocations.TryParse(openFileDialog1.FileName, out aTaskAllocation);
                if (aTaskAllocation.AllocationErrorList != null) UpdateErrors(aTaskAllocation.AllocationErrorList);
                CompleteErrorList.Add("END PROCESSING TAN FILE: " + openFileDialog1.SafeFileName);

                if (aTaskAllocation.isValid) fileValiditys += "TAN file is valid\n\n";
                else fileValiditys += "TAN file is invalid\n\n";


                //TODO get rid of this test
                //aTaskAllocation.ConfigPath = "Test3.csv";


                //Process CONFIG file
                string configPath = "";
                if (aTaskAllocation.ConfigPath == null)
                {
                    configPath = "File Name Missing";
                    fileValiditys += "Configuration file is invalid\n\n";
                }
                else configPath = aTaskAllocation.ConfigPath;
                        
                CompleteErrorList.Add("START PROCESSING CONFIG FILE: " + configPath);
                if (aTaskAllocation.ConfigPath != null)
                {
                    //Get directory of TAN file and add config path
                    string directoryPath = Path.GetDirectoryName(openFileDialog1.FileName);
                    string configFullPath = directoryPath + "\\" + aTaskAllocation.ConfigPath;

                    Configuration.TryParse(configFullPath, out aConfiguration);
                    if (aConfiguration.ConfigurationErrorList != null) UpdateErrors(aConfiguration.ConfigurationErrorList);

                    if (aTaskAllocation.isValid) fileValiditys += "Configuration file is valid\n\n";
                    else fileValiditys += "Configuration file is invalid\n\n";
                }
                CompleteErrorList.Add("END PROCESSING CONFIG FILE: " + configPath);

                if(aTaskAllocation.isValid == true && aConfiguration.isValid == true)
                {
                    allocationsToolStripMenuItem.Enabled = true;
                }
                else
                {
                    allocationsToolStripMenuItem.Enabled = false;
                }

                //Update text
                label1.Text = "";
                label1.Text += fileValiditys;
                foreach (Allocation al in aTaskAllocation.SetOfAllocations)
                {
                    label1.Text += string.Format("Allocation ID: {0}\n", al.ID);
                    label1.Text += al.MatrixToString();
                }
            }
        }

        private void ErrorListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ErrorForm errorForm = new ErrorForm();
            errorForm.ShowDialog();
        }

        private void UpdateErrors(List<string> errors)
        {
            if (errors.Count > 0)
            {
                int errorCounter = 1;
                foreach (string str in errors)
                {
                    StringBuilder strBuild = new StringBuilder();
                    strBuild.Append("Error ");
                    strBuild.Append(errorCounter);
                    strBuild.Append(": ");
                    strBuild.Append(str);
                    CompleteErrorList.Add(strBuild.ToString());
                    errorCounter++;
                }
            }
        }


        private void allocationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            label1.Text = "";
            label1.Text += fileValiditys;
            //Calculate allocations
            CompleteErrorList.Add("START PROCESSING ALLOCATIONS:");
            foreach (Allocation al in aTaskAllocation.SetOfAllocations)
            {
                List<string> errors = new List<string>();

                //get time
                double time = al.CalculateTime(aConfiguration, out errors);
                string timeString = time.ToString("0.00");

                //get energy
                double energy = al.CalculateEnergy(aConfiguration, out errors);
                string energyString = energy.ToString("0.00");

                //update text
                UpdateErrors(errors);
                label1.Text += string.Format("Allocation ID: {0}, Time: {1:0.00}, Energy: {2:0.00}\n", al.ID, timeString, energyString);
                label1.Text += al.MatrixToString();
            }
            CompleteErrorList.Add("END PROCESSING ALLOCATIONS: ");
        }
    }
}
