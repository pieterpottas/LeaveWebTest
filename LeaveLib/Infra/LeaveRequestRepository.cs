using System.Collections.Generic;
using System.Linq;
using LeaveLib.Domain;

namespace LeaveLib.Infra
{
    public class LeaveRequestRepository
    {
        private static readonly List<LeaveRequest> LeaveRequests = new List<LeaveRequest>();

        public LeaveRequest GetById(int id)
        {
            return LeaveRequests.SingleOrDefault(s => s.Id == id);
        }

        public List<LeaveRequest> GetAll()
        {
            return LeaveRequests;
        }

        public void SaveOrUpdate(LeaveRequest leaveRequest)
        {
            if (leaveRequest.Id == 0)
            {
                leaveRequest.Id = LeaveRequests.Count + 1;
                LeaveRequests.Add(leaveRequest);
            }
            else
            {
                foreach (LeaveRequest t in LeaveRequests)
                {
                    if (t.Id != leaveRequest.Id)
                        continue;

                    t.Leave = leaveRequest.Leave;
                    t.Approval = leaveRequest.Approval;
                    t.TotalCount = leaveRequest.TotalCount;
                }
            }

        }
    }
}