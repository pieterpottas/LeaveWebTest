using System;

namespace LeaveLib.Domain
{
    public class Leave
    {
        public Leave(Employee employee , DateTime startDt , DateTime expireDt, LeaveType leaveType, LeaveEndOfLife eol, int amountDays)
        {
            if (employee == null)
                throw new Exception("Invalid employee");

            Employee = employee;
            LeaveType = leaveType;
            StartDateTime = startDt;
            ExpireDateTime = ExpireDateTime;
            LeaveEndOfLife = eol;
            TotalDays = amountDays;

        }

        public int Id { get; set; }

        public Employee Employee { get; set; }

        public LeaveType LeaveType { get; set; }

        public LeaveEndOfLife LeaveEndOfLife { get; set; }

        public int TotalDays { get; set; }

        public DateTime? ExpireDateTime { get; set; }

        public DateTime? StartDateTime { get; set; }

        public void Invalidate()
        {
            ExpireDateTime = DateTime.Now;
            LeaveEndOfLife = LeaveEndOfLife.Expire;
        }

        public void Take(LeaveRequest leaveRequest)
        {
            if (TotalDays < leaveRequest.TotalCount)
                throw new Exception("No enough leave days");

            if (leaveRequest.Approval != true)
                throw new Exception("No approved");

            TotalDays -= leaveRequest.TotalCount;
        }

        public void Cancel(LeaveRequest leaveRequest)
        {
            if (TotalDays < leaveRequest.TotalCount)
                throw new Exception("No enough leave days");

            if (leaveRequest.Approval != true)
                throw new Exception("No approved");

            TotalDays += leaveRequest.TotalCount;
        }

        public bool IsActive
        {
            get
            {
                return StartDateTime != null && 
                       ExpireDateTime != null && 
                       StartDateTime <= DateTime.Now &&
                       ExpireDateTime > DateTime.Now;
            }
        }

       
    }
}