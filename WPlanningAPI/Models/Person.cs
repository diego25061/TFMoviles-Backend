using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WPlanningAPI.Models
{
    public class Person
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public static Person buildFromDb(DB.Person DbPerson)
        {
            return new Person { Name = DbPerson.FirstName, LastName = DbPerson.LastName, Email = DbPerson.Email, Phone = DbPerson.Phone };
        }
    }
}