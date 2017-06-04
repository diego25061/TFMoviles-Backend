using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WPlanningAPI.Models;

namespace WPlanningAPI.Controllers
{
    public class SubscriptionTypeController : BaseController
    {
        // GET: SubscriptionType
        public JsonResult Index()
        {
            /*
            var db = new WPlanningDBEntities();
            List<SubscriptionType> subscriptionsList = db.SubscriptionType.ToList<SubscriptionType>();

            var list = JsonConvert.SerializeObject(subscriptionsList, Formatting.None , new JsonSerializerSettings()
            {
                ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            });

            List<object> lista = new List<object>();

            foreach (SubscriptionType sub in subscriptionsList)
            {
                lista.Add(new { Months = sub.Months , WPlanners = sub.QuantityWPlanners, Description = sub.Description, Name = sub.Name, Cost = sub.Cost });
            }
            
            */

            var lista = SubscriptionType.getAllFromDb();
            
            return Json(new { lista }, JsonRequestBehavior.AllowGet);
            //return View();

        }
        

        
        
    }
}