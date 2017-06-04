using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WPlanningAPI.Models;

namespace WPlanningAPI.Controllers
{
    public class SubscriptionTypeController : Controller
    {
        // GET: SubscriptionType
        public JsonResult Index()
        {
            var db = new WPlanningDBEntities();
            var list = db.SubscriptionType.ToList<SubscriptionType>();
            return Json(list, JsonRequestBehavior.AllowGet);
            //return View();

        }

        
        
    }
}