namespace LeaveLib.Domain
{
    public class LeaveConfiguration
    {
        public int Id { get; set; }

        public int AmountDays { get; set; }

        public int ValidDays { get; set; }

        public LeaveType LeaveType { get; set; }

        public LeaveEndOfLife LeaveEndOfLife { get; set; }

    }
}