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
    public partial class Member_MemberRenewalOrg : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string OrganisationID = (string)(Session["OrganisationID"]);
                DALMembership dao = new DALMembership();
                MemberInfo member = new MemberInfo();
                member = dao.GetOrgData(OrganisationID);
                if (member.DonorTier != null)
                {
                    // if show date from gv to tb
                    // datetime.Value = (member.ExpiryDate).ToShortDateString();
                    datetime.Value = DateTime.Now.AddYears(1).ToString("dd/MM/yyyy");
                    paymentreceiveddate.Value = DateTime.Now.ToString("dd/MM/yyyy");
                    organisationID.Value = OrganisationID;
                    UserRenewalHeader.InnerText = "Renewal For " + organisationID.Value;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "AlertFailureDisplay", "displayFailure();", true);
                }

            }
        }

        protected void Submit_Renewal(object sender, EventArgs e)
        {
            if (cbpaid.Checked)
            {
                int createorgcheck = 0;
                var expiry_date = datetime.Value;
                var donortier = Donor_Tier.Value;
                var dets = memdets.Value;
                var paymentmode = PaymentMode.Value;
                var paymentdate = paymentreceiveddate.Value;

                dets = "Membership";
                if (!string.IsNullOrEmpty(donortier) && !string.IsNullOrEmpty(paymentmode))
                {
                    if (donortier == "Friend of IPS")
                    {
                        expiry_date = datetime.Value;
                    }
                    else if (donortier == "Lifetime Friend of IPS" || donortier == "Lifetime Benefactor of IPS"
                      || donortier == "Lifetime Patron of IPS")
                    {
                        expiry_date = "NA";
                        datetime.Value = expiry_date;
                    }

                    DALMembership mem = new DALMembership();
                    // individual
                    int updateorgcheck = mem.EditOrg(donortier, expiry_date, organisationID.Value, DateTime.Now);
                    createorgcheck = mem.InsertOrgContribution(organisationID.Value, decimal.Parse(memfee.Value), paymentdate, DateTime.Now, dets, PaymentMode.Value, remarks.Value);

                    if (createorgcheck == 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "AlertDisplay", "showControlsAfterPostBackChecked();", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "AlertFailureDisplay", "showControlsAfterPostBackCheckedFailure();", true);
                }

            }
            else
            {
                //Update Membership Table if unchecked
                var expiry_date = datetime.Value;
                var donortier = Donor_Tier.Value;
                if (!string.IsNullOrEmpty(donortier))
                {
                    if (donortier == "Friend of IPS")
                    {
                        expiry_date = datetime.Value;
                    }
                    else if (donortier == "Lifetime Friend of IPS" || donortier == "Lifetime Benefactor of IPS"
                      || donortier == "Lifetime Patron of IPS")
                    {
                        expiry_date = "NA";
                        datetime.Value = expiry_date;
                    }

                    DALMembership mem = new DALMembership();

                    //individual
                    int orgcheck = mem.EditOrg(donortier, expiry_date, organisationID.Value, DateTime.Now);
                    if (orgcheck > 0)
                    {
                        //    ScriptManager.RegisterStartupScript(Page, GetType(), "AlertDisplay", "displaySuccess('Successfully Renewed!');", true);
                        ScriptManager.RegisterStartupScript(Page, GetType(), "AlertDisplay", "showControlsAfterPostBackUnchecked();", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "AlertFailureDisplay", "displayFailure();", true);
                }
            }

        }


    }
}