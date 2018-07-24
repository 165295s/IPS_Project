using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace IPS_Prototype.Class
{
    public class CommonMethods
    {
        public static void ViewAlert(Page CurrentPage, string msg, int alerttype)
        {
            if(alerttype == 0)
            {
                CurrentPage.ClientScript.RegisterStartupScript(CurrentPage.GetType(), "AlertDisplay", "displayFailure('" + msg + "');", true);
            }
            else
            {
                CurrentPage.ClientScript.RegisterStartupScript(CurrentPage.GetType(), "AlertDisplay", "displaySuccess('" + msg + "');", true);
            }
            
        }
    }
}