using HRDataAccess;
using HRModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRAdmin.Pages
{
    public class IndexModel : PageModel
    {
       // private readonly ILogger<IndexModel> _logger;
        private readonly IHRAdminRepository _IHRAdminRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
       // string UserID = "";

        public List<viewActiveEmployees_HRapp> lstViewActiveEmployees = new List<viewActiveEmployees_HRapp>();
        public viewActiveEmployees_HRapp ViewActiveEmployees = new viewActiveEmployees_HRapp();
        public SelectList ActiveEmpStatusSelectList;
        GlobalModel GlobalModel = new GlobalModel(); 
        public List<viewActiveEmployees_HRapp> StatusList { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IHRAdminRepository"></param>
        /// <param name="httpContextAccessor"></param>
        public IndexModel(IHRAdminRepository IHRAdminRepository, IHttpContextAccessor httpContextAccessor)
        {
            _IHRAdminRepository = IHRAdminRepository;
            _httpContextAccessor = httpContextAccessor;



            //var uniqueCategories = lstViewActiveEmployees
            //                       .Select(p => p.EmployeeStatus)
            //                       .Distinct();

            //This is the list that will be used to populate Status dropdown
            lstViewActiveEmployees = _IHRAdminRepository.GetActiveEmployees();
            lstViewActiveEmployees.Insert(0, new viewActiveEmployees_HRapp { EmployeeStatus = "...Select Status" });
            var ActiveEmpStatusOrder = lstViewActiveEmployees.Select(A => new
            {
                Status = A.Status,
                EmployeeStatus = string.Format("{0} ", A.EmployeeStatus)

            }).Distinct();
            ActiveEmpStatusSelectList = new SelectList(ActiveEmpStatusOrder, "Status", "EmployeeStatus");

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewActiveEmployees_HRapp"></param>
        /// <returns></returns>
        public IActionResult OnPostEmployeeSearchResults(viewActiveEmployees_HRapp viewActiveEmployees_HRapp, string SortColumn = "", string SortOrder = "")
        {
            //ViewActiveEmployees.EmpID = EmpID;
            return ViewComponent("EmployeeSearchResults", new { viewActiveEmployees_HRapp = viewActiveEmployees_HRapp, SortColumn = SortColumn, SortOrder = SortOrder });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewActiveEmployees_HRapp"></param>
        /// <param name="SortColumn"></param>
        /// <param name="SortOrder"></param>
        /// <returns></returns>
        public IActionResult OnPostSortColumn(viewActiveEmployees_HRapp viewActiveEmployees_HRapp, string SortColumn = "", string SortOrder = "")
        {
            GlobalModel.viewActiveEmployees_HRapp = viewActiveEmployees_HRapp;
            GlobalModel.SortColumn = SortColumn;
            GlobalModel.SortOrder = SortOrder;

            return ViewComponent("EmployeeSearchResults",new  { viewActiveEmployees_HRapp = viewActiveEmployees_HRapp, SortColumn = SortColumn, SortOrder = SortOrder });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="EmpID"></param>
        /// <returns></returns>
        public IActionResult OnGetEmployeeDetails(int EmpID = 0)
        {
            return ViewComponent("EmployeeDetails", EmpID);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>
        public JsonResult OnGetEmployeeName(string term)
        {
            List<string> ActiveEmployee = null;
           // lstViewActiveEmployees = _IHRAdminRepository.GetActiveEmployees();

            if (term != null)
            {
                //Search by both first or last name

                ActiveEmployee = _IHRAdminRepository.GetActiveEmployees()
                                    .Where(s => s.FirstName.ToUpper().StartsWith(term.Trim().ToUpper())
                                    || s.LastName.ToUpper().StartsWith(term.Trim().ToUpper())
                                    || (s.FirstName.ToUpper() + " " +
                                       s.LastName.ToUpper()).StartsWith(term.Trim().ToUpper()))
                                     .Select(x => x.LastName + " " + x.FirstName ).ToList();

            }

            return new JsonResult(ActiveEmployee, new Newtonsoft.Json.JsonSerializerSettings());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="term"></param>
        /// <returns>Employee NUID</returns>
        public JsonResult OnGetEmployeeNUID(string term)
        {
            List<string> NUIDList = null;
           // lstViewActiveEmployees = _IHRAdminRepository.GetActiveEmployees();
           

            if (term != null)
            {
                NUIDList = _IHRAdminRepository.GetActiveEmployees().Where(s => s.EmpID.ToUpper().StartsWith(term.Trim().ToUpper()))
                    .Select(x => x.EmpID).ToList();

            }

            return new JsonResult(NUIDList, new Newtonsoft.Json.JsonSerializerSettings());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="term"></param>
        /// <returns>Persons Number is HREmpID</returns>
        public JsonResult OnGetPersonsNumber(string term)
        {
            List<string> HREmpIDList = null;
            
            if (term != null)
            {
                HREmpIDList = _IHRAdminRepository.GetActiveEmployees().Where(s => s.HREmpID.StartsWith(term.Trim().ToUpper()))
                    .Select(x => x.HREmpID).ToList();

            }

            return new JsonResult(HREmpIDList, new Newtonsoft.Json.JsonSerializerSettings());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="term"></param>
        /// <returns>PositionNumber</returns>

        public JsonResult OnGetPositionNumber(int term)
        {
            List<int> HREmpIDList = null;

           
                HREmpIDList = _IHRAdminRepository.GetActiveEmployees().Where(s => s.PositionID.Equals(term))
                    .Select(x => x.PositionID).ToList();

            return new JsonResult(HREmpIDList, new Newtonsoft.Json.JsonSerializerSettings());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="term"></param>
        /// <returns>Supervisor</returns>
        public JsonResult OnGetSupervisor(string term)
        {
            List<string> ActiveEmployee = null;
            // lstViewActiveEmployees = _IHRAdminRepository.GetActiveEmployees();

            if (term != null)
            {
                

                ActiveEmployee = _IHRAdminRepository.GetActiveEmployees()
                                    .Where(s => s.SupFirstName.ToUpper().StartsWith(term.Trim().ToUpper())
                                    || s.SupLastName.ToUpper().StartsWith(term.Trim().ToUpper())
                                    || (s.SupFirstName.ToUpper() + " " +
                                       s.SupLastName.ToUpper()).StartsWith(term.Trim().ToUpper()))
                                     .Select(x => x.SupLastName + " " + x.SupFirstName).Distinct().ToList();

            }

            return new JsonResult(ActiveEmployee, new Newtonsoft.Json.JsonSerializerSettings());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>
        public JsonResult OnGetJobCode(int term)
        {
            List<int> JobCodeList = null;

            JobCodeList = _IHRAdminRepository.GetActiveEmployees().Where(s => s.JobCode.Equals(term))
                .Select(x => x.JobCode).Distinct().ToList();

            return new JsonResult(JobCodeList, new Newtonsoft.Json.JsonSerializerSettings());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>
        public JsonResult OnGetJobTitle(string term)
        {
            List<string> JobTitleList = null;

            JobTitleList = _IHRAdminRepository.GetActiveEmployees().Where(s => s.JobTitle.ToUpper().StartsWith(term.Trim().ToUpper()))
                .Select(x => x.JobTitle).Distinct().ToList();

            return new JsonResult(JobTitleList, new Newtonsoft.Json.JsonSerializerSettings());
        }
    }
}
