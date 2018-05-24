using System;
using LeaveLib.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LeaveTests
{
    [TestClass]
    public class DomainTest
    {
        [TestMethod]
        public void GenerateSickLeaveTest()
        {
            Employee employee = new Employee("Test user");

            LeaveConfiguration leaveConfig = new LeaveConfiguration()
            {
                AmountDays = 30,
                LeaveType = LeaveType.Sick,
                LeaveEndOfLife = LeaveEndOfLife.Expire,
                ValidDays = 365
            };
            
            LeaveGenerator generator = new LeaveGenerator();

            Leave leave = generator.Generate(employee, leaveConfig);

            Assert.IsNotNull(leave);
        }
    }
}
