using System;
using System.Collections.Generic;
using System.Linq;

namespace LeaveLib.Domain
{
    public class LeaveGenerator
    {
        public Leave Generate(Employee employee, LeaveConfiguration config)
        {
            if (employee == null)
                throw new Exception("Employee is null");

            //leave always start on new year
            DateTime leaveStartDate = new DateTime(DateTime.Now.Year,1,1);
            DateTime leaveCorrectStartDate = leaveStartDate;
            int leaveDaysLeft       = 0;

            //determine leave left and start
            if (employee.LeaveList != null)
            {
                if (employee.LeaveList.Any(a => a.IsActive && a.LeaveType == config.LeaveType))
                    throw new Exception("Sick leave still active");

                List<Leave> leaveList = employee.LeaveList.Where(w => w.LeaveType == config.LeaveType).OrderByDescending(o => o.ExpireDateTime).ToList();

                Leave lastSickLeave = leaveList.FirstOrDefault();

                if (lastSickLeave != null)
                {
                    if (lastSickLeave.ExpireDateTime.HasValue)
                        leaveStartDate = lastSickLeave.ExpireDateTime.Value.AddDays(1);

                    if (lastSickLeave.LeaveEndOfLife == LeaveEndOfLife.Rollover)
                        leaveDaysLeft += lastSickLeave.TotalDays;
                }

                var leaveCount = leaveList.Count(w => w.LeaveEndOfLife == LeaveEndOfLife.Rollover);

                if (leaveCount >= 3)
                    leaveDaysLeft = 0;
            }

            DateTime expireDateTime = leaveCorrectStartDate.AddDays(config.ValidDays);

            leaveDaysLeft += config.AmountDays;

            Leave leave = new Leave(employee,leaveStartDate,expireDateTime,config.LeaveType,config.LeaveEndOfLife,leaveDaysLeft);

            return leave;
        }
    }
}