using System;

namespace LeaveLib.Domain
{
    public class LeaveRequest
    {
        public int TotalCount { get; set; }

        public bool? Approval { get; set; }

        public Leave Leave { get; set; }

        public int Id { get; set; }

        public LeaveRequest(Leave leave , int days)
        {
            TotalCount = days;
            Leave = leave;
        }

        public void Approve(Employee employee)
        {
            if (Approval == null)
                throw new Exception("Approval already finalized");

            if (Leave.Employee.Manager != employee)
                throw new Exception("Not my manager");

            Leave.Take(this);

            Approval = true;
        }

        public void Deny(Employee employee)
        {
            if (Approval == null)
                throw new Exception("Approval already finalized");

            if (Leave.Employee.Manager != employee)
                throw new Exception("Not my manager");

            Approval = false;
        }


    }
}