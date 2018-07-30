using IPS_Prototype.DAL;
using IPS_Prototype.Model;
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
                member = dao.GetOrganisationDataRenewal(OrganisationID);
                if (member.DonorTier != null)
                {
                    datetime.Value = DateTime.Now.AddYears(1).ToString("dd/MM/yyyy");
                    paymentreceiveddate.Value = DateTime.Now.ToString("dd/MM/yyyy");
                    organisationID.Value = OrganisationID;
                    UserRenewalHeader.InnerText = "Renewal For " + organisationID.Value;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "AlertFailureDisplay", "displayFailure();", true);
                }
                if (member != null && member.MemberId > 0)
                    PopulateControlsIfPartiallyPaidOrg(member.MemberId, OrganisationID);
            }
        }

        private void PopulateControlsIfPartiallyPaidOrg(int memberId, string name)
        {
            DALMembership dao = new DALMembership();
            bool hasPartialPayment = dao.HasMemberPartiallyPaid(memberId);
            if (hasPartialPayment)
            {
                float amountPaid = dao.GetPartialPayment(memberId);
                var member = dao.GetOrganisationDataRenewal(name);
                hdnDonor.Value = member.DonorTier;
                hdnInstallment.Value = amountPaid.ToString();
                hdnExpDate.Value = member.ExpiryDate;
                hdnStatus.Value = "Partial";
                memfee.Disabled = true;
            }
            else
            {
                hdnStatus.Value = "Full";
            }
        }

        protected void Submit_Renewal(object sender, EventArgs e)
        {
            DALMembership mem = new DALMembership();
            if (cbpaid.Checked)
            {
                int createorgcheck = 0;
                IndividualContribution individualContribution = new IndividualContribution();
                individualContribution.PaymentReceivedDate = DateTime.Parse(paymentreceiveddate.Value);
                individualContribution.PaymentPurpose = "Membership";
                individualContribution.PaymentMode = PaymentMode.Value;
                individualContribution.ExpiryDate = datetime.Value;
                individualContribution.DonorTier = Donor_Tier.Value;
                individualContribution.PaymentDetails = remarks.Value;
                individualContribution.CreatedDate = DateTime.Now;
                individualContribution.ContributionCreatedDate = DateTime.Now;
                individualContribution.ContributionDate = DateTime.Now;
                individualContribution.OrgId = int.Parse(Session["OrgId"].ToString());
                individualContribution.Status = "Full";

                if (hdnStatus.Value.Contains("Partial"))
                {
                    if (cbInstallment.Checked)
                    {
                        individualContribution.Amoount = decimal.Parse(txtInstallment.Value);
                        individualContribution.Status = "Installment";
                    }

                    int insertInstallment = 0;
                    insertInstallment = mem.InsertIndividualContributionInstallmentOrganisation(individualContribution);
                    if (insertInstallment > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "AlertDisplay", "showControlsAfterPostBackChecked();", true);
                    }
                }
                else if (memfee.Disabled.Equals(false))
                {
                    if (!string.IsNullOrEmpty(individualContribution.DonorTier) && !string.IsNullOrEmpty(individualContribution.PaymentMode))
                    {
                        individualContribution.Amoount = decimal.Parse(memfee.Value);
                        individualContribution.TotalAmount = decimal.Parse(memfee.Value);
                        
                        if (cbInstallment.Checked)
                        {
                            individualContribution.Amoount = decimal.Parse(txtInstallment.Value);
                            individualContribution.Status = "Installment";
                        }

                        if (individualContribution.DonorTier == "Friend of IPS")
                        {
                            individualContribution.ExpiryDate = datetime.Value;
                        }
                        else if (individualContribution.DonorTier == "Lifetime Friend of IPS" || individualContribution.DonorTier == "Lifetime Benefactor of IPS"
                          || individualContribution.DonorTier == "Lifetime Patron of IPS")
                        {
                            datetime.Value = individualContribution.ExpiryDate = "NA";
                        }
                        int updateorgcheck = mem.EditOrg(individualContribution.DonorTier, individualContribution.ExpiryDate, organisationID.Value, DateTime.Now);
                        createorgcheck = mem.InsertOrganisationContribution(individualContribution);
                        if (createorgcheck > 0)
                        {
                            ScriptManager.RegisterStartupScript(Page, GetType(), "AlertDisplay", "showControlsAfterPostBackChecked();", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "AlertFailureDisplay", "showControlsAfterPostBackCheckedFailure();", true);
                    }
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