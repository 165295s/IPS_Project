using IPS_Prototype.DAL;
using IPS_Prototype.RetrieveClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IPS_Prototype
{
    public partial class Fundraising_Management : System.Web.UI.Page
    {
        DALFundraising fundraising = new DALFundraising();

        protected void Page_Load(object sender, EventArgs e)
        {
            //getAllPerspectives
            if (!IsPostBack)
            {
                bindDonors();
            }
            else
            {
                //If error, display failure message
                ScriptManager.RegisterStartupScript(Page, GetType(), "AlertFailureDisplay", "displayFailure();", true);
            }
        }

        private void bindDonors()
        {
            gvDonors.DataSource = fundraising.getAllDonors();
            gvDonors.DataBind();
            gvDonors.HeaderRow.TableSection = TableRowSection.TableHeader;
        }

        //When user clicks select btn
        protected void gvDonors_RowEditing(object sender, GridViewEditEventArgs e)
        {
            EventInfo fr = new EventInfo();

            var strIndID = gvDonors.Rows[e.NewEditIndex].Cells[0].Text;
            int indID;

            var strOrgID = gvDonors.Rows[e.NewEditIndex].Cells[1].Text;
            int orgID;

            var strProsID = gvDonors.Rows[e.NewEditIndex].Cells[4].Text;
            int prosID;
            if (int.TryParse(strProsID, out prosID) && (!string.IsNullOrEmpty(strProsID)))
            {
                int donationID = int.Parse(gvDonors.Rows[e.NewEditIndex].Cells[3].Text);

                Session["check"] = "Select";
                Session["DonorProsEdit"] = prosID;
                Session["DonorProsEditID"] = donationID;
                Session["DonorPros"] = null;
                Response.Redirect("Fundraising_AddDonations.aspx");
            }
            else if (int.TryParse(strOrgID, out orgID))
            {
                //   int orgID = int.Parse(gvDonors.Rows[e.NewEditIndex].Cells[1].Text);
                string orgName = gvDonors.Rows[e.NewEditIndex].Cells[2].Text;
                int donationID = int.Parse(gvDonors.Rows[e.NewEditIndex].Cells[3].Text);

                Session["check"] = "Select";
                Session["DonorOrgEdit"] = orgID;
                Session["DonorOrgEditID"] = donationID;
                Session["DonorOrg"] = null;
                Response.Redirect("Fundraising_AddDonations.aspx");
            }
            else if (int.TryParse(strIndID, out indID))
            {
                string indName = gvDonors.Rows[e.NewEditIndex].Cells[2].Text;
                int donationID = int.Parse(gvDonors.Rows[e.NewEditIndex].Cells[3].Text);

                if (indID > 0)
                {
                    Session["check"] = "Select";
                    Session["DonorIndEdit"] = indID;
                    Session["DonorIndEditID"] = donationID;
                    Session["DonorInd"] = null;
                    Response.Redirect("Fundraising_AddDonations.aspx");
                }
            }
        }

        protected void gvDonors_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDonors.PageIndex = e.NewPageIndex;
            this.bindDonors();
        }
    }
}