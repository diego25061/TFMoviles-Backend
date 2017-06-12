using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WPlanningAPI.Controllers
{
    public class WeddingController : BaseController
    {

        [HttpPost]
        public ActionResult Index(CoupleWeddingDataModel CoupleWedding)
        {

            try
            {
                var db = new DB.WPlanningDBEntities();
                DB.Couple couple = new DB.Couple()
                {
                    SharedEmail = CoupleWedding.SharedEmail,
                    Usr = CoupleWedding.CredsUser,
                    Password = CoupleWedding.CredsPassword,
                    WPlannerId = CoupleWedding.PlannerId,
                    Address = CoupleWedding.Address,
                    LastNameIdentifier = CoupleWedding.LastName,
                    Status = "ACT",
                    Phone = CoupleWedding.Phone
                };

                db.Couple.Add(couple);
                db.SaveChanges();

                DB.Wedding wedding = new DB.Wedding()
                {
                    CoupleId = couple.CoupleId,
                    Date = new DateTime(CoupleWedding.WedDateYear, CoupleWedding.WedDateMonth, CoupleWedding.WedDateDay),
                    InitialBudget = CoupleWedding.WedBudget,
                    Location = CoupleWedding.WedAddress,
                    QuantityInvitations = CoupleWedding.WedGuestQuantity,
                };


                db.Wedding.Add(wedding);
                db.SaveChanges();
                return Content("" + wedding.WeddingId);
            }
            catch (Exception ex)
            {
                mostrarMensajeException(ex);
                return Content("-1");
            }
        }

        public class CoupleWeddingDataModel
        {
            public string LastName { get; set; }
            public string SharedEmail { get; set; }
            public string Address { get; set; }
            public string Phone { get; set; }
            public string CredsUser { get; set; }
            public string CredsPassword { get; set; }
            public float WedBudget { get; set; }
            public int WedDateYear { get; set; }
            public int WedDateMonth { get; set; }
            public int WedDateDay { get; set; }
            public string WedAddress { get; set; }
            public int WedGuestQuantity { get; set; }
            public int PlannerId { get; set; }
        }

    }
}