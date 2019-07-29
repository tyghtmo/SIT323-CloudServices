using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIT323Assignment1
{
    public class Allocation
    {
        public int ID { get; set; }
        public int[,] AllocationMatrix { get; set; }

        public Allocation(int id, int[,] matrix)
        {
            ID = id;
            AllocationMatrix = matrix;
        }

        public Allocation(int id, List<string> matrix)
        {
            ID = id;

            int rows = matrix.Count;
            int columns = matrix[1].Replace(",", "").Length;
            AllocationMatrix = new int[rows,columns];

            for (int i = 0 ; i < rows; i++)
            {
                for(int j = 0; j < columns; j++)
                {
                    string[] substrings = matrix[i].Split(',');
                    int input = ToInt32(substrings[j]);
                    if (input == -1)
                    {
                       //error
                    }
                    else
                    {
                        AllocationMatrix[i,j] = input;
                    }
                   
                }
            }
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

        public override string ToString()
        {
            string str = "ID: " + ID + "\n";

            for (int i = 0; i < AllocationMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < AllocationMatrix.GetLength(1); j++)
                {
                    str = str + AllocationMatrix[i,j] + ",";
                }
                str = str.Substring(0, str.Length - 1);
                str = str + "\n";
            }

            return str;
        }

        public static Boolean ValidateAllocation(Allocation allocation)
        {
            Boolean isValid = true;

            //Check ID is valid
            if(allocation.ID < 0)
            {
                isValid = false;
            }

            int rows = allocation.AllocationMatrix.GetLength(0);
            int columns = allocation.AllocationMatrix.GetLength(1);

            for(int i = 0; i < columns; i++)
            {
                int columnSum = 0;
                for(int j = 0; j < rows; j++)
                {
                    //Add each column to determine if there is more or less than one 1 
                    columnSum += allocation.AllocationMatrix[j, i];
                }

                if(columnSum != 1)
                {
                    isValid = false;
                    break;
                }
            }

            return isValid;
        }
    }
}
