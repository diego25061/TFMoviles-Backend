using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Helpers;

namespace WPlanningAPI.Controllers
{
    public class CompanyController : Controller
    {
        // GET: Company
        [HttpGet]
        public ActionResult Index()
        {
            try
            {
                List<Models.WPlannerCompany> companyList = new List<Models.WPlannerCompany>();
                var db = new DB.WPlanningDBEntities();

                foreach (var dbCompany in db.WPlannerCompany)
                {
                    companyList.Add(Models.WPlannerCompany.buildFullFromDb(dbCompany));
                }
                return Json(companyList, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return Json(null);
                //throw new Exception();
            }

        }


        [Route("Company/{Id}")]
        public ActionResult Index(int Id)
        {
            var db = new DB.WPlanningDBEntities();
            var company = db.WPlannerCompany.Where(x => x.WPlannerCompanyId == Id).First();
            return Json(Models.WPlannerCompany.buildFullFromDb(company), JsonRequestBehavior.AllowGet);
        }

        [Route("Company/validateCredentials")]
        public ActionResult validateCredentials(string user, string password)
        {
            var db = new DB.WPlanningDBEntities();
            try
            {
                var company = db.WPlannerCompany.Where(x => x.Usr == user && x.Password == password).First();
                return Content("" + company.WPlannerCompanyId);
            }
            catch (Exception ex)
            {
                return Content("-1");
            }
        }


        //=================================================================================================================================
        
        [HttpPost]
        public ActionResult Index(CreateCompanyDataModel dataModel)
        {
            try
            {
                var db = new DB.WPlanningDBEntities();
                DB.WPlannerCompany company = new DB.WPlannerCompany
                {
                    CompanyName = dataModel.Name,
                    Email = dataModel.Email,
                    Usr = dataModel.User,
                    Password = dataModel.Password,
                    Phone = dataModel.Phone,
                    Address = dataModel.Address,
                    SubscriptionType = db.SubscriptionType.Where(x => x.SubscriptionTypeId == dataModel.SubscriptionType).First()
                };
                db.WPlannerCompany.Add(company);
                db.SaveChanges();
                return Content("" + company.WPlannerCompanyId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return Content("-1");
            }
        }

        public class CreateCompanyDataModel
        {
            public string Name { get; set; }
            public string Email { get; set; }
            public string User { get; set; }
            public string Password { get; set; }
            public string Phone { get; set; }
            public string Address { get; set; }
            public int SubscriptionType { get; set; }
        }
    }
}