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

        /*
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
                //Wedding = wedding,
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
    }*/

        [HttpDelete]
        [Route("Couple/{Id}")]
        public ActionResult Index(int Id)
        {

            try
            {
                var db = new DB.WPlanningDBEntities();
                DB.Couple couple = db.Couple.First(c => c.CoupleId == Id);
                if (couple == null || couple.Status != "ACT")
                    return HttpNotFound("No existe pareja");
                couple.Status = "INA";
                db.SaveChanges();
                return Content("true");
            }
            catch(Exception ex)
            {
                mostrarMensajeException(ex);
                return new HttpStatusCodeResult(500);
            }
        }




        [HttpPut]
        public ActionResult Index(UpdateCoupleDataModel updateCoupleDataModel)
        {
            try
            {
                var db = new DB.WPlanningDBEntities();
                DB.Couple couple = db.Couple.First(c => c.CoupleId == updateCoupleDataModel.Id);
                if (couple == null || couple.Status != "ACT")
                    return HttpNotFound("No existe pareja");
                couple.LastNameIdentifier = updateCoupleDataModel.LastName;
                couple.SharedEmail = updateCoupleDataModel.SharedEmail;
                couple.Address = updateCoupleDataModel.Address;
                couple.Phone = updateCoupleDataModel.Phone;
                couple.Usr = updateCoupleDataModel.CredsUser;
                couple.Password = updateCoupleDataModel.CredsPassword;
                db.SaveChanges();
                db.Dispose();
                return Content("true");
            }
            catch (Exception ex)
            {
                mostrarMensajeException(ex);
                return new HttpStatusCodeResult(500);
            }

        }

        [Route("Couple/validateCredentials")]
        public ActionResult validateCredentials(string user, string password)
        {
            try
            {
                var db = new DB.WPlanningDBEntities();
                var couple = db.Couple.Where(x => x.Usr == user && password == x.Password).First();
                if (couple.Status != "ACT")
                    return HttpNotFound();
                return Content("" + couple.CoupleId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return HttpNotFound();
                //return Content("-1");
            }
        }
        public class UpdateCoupleDataModel
        {
            public int Id { get; set; }
            public string LastName { get; set; }
            public string SharedEmail { get; set; }
            public string Address { get; set; }
            public string Phone { get; set; }
            public string CredsUser { get; set; }
            public string CredsPassword { get; set; }
        }

    }
}