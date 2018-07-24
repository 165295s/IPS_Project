using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IPS_Prototype.DAL;

namespace IPS_Prototype
{
    public partial class Events_Add : System.Web.UI.Page
    {
        EventsDAO dao = new EventsDAO();
        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = dao.GetEventCode();
            EventType_ddl.DataSource = dt;
            EventType_ddl.DataTextField = "Code_Desc";
            EventType_ddl.DataValueField = "Code";
            EventType_ddl.DataBind();
        }

        protected void EventAddClick(object sender, EventArgs e)
        {
            string personname = (string)(Session["name"]);
            string paid;
            
            string fundraising;
            if(eventPaid.Checked == true)
            {
                paid = "Y";
            }
            else
            {
                paid = "N";
            }

            if(CheckFund.Checked == true)
            {
                fundraising = "Y";
            }
            else
            {
                fundraising = "N";
            }

            DateTime hdate = DateTime.Parse(datetime.Value);
            DateTime edate = DateTime.Parse(datetimeEnd.Value);

            int result = dao.AddEvent(Event_Input_Name.Value, EventType_ddl.Value, Event_Input_Description.Value, Event_Input_Venue.Value, paid, personname, fundraising, hdate, edate, DateTime.Now);

            DataTable dt = new DataTable();
            dt = dao.GetEventID();
            string id = dt.Rows[0]["Event_Id"].ToString();
            if (result == 1)
            {
                if(eventPaid.Checked == true)
                {
                    Response.Redirect("Events_Invite.aspx?Name=" + Event_Input_Name.Value + "&Date=" + datetime.Value + "&ID=" + id + "&Paid=true");
                }
                else
                {
                    Response.Redirect("Events_Invite.aspx?Name=" + Event_Input_Name.Value + "&Date=" + datetime.Value + "&ID=" + id + "&Paid=false");
                }
                
            }
            else
            {

            }
        }
    }
}