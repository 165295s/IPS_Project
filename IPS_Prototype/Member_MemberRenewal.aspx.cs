using IPS_Prototype.DAL;
using IPS_Prototype.RetrieveClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IPS_Prototype
{
    public partial class Member_MemberRenewal : System.Web.UI.Page
    {
        DALMembership mem = new DALMembership();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                gvOrg.DataSource = mem.getAllMembershipRenewalDetailOrg();
                gvOrg.DataBind();
                gvOrg.HeaderRow.TableSection = TableRowSection.TableHeader;
                Session.Add("OrganisationID", null);

                gvPerson.DataSource = mem.getAllMembershipRenewalDetailPerson();
                gvPerson.DataBind();
                gvPerson.UseAccessibleHeader = true;
                gvPerson.HeaderRow.TableSection = TableRowSection.TableHeader;
                Session.Add("IndividualID", null);
                Session.Add("ContributionID", null);
            }
            else
            {
                //If error, display failure message
                ScriptManager.RegisterStartupScript(Page, GetType(), "AlertFailureDisplay", "displayFailure();", true);
            }
        }

        protected void gvPerson_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //Get values from rows in table where edit button event is triggered
            string indname = gvPerson.Rows[e.NewEditIndex].Cells[1].Text;
            string email = gvPerson.Rows[e.NewEditIndex].Cells[2].Text;
            string expirydate = gvPerson.Rows[e.NewEditIndex].Cells[4].Text;
            string personId = gvPerson.Rows[e.NewEditIndex].Cells[0].Text;
            string contributionId = gvPerson.Rows[e.NewEditIndex].Cells[6].Text;

            //Store values taken from table in session and redirect to User_Add.aspx
            Session.Add("ContributionID", contributionId);
            Session.Add("IndividualID", email);
            Session.Add("PersonId", personId);
            Response.Redirect("Member_MemberRenewalInd.aspx");
        }

        protected void gvOrg_RowEditing(object sender, GridViewEditEventArgs e)
        {
            string name = gvOrg.Rows[e.NewEditIndex].Cells[0].Text;
            string expirydate = gvOrg.Rows[e.NewEditIndex].Cells[2].Text;
            string orgId = gvOrg.Rows[e.NewEditIndex].Cells[3].Text;
            Session.Add("OrganisationID", name);
            Session.Add("OrgId", orgId);
            Response.Redirect("Member_MemberRenewalOrg.aspx");
        }

        protected string HasMemberPaid(int personId)
        {
            return mem.HasMemberPaid(personId) ? "true" : "false";
        }
        protected string HasMemberPaidOrg(int orgId)
        {
            return mem.HasMemberPaidOrg(orgId) ? "true" : "false";
        }
        //HasMemberPaidOrg

        protected void btnTER_Click(object sender, EventArgs e)
        {
            //Add IndividualID to session and redirect to Member_MemberTerInd.aspx
            GridViewRow row = (GridViewRow)((Button)sender).NamingContainer;
            string indname = row.Cells[1].Text;
            string expirydate = row.Cells[4].Text;

            string email = row.Cells[2].Text;
            string contributionId = row.Cells[6].Text;
            string personId = row.Cells[0].Text;

            Session.Add("IndividualID", email);
            Session.Add("ContributionID", contributionId);
            Response.Redirect("Member_MemberTerInd.aspx");
        }
        protected void btnTEROrg_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)((Button)sender).NamingContainer;
            string name = row.Cells[0].Text;
            string expirydate = row.Cells[2].Text;
            Session.Add("OrganisationID", name);
            Response.Redirect("Member_MemberTerOrg.aspx");
        }

        protected void gvPerson_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            int statuscolumnIndex = 5; // check in your gridview
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string status = DataBinder.Eval(e.Row.DataItem, "CONTRIBUTION_STATUS").ToString();
                if (status == "Full")
                    e.Row.Cells[statuscolumnIndex].ForeColor = System.Drawing.Color.SeaGreen;
                else if (status == "Installment")
                {
                    e.Row.Cells[statuscolumnIndex].ForeColor = System.Drawing.Color.DarkOrange;
                }
            }
        }

        protected void gvOrg_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            int statuscolumnIndex = 4; // check in your gridview
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string status = DataBinder.Eval(e.Row.DataItem, "CONTRIBUTION_STATUS").ToString();
                if (status == "Full")
                    e.Row.Cells[statuscolumnIndex].ForeColor = System.Drawing.Color.SeaGreen;
                else if (status == "Installment")
                {
                    e.Row.Cells[statuscolumnIndex].ForeColor = System.Drawing.Color.DarkOrange;
                }
            }
        }
    }
}
