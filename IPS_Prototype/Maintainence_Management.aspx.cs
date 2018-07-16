using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IPS_Prototype.DAL;

namespace IPS_Prototype
{
    
    public partial class Maintain_CodeLkUp : System.Web.UI.Page
    {
        DatabaseDAO dao = new DatabaseDAO();
        protected void Page_Load(object sender, EventArgs e)
        {
            //Bind data to table retrieved from DatabaseDAO method name GetLookup on Page Load
            Maintainence_Table.DataSource = dao.GetLookup();
            Maintainence_Table.DataBind();
            Maintainence_Table.HeaderRow.TableSection = TableRowSection.TableHeader;

            //Whenever user redirects back to this page ensure sessions are cleared so as not to override the next session values on edit
            Session.Add("type", null);
            Session.Add("lookup", null);
            Session.Add("codedesc", null);
        }

        protected void AddMaintainenceCode(object sender, EventArgs e)
        {
            //Add type to session and redirect to Maintainece_Add.aspx
            Session.Add("type", hiddenvalue.Value);
            Response.Redirect("Maintainence_Add.aspx");
        }

        protected void CodeTable_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //Deletes the row which the delete button event is triggered on
            DatabaseDAO dao = new DatabaseDAO();
            int check = dao.DeleteCode(Maintainence_Table.Rows[e.RowIndex].Cells[0].Text, Maintainence_Table.Rows[e.RowIndex].Cells[2].Text);

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

        protected void CodeTable_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //Get values from rows in table where edit button event is triggered
            string type = Maintainence_Table.Rows[e.NewEditIndex].Cells[0].Text;
            string lookup = Maintainence_Table.Rows[e.NewEditIndex].Cells[1].Text;
            string codedesc = Maintainence_Table.Rows[e.NewEditIndex].Cells[2].Text;

            //Store values taken from table in session and redirect to Maintainence_Add.aspx
            Session.Add("type", type);
            Session.Add("lookup", lookup);
            Session.Add("codedesc", codedesc);
            Response.Redirect("Maintainence_Add.aspx");
        }
    }
}