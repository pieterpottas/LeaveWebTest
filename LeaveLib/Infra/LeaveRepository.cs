using System.Collections.Generic;
using System.Linq;
using LeaveLib.Domain;

namespace LeaveLib.Infra
{
    public class LeaveRepository
    {
        private static readonly List<Leave> Leaves = new List<Leave>();

        public Leave GetById(int id)
        {
            return Leaves.SingleOrDefault(s => s.Id == id);
        }

        public List<Leave> GetAll()
        {
            return Leaves;
        }

        public void SaveOrUpdate(Leave leave)
        {
            if (leave.Id == 0)
            {
                leave.Id = Leaves.Count + 1;
                Leaves.Add(leave);
            }
            else
            {
                foreach (Leave t in Leaves)
                {
                    if (t.Id != leave.Id)
                        continue;

                    t.Employee       = leave.Employee;
                    t.ExpireDateTime = leave.ExpireDateTime;
                    t.LeaveEndOfLife = leave.LeaveEndOfLife;
                    t.LeaveType      = leave.LeaveType;
                    t.StartDateTime  = leave.StartDateTime;
                    t.TotalDays      = leave.TotalDays;
                }
            }

        }
    }
}