using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SIT323Assignment1;

namespace SIT323Assignment1Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            //Arrange
            TaskAllocations test = new TaskAllocations("Test1.tan");

            //Act
            TaskAllocations.TryParse("Test1.tan", out test);
            //Assert

        }
    }
}
