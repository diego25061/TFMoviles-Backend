using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WPlanningAPI.Models;

namespace WPlanningAPI.Controllers
{
    public class WPlannerController : Controller
    {
        // GET: Planner
        public JsonResult Index()
        {
            var db = new WPlanningDBEntities();
            return Json(db.WPlanner.ToList<WPlanner>(), JsonRequestBehavior.AllowGet);
        }


        public ActionResult Generate(int? Quantity)
        {
            if (!Quantity.HasValue)
            {
                return HttpNotFound("Generate method requires quantity");
            }

            int quantity = Quantity.Value;
            List<WPlanner> planners = null;

            for (int i = 0 ; i < quantity; i++ )
            {
                WPlanner planner = new WPlanner { Address = "Larry", Password = "97zxc72890d", Usr = "Lar98" };
            }
            
            return Json(planners, JsonRequestBehavior.AllowGet);
        }
    }
}