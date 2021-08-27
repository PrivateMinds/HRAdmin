using HRDataAccess;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRAdmin.Pages.ViewComponents
{
    [ViewComponent(Name = "EmployeeDetails")]
    public class EmployeeDetails : ViewComponent
    {
        private readonly IHRAdminRepository _IHRAdminRepository;
        public EmployeeDetails(IHRAdminRepository IHRAdminRepository)
        {
            _IHRAdminRepository = IHRAdminRepository;

        }
        public IViewComponentResult Invoke(int EmpID = 0)
        {
            return View();
        }
    }
}
