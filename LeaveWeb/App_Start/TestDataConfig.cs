using LeaveLib.Domain;
using LeaveLib.Infra;

namespace LeaveWeb
{
    public class TestDataConfig
    {
        public static void Create()
        {
            CreateLeaveConfiguration();
            CreateEmployees();
            CreateLeave();
        }

        private static void CreateEmployees()
        {
            EmployeeRepository repository = new EmployeeRepository();

            repository.SaveOrUpdate(new Employee("John Doe"));
            repository.SaveOrUpdate(new Employee("Jane Doe"));
            repository.SaveOrUpdate(new Employee("Mary Sue"));
            repository.SaveOrUpdate(new Employee("Travis McMac"));
            repository.SaveOrUpdate(new Employee("Harvey Davidson"));
            repository.SaveOrUpdate(new Employee("ElNino Kawasaki"));
        }

        private static void CreateLeave()
        {
            EmployeeRepository employeeRepository = new EmployeeRepository();
            LeaveConfigurationRepository leaveConfigurationRepository = new LeaveConfigurationRepository();
            LeaveRepository leaveRepository = new LeaveRepository();

            LeaveGenerator generator = new LeaveGenerator();

            foreach (Employee employee in employeeRepository.GetAll())
            {
                foreach (var leaveConfig in leaveConfigurationRepository.GetAll())
                {
                    Leave leave =  generator.Generate(employee, leaveConfig);

                    employee.LeaveList.Add(leave);

                    leaveRepository.SaveOrUpdate(leave);
                }
            }
        }
        
        private static void CreateLeaveConfiguration()
        {
            LeaveConfigurationRepository repository = new LeaveConfigurationRepository();

            repository.SaveOrUpdate(new LeaveConfiguration()
            {
                AmountDays = 30,
                LeaveEndOfLife = LeaveEndOfLife.Rollover,
                LeaveType = LeaveType.Sick,
                ValidDays = 365*3,
            });

            repository.SaveOrUpdate(new LeaveConfiguration()
            {
                AmountDays = 20,
                LeaveEndOfLife = LeaveEndOfLife.Expire,
                LeaveType = LeaveType.Normal,
                ValidDays = 365,
            });

            repository.SaveOrUpdate(new LeaveConfiguration()
            {
                AmountDays = 3,
                LeaveEndOfLife = LeaveEndOfLife.Expire,
                LeaveType = LeaveType.Family,
                ValidDays = 365,
            });

        }
    }
}