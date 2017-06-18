using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WPlanningAPI.Controllers
{
    public class BudgetController : BaseController
    {
        // GET: Budget
        [Route("Budget/{weddingId}")]
        public ActionResult Index(int WeddingId)
        {
            try
            {
                var db = new DB.WPlanningDBEntities();
                var listaExpenses = db.Expense.Where(x => x.WeddingId == WeddingId);
                if (listaExpenses == null)
                    return HttpNotFound();

                double totalExpense = 0;
                foreach( var expense in listaExpenses)
                {
                    totalExpense += expense.Price;
                }

                return Content(totalExpense.ToString());

            }
            catch (Exception ex)
            {
                mostrarMensajeException(ex);
                return new HttpStatusCodeResult(500);
            }
        }
    }
}