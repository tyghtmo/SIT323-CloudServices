using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIT323Assignment1
{
    class Configuration
    {
        #region Constants
        private const char delimiter = ',';
        private const string comment = "//";
        private const string doubleQuote = "\"";
        private const string emptySpace = "";

        //Errors
        private const string invalidLineError = "Invalid line in file: ";
        private const string stringToIntError = "String to Int error: ";
        #endregion

        #region Properties
        //Keys
        private readonly string logFileKey = "DEFAULT-LOGFILE";

        private readonly string tasksLimitsKey = "LIMITS-TASKS";
        private readonly string processorsLimitsKey = "LIMIT-PROCESSORS";
        private readonly string processorsFreqLimitsKey = "LIMIT-PROCESSORS-FREQUENCIES";

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
        public Dictionary<int, int> ProcessorFrequencies { get; set; }
        public Dictionary<int, int> CoefficientValues { get; set; }

        //Errors
        public List<String> ConfigurationErrorList = new List<string>();

        //Filename
        public string ConfigurationFilePath { get; set; }
        #endregion

        #region Constructers
        public Configuration(string path)
        {
            ConfigurationFilePath = path;
        }
        #endregion
    }
}
