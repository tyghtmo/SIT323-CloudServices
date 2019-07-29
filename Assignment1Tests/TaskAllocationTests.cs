using Microsoft.VisualStudio.TestTools.UnitTesting;
using SIT323Assignment1;
using System.Linq;

namespace Assignment1Tests
{
    [TestClass]
    public class TaskAllocationTests
    {
        const string test1FilePath = "C:\\Users\\Tyson\\source\\repos\\SIT323Assignment1\\Test1.tan";

        [TestMethod]
        public void TestMethod1()
        {
            //Arrange
            string expectedConfigFile = "Test1.csv";

            //Act
            TaskAllocations test = new TaskAllocations(test1FilePath);
            TaskAllocations.TryParse(test1FilePath, out test);
            string configFile = test.ConfigPath;

            //Assert
            Assert.AreEqual(expectedConfigFile, configFile, "CONFIG-FILE path is incorrect in Test1.tan");

        }

        [TestMethod]
        public void TestMethod2()
        {
            //Arrange
            int expectedTasks = 5;

            //Act
            TaskAllocations test = new TaskAllocations(test1FilePath);
            TaskAllocations.TryParse(test1FilePath, out test);
            int tasks = test.Tasks;

            //Assert
            Assert.AreEqual(expectedTasks, tasks, "TASKS is incorrect in Test1.tan");
        }

        [TestMethod]
        public void TestMethod3()
        {
            //Arrange
            int expectedProcessors = 3;

            //Act
            TaskAllocations test = new TaskAllocations(test1FilePath);
            TaskAllocations.TryParse(test1FilePath, out test);
            int processors = test.Processors;

            //Assert
            Assert.AreEqual(expectedProcessors, processors, "PROCESSORS is incorrect in Test1.tan");

        }

        [TestMethod]
        public void TestMethod4()
        {
            //Arrange
            int expectedAllocations = 8;

            //Act
            TaskAllocations test = new TaskAllocations(test1FilePath);
            TaskAllocations.TryParse(test1FilePath, out test);
            int allocations = test.NumAllocations;

            //Assert
            Assert.AreEqual(expectedAllocations, allocations, "ALLOCATIONS is incorrect in Test1.tan");

        }

        [TestMethod]
        public void TestMethod5()
        {
            //Arrange
            int expectedID = 1;
            int[,] expectedMatrix1 = { { 1, 1, 0, 0, 0 }, { 0, 0, 1, 1, 0 }, { 0, 0, 0, 0, 1 } };

            //Act
            TaskAllocations test = new TaskAllocations(test1FilePath);
            TaskAllocations.TryParse(test1FilePath, out test);
            int id = test.SetOfAllocations[0].ID;
            int[,] matrix = test.SetOfAllocations[0].AllocationMatrix;

            //Assert
            Assert.AreEqual(expectedID, id, "ALLOCATION-ID 1 has incorrect ID");
            CollectionAssert.AreEqual(expectedMatrix1, matrix, "ALLOCATION-ID 1 is incorrect in Test1.tan");

        }

        [TestMethod]
        public void TestMethod6()
        {
            //Arrange
            int expectedID = 2;
            int[,] expectedMatrix1 = { { 1, 1, 0, 0, 0 }, { 0, 0, 0, 0, 1 }, { 0, 0, 1, 1, 0 } };

            //Act
            TaskAllocations test = new TaskAllocations(test1FilePath);
            TaskAllocations.TryParse(test1FilePath, out test);
            int id = test.SetOfAllocations[1].ID;
            int[,] matrix = test.SetOfAllocations[1].AllocationMatrix;

            //Assert
            Assert.AreEqual(expectedID, id, "ALLOCATION-ID 2 has incorrect ID");
            CollectionAssert.AreEqual(expectedMatrix1, matrix, "ALLOCATION-ID 2 is incorrect in Test1.tan");

        }

        [TestMethod]
        public void TestMethod7()
        {
            //Arrange
            int expectedID = 3;
            int[,] expectedMatrix1 = { { 1, 0, 0, 1, 0 }, { 0, 1, 1, 0, 0 }, { 0, 0, 0, 0, 1 } };

            //Act
            TaskAllocations test = new TaskAllocations(test1FilePath);
            TaskAllocations.TryParse(test1FilePath, out test);
            int id = test.SetOfAllocations[2].ID;
            int[,] matrix = test.SetOfAllocations[2].AllocationMatrix;

            //Assert
            Assert.AreEqual(expectedID, id, "ALLOCATION-ID 3 has incorrect ID");
            CollectionAssert.AreEqual(expectedMatrix1, matrix, "ALLOCATION-ID 3 is incorrect in Test1.tan");

        }

