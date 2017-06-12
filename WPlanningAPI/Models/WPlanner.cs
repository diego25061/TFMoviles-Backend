using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WPlanningAPI.DB;

namespace WPlanningAPI.Models
{
    public class WPlanner
    {

        public Person Person { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public WPlannerCompany Company { get; set; }

        

        public static WPlanner buildFromDb(DB.WPlanner dbPlanner)
        {
            var db = new WPlanningDBEntities();
            //DB.WPlanner dbPlanner = db.WPlanner.Where(w => w.WPlannerId == id).First(); ;

            WPlanner planner = new WPlanner {
                Address = dbPlanner.Address,
                //Company = WPlannerCompany.buildFromDb(dbPlanner.WPlannerCompany),
                Password = dbPlanner.Password,
                //Person = Person.buildFromDb(dbPlanner.Person),
                Phone = dbPlanner.Telephone,
                User = dbPlanner.Usr
            };
            /*
            if (dbPlanner.WPlannerCompany != null)
                planner.Company = WPlannerCompany.buildFromDb(dbPlanner.WPlannerCompany);
            if (dbPlanner.Person != null)
                planner.Person = Person.buildFromDb(dbPlanner.Person);
                */
            
            return planner;
        }

        public static WPlanner buildFullFromDb(DB.WPlanner dbPlanner)
        {
            var db = new WPlanningDBEntities();
            //DB.WPlanner dbPlanner = db.WPlanner.Where(w => w.WPlannerId == id).First(); ;

            WPlanner planner = new WPlanner
            {
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
        /*
        public object getJsonObjectNoPass()
        {
            return new { User = "asd" };
        }*/

        /// <summary>
        /// 48-57
        /// 98-121
        /// </summary>
        /// <returns></returns>
        public static string generatePassword()
        {
            char[] pass =  new char[8];
            Random rand = new Random();
            rand.NextDouble();
            for( int i = 0; i < 8; i++)
            {
                pass[i] = (char) (int) (rand.NextDouble() * (121 - 98) + 98);
            }

            string password = new string(pass);
            return password;
        }
    }
}