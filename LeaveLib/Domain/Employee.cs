using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Configuration;

namespace LeaveLib.Domain
{
    public class Employee
    {
        public Employee(string fullName)
        {
            Manager = null;
            FullName = fullName;
            LeaveList = new List<Leave>();
            RequestList = new List<LeaveRequest>();
        }

        public int Id { get; set; }

        public string FullName { get; set; }

        public Employee Manager { get; set; }

        public IList<Leave> LeaveList { get; set; }

        public IList<LeaveRequest> RequestList { get; set; }

        public LeaveRequest RequestLeave(LeaveType leaveType, int dayAmount)
        {
            Leave currentLeave = null;

            if (LeaveList == null)
                throw new Exception("No leave allocated to employee");

            currentLeave = LeaveList.SingleOrDefault(a => a.LeaveType == leaveType && a.IsActive);

            if (currentLeave == null)
                throw new Exception("All leave has expired");

            LeaveRequest leaveRequest = new LeaveRequest(currentLeave,dayAmount);

            return leaveRequest;
        }


    }

}