        [TestMethod]
        public void TestMethod8()
        {
            //Arrange
            int expectedID = 4;
            int[,] expectedMatrix1 = { { 1, 0, 0, 1, 0 }, { 0, 0, 0, 0, 1 }, { 0, 1, 1, 0, 0 } };

            //Act
            TaskAllocations test = new TaskAllocations(test1FilePath);
            TaskAllocations.TryParse(test1FilePath, out test);
            int id = test.SetOfAllocations[3].ID;
            int[,] matrix = test.SetOfAllocations[3].AllocationMatrix;

            //Assert
            Assert.AreEqual(expectedID, id, "ALLOCATION-ID 4 has incorrect ID");
            CollectionAssert.AreEqual(expectedMatrix1, matrix, "ALLOCATION-ID 4 is incorrect in Test1.tan");

        }

        [TestMethod]
        public void TestMethod9()
        {
            //Arrange
            int expectedID = 5;
            int[,] expectedMatrix1 = { { 0, 1, 0, 1, 0 }, { 1, 0, 1, 0, 0 }, { 0, 0, 0, 0, 1 } };

            //Act
            TaskAllocations test = new TaskAllocations(test1FilePath);
            TaskAllocations.TryParse(test1FilePath, out test);
            int id = test.SetOfAllocations[4].ID;
            int[,] matrix = test.SetOfAllocations[4].AllocationMatrix;

            //Assert
            Assert.AreEqual(expectedID, id, "ALLOCATION-ID 5 has incorrect ID");
            CollectionAssert.AreEqual(expectedMatrix1, matrix, "ALLOCATION-ID 5 is incorrect in Test1.tan");

        }

        [TestMethod]
        public void TestMethod10()
        {
            //Arrange
            int expectedID = 6;
            int[,] expectedMatrix1 = { { 0, 0, 1, 0, 0 }, { 1, 1, 0, 1, 0 }, { 0, 0, 0, 0, 1 } };

            //Act
            TaskAllocations test = new TaskAllocations(test1FilePath);
            TaskAllocations.TryParse(test1FilePath, out test);
            int id = test.SetOfAllocations[5].ID;
            int[,] matrix = test.SetOfAllocations[5].AllocationMatrix;

            //Assert
            Assert.AreEqual(expectedID, id, "ALLOCATION-ID 5 has incorrect ID");
            CollectionAssert.AreEqual(expectedMatrix1, matrix, "ALLOCATION-ID 5 is incorrect in Test1.tan");

        }

        [TestMethod]
        public void TestMethod11()
        {
            //Arrange
            int expectedID = 7;
            int[,] expectedMatrix1 = { { 0, 1, 0, 1, 0 }, { 0, 0, 0, 0, 1 }, { 1, 0, 1, 0, 0 } };

            //Act
            TaskAllocations test = new TaskAllocations(test1FilePath);
            TaskAllocations.TryParse(test1FilePath, out test);
            int id = test.SetOfAllocations[6].ID;
            int[,] matrix = test.SetOfAllocations[6].AllocationMatrix;

            //Assert
            Assert.AreEqual(expectedID, id, "ALLOCATION-ID 6 has incorrect ID");
            CollectionAssert.AreEqual(expectedMatrix1, matrix, "ALLOCATION-ID 6 is incorrect in Test1.tan");

        }

        [TestMethod]
        public void TestMethod12()
        {
            //Arrange
            int expectedID = 8;
            int[,] expectedMatrix1 = { { 0, 0, 1, 0, 0 }, { 0, 0, 0, 0, 1 }, { 1, 1, 0, 1, 0 } };

            //Act
            TaskAllocations test = new TaskAllocations(test1FilePath);
            TaskAllocations.TryParse(test1FilePath, out test);
            int id = test.SetOfAllocations[7].ID;
            int[,] matrix = test.SetOfAllocations[7].AllocationMatrix;

            //Assert
            Assert.AreEqual(expectedID, id, "ALLOCATION-ID 7 has incorrect ID");
            CollectionAssert.AreEqual(expectedMatrix1, matrix, "ALLOCATION-ID 7 is incorrect in Test1.tan");

        }
    }
}
