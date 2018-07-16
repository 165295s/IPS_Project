using IPS_Prototype.DAL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IPS_Prototype.Class;

namespace IPS_Prototype
{
    public partial class IPS_Vertical : System.Web.UI.MasterPage
    {
        
        string currentPageName = "";
        bool hasAccess = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            try { 
            
            //Retrieves session from Login.aspx
            string fullname = (string)(Session["name"]);
            string role = (string)(Session["role"]);
            string Email = (string)(Session["email"]);

            //Display retrieved values from session into relevant textbox
            name.Text = fullname;
            full_name.Text = fullname;
            user_level.Text = role;
            email.Text = Email;

            //Checks the role to determine what page current user can access and display it on MasterPage sidebar based on the sitemap
            if(role == "SuperAdmin")
            {
                SiteMapDataSource1.SiteMapProvider = "Admin";
            }
            else if(role == "Executive")
            {
                SiteMapDataSource1.SiteMapProvider = "Staff";
            }

            // Every time user visits a page
            // Get his current page name
            // And make sure he has access right
            currentPageName = GetCurrentPageName();
            DatabaseDAO userObj = new DatabaseDAO();
            hasAccess = userObj.AccessRight(role, currentPageName);
            //hasAccess = accessRight(RoleAction.roles, currentPageName);
            if (hasAccess == false)
            {
                //CATCH ERRORS INTO ERROR LOG
                ErrorLog.WriteErrorLog(fullname + " tried to access a prohibited site: " + currentPageName + " \r\nWith their Administrative Status of: " + role + ".");


                // DO NOT allow user to access this page
                html.Attributes.CssStyle.Add("display", "none");
                ScriptManager.RegisterStartupScript(Page, GetType(), "AlertUnauthorised", "alert('Unathorised Access!');window.location.href='Login.aspx';", true);
            }
            }
            catch(Exception ex)
            {
                //Catch error and write to ErrorLog.txt
                ErrorLog.WriteErrorLog(ex.ToString());
            }

        }


        //Method to get the currentPage URL
        public string GetCurrentPageName()
        {
            string urlPath = Request.Url.AbsolutePath;
            FileInfo fileInfo = new FileInfo(urlPath);
            string pageName = fileInfo.Name;
            return pageName;
        }

        
    }
}