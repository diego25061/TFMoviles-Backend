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
        public ActionResult Index(AddCoupleWeddingDataModel CoupleWedding)
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

        /// <summary>
        /// Si no se da algun valor del updateWEddingdataModel, se tomará como 0 por defecto o string vacio "" por defecto. Dar siempre todo
        /// </summary>
        /// <param name="updateWeddingDataModel"></param>
        /// <returns></returns>
        [HttpPut]
        public ActionResult Index(UpdateWeddingDataModel updateWeddingDataModel)
        {
            try
            {
                if (updateWeddingDataModel.Id == 0)
                {
                    return HttpNotFound("Id not given");
                }

                var db = new DB.WPlanningDBEntities();
                DB.Wedding wedding = db.Wedding.First(x => x.WeddingId == updateWeddingDataModel.Id);
                if (wedding == null)
                {
                    return HttpNotFound("Wedding does not exist");
                }

                wedding.Location = updateWeddingDataModel.Address;
                wedding.Date = new DateTime(
                    updateWeddingDataModel.DateYear,
                    updateWeddingDataModel.DateMonth,
                    updateWeddingDataModel.DateDay
                    );

                wedding.InitialBudget = updateWeddingDataModel.Budget;
                wedding.QuantityInvitations = updateWeddingDataModel.GuestQuantity;
                db.SaveChanges();

                return Content("true");
            }
            catch (Exception ex)
            {
                mostrarMensajeException(ex);
                //return Content("-1");
                return new HttpStatusCodeResult(500);
            }
        }

        public class AddCoupleWeddingDataModel
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

        public class UpdateWeddingDataModel
        {
            public int Id { get; set; }
            public float Budget { get; set; }
            public int DateYear { get; set; }
            public int DateMonth { get; set; }
            public int DateDay { get; set; }
            public string Address { get; set; }
            public int GuestQuantity { get; set; }
        }
    }
}