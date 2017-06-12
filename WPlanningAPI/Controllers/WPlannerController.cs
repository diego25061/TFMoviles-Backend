using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WPlanningAPI.DB ;

namespace WPlanningAPI.Controllers
{
    public class WPlannerController : BaseController
    {
        // GET: Planner
        public JsonResult Index()
        {
            var db = new WPlanningDBEntities();
            List<Models.WPlanner> wPlannersList = new List<Models.WPlanner>();
            var dbList = db.WPlanner.ToList<DB.WPlanner>();
            foreach ( var dbPlanner in dbList){
                wPlannersList.Add(Models.WPlanner.buildFromDb(dbPlanner));
            }
            return Json(wPlannersList, JsonRequestBehavior.AllowGet);
        }

        [Route("WPlanner/{Id}")]
        public JsonResult Index(int Id)
        {
            //return Json(db.WPlanner.Where(w => w.WPlannerId == Id).First(),JsonRequestBehavior.AllowGet);
            var db = new WPlanningDBEntities();
            return Json(Models.WPlanner.buildFromDb(db.WPlanner.Where(w => w.WPlannerId == Id).First()), JsonRequestBehavior.AllowGet);
        }

        /*
        public ActionResult Generate(int? Quantity)
        {
            if (!Quantity.HasValue)
            {
                return HttpNotFound("Generate method requires quantity");
            }

            int quantity = Quantity.Value;
            List<Models.DB.WPlanner> planners = null;

            for (int i = 0 ; i < quantity; i++ )
            {
                Models.DB.WPlanner planner = new Models.DB.WPlanner { Address = "Larry", Password = "97zxc72890d", Usr = "Lar98" };
            }
            
            return Json(planners, JsonRequestBehavior.AllowGet);
        }
        */

        public ActionResult ValidateCredentials(string User, string Password){
            if ( isEmptyOrSpaces(User) || isEmptyOrSpaces(Password))
            {
                return HttpNotFound("Parameters missing");
            }
            var db = new WPlanningDBEntities();
            var planner = db.WPlanner.Where(w => w.Password.Equals(Password) && w.Usr.Equals(User)).FirstOrDefault();
            if (planner != null)
            {
                return Content(""+planner.WPlannerId);
            }
            return Content(""+(-1));
        }

        //[HttpPut]
        [Route("WPlanner/{Id}/ResetPassword")]
        public ActionResult ResetPassword(int Id)
        {
            try
            {
                var db = new WPlanningDBEntities();
                var planner = db.WPlanner.Where(x => x.WPlannerId == Id).First();
                planner.Password = Models.WPlanner.generatePassword();
                db.SaveChanges();
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Content("false");
            }
            return Content("true");
            //return RedirectToAction("" + Id);
        }

    }
}