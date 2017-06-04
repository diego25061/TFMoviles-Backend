using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WPlanningAPI.Models.DB;

namespace WPlanningAPI.Models
{
    public class SubscriptionType
    {
        public int Months { get; set; }
        public int WPlannerQuantity { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public double Cost { get; set; }


        public static List<SubscriptionType> getAllFromDb(){
            var db = new WPlanningDBEntities();
            List<DB.SubscriptionType> DBSubscriptionsList = db.SubscriptionType.ToList<DB.SubscriptionType>();
            List<SubscriptionType> lista = new List<SubscriptionType>();
            db.Dispose();
            foreach (var sub in DBSubscriptionsList){
                lista.Add(new SubscriptionType { Months = sub.Months , Cost = sub.Cost , Description = sub.Description , Name = sub.Name, WPlannerQuantity = sub.QuantityWPlanners });
            }
            return lista;
        }

        public static SubscriptionType buildFromDb(DB.SubscriptionType sub)
        {
            return new SubscriptionType { Months = sub.Months, Cost = sub.Cost, Description = sub.Description, Name = sub.Name, WPlannerQuantity = sub.QuantityWPlanners };
        }
    }
}