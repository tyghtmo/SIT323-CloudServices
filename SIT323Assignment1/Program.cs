using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SIT323Assignment1
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            
            string filePath = "C:\\Users\\Tyson\\source\\repos\\SIT323Assignment1\\Test3.tan";
            TaskAllocations test = new TaskAllocations(filePath);
            Boolean valid = test.Validate();
            

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new SIT323Assignment1Form());
        }
    }
}
