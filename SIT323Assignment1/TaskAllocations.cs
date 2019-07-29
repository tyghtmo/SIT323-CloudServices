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
        #region Constants
        private const char delimiter = ',';
        private const string comment = "//";
        private const string doubleQuote = "\"";
        private const string emptySpace = "";

        //Errors
        private const string invalidLineError = "Invalid line in file: ";
        private const string stringToIntError = "String to Int error: ";
        private const string invalidAllocationError = "Invalid Allocation: ";
        private const string diffNoAllocationToExpectedError = "Different number of Allocations than expected. {0} of {1} expected";
        #endregion

        #region Properties
        //Keys
        private readonly string configPathKey = "CONFIG-FILE";
        private readonly string tasksKey = "TASKS";
        private readonly string processorsKey = "PROCESSORS";
        private readonly string allocationsKey = "ALLOCATIONS";
        private readonly string allocationIDKey = "ALLOCATION-ID";

        //Values
        public string ConfigPath { get; set; }
        public int Tasks { get; set; }
        public int Processors { get; set; }
        public int NumAllocations { get; set; }
        public List<Allocation> SetOfAllocations { get; set; }
        #endregion

        #region Constructers
        public TaskAllocations(string filePath)
        {
            ConfigPath = filePath;
        }
        #endregion

        #region Methods
        //Helper method to convert strings to Int32
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

        /// <summary>
        /// Determines if a .tan file is valid and parses the data to an instance of TaskAllocation
        /// </summary>
        /// 
        /// <param name="path">
        /// A string containg the filepath of a .tan file
        /// </param>
        /// 
        /// <param name="anAllocation">
        /// An instance of TaskAllocation that is populated with data from the .tan file being parsed
        /// </param>
        /// 
        /// <returns>
        /// A boolean depending on if the file is valid and error free.
        /// </returns>
        public static Boolean TryParse(string path, out TaskAllocations anAllocation)
        {
            anAllocation = new TaskAllocations(path);
            List<String> ErrorList = new List<string>();
            anAllocation.SetOfAllocations = new List<Allocation>();

            //Begin parsing TAN file
            StreamReader file = new StreamReader(path);
            while (!file.EndOfStream)
            {
                string line = file.ReadLine();

                if(line.Length == 0) { }
                else if (line.Contains(comment))
                {
                    if (line.StartsWith(comment))
                    {
                        //Comment Found
                    }
                    else
                    {
                        string error = invalidLineError + line;
                        ErrorList.Add(error);
                    }
                }
                else if (line.Contains(anAllocation.configPathKey))
                {
                    string[] substrings = line.Split(delimiter);

                    //Check Line is valid and only contains expected number of arguements
                    if(substrings[0] == anAllocation.configPathKey && substrings.Count() == 2)
                    {
                        string configPath = substrings[1].Replace(doubleQuote, emptySpace);
                        anAllocation.ConfigPath = configPath;
                    }
                    else
                    {
                        //TODO errors
                    }
                }
                else if (line.Contains(anAllocation.tasksKey))
                {
                    string[] substrings = line.Split(delimiter);
                    int input = ToInt32(substrings[1]);

                    if(input == -1)
                    {
                        string error = stringToIntError + line;
                        ErrorList.Add(error);
                    }
                    else
                    {
                        anAllocation.Tasks = input;
                    }

                }
                else if (line.Contains(anAllocation.processorsKey))
                {
                    string[] substrings = line.Split(delimiter);

                    int input = ToInt32(substrings[1]);
                    if (input == -1)
                    {
                        string error = stringToIntError + line;
                        ErrorList.Add(error);
                    }
                    else
                    {
                        anAllocation.Processors = input;
                    }
                }
                else if (line.Contains(anAllocation.allocationsKey))
                {
                    string[] substrings = line.Split(delimiter);
                    int input = ToInt32(substrings[1]);
                    if (input == -1)
                    {
                        string error = stringToIntError + line;
                        ErrorList.Add(error);
                    }
                    else
                    {
                        anAllocation.NumAllocations = input;
                    }
                }
                else if (line.Contains(anAllocation.allocationIDKey))
                {
                    string[] substrings = line.Split(delimiter);
                    int id = ToInt32(substrings[1]);
                    if (id == -1)
                    {
                        string error = stringToIntError + line;
                        ErrorList.Add(error);
                    }
                    
                    List<String> allocationMatrix = new List<string>();

                    while (line.Length != 0)
                    {
                        line = file.ReadLine();

                        if (line != emptySpace && line != null)
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
                    string error = invalidLineError + line;
                    ErrorList.Add(error);
                }
            }
            file.Close();

            //Test that number of allocations is correct
            int noAllocations = anAllocation.SetOfAllocations.Count;
            int expectedAllocations = anAllocation.NumAllocations;
            if (noAllocations != expectedAllocations)
            {
                string error = string.Format(diffNoAllocationToExpectedError, noAllocations, expectedAllocations);
                ErrorList.Add(error);
            }

            //Test that allocations are valid
            foreach(Allocation a in anAllocation.SetOfAllocations)
            {
                if (!Allocation.ValidateAllocation(a))
                {
                    string error = invalidAllocationError;
                    error = error + a.ToString();
                    ErrorList.Add(error);
                }
            }

            return ErrorList.Count == 0;
        }
        #endregion
    }
}
