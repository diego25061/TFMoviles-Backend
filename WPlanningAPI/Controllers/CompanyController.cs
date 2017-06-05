using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Helpers;
using WPlanningAPI.Models.DB;

namespace WPlanningAPI.Controllers
{
    public class CompanyController : Controller
    {
        // GET: Company
        public ActionResult Index()
        {
            var db = new WPlanningDBEntities();
            List<Models.WPlannerCompany> companyList = new List<Models.WPlannerCompany>();
            foreach ( var dbCompany in db.WPlannerCompany)
            {
                companyList.Add(Models.WPlannerCompany.buildFullFromDb(dbCompany));
            }
            return Json(companyList, JsonRequestBehavior.AllowGet);
        }
        [Route("Company/{Id}")]
        public ActionResult Index(int Id)
        {
            var db = new WPlanningDBEntities();
            var company = db.WPlannerCompany.Where(x => x.WPlannerCompanyId == Id).First();
            return Json(Models.WPlannerCompany.buildFullFromDb(company), JsonRequestBehavior.AllowGet);
        }
    }
}