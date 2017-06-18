using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WPlanningAPI.Controllers
{
    public class ActivityController : BaseController
    {
        // GET: Activity

        [HttpPost]
        public ActionResult Index(AddActivityDataModel activityDataModel)
        {
            try
            {
                var db = new DB.WPlanningDBEntities();
                var wedding = db.Wedding.First(x => x.WeddingId == activityDataModel.WeddingId);
                if (wedding == null)
                    return HttpNotFound("Wedding for activity not found");
                var actividad = new DB.Activity
                {
                    ActivityName = activityDataModel.Name,
                    Date = new DateTime(
                        activityDataModel.DateYear,
                        activityDataModel.DateMonth,
                        activityDataModel.DateDay)
                };

                wedding.Activity.Add(actividad);

                db.SaveChanges();
                return (Content("" + actividad.ActivityId));
            }
            catch (Exception ex)
            {
                mostrarMensajeException(ex);
                return new HttpStatusCodeResult(500);
            }
        }

        [HttpDelete]
        public ActionResult Index(int Id)
        {
            try
            {
                var db = new DB.WPlanningDBEntities();
                var actividad = db.Activity.First(x => x.ActivityId == Id);
                if (actividad == null)
                    return HttpNotFound("Actividad con id inexistente");
                db.Activity.Remove(actividad);
                db.SaveChanges();
                return Content("true");
            }
            catch (Exception ex)
            {
                mostrarMensajeException(ex);
                return new HttpStatusCodeResult(500);
            }
        }

        [HttpGet]
        [Route("Wedding/{WeddingId}/Activity")]
        public ActionResult List(int WeddingId)
        {
            try
            {
                var db = new DB.WPlanningDBEntities();
                var lista = db.Activity.Where(x => x.WeddingId == WeddingId);
                List<ListActivitiesDataModel> listaNueva = new List<ListActivitiesDataModel>();
                foreach( var activity in lista)
                {
                    listaNueva.Add(new ListActivitiesDataModel
                    {
                        Id = activity.WeddingId,
                        Name = activity.ActivityName,
                        DateYear = activity.Date.Year,
                        DateMonth = activity.Date.Month,
                        DateDay = activity.Date.Day,
                        Done = activity.Done,
                        WeddingId = activity.WeddingId
                    });
                }

                db.SaveChanges();
                return Json(listaNueva,JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                mostrarMensajeException(ex);
                return new HttpStatusCodeResult(500);
            }
        }

        public class AddActivityDataModel
        {
            public int WeddingId { get; set; }
            public string Name { get; set; }
            public int DateYear { get; set; }
            public int DateMonth { get; set; }
            public int DateDay { get; set; }
        }

        public class ListActivitiesDataModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int DateYear { get; set; }
            public int DateMonth { get; set; }
            public int DateDay { get; set; }
            public bool Done { get; set; }
            public int WeddingId { get; set; }
        }
    }
}