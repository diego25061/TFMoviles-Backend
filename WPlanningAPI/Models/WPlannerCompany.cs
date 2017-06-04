using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WPlanningAPI.Models
{
    public class WPlannerCompany
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public SubscriptionType SubscriptionType { get; set; }

        public static WPlannerCompany buildFromDb(DB.WPlannerCompany DbCompany)
        {
            WPlannerCompany company = new WPlannerCompany
            {
                Name = DbCompany.CompanyName,
                Address = DbCompany.Address,
                Phone = DbCompany.Telephone,
                Email = DbCompany.Email,
                User = DbCompany.Usr,
                Password = DbCompany.Password,
                SubscriptionType = SubscriptionType.buildFromDb(DbCompany.SubscriptionType)
            };
            return company;
        }

    }
}