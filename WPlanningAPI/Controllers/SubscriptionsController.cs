using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Helpers;
using WPlanningAPI.Models;
using System.Web.Http.Results;

namespace WPlanningAPI.Controllers
{
    public class SubscriptionsController : Controller
    {
        // GET: api/Subscriptions
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Subscriptions/5
        public JsonResult Get(int id)
        {
            var a = new Subscription { Name = "hi" };
            return Json( a);
        }

        // POST: api/Subscriptions
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Subscriptions/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Subscriptions/5
        public void Delete(int id)
        {
        }

        //=======================================================!!!!!
        
        public JsonResult List(){
            return Json(new Subscription[]{
                new Subscription {Name="PRO" },
                new Subscription {Name="Basic bitch" }
            }, JsonRequestBehavior.AllowGet);
        }

    }
}
