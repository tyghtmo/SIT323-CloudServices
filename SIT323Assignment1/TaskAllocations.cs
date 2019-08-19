﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
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
        private const string missingKeyError = "File is missing a key: ";
        private const string multipleKeyError = "File contains multiple keys: ";
        private const string invalidLineError = "Invalid line in file: ";
        private const string stringToIntError = "String to Int error: ";
        private const string missingSeperatorError = "Invalid seperator: ";
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

        //Errors
        public Boolean isValid;
        public List<String> AllocationErrorList= new List<string>();

        //Filename
        public string taskAllocationPath { get; set; }
        #endregion

        #region Constructers
        public TaskAllocations(string filePath)
        {
            taskAllocationPath = filePath;
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

        public Boolean Validate()
        {

            StreamReader file = new StreamReader(taskAllocationPath);
            string tanContents = file.ReadToEnd();

            //Check file contains all keys
            Regex configPathRegex = new Regex(@"CONFIG-FILE,.+");
            MatchCollection configPathMatch = configPathRegex.Matches(tanContents);
            if (configPathMatch.Count == 0)
            {
                AllocationErrorList.Add(missingKeyError + configPathKey);
            }
            if(configPathMatch.Count > 1)
            {
                AllocationErrorList.Add(multipleKeyError + configPathKey);
            }

            Regex tasksRegex = new Regex(@"TASKS,\d+");
            MatchCollection tasksMatch = tasksRegex.Matches(tanContents);
            if (tasksMatch.Count == 0)
            {
                AllocationErrorList.Add(missingKeyError + tasksKey);
            }
            if (tasksMatch.Count > 1)
            {
                AllocationErrorList.Add(multipleKeyError + tasksKey);
            }

            Regex processorsRegex = new Regex(@"PROCESSORS,\d+");
            MatchCollection processorsMatch = processorsRegex.Matches(tanContents);
            if (processorsMatch.Count == 0)
            {
                AllocationErrorList.Add(missingKeyError + processorsKey);
            }
            if (processorsMatch.Count > 1)
            {
                AllocationErrorList.Add(multipleKeyError + processorsKey);
            }

            Regex allocationRegex = new Regex(@"ALLOCATIONS,\d+");
            MatchCollection allocationMatch = allocationRegex.Matches(tanContents);
            if (allocationMatch.Count == 0)
            {
                AllocationErrorList.Add(missingKeyError + allocationsKey);
            }
            if (allocationMatch.Count > 1)
            {
                AllocationErrorList.Add(multipleKeyError + allocationsKey);
            }

            //Check No. of Allocations is correct
            string line = "";
            int expectedAllocations = -1;

            if (allocationMatch.Count > 0)
            {
                line = allocationMatch[0].Value;
                string[] substrings = line.Split(delimiter);
                expectedAllocations = ToInt32(substrings[1]);
                if (expectedAllocations == -1) AllocationErrorList.Add(stringToIntError + line);

                Regex allocationIDRegex = new Regex(@"ALLOCATION-ID,\d+");
                MatchCollection allocationIDMatch = allocationIDRegex.Matches(tanContents);
                int actualAllocations = allocationIDMatch.Count;


                if (expectedAllocations != actualAllocations)
                {
                    AllocationErrorList.Add(string.Format(diffNoAllocationToExpectedError, actualAllocations, expectedAllocations));
                }
            }  

            //Validate allocations
            foreach (Allocation a in SetOfAllocations)
            {
                if (!Allocation.ValidateAllocation(a))
                {
                    string error = invalidAllocationError;
                    error = error + a.ToString();
                    AllocationErrorList.Add(error);
                }
            }

            //Check filename is valid
            Regex tanFileRegex = new Regex(@".+\.tan$");
            MatchCollection tanFileMatch = tanFileRegex.Matches(taskAllocationPath);

            return (AllocationErrorList.Count == 0);
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
            List<String> ParsingErrorList = new List<string>();
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
                        ParsingErrorList.Add(error);
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
                        ParsingErrorList.Add(invalidLineError + line);
                    }
                }
                else if (line.Contains(anAllocation.tasksKey))
                {
                    string[] substrings = line.Split(delimiter);
                    int input = ToInt32(substrings[1]);

                    if(input == -1) ParsingErrorList.Add(stringToIntError + line);
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
                        ParsingErrorList.Add(error);
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
                        ParsingErrorList.Add(error);
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
                        ParsingErrorList.Add(error);
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
                    ParsingErrorList.Add(error);
                }
            }
            file.Close();

            //Add parsing errors to instance error list
            anAllocation.AllocationErrorList.AddRange(ParsingErrorList);

            //Check validity of file
            anAllocation.isValid = anAllocation.Validate();

            return ParsingErrorList.Count == 0;
        }
        #endregion
    }
}
