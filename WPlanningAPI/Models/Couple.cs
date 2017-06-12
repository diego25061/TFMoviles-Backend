using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WPlanningAPI.Models;

namespace WPlanningAPI.Models
{
    public class Couple
    {

        public string LastName { get; set; }
        public string SharedEmail { get; set; }
        public string Usr { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Status { get; set; }


        public static Couple buildFromDB(DB.Couple dbCouple) {

            Couple couple = new Couple
            {
                LastName = dbCouple.LastNameIdentifier,
                SharedEmail = dbCouple.SharedEmail,
                Usr = dbCouple.Usr,
                Password = dbCouple.Password
            };
            return couple;
        }

    }
}