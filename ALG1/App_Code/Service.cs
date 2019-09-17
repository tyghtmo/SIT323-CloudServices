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
[DataContract]
public class Service : IService
{
    [DataMember]
    public ConfigurationData configurationData;

    private int AllocationIDCounter = 1;

	public List<string> GetAllocations(ConfigurationData configData)
	{
        List<string> allocationList = new List<string>();
        configurationData = configData;
        double[,] taskProTimes = GetTaskProcessorTimes();
        allocationList = RandomAllocation(taskProTimes);

        return allocationList;
	}

    private List<string> RandomAllocation(double[,] taskProcessorTimes)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        int processors = taskProcessorTimes.GetLength(0);
        int tasks = taskProcessorTimes.GetLength(1);

        double[,] allocation = new double[processors, tasks];
        List<string> goodAllocations = new List<string>();

        double time = 0;
        double energy = 0;

        Random randNo = new Random();
        double currentBest = double.MaxValue;

        while (stopwatch.ElapsedMilliseconds <= 30000)
        {
            
            allocation = new double[processors, tasks];

            for (int i = 0; i < tasks; i++)
            {
                int selectedProcessor = randNo.Next(0, 3);
                allocation[selectedProcessor, i] = taskProcessorTimes[selectedProcessor, i];

            }

            time = CalculateTime(allocation);
            energy = CalculateEnergy(allocation);

            if(time < currentBest)
            {
                goodAllocations.Clear();
                goodAllocations.Add(MatrixToString(allocation));
                currentBest = time;
            }
            else if(time == currentBest)
            {
                goodAllocations.Add(MatrixToString(allocation));
            }

        }

        //Remove duplicates and convert back to list
        ICollection<string> noDuplicates = new HashSet<string>(goodAllocations);
        List<string> allocations = noDuplicates.ToList<string>();

        //Convert task / processor time matrix to 1 0 matrix.
        for(int i = 0; i < allocations.Count; i++)
        {
            allocations[i] = ConvertTo10Matrix(allocations[i], time, energy, processors, tasks);
        }

        return allocations;
    }

    private double CalculateEnergy(double[,] allocation)
    {
        return 1;
    }

    private string ConvertTo10Matrix(string taskProcessorMatrix, double time, double energy, int processors, int tasks)
    {
        string matrixStr = string.Format("Allocation ID = {0}, Time = {1:0.00}, Energy = {2:0.00}\n", AllocationIDCounter++, time, energy);
        List<string> processorList = taskProcessorMatrix.Split('\n').ToList<string>();
        int[,] completeMatrix = new int[processors, tasks];

        for(int i = 0; i < processorList.Count; i++)
        {
            List<string> taskList = processorList[i].Split(',').ToList<string>();

            for(int j = 0; j < taskList.Count; j++)
            {
                if (taskList[j] != "")
                {
                    if(double.Parse(taskList[j]) == 0)
                    {
                        completeMatrix[i, j] = 0;
                    }
                    else
                    {
                        completeMatrix[i, j] = 1;
                    }
                }
            }

        }

        matrixStr += MatrixToString(completeMatrix);

        return matrixStr;
    }

    private double CalculateTime(double[,] allocation)
    {
        double time = 0;

        for(int processor = 0; processor < allocation.GetLength(0); processor++)
        {
            double rowSum = 0;
            for(int task = 0; task < allocation.GetLength(1); task++)
            {
                rowSum += allocation[processor, task];
            }
            if(rowSum > time)
            {
                time = rowSum;
            }
        }

        return time;
    }

    private double[,] GetTaskProcessorTimes()
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

    private string MatrixToString(double[,] matrix)
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

    private string MatrixToString(int[,] matrix)
    {
        string str = "";

        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                str += String.Format("{0}, ", matrix[i, j]);
            }

            //Remove last , 
            str = str.Substring(0, str.Length - 1);
            str += "\n";
        }
        str += "\n";

        return str;
    }
}
