using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SIT323Assignment1
{
    public class Configuration
    {
        #region Constants
        private const char delimiter = ',';
        private const string comment = "//";
        private const string doubleQuote = "\"";
        private const string emptySpace = "";

        //Regex
        //TODO make all regex constants

        //Errors
        private const string missingKeyError = "CSV File is missing a key: ";
        private const string multipleKeyError = "CSV File contains multiple keys: ";
        private const string invalidLineError = "Invalid line in CSV file: ";
        private const string stringToIntError = "String to Int error: ";
        private const string missingSeperatorError = "Invalid seperator: ";
        private const string invalidFileError = "File type is invalid, must be .csv: ";
        private const string commentLineError = "Invalid comment on line: ";
        private const string invalidSeperatorError = "Invalid seperator on line: ";
        private const string invalidIDError = "{0} with this ID already exists: {1}";
        #endregion

        #region Properties
        //Keys
        private readonly string logFileKey = "DEFAULT-LOGFILE";
        private readonly string tasksLimitsKey = "LIMITS-TASKS";
        private readonly string processorsLimitsKey = "LIMITS-PROCESSORS";
        private readonly string processorsFreqLimitsKey = "LIMITS-PROCESSOR-FREQUENCIES";
        private readonly string programMaxDurationKey = "PROGRAM-MAXIMUM-DURATION";
        private readonly string programTasksKey = "PROGRAM-TASKS";
        private readonly string programProcessorsKey = "PROGRAM-PROCESSORS";
        private readonly string runtimeRefFreqKey = "RUNTIME-REFERENCE-FREQUENCY";
        private readonly string taskIDKey = "TASK-ID";
        private readonly string runtimeKey = "RUNTIME";
        private readonly string processorIDKey = "PROCESSOR-ID";
        private readonly string frequencyKey = "FREQUENCY";
        private readonly string coefficientIDKey = "COEFFICIENT-ID";
        private readonly string valueKey = "VALUE";

        //Values
        public string LogFilePath { get; set; }

        public int TasksMin { get; set; }
        public int TasksMax { get; set; }
        public int ProcessorsMin { get; set; }
        public int ProcessorsMax { get; set; }
        public int ProcessorsFrequencyMin { get; set; }
        public int ProcessorsFrequencyMax { get; set; }

        public int ProgramMaxDuration { get; set; }
        public int ProgramTasks { get; set; }
        public int ProgramProcessors { get; set; }

        public int RuntimeReferenceFrequency { get; set; }

        public Dictionary<int, int> TaskRuntimes { get; set; }
        public Dictionary<int, double> ProcessorFrequencies { get; set; }
        public Dictionary<int, int> CoefficientValues { get; set; }

        //Errors
        public bool isValid { get; set; }
        public List<String> ConfigurationErrorList = new List<string>();

        //Filename
        public string ConfigurationFilePath { get; set; }
        #endregion

        #region Constructers
        public Configuration(string path)
        {
            ConfigurationFilePath = path;
        }

        public Configuration() { }
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
        /// Validates that all keys are present and the filename is correct
        /// </summary>
        /// <returns>Bool determined by any errors present</returns>
        public bool Validate()
        {

            StreamReader file = new StreamReader(ConfigurationFilePath);
            string configurationContents = file.ReadToEnd();
            file.Close();

            //Check file contains all keys
            Regex logPathRegex = new Regex(@"DEFAULT-LOGFILE");
            MatchCollection logPathMatch = logPathRegex.Matches(configurationContents);
            if (logPathMatch.Count == 0)
            {
                ConfigurationErrorList.Add(missingKeyError + logFileKey);
            }
            if (logPathMatch.Count > 1)
            {
                ConfigurationErrorList.Add(multipleKeyError + logFileKey);
            }

            Regex limitTasksRegex = new Regex(@"LIMITS-TASKS");
            MatchCollection limitTasksMatch = limitTasksRegex.Matches(configurationContents);
            if (limitTasksMatch.Count == 0)
            {
                ConfigurationErrorList.Add(missingKeyError + tasksLimitsKey);
            }
            if (limitTasksMatch.Count > 1)
            {
                ConfigurationErrorList.Add(multipleKeyError + tasksLimitsKey);
            }

            Regex limitsProcessorsRegex = new Regex(@"LIMITS-PROCESSORS");
            MatchCollection limitProcessorsMatch = limitsProcessorsRegex.Matches(configurationContents);
            if (limitProcessorsMatch.Count == 0)
            {
                ConfigurationErrorList.Add(missingKeyError + processorsLimitsKey);
            }
            if (limitProcessorsMatch.Count > 1)
            {
                ConfigurationErrorList.Add(multipleKeyError + processorsLimitsKey);
            }

            Regex limitProcessorFrequenciesRegex = new Regex(@"LIMITS-PROCESSOR-FREQUENCIES");
            MatchCollection limitProcessorsFrequenciesMatch = limitProcessorFrequenciesRegex.Matches(configurationContents);
            if (limitProcessorsFrequenciesMatch.Count == 0)
            {
                ConfigurationErrorList.Add(missingKeyError + processorsFreqLimitsKey);
            }
            if (limitProcessorsFrequenciesMatch.Count > 1)
            {
                ConfigurationErrorList.Add(multipleKeyError + processorsFreqLimitsKey);
            }

            Regex programMaxDurationRegex = new Regex(@"PROGRAM-MAXIMUM-DURATION");
            MatchCollection programMaxDurationMatch = programMaxDurationRegex.Matches(configurationContents);
            if (programMaxDurationMatch.Count == 0)
            {
                ConfigurationErrorList.Add(missingKeyError + programMaxDurationKey);
            }
            if (programMaxDurationMatch.Count > 1)
            {
                ConfigurationErrorList.Add(multipleKeyError + programMaxDurationKey);
            }

            Regex programTasksRegex = new Regex(@"PROGRAM-TASKS");
            MatchCollection programTasksMatch = programTasksRegex.Matches(configurationContents);
            if (programTasksMatch.Count == 0)
            {
                ConfigurationErrorList.Add(missingKeyError + programTasksKey);
            }
            if (programTasksMatch.Count > 1)
            {
                ConfigurationErrorList.Add(multipleKeyError + programTasksKey);
            }

            Regex programProcessorsRegex = new Regex(@"PROGRAM-PROCESSORS");
            MatchCollection programProcessorsMatch= programProcessorsRegex.Matches(configurationContents);
            if (programProcessorsMatch.Count == 0)
            {
                ConfigurationErrorList.Add(missingKeyError + programProcessorsKey);
            }
            if (programProcessorsMatch.Count > 1)
            {
                ConfigurationErrorList.Add(multipleKeyError + programProcessorsKey);
            }

            Regex runtimeRefFrequencyRegex= new Regex(@"RUNTIME-REFERENCE-FREQUENCY");
            MatchCollection runtimeRefFrequencyMatch= runtimeRefFrequencyRegex.Matches(configurationContents);
            if (runtimeRefFrequencyMatch.Count == 0)
            {
                ConfigurationErrorList.Add(missingKeyError + runtimeRefFreqKey);
            }
            if (runtimeRefFrequencyMatch.Count > 1)
            {
                ConfigurationErrorList.Add(multipleKeyError + runtimeRefFreqKey);
            }

            //Check filename is valid
            Regex csvFileRegex = new Regex(@".+\.csv$");
            MatchCollection csvFileMatch = csvFileRegex.Matches(ConfigurationFilePath);
            if (csvFileMatch.Count == 0) ConfigurationErrorList.Add(invalidFileError + ConfigurationFilePath);

            isValid = ConfigurationErrorList.Count == 0;
            return (isValid);
        }

        /// <summary>
        /// Determines if a .csv file is valid and parses the data to an instance of Configuration
        /// </summary>
        /// <param name="path">A string containg the filepath of a .csv file</param>
        /// <param name="aConfiguration">An instance of Configuration that is populated with data from the .csv file being parsed</param>
        /// <returns>A bool depending on if the file is valid and error free.</returns>
        public static bool TryParse(string path, out Configuration aConfiguration)
        {
            aConfiguration = new Configuration(path);
            List<String> ParsingErrorList = new List<string>();
            aConfiguration.TaskRuntimes = new Dictionary<int, int>();
            aConfiguration.ProcessorFrequencies = new Dictionary<int, double>();
            aConfiguration.CoefficientValues = new Dictionary<int, int>();

            Regex singleSeperatorRegex = new Regex(@"^.*,.*$");
            Regex doubleSeperatorRegex = new Regex(@"^.*,.*,.*$");
            Regex doubleDigitSeperatorRegex = new Regex(@"^\d+,-*\d+\.?\d*$");
            Regex textDigitSeperatorRegex = new Regex(@"^.+,\d+$");
            Regex textDoubleDigitSeperatorRegex = new Regex(@"^.+(?:,\d+){2}$");

            //Begin parsing TAN file
            StreamReader file = new StreamReader(path);
            while (!file.EndOfStream)
            {
                string line = file.ReadLine();
                MatchCollection singleSeperatorMatch = singleSeperatorRegex.Matches(line);
                MatchCollection doubleSeperatorMatch = doubleSeperatorRegex.Matches(line);
                MatchCollection textDigitSeperatorMatch = textDigitSeperatorRegex.Matches(line);
                MatchCollection textDoubleDigitSeperatorMatch = textDoubleDigitSeperatorRegex.Matches(line);

                if (line.Length == 0) { }
                else if (line.Contains(comment))
                {
                    if (line.StartsWith(comment))
                    {
                        //Comment Found
                    }
                    else
                    {
                        ParsingErrorList.Add(commentLineError + line);
                    }
                }
                else if (singleSeperatorMatch.Count == 0)
                {
                    ParsingErrorList.Add(invalidSeperatorError + line);
                }
                else if (line.Contains(aConfiguration.logFileKey))
                {
                    string[] substrings = line.Split(delimiter);

                    //Check Line is valid and only contains expected number of arguements
                    if (substrings[0] == aConfiguration.logFileKey && substrings.Count() == 2)
                    {
                        string logPath = substrings[1].Replace(doubleQuote, emptySpace);
                        aConfiguration.LogFilePath = logPath;
                    }
                    else
                    {
                        ParsingErrorList.Add(invalidLineError + line);
                    }
                }
                else if (line.Contains(aConfiguration.tasksLimitsKey))
                {
                    string[] substrings = line.Split(delimiter);
                    if (textDoubleDigitSeperatorMatch.Count == 1 && substrings[0] == aConfiguration.tasksLimitsKey) {

                        aConfiguration.TasksMin = Int32.Parse(substrings[1]);
                        aConfiguration.TasksMax = Int32.Parse(substrings[2]);
                    }
                    else if(doubleSeperatorMatch.Count == 0)
                    {
                        ParsingErrorList.Add(invalidSeperatorError + line);
                    }
                    else
                    {
                        ParsingErrorList.Add(stringToIntError + line);
                    }

                }
                else if (line.Contains(aConfiguration.processorsLimitsKey))
                {
                    string[] substrings = line.Split(delimiter);
                    if (textDoubleDigitSeperatorMatch.Count == 1 && substrings[0] == aConfiguration.processorsLimitsKey)
                    {

                        aConfiguration.ProcessorsMin = Int32.Parse(substrings[1]);
                        aConfiguration.ProcessorsMax = Int32.Parse(substrings[2]);
                    }
                    else if (doubleSeperatorMatch.Count == 0)
                    {
                        ParsingErrorList.Add(invalidSeperatorError + line);
                    }
                    else
                    {
                        ParsingErrorList.Add(stringToIntError + line);
                    }

                }
                else if (line.Contains(aConfiguration.processorsFreqLimitsKey))
                {
                    string[] substrings = line.Split(delimiter);
                    if (textDoubleDigitSeperatorMatch.Count == 1 && substrings[0] == aConfiguration.processorsFreqLimitsKey)
                    {
                        aConfiguration.ProcessorsFrequencyMin = Int32.Parse(substrings[1]);
                        aConfiguration.ProcessorsFrequencyMax = Int32.Parse(substrings[2]);
                    }
                    else if (doubleSeperatorMatch.Count == 0)
                    {
                        ParsingErrorList.Add(invalidSeperatorError + line);
                    }
                    else
                    {
                        ParsingErrorList.Add(stringToIntError + line);
                    }

                }
                else if (line.Contains(aConfiguration.programMaxDurationKey))
                {
                    string[] substrings = line.Split(delimiter);
                    if (textDigitSeperatorMatch.Count == 1 && substrings[0] == aConfiguration.programMaxDurationKey)
                    {
                        aConfiguration.ProgramMaxDuration = Int32.Parse(substrings[1]);
                    }
                    else if (singleSeperatorMatch.Count == 0)
                    {
                        ParsingErrorList.Add(invalidSeperatorError + line);
                    }
                    else
                    {
                        ParsingErrorList.Add(stringToIntError + line);
                    }

                }
                else if (line.Contains(aConfiguration.programTasksKey))
                {
                    string[] substrings = line.Split(delimiter);
                    if (textDigitSeperatorMatch.Count == 1 && substrings[0] == aConfiguration.programTasksKey)
                    {
                        aConfiguration.ProgramTasks = Int32.Parse(substrings[1]);
                    }
                    else if (singleSeperatorMatch.Count == 0)
                    {
                        ParsingErrorList.Add(invalidSeperatorError + line);
                    }
                    else
                    {
                        ParsingErrorList.Add(stringToIntError + line);
                    }

                }
                else if (line.Contains(aConfiguration.programProcessorsKey))
                {
                    string[] substrings = line.Split(delimiter);
                    if (textDigitSeperatorMatch.Count == 1 && substrings[0] == aConfiguration.programProcessorsKey)
                    {
                        aConfiguration.ProgramProcessors = Int32.Parse(substrings[1]);
                    }
                    else if (singleSeperatorMatch.Count == 0)
                    {
                        ParsingErrorList.Add(invalidSeperatorError + line);
                    }
                    else
                    {
                        ParsingErrorList.Add(stringToIntError + line);
                    }

                }
                else if (line.Contains(aConfiguration.runtimeRefFreqKey))
                {
                    string[] substrings = line.Split(delimiter);
                    if (textDigitSeperatorMatch.Count == 1 && substrings[0] == aConfiguration.runtimeRefFreqKey)
                    {
                        aConfiguration.RuntimeReferenceFrequency = Int32.Parse(substrings[1]);
                    }
                    else if (singleSeperatorMatch.Count == 0)
                    {
                        ParsingErrorList.Add(invalidSeperatorError + line);
                    }
                    else
                    {
                        ParsingErrorList.Add(stringToIntError + line);
                    }

                }
                else if (line.Contains(aConfiguration.taskIDKey) && line.Contains(aConfiguration.runtimeKey))
                {
                    line = file.ReadLine();
                    do
                    {
                        if (line.Contains(comment))
                        {
                            if (line.StartsWith(comment)) break;
                            else
                            {
                                ParsingErrorList.Add(commentLineError + line);
                                break;
                            }
                        }

                        string[] substrings = line.Split(delimiter);
                        MatchCollection doubleDigitSeperatorMatch = doubleDigitSeperatorRegex.Matches(line);
                        singleSeperatorMatch = singleSeperatorRegex.Matches(line);

                        if (doubleDigitSeperatorMatch.Count == 1)
                        {
                            if (!aConfiguration.TaskRuntimes.ContainsKey(Int32.Parse(substrings[0])))
                            {
                                aConfiguration.TaskRuntimes.Add(Int32.Parse(substrings[0]), Int32.Parse(substrings[1]));
                            }
                            else
                            {
                                ParsingErrorList.Add(string.Format(invalidIDError, aConfiguration.taskIDKey, line));
                            }
                        }
                        else if (singleSeperatorMatch.Count == 0)
                        {
                            ParsingErrorList.Add(invalidSeperatorError + line);
                        }
                        else
                        {
                            ParsingErrorList.Add(stringToIntError + line);
                        }
                        line = file.ReadLine();

                    } while (line != null && line.Length != 0);

                }
                else if (line.Contains(aConfiguration.processorIDKey) && line.Contains(aConfiguration.frequencyKey))
                {
                    line = file.ReadLine();
                    do
                    {
                        if (line.Contains(comment))
                        {
                            if (line.StartsWith(comment)) break;
                            else
                            {
                                ParsingErrorList.Add(commentLineError + line);
                                break;
                            }
                        }

                        string[] substrings = line.Split(delimiter);
                        MatchCollection doubleDigitSeperatorMatch = doubleDigitSeperatorRegex.Matches(line);
                        singleSeperatorMatch = singleSeperatorRegex.Matches(line);

                        if (doubleDigitSeperatorMatch.Count == 1)
                        {
                            if (!aConfiguration.ProcessorFrequencies.ContainsKey(Int32.Parse(substrings[0])))
                            {
                                aConfiguration.ProcessorFrequencies.Add(Int32.Parse(substrings[0]), double.Parse(substrings[1]));
                            }
                            else
                            {
                                ParsingErrorList.Add(string.Format(invalidIDError, aConfiguration.processorIDKey, line));
                            }
                        }
                        else if (singleSeperatorMatch.Count == 0)
                        {
                            ParsingErrorList.Add(invalidSeperatorError + line);
                        }
                        else
                        {
                            ParsingErrorList.Add(stringToIntError + line);
                        }
                        line = file.ReadLine();

                    } while (line != null && line.Length != 0);

                }
                else if (line.Contains(aConfiguration.coefficientIDKey) && line.Contains(aConfiguration.valueKey))
                {
                    line = file.ReadLine();
                    do
                    {
                        if (line.Contains(comment))
                        {
                            if (line.StartsWith(comment)) break;
                            else
                            {
                                ParsingErrorList.Add(commentLineError + line);
                                break;
                            }
                        }

                        string[] substrings = line.Split(delimiter);
                        MatchCollection doubleDigitSeperatorMatch = doubleDigitSeperatorRegex.Matches(line);
                        singleSeperatorMatch = singleSeperatorRegex.Matches(line);

                        if (doubleDigitSeperatorMatch.Count == 1)
                        {
                            if (!aConfiguration.CoefficientValues.ContainsKey(Int32.Parse(substrings[0])))
                            {
                                aConfiguration.CoefficientValues.Add(Int32.Parse(substrings[0]), Int32.Parse(substrings[1]));
                            }
                            else
                            {
                                ParsingErrorList.Add(string.Format(invalidIDError, aConfiguration.coefficientIDKey, line));
                            }
                        }
                        else if (singleSeperatorMatch.Count == 0)
                        {
                            ParsingErrorList.Add(invalidSeperatorError + line);
                        }
                        else
                        {
                            ParsingErrorList.Add(stringToIntError + line);
                        }
                        line = file.ReadLine();

                    } while (line != null && line.Length != 0);

                }
                else
                {
                    string error = invalidLineError + line;
                    ParsingErrorList.Add(error);
                }
            }
            file.Close();

            //Add parsing errors to instance error list
            aConfiguration.ConfigurationErrorList.AddRange(ParsingErrorList);

            //Check validity of file
            aConfiguration.isValid = (aConfiguration.Validate() && ParsingErrorList.Count == 0);

            return ParsingErrorList.Count == 0;
        }
        #endregion
    }
}
