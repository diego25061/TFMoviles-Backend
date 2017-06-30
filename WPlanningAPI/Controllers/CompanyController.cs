using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Helpers;

namespace WPlanningAPI.Controllers
{
    public class CompanyController : BaseController
    {
        // GET: Company
        /*
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
        */

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

                for (int i = 0; i< company.SubscriptionType.QuantityWPlanners; i++)
                {
                    DB.WPlanner planner = new DB.WPlanner
                    {
                        Usr = Models.WPlanner.generateName(company),
                        Password = Models.WPlanner.generatePassword(),
                        WPlannerCompanyId = company.WPlannerCompanyId
                    };
                    db.WPlanner.Add(planner);
                }

                db.SaveChanges();
                return Content("" + company.WPlannerCompanyId);
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.StackTrace);
                mostrarMensajeException(ex);
                return new HttpStatusCodeResult(500);
                //Content("-1");
            }
        }

        [Route("Company/validateCredentials")]
        public ActionResult validateCredentials(string user, string password)
        {
            var db = new DB.WPlanningDBEntities();
            try
            {
                var resultSet = db.WPlannerCompany.Where(x => x.Usr == user && x.Password == password);
                if (resultSet.Count() == 0)
                    return Content("-2");

                return Content("" + resultSet.First().WPlannerCompanyId);

            }
            catch (Exception ex)
            {
                mostrarMensajeException(ex);
                return Content("-1");
            }
        }

        [Route("Company/{Id}")]
        public ActionResult Index(int Id)
        {
            /*
            var db = new DB.WPlanningDBEntities();
            var company = db.WPlannerCompany.Where(x => x.WPlannerCompanyId == Id).First();
            return Json(Models.WPlannerCompany.buildFullFromDb(company), JsonRequestBehavior.AllowGet);*/
            try
            {
                var db = new DB.WPlanningDBEntities();
                var dbCompany = db.WPlannerCompany.Where(x => x.WPlannerCompanyId == Id).First();
                ViewCompanyDataModel company;
                company = new ViewCompanyDataModel
                {
                    Name = dbCompany.CompanyName,
                    Address = dbCompany.Address,
                    Email = dbCompany.Email,
                    Phone = dbCompany.Phone,
                    Id = dbCompany.WPlannerCompanyId,
                    SubscriptionName = dbCompany.SubscriptionType.Name
                };
                company.PlannerUsers = new List<ViewCompanyDataModel.PlannerViewCompanyDataModel>();
                foreach (var dbPlanner in dbCompany.WPlanner)
                {
                    company.PlannerUsers.Add(new ViewCompanyDataModel.PlannerViewCompanyDataModel
                    {
                        Id = dbPlanner.WPlannerId,
                        Password = dbPlanner.Password,
                        User = dbPlanner.Usr
                    });
                }
                return Json(company, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                mostrarMensajeException(ex);
                return HttpNotFound(ex.Message);
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


        public class ViewCompanyDataModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
            public string Phone { get; set; }
            public string Address { get; set; }
            public string SubscriptionName { get; set; }
            public List<PlannerViewCompanyDataModel> PlannerUsers;

            public class PlannerViewCompanyDataModel
            {
                public int Id { get; set; }
                public string User { get; set; }
                public string Password { get; set; }
            }
        }

    }
}