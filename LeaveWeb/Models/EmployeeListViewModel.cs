using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Web;
using System.Web.Mvc;

namespace LeaveWeb.Models
{
    public class LeaveRequestListViewModel
    {
        public LeaveRequestListViewModel()
        {
            LeaveRequests = new List<LeaveRequestListItemViewModel>();
        }

        public List<LeaveRequestListItemViewModel> LeaveRequests { get; set; }
    }

    public class LeaveRequestListItemViewModel
    {
        public LeaveRequestListItemViewModel()
        {
            LeaveList = new List<SelectListItem>();
        }

        public int Id { get; set; }

        public int? LeaveId { get; set; }

        public List<SelectListItem> LeaveList { get; set; }

        public int TotalCount { get; set; }
    }

    public class EmployeeListViewModel
    {
        public EmployeeListViewModel()
        {
            EmployeeList = new List<EmployeeListItemViewModel>();
        }

        public List<EmployeeListItemViewModel> EmployeeList { get; set; }
         
    }

    public class EmployeeListItemViewModel
    {

        public EmployeeListItemViewModel()
        {
            ManagerList = new List<SelectListItem>();
        }

        public int Id { get; set; }

        public string FullName { get; set; }

        public int? ManagerId { get; set; }

        public List<SelectListItem> ManagerList { get; set; }
    }
}