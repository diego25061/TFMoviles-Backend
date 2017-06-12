using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WPlanningAPI.Models;

namespace WPlanningAPI.Controllers
{
    public class CoupleController : BaseController
    {
        // GET: Couple

        /*
        public ActionResult Index()
        {
            return View();
        }
        */


        [HttpPost]
        public ActionResult Index(Couple couple)
        {
            try
            {
                DB.Wedding wedding = new DB.Wedding();

                var db = new DB.WPlanningDBEntities();

                var dbCouple = new DB.Couple()
                {
                    Address = couple.Address,
                    LastNameIdentifier = couple.LastName,
                    Password = couple.Password,
                    Phone = couple.Phone,
                    SharedEmail = couple.SharedEmail,
                    Status = "ACT",
                    Usr = couple.Usr,
                    Wedding = wedding,
                    WPlannerId = 0
                };
                db.Couple.Add(dbCouple);
                db.SaveChanges();
                return Content("" + dbCouple.CoupleId);
            }catch(Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return Content("-1");
            }
        }
        


        [Route("Couple/validateCredentials")]
        public ActionResult validateCredentials(string user, string password)
        {
            try
            {
                var db = new DB.WPlanningDBEntities();
                var couple = db.Couple.Where(x => x.Usr == user && password == x.Password).First();
                return Content("" +couple.CoupleId);
            }catch(Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return Content("-1");
            }
        }
        
    }
}