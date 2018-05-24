using System.Collections.Generic;
using System.Linq;
using LeaveLib.Domain;

namespace LeaveLib.Infra
{
    public class LeaveConfigurationRepository
    {
        private static readonly List<LeaveConfiguration> LeaveConfigurations = new List<LeaveConfiguration>();

        public LeaveConfiguration GetById(int id)
        {
            return LeaveConfigurations.SingleOrDefault(s => s.Id == id);
        }

        public List<LeaveConfiguration> GetAll()
        {
            return LeaveConfigurations;
        }

        public void SaveOrUpdate(LeaveConfiguration leaveConfig)
        {
            if (leaveConfig.Id == 0)
            {
                leaveConfig.Id = LeaveConfigurations.Count + 1;
                LeaveConfigurations.Add(leaveConfig);
            }
            else
            {
                foreach (LeaveConfiguration t in LeaveConfigurations)
                {
                    if (t.Id != leaveConfig.Id)
                        continue;

                    t.AmountDays = leaveConfig.AmountDays;
                    t.LeaveEndOfLife = leaveConfig.LeaveEndOfLife;
                    t.LeaveType = leaveConfig.LeaveType;
                    t.ValidDays = leaveConfig.ValidDays;
                }
            }

        }
    }
}