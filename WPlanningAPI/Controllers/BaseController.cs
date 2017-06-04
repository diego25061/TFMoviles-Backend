using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WPlanningAPI.Controllers
{
    public class BaseController: Controller 
    {

        protected bool isEmptyOrSpaces(string s)
        {
            if (s == null)
                return true;
            bool onlySpaces = true;
            foreach (char c in s)
            {
                if (c != 20) onlySpaces = false;
            }
            return (s.Equals("") || onlySpaces);
        }
    }

}