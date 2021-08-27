using HRDataAccess;
using HRModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRAdmin.Pages.ViewComponents
{
    [ViewComponent(Name = "EmployeeSearchResults")]
    public class EmployeeSearchResults : ViewComponent
    {
        private readonly IHRAdminRepository _IHRAdminRepository;
        GlobalModel GlobalModel = new GlobalModel();
        public EmployeeSearchResults(IHRAdminRepository IHRAdminRepository)
        {
            _IHRAdminRepository = IHRAdminRepository;
           
        }
        /// <summary>
        /// This method will filter employee data based on users search criteria.
        /// </summary>
        /// <param name="viewActiveEmployees_HRapp"></param>
        /// <returns></returns>
        public IViewComponentResult Invoke(viewActiveEmployees_HRapp viewActiveEmployees_HRapp, string SortColumn = "", string SortOrder = "")
        {
            var Firstname = "";
            var LastName = "";
            var SupFirstname = "";
            var SupLastName = "";

            //if (SortColumn == "")
            //{


                if (viewActiveEmployees_HRapp != null)
                {
                    if (viewActiveEmployees_HRapp.LastName != null)
                    {
                        Firstname = viewActiveEmployees_HRapp.LastName.Substring(viewActiveEmployees_HRapp.LastName.IndexOf(" ") + 1);// get everything after openig"DOUBLE QUOTE" which is th empID 
                        LastName = viewActiveEmployees_HRapp.LastName.Substring(0, viewActiveEmployees_HRapp.LastName.IndexOf(" ")).Trim(); //get everything before opening " which is the empname.
                    }
                    if (viewActiveEmployees_HRapp.SupFirstName != null)
                    {
                        SupFirstname = viewActiveEmployees_HRapp.SupFirstName.Substring(viewActiveEmployees_HRapp.SupFirstName.IndexOf(" ") + 1);
                        SupLastName = viewActiveEmployees_HRapp.SupFirstName.Substring(0, viewActiveEmployees_HRapp.SupFirstName.IndexOf(" ")).Trim();
                    }
                    if (viewActiveEmployees_HRapp.Status == null) { viewActiveEmployees_HRapp.Status = ""; }
                    if (viewActiveEmployees_HRapp.EmpID == null) { viewActiveEmployees_HRapp.EmpID = ""; }
                    if (viewActiveEmployees_HRapp.Exempt == "NULL") { viewActiveEmployees_HRapp.Exempt = ""; }
                    if (viewActiveEmployees_HRapp.HREmpID == null) { viewActiveEmployees_HRapp.HREmpID = ""; }
                    if (viewActiveEmployees_HRapp.JobTitle == null) { viewActiveEmployees_HRapp.JobTitle = ""; }

                    GlobalModel.ActiveEmployeesListData = _IHRAdminRepository.GetActiveEmployees();
                    GlobalModel.ActiveEmployeesListData = GlobalModel.ActiveEmployeesListData.Where(a => (a.FirstName.Trim() == Firstname.Trim() || Firstname.Trim() == "")
                                                                                                && (a.LastName.Trim() == LastName.Trim() || LastName.Trim() == "")
                                                                                                && (a.EmpID.Trim() == viewActiveEmployees_HRapp.EmpID.Trim() || viewActiveEmployees_HRapp.EmpID.Trim() == "")
                                                                                                && (a.Status.Trim() == viewActiveEmployees_HRapp.Status.Trim() || viewActiveEmployees_HRapp.Status.Trim() == "")
                                                                                                && (a.Exempt.Trim() == viewActiveEmployees_HRapp.Exempt.Trim() || viewActiveEmployees_HRapp.Exempt.Trim() == "")
                                                                                                && (a.PositionID == viewActiveEmployees_HRapp.PositionID || viewActiveEmployees_HRapp.PositionID == 0)
                                                                                                && (a.HREmpID.Trim() == viewActiveEmployees_HRapp.HREmpID.Trim() || viewActiveEmployees_HRapp.HREmpID.Trim() == "")
                                                                                                && (a.SupFirst.Trim() == SupFirstname.Trim() || SupFirstname.Trim() == "")
                                                                                                && (a.SupLast.Trim() == SupLastName.Trim() || SupLastName.Trim() == "")
                                                                                                && (a.JobTitle.Trim() == viewActiveEmployees_HRapp.JobTitle.Trim() || viewActiveEmployees_HRapp.JobTitle.Trim() == "")
                                                                                                && (a.JobCode == viewActiveEmployees_HRapp.JobCode || viewActiveEmployees_HRapp.JobCode == 0)).ToList();
                GlobalModel.ActiveEmployeesListData = GlobalModel.ActiveEmployeesListData.OrderBy(x => x.LastName).ThenBy(x => x.FirstName).ToList();

            }
                
            //}
            //else
            //{
               // SortColumns(SortColumn, SortOrder);
           // }
        
            if(SortColumn != "")
            {
                SortColumns(SortColumn, SortOrder);
            }
            return View(GlobalModel);
        }
        /// <summary>
        /// This method will sort columns either ASD or DSD
        /// </summary>
        /// <param name="GlobalModel"></param>
        /// <returns></returns>
        public IViewComponentResult SortColumns(string SortColumn = "", string SortOrder = "")
        {
            if (SortColumn != "")
            {
                switch (SortColumn)
                {
                    case "Name":
                        if (SortOrder == "ASD") //Default is ASD - ascending 
                        {
                            GlobalModel.ActiveEmployeesListData = GlobalModel.ActiveEmployeesListData.OrderBy(x => x.LastName).ThenBy(x => x.FirstName).ToList();
                        }
                        else
                        {
                            GlobalModel.ActiveEmployeesListData = GlobalModel.ActiveEmployeesListData.OrderByDescending(x => x.LastName).ThenBy(x => x.FirstName).ToList();

                        }

                        break;
                    case "JobTitle":
                        if (SortOrder == "ASD")
                        {
                            GlobalModel.ActiveEmployeesListData = GlobalModel.ActiveEmployeesListData.OrderBy(x => x.JobTitle).ToList();
                        }
                        else
                        {
                            GlobalModel.ActiveEmployeesListData = GlobalModel.ActiveEmployeesListData.OrderByDescending(x => x.JobTitle).ToList();

                        }

                        break;
                    case "EmpID":
                        if (SortOrder == "ASD")
                        {
                            GlobalModel.ActiveEmployeesListData = GlobalModel.ActiveEmployeesListData.OrderBy(x => x.EmpID).ToList();
                        }
                        else
                        {
                            GlobalModel.ActiveEmployeesListData = GlobalModel.ActiveEmployeesListData.OrderByDescending(x => x.EmpID).ToList();

                        }

                        break;
                    case "Status":
                        if (SortOrder == "ASD")
                        {
                            GlobalModel.ActiveEmployeesListData = GlobalModel.ActiveEmployeesListData.OrderBy(x => x.Status).ToList();
                        }
                        else
                        {
                            GlobalModel.ActiveEmployeesListData = GlobalModel.ActiveEmployeesListData.OrderByDescending(x => x.Status).ToList();

                        }

                        break;
                    case "Supervisor":
                        if (SortOrder == "ASD")
                        {
                            GlobalModel.ActiveEmployeesListData = GlobalModel.ActiveEmployeesListData.OrderBy(x => x.SupLast).ThenBy(x => x.SupFirst).ToList();
                        }
                        else
                        {
                            GlobalModel.ActiveEmployeesListData = GlobalModel.ActiveEmployeesListData.OrderByDescending(x => x.SupLast).ThenBy(x => x.SupFirst).ToList();

                        }

                        break;
                    case "FTE":
                        if (SortOrder == "ASD")
                        {
                            GlobalModel.ActiveEmployeesListData = GlobalModel.ActiveEmployeesListData.OrderBy(x => x.FTE).ToList();
                        }
                        else
                        {
                            GlobalModel.ActiveEmployeesListData = GlobalModel.ActiveEmployeesListData.OrderByDescending(x => x.FTE).ToList();

                        }

                        break;

                    default:
                        GlobalModel.ActiveEmployeesListData = GlobalModel.ActiveEmployeesListData.OrderBy(x => x.LastName).ThenBy(x => x.FirstName).ToList();
                      //  GlobalModel.ActiveEmployeesListData = GlobalModel.ActiveEmployeesListData.OrderBy(x => x.FirstName).ToList();
                        break;
                }
            }

            return View(GlobalModel);
        }
    
   }
}
