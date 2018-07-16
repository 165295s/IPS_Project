using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using IPS_Prototype.RetrieveClass;
using IPS_Prototype.DAL;
using IPS_Prototype.Class;
using IPS_Prototype.Model;

namespace IPS_Prototype
{
    public partial class Member_MemberRenewalInd : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string IndividualID = (string)(Session["IndividualID"]);
                DALMembership dao = new DALMembership();
                MemberInfo member = new MemberInfo();
                member = dao.GetIndividualData(IndividualID);
                if (member.DonorTier != null)
                {
                    // if show date from gv to tb
                    // datetime.Value = (member.ExpiryDate).ToShortDateString();
                    datetime.Value = DateTime.Now.AddYears(1).ToString("dd/MM/yyyy");
                    paymentreceiveddate.Value = DateTime.Now.ToString("dd/MM/yyyy");
                    individualName.Value = member.IndividualName;
                    individualID.Value = IndividualID;
                    UserRenewalHeader.InnerText = "Renewal For " + individualName.Value;
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
                int createindcheck = 0;
                IndividualContribution individualContribution = new IndividualContribution();
                individualContribution.Amoount = decimal.Parse(memfee.Value);
                individualContribution.PaymentReceivedDate = DateTime.Parse(paymentreceiveddate.Value);
                individualContribution.PaymentPurpose= "Membership";
                individualContribution.PaymentMode = PaymentMode.Value;
                individualContribution.ExpiryDate = datetime.Value;
                individualContribution.DonorTier = Donor_Tier.Value;
                individualContribution.TotalAmount = decimal.Parse(memfee.Value);
                individualContribution.PaymentDetails = remarks.Value;
                individualContribution.CreatedDate = DateTime.Now;
                individualContribution.ContributionCreatedDate = DateTime.Now;
                individualContribution.ContributionDate = DateTime.Now;
                individualContribution.PersonId = int.Parse(Session["PersonId"].ToString());
                individualContribution.Status = "Full";
                if (cbInstallment.Checked)
                {
                    individualContribution.Amoount = decimal.Parse(txtInstallment.Value);
                    individualContribution.Status = "Installment";
                }
                if (!string.IsNullOrEmpty(individualContribution.DonorTier) && !string.IsNullOrEmpty(individualContribution.PaymentMode))
                {
                    if (individualContribution.DonorTier == "Friend of IPS")
                    {
                        individualContribution.ExpiryDate = datetime.Value;
                    }
                    else if (individualContribution.DonorTier == "Lifetime Friend of IPS" || individualContribution.DonorTier == "Lifetime Benefactor of IPS"
                      || individualContribution.DonorTier == "Lifetime Patron of IPS")
                    {
                       datetime.Value= individualContribution.ExpiryDate = "NA";
                    }

                    DALMembership mem = new DALMembership();
                    // individual
                    int updateindcheck = mem.EditIndividual(individualContribution.DonorTier, individualContribution.ExpiryDate, individualID.Value, DateTime.Now);
                                   
                   createindcheck = mem.InsertIndividualContribution(individualContribution);
                    //decimal amt, string paymentreceiveddate, DateTime createddate, string paymentdets, string paymentmode, string paymentpurpose, decimal totalamt, DateTime contridate
                    if (createindcheck == 0)
                    {
                        //ScriptManager.RegisterStartupScript(Page, GetType(), "AlertDisplay", "displaySuccess('Successfully Renewed!');", true);
                        ScriptManager.RegisterStartupScript(Page, GetType(), "AlertDisplay", "showControlsAfterPostBackChecked();", true);
                    }
                }
                else
                {
                    //ScriptManager.RegisterStartupScript(Page, GetType(), "AlertFailureDisplay", "displayFailure();", true);
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
                    int indcheck = mem.EditIndividual(donortier, expiry_date, individualID.Value, DateTime.Now); 
                    if (indcheck > 0)
                    {
                        // ScriptManager.RegisterStartupScript(Page, GetType(), "AlertDisplay", "displaySuccess('Successfully Renewed!');", true);
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


