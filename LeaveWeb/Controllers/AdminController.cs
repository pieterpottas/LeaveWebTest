using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LeaveLib.Domain;
using LeaveLib.Infra;
using LeaveWeb.Models;

namespace LeaveWeb.Controllers
{
    public class AdminController : Controller
    {
        public ActionResult RequestForLeave()
        {
            LeaveRequestListViewModel viewModel = new LeaveRequestListViewModel();

            LeaveRequestRepository leaveRequestRepository = new LeaveRequestRepository();
            LeaveRepository leaveRepository = new LeaveRepository();

            foreach (var leaveRequest in leaveRequestRepository.GetAll())
            {
                LeaveRequestListItemViewModel listItemViewModel = new LeaveRequestListItemViewModel();

                listItemViewModel.Id = leaveRequest.Id;
                listItemViewModel.TotalCount = leaveRequest.TotalCount;
                listItemViewModel.LeaveId = leaveRequest.Leave?.Id;

                listItemViewModel.LeaveList.Add(new SelectListItem()
                {
                    Value = null,
                    Text = "",
                });

                listItemViewModel.LeaveList.AddRange(leaveRepository.GetAll().Select(s => new SelectListItem()
                {
                    Value = s.Id.ToString(),
                    Text = String.Format("{0}-{1}",s.LeaveType,s.Employee?.FullName),
                    Selected = s.Id == leaveRequest.Leave?.Id,
                    Disabled = false
                }));

                viewModel.LeaveRequests.Add(listItemViewModel);
            }

            LeaveRequestListItemViewModel listItemViewModel2 = new LeaveRequestListItemViewModel() {Id = 0};

            listItemViewModel2.LeaveList.Add(new SelectListItem()
            {
                Value = null,
                Text = "",
            });

            listItemViewModel2.LeaveList.AddRange(leaveRepository.GetAll().Select(s => new SelectListItem()
            {
                Value = s.Id.ToString(),
                Text = String.Format("{0}-{1}", s.LeaveType, s.Employee?.FullName),
                Selected = false,
                Disabled = false
            }));

            viewModel.LeaveRequests.Add(listItemViewModel2);

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult RequestForLeave(FormCollection fc)
        {
            LeaveRequestListViewModel leaveRequestListViewModel = new LeaveRequestListViewModel();

            TryUpdateModel(leaveRequestListViewModel);

            if (ModelState.IsValid)
            {
                LeaveRequestRepository repository = new LeaveRequestRepository();
                LeaveRepository leaveRepository = new LeaveRepository();

                foreach (var itemViewModel in leaveRequestListViewModel.LeaveRequests)
                {
                    LeaveRequest leaveRequest = repository.GetById(itemViewModel.Id);
                    Leave leave = leaveRepository.GetById(itemViewModel.LeaveId ?? 0);

                    if (leaveRequest == null)
                    {
                        //insert
                        if (itemViewModel.TotalCount > 0 && itemViewModel.Id == 0)
                        {
                            leaveRequest = new LeaveRequest(leave, itemViewModel.TotalCount);
                            repository.SaveOrUpdate(leaveRequest);
                        }
                    }
                    else
                    {
                        //update
                        leaveRequest.Leave = leave;
                        leaveRequest.TotalCount = itemViewModel.TotalCount;
                        repository.SaveOrUpdate(leaveRequest);
                    }
                }
                return RedirectToAction("RequestForLeave");
            }

            return RequestForLeave();
        }

        [HttpGet]
        public ActionResult Employee()
        {
            EmployeeListViewModel employeeListViewModel = new EmployeeListViewModel();

            EmployeeRepository repository = new EmployeeRepository();

            foreach (var employee in repository.GetAll())
            {
                EmployeeListItemViewModel employeeListItemViewModel = new EmployeeListItemViewModel()
                {
                    Id = employee.Id,
                    FullName = employee.FullName,
                    ManagerId = employee.Manager?.Id,  
                };

                employeeListItemViewModel.ManagerList.Add(new SelectListItem()
                {
                    Value = null,
                    Text = "",
                });

                employeeListItemViewModel.ManagerList.AddRange(repository.GetAll().Where(w => employee.Id != w.Id).Select(s => new SelectListItem()
                {
                    Value = s.Id.ToString(),
                    Text = s.FullName,
                    Selected = s.Id == employee.Manager?.Id,
                    Disabled = false
                }));

                employeeListViewModel.EmployeeList.Add(employeeListItemViewModel);
            }

            EmployeeListItemViewModel listItemViewModel = new EmployeeListItemViewModel() {Id = 0};

            listItemViewModel.ManagerList.Add(new SelectListItem()
            {
                Value = null,
                Text = "",
            });

            listItemViewModel.ManagerList.AddRange(repository.GetAll().Select(s => new SelectListItem()
            {
                Value = s.Id.ToString(),
                Text = s.FullName,
                Disabled = false
            }).ToArray());

            employeeListViewModel.EmployeeList.Add(listItemViewModel);

            return View(employeeListViewModel);
        }

        [HttpPost]
        public ActionResult Employee(FormCollection formCollection)
        {
            EmployeeListViewModel employeeListViewModel = new EmployeeListViewModel();

            TryUpdateModel(employeeListViewModel);

            if (ModelState.IsValid)
            {
                EmployeeRepository repository = new EmployeeRepository();
                LeaveConfigurationRepository leaveConfigurationRepository = new LeaveConfigurationRepository();

                foreach (EmployeeListItemViewModel itemViewModel in employeeListViewModel.EmployeeList)
                {
                    Employee employee = repository.GetById(itemViewModel.Id);

                    if (employee == null )
                    {
                        //insert
                        if (String.IsNullOrWhiteSpace(itemViewModel.FullName) == false && itemViewModel.Id == 0)
                        {
                            employee = new Employee(itemViewModel.FullName) {Manager = repository.GetById(itemViewModel.ManagerId ?? 0)};

                            LeaveGenerator generator = new LeaveGenerator();
                            LeaveRepository leaveRepository = new LeaveRepository();

                            foreach (var leaveConfig in leaveConfigurationRepository.GetAll())
                            {
                                Leave leave = generator.Generate(employee, leaveConfig);

                                employee.LeaveList.Add(leave);

                                leaveRepository.SaveOrUpdate(leave);
                            }
                            
                            repository.SaveOrUpdate(employee);
                        }
                    }
                    else
                    {
                        //update
                        employee.FullName = itemViewModel.FullName;
                        employee.Manager = repository.GetById(itemViewModel.ManagerId ?? 0);
                        repository.SaveOrUpdate(employee);
                    }
                }
                
                return RedirectToAction("Employee");
            }

            return Employee();
        }
    }
}