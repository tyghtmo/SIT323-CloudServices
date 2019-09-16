using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using ConfigurationDataLibrary;

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
            //TODO: remove
            Configuration aConf = new Configuration();
            Configuration.TryParse(@"C: \Users\Tyson\source\repos\SIT323Assignment1\Files for Unit Testing\Test1.csv", out aConf);
            ConfigurationData ConfData = new ConfigurationData();
            ConfData.LogFilePath = "1000";

            LocalConfigurationWebService.ServiceClient localWS = new LocalConfigurationWebService.ServiceClient();
            localWS.GetAllocations(ConfData);
            string al = localWS.GetAllocations(ConfData);

            //Console.WriteLine(al);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new SIT323Assignment1Form());
        }
    }
}
