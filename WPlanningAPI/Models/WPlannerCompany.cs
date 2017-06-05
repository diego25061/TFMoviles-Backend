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
        public List<WPlanner> WPlannerList { get; set; }

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
            };
            return company;
        }

        public static WPlannerCompany buildFullFromDb(DB.WPlannerCompany DbCompany)
        {
            WPlannerCompany company = buildFromDb(DbCompany);
            company.SubscriptionType = SubscriptionType.buildFromDb(DbCompany.SubscriptionType);

            company.WPlannerList = new List<WPlanner>();
            foreach ( var wplanner in DbCompany.WPlanner.ToList<DB.WPlanner>())
            {
                if(wplanner!=null)
                    company.WPlannerList.Add(WPlanner.buildFromDb(wplanner));
            }
            return company;
        }
        

    }
}