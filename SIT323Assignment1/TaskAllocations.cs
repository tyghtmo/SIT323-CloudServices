using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIT323Assignment1
{
    public class TaskAllocations
    {

        public string ConfigPath { get; set; }
        public int Tasks { get; set; }
        public int Processors { get; set; }
        public int NumAllocations { get; set; }
        public List<Allocation> SetOfAllocations { get; set; }

        public TaskAllocations(string filePath)
        {
            ConfigPath = filePath;
        }

        public static int ToInt32(string input)
        {
            if (Int32.TryParse(input, out int anInt))
            {
                return anInt;
            }
            else
            {
                return -1;
            }

        }

        public static Boolean TryParse(string path, out TaskAllocations anAllocation)
        {
            anAllocation = new TaskAllocations(path);
            List<String> Errors = new List<string>();
            anAllocation.SetOfAllocations = new List<Allocation>();

            //Begin parsing TAN file
            StreamReader file = new StreamReader(path);
            while (!file.EndOfStream)
            {
                string line = file.ReadLine();

                if(line.Length == 0) { }
                else if (line.Contains("//"))
                {
                    if (line.StartsWith("//"))
                    {
                        //Comment Found
                    }
                    else
                    {
                        string error = "Invalid line in file" + line;
                        Errors.Add(error);
                    }
                }
                else if (line.Contains("CONFIG-FILE"))
                {
                    string[] substrings = line.Split(',');
                    anAllocation.ConfigPath = substrings[1].Replace("\"","");
                }
                else if (line.Contains("TASKS"))
                {
                    string[] substrings = line.Split(',');
                    int input = ToInt32(substrings[1]);
                    if(input == -1)
                    {
                        string error = "String to Int error: " + line;
                        Errors.Add(error);
                    }
                    else
                    {
                        anAllocation.Tasks = input;
                    }

                }
                else if (line.Contains("PROCESSORS"))
                {
                    string[] substrings = line.Split(',');
                    int input = ToInt32(substrings[1]);
                    if (input == -1)
                    {
                        string error = "String to Int error: " + line;
                        Errors.Add(error);
                    }
                    else
                    {
                        anAllocation.Processors = input;
                    }
                }
                else if (line.Contains("ALLOCATIONS"))
                {
                    string[] substrings = line.Split(',');
                    int input = ToInt32(substrings[1]);
                    if (input == -1)
                    {
                        string error = "String to Int error: " + line;
                        Errors.Add(error);
                    }
                    else
                    {
                        anAllocation.NumAllocations = input;
                    }
                }
                else if (line.Contains("ALLOCATION-ID"))
                {
                    string[] substrings = line.Split(',');
                    int id = ToInt32(substrings[1]);
                    if (id == -1)
                    {
                        string error = "String to Int error: " + line;
                        Errors.Add(error);
                    }
                    
                    List<String> allocationMatrix = new List<string>();

                    while (line.Length != 0)
                    {
                        line = file.ReadLine();
                        if (line != "" && line != null)
                        {
                            allocationMatrix.Add(line);
                        }
                        else break;
                    }

                    Allocation aAllocation = new Allocation(id, allocationMatrix);
                    anAllocation.SetOfAllocations.Add(aAllocation);

                }
                else
                {
                    string error = "Invalid line in file: " + line;
                    Errors.Add(error);
                }
            }
            file.Close();

            //Test that number of allocations is correct
            int noAllocations = anAllocation.SetOfAllocations.Count;
            int expectedAllocations = anAllocation.NumAllocations;
            if (noAllocations != expectedAllocations)
            {
                string error = "Different number of Allocations than expected. " + noAllocations + " of " + expectedAllocations + " expected";
                Errors.Add(error);
            }

            //Test that allocations are valid
            foreach(Allocation a in anAllocation.SetOfAllocations)
            {
                if (!Allocation.ValidateAllocation(a))
                {
                    string error = "Invalid Allocation\n";
                    error = error + a.ToString();
                    Errors.Add(error);
                }
            }

            return Errors.Count == 0;
        }

    }
}
