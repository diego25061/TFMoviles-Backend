using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WPlanningAPI.Models.DB;

namespace WPlanningAPI.Models
{
    public class WPlanner
    {
        public string Address { get; set; }
        public WPlannerCompany Company { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public string User { get; set; }
        public Person Person { get; set; }

        public static WPlanner getFromDb(int id)
        {
            var db = new WPlanningDBEntities();
            DB.WPlanner dbPlanner = db.WPlanner.Where(w => w.WPlannerId == id).First(); ;

            WPlanner planner = new WPlanner {
                Address = dbPlanner.Address,
                //Company = WPlannerCompany.buildFromDb(dbPlanner.WPlannerCompany),
                Password = dbPlanner.Password,
                //Person = Person.buildFromDb(dbPlanner.Person),
                Phone = dbPlanner.Telephone,
                User = dbPlanner.Usr
            };

            if (dbPlanner.WPlannerCompany != null)
                planner.Company = WPlannerCompany.buildFromDb(dbPlanner.WPlannerCompany);
            if (dbPlanner.Person != null)
                planner.Person = Person.buildFromDb(dbPlanner.Person);

            return planner;
        }
    }
}