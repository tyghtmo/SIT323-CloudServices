using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using ConfigurationDataLibrary;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service" in code, svc and config file together.
[DataContract]
public class Service : IService
{
    [DataMember]
    public ConfigurationData configurationData;

	public string GetAllocations(ConfigurationData configData)
	{
        configurationData = configData;
        double[,] taskProTimes = getTaskProcessorTimes();


        return string.Format(matrixToString(taskProTimes));
	}

    private double[,] getTaskProcessorTimes()
    {
        int rows = configurationData.ProgramProcessors;
        int columns = configurationData.ProgramTasks;
        double[,] taskProcessorTimes = new double[rows,columns];
        if ( configurationData.TaskRuntimes != null)
        {
            for (int processors = 0; processors < rows; processors++)
            {
                for(int tasks = 0; tasks < columns; tasks++)
                {
                    double time = configurationData.TaskRuntimes[tasks + 1] * (configurationData.RuntimeReferenceFrequency / configurationData.ProcessorFrequencies[processors + 1]);
                    taskProcessorTimes[processors, tasks] = time;
                }
            }
        }

        return taskProcessorTimes;
    }

    private string matrixToString(double[,] matrix)
    {
        string str = "";

        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                str += String.Format("{0:0.00}, ", matrix[i, j]);
            }

            //Remove last , 
            str = str.Substring(0, str.Length - 1);
            str += "\n";
        }
        str += "\n";

        return str;
    }
}
