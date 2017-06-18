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






        [Route("Couple/{Id}/Catalogue")]
        public ActionResult Catalogues(int Id)
        {
            try
            {
                var db = new DB.WPlanningDBEntities();
                //DB.Couple couple = db.Couple.First(c => c.CoupleId == Id);
                var weddingList = db.Wedding.Where(x => x.CoupleId == Id);
                /*
                if (weddingList.Count() == 0)
                    //return HttpNotFound("Couple has no wedding!");
                    return new HttpStatusCodeResult(500, "Given couple has no wedding!");*/

                DB.Wedding wedding = weddingList.First();

                var dbCataloguexWedding = db.CatalogueXWeddin.Where(x => x.WeddingId == wedding.WeddingId);
                List<DB.Catalogue> dbCatalogueList = new List<DB.Catalogue>();

                foreach (var catxWedding in dbCataloguexWedding)
                {
                    dbCatalogueList.Add(catxWedding.Catalogue);
                }

                List<CatalogueDataModel> catalogueList = new List<CatalogueDataModel>();


                foreach (var cat in dbCatalogueList)
                {
                    catalogueList.Add(new CatalogueDataModel
                    {
                        Id = cat.CatalogueId,
                        Title = cat.CatalogueTyp.Title,
                        Description = cat.CatalogueTyp.BriefDescription,
                        ImageLink = cat.CatalogueTyp.BriefDescription,
                        Options = new List<CatalogueDataModel.CatalogueOptionDataModel>()

                    });
                }

                foreach (var cat in catalogueList)
                    foreach (var catalogueXOption in db.CatalogueXOption.Where(x => x.CatalogueId == cat.Id))
                    {
                        DB.Option dbOption = catalogueXOption.Option;
                        cat.Options.Add(new CatalogueDataModel.CatalogueOptionDataModel
                        {
                            OptionId = dbOption.OptionId,
                            Chosen = dbOption.Chosen,
                            Cost = (float)dbOption.OptType.Cost,
                            Name = dbOption.OptType.Name,
                            Description = dbOption.OptType.Description,
                            ImageLink = dbOption.OptType.ImageLink
                        });
                    }

                return Json(catalogueList, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {

                mostrarMensajeException(ex);
                return new HttpStatusCodeResult(500);
            }
        }


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
            catch (Exception ex)
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

                mostrarMensajeException(ex);
                return new HttpStatusCodeResult(500);
                // Console.WriteLine(ex.StackTrace);
                // return HttpNotFound();
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

        public class CatalogueDataModel
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string ImageLink { get; set; }
            public string Description { get; set; }

            public List<CatalogueOptionDataModel> Options;

            public class CatalogueOptionDataModel
            {
                public int OptionId { get; set; }
                public string Name { get; set; }
                public string Description { get; set; }
                public float Cost { get; set; }
                public bool Chosen { get; set; }
                public string ImageLink { get; set; }
            }
        }
    }
}