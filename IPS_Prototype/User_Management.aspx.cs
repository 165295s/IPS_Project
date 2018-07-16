using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IPS_Prototype.DAL;

namespace IPS_Prototype
{
    public partial class User_Management : System.Web.UI.Page
    {
        DatabaseDAO db = new DatabaseDAO();
        protected void Page_Load(object sender, EventArgs e)
        {
            //Bind data to table retrieved from DatabaseDAO method name GetUsers on Page Load
            UserTable.DataSource = db.GetUsers();
            UserTable.DataBind();
            UserTable.HeaderRow.TableSection = TableRowSection.TableHeader;

            //Whenever user redirects back to this page ensure sessions are cleared so as not to override the next session values on edit
            Session.Add("UserID", null);
        }

        protected void UserTable_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //Get values from rows in table where edit button event is triggered
            string name = UserTable.Rows[e.NewEditIndex].Cells[0].Text;
            string email = UserTable.Rows[e.NewEditIndex].Cells[1].Text;
            string role = UserTable.Rows[e.NewEditIndex].Cells[2].Text;
            string userid = UserTable.Rows[e.NewEditIndex].Cells[3].Text;

            //Store values taken from table in session and redirect to User_Add.aspx
            Session.Add("UserID", userid);
            Response.Redirect("User_Add.aspx");
        }

        protected void UserTable_RowDeleting(object sender,GridViewDeleteEventArgs e)
        {
            //Deletes the row which the delete button event is triggered on
            DatabaseDAO dao = new DatabaseDAO();
            int check = dao.DeleteUser(UserTable.Rows[e.RowIndex].Cells[3].Text);

            //Check for exception errors in Database DAO
            if (check == 0)
            {
                //If error display error message
                ScriptManager.RegisterStartupScript(Page, GetType(), "AlertFailureDisplay", "displayFailure();", true);
            }
            else
            {
                //If no error refresh page
                Response.Redirect(Request.RawUrl);
            }
        }

        protected void UserAddClick(object sender, EventArgs e)
        {
            //Add UserID to session and redirect to User_Add.aspx
            Session.Add("UserID", null);
            Response.Redirect("User_Add.aspx");
        }
    }
}