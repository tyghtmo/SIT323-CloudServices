using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using ConfigurationDataLibrary;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service" in code, svc and config file together.
public class Service : IService
{
    public ConfigurationData configurationData;
    private int processors;
    private int tasks;

    public string GetAllocations(ConfigurationData configDate)
    {
        configurationData = configDate;
        processors = configDate.ProgramProcessors;
        tasks = configDate.ProgramTasks;

        double[,] taskProTimes = GetTaskProcessorTimes();

        List<string> allocations = HeuristicAlgorithm(taskProTimes);

        double[,] exampleAllocation = new double[6, 3] { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } };
        return string.Format("Allocations,{0}\n\n{1}", allocations.Count, AllocationToString(exampleAllocation, 1));
    }

    private List<string> HeuristicAlgorithm(double[,] taskProTimes)
    {
        List<string> bestAllocations = new List<string>();
        double bestEnergy = Double.MaxValue;
        double bestTime = double.MaxValue;
        double maxAllocationTime = configurationData.ProgramMaxDuration;

        //Algorithm 2 
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        while (stopwatch.ElapsedMilliseconds <= configurationData.AlgorithmMaxRuntime)
        {

            //


            //Compare to List

            //Calculate allocation
            double time = 0;
            double energy = 0;


            //Exit conditions
            if (energy < bestEnergy && time < configurationData.ProgramMaxDuration)
            {
                bestTime = time;
                bestEnergy = energy;
            }
            else if (energy == bestEnergy && time < configurationData.ProgramMaxDuration)
            {

            }
        }

        return bestAllocations;
    }

    private double[,] GetTaskProcessorTimes()
    {
        int rows = configurationData.ProgramProcessors;
        int columns = configurationData.ProgramTasks;
        double[,] taskProcessorTimes = new double[rows, columns];
        if (configurationData.TaskRuntimes != null)
        {
            for (int processors = 0; processors < rows; processors++)
            {
                for (int tasks = 0; tasks < columns; tasks++)
                {
                    double time = configurationData.TaskRuntimes[tasks + 1] * (configurationData.RuntimeReferenceFrequency / configurationData.ProcessorFrequencies[processors + 1]);
                    taskProcessorTimes[processors, tasks] = time;
                }
            }
        }

        return taskProcessorTimes;
    }

    private string AllocationToString(double[,] allocation, int id)
    {
        string str = "ALLOCATION-ID," + id + "\n\n";

        for (int i = 0; i < allocation.GetLength(0); i++)
        {
            for (int j = 0; j < allocation.GetLength(1); j++)
            {
                str = str + allocation[i, j] + ",";
            }

            //Remove last , 
            str = str.Substring(0, str.Length - 1);
            str = str + "\n";
        }

        //Remove last \n 
        //str = str.Substring(0, str.Length - 1);

        return str;
    }
}
