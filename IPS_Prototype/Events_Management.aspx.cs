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
    public partial class Events_Management : System.Web.UI.Page
    {
        EventsDAO dao = new EventsDAO();
        protected void Page_Load(object sender, EventArgs e)
        {

            EventTable.DataSource = dao.GetEvents();
            EventTable.DataBind();
            EventTable.HeaderRow.TableSection = TableRowSection.TableHeader;

        }

        protected void EventsAddClick(object sender, EventArgs e)
        {
            Response.Redirect("Events_Add.aspx");
        }

        protected void GuestListClick(object sender, EventArgs e)
        {

            LinkButton btn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            string name = EventTable.Rows[row.RowIndex].Cells[0].Text;
            string date = EventTable.Rows[row.RowIndex].Cells[1].Text;
            DataTable dt = new DataTable();
            dt = dao.GetSpecificEventID(name, Convert.ToDateTime(date));
            string id = dt.Rows[0]["EVENT_ID"].ToString();
            Response.Redirect("Events_GuestList.aspx?ID=" + id + "&Name=" + name + "&Date=" + date);
        }

        protected void AttendanceClick(object sender, EventArgs e)
        {
            Response.Redirect("Attendance.aspx");
        }

        protected void ContributionClick(object sender, EventArgs e)
        {
            Response.Redirect("Contribution.aspx");
        }

        protected void InviteClick(object sender, EventArgs e)
        {
            string finalpaid = "";
            LinkButton btn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            string name = EventTable.Rows[row.RowIndex].Cells[0].Text;
            string date = EventTable.Rows[row.RowIndex].Cells[1].Text;
            DataTable dt = new DataTable();
            dt = dao.GetSpecificEventPaid(name, Convert.ToDateTime(date));
            string paid = dt.Rows[0]["PAID_EVENT"].ToString();

            DataTable dt1 = new DataTable();
            dt1 = dao.GetSpecificEventID(name, Convert.ToDateTime(date));
            string id = dt1.Rows[0]["EVENT_ID"].ToString();

            if (paid == "Y")
            {
                finalpaid = "true";
            }
            else
            {
                finalpaid = "false";
            }
            Response.Redirect("Events_Invite.aspx?Name=" + name + "&Date=" + date + "&ID=" + id + "&Paid=" + finalpaid);
        }

     
    }
}