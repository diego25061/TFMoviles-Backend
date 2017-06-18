using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WPlanningAPI.Controllers
{
    public class OptionController : BaseController
    {

        [HttpPost]
        //[Route("Catalogue/{Id}/Option")]
        public ActionResult Index(AddOptionDataModel addOptionDataModel)
        {
            try
            {
                var db = new DB.WPlanningDBEntities();

                DB.Option option = new DB.Option
                {
                    Chosen = false,
                };

                /*
                if (addOptionDataModel.OptionTypeId == 0)
                {
                */
                option.OptType = new DB.OptType
                {
                    Cost = addOptionDataModel.Cost,
                    Name = addOptionDataModel.Name,
                    Description = addOptionDataModel.Description,
                    ImageLink = addOptionDataModel.ImageLink,
                    
                };

                db.CatalogueXOption.Add(new DB.CatalogueXOption
                {
                    Option = option,
                    CatalogueId = addOptionDataModel.CatalogueId
                });

                /*            
                }            
                else
                option.OptType = db.OptType.First(x => x.OptionTypeId == addOptionDataModel.OptionTypeId);
                */
                db.Option.Add(option);
                db.SaveChanges();
                return Content("" + option.OptionId);

            }
            catch (Exception ex)
            {
                mostrarMensajeException(ex);
                return new HttpStatusCodeResult(500);
            }
        }

        public class AddOptionDataModel
        {
            public int CatalogueId { get; set; }
            //public int OptionTypeId { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public float Cost { get; set; }
            //public bool Chosen { get; set; }
            public string ImageLink { get; set; }

        }
    }
}