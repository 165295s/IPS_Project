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
                member = dao.GetIndividualDataRenewal(IndividualID);
                if (member.DonorTier != null)
                {
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
                if (member != null && member.MemberId > 0)
                    PopulateControlsIfPartiallyPaid(member.MemberId, IndividualID);
            }
        }

        private void PopulateControlsIfPartiallyPaid(int memberId, string emailAddress)
        {
            DALMembership dao = new DALMembership();
            bool hasPartialPayment = dao.HasMemberPartiallyPaid(memberId);
            if (hasPartialPayment)
            {
                int  contributionId= Convert.ToInt32(Session["ContributionID"]);
                float amountPaid = dao.GetPartialPayment(contributionId);
                var member = dao.GetIndividualDataRenewal(emailAddress);
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
                int createindcheck = 0;
                IndividualContribution individualContribution = new IndividualContribution();
                individualContribution.PaymentReceivedDate = DateTime.Parse(paymentreceiveddate.Value);
                individualContribution.PaymentPurpose = "Membership";
                individualContribution.PaymentMode = PaymentMode.Value;
                individualContribution.ExpiryDate = datetime.Value;
                individualContribution.DonorTier = Donor_Tier.Value;
                individualContribution.PaymentDetails = remarks.Value;
                individualContribution.CreatedDate = DateTime.Now;
                individualContribution.ContributionCreatedDate = DateTime.Now;
                individualContribution.ContributionId = (string)(Session["ContributionID"]);
                individualContribution.ContributionDate = DateTime.Now;
                individualContribution.PersonId = int.Parse(Session["PersonId"].ToString());
                individualContribution.Status = "Full";

                if (hdnStatus.Value.Contains("Partial"))
                {
                    if (!string.IsNullOrEmpty(individualContribution.PaymentMode))
                    {
                        if (cbInstallment.Checked)
                        {
                            individualContribution.Amoount = decimal.Parse(txtInstallment.Value);
                            individualContribution.Status = "Installment";
                        }

                        int insertInstallment = 0;
                        insertInstallment = mem.InsertIndividualContributionInstallment(individualContribution);
                        if (insertInstallment > 0)
                        {
                            ScriptManager.RegisterStartupScript(Page, GetType(), "AlertDisplay", "showControlsAfterPostBackChecked();", true);

                            DALMembership dao = new DALMembership();
                                int contributionId = Convert.ToInt32(Session["ContributionID"]);
                                float amountPaid = dao.GetPartialPayment(contributionId);
                                hdnInstallment.Value = amountPaid.ToString();
                                hdnStatus.Value = "Partial";
                                memfee.Disabled = true;
                                txtInstallment.Value = amountPaid.ToString();                         
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "AlertFailureDisplay", "showControlsAfterPostBackCheckedInstallmentFailure();", true);
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
                        int updateindcheck = mem.EditIndividual(individualContribution.DonorTier, individualContribution.ExpiryDate, individualID.Value, DateTime.Now);
                        createindcheck = mem.InsertIndividualContribution(individualContribution);
                        if (createindcheck > 0)
                        {
                            ScriptManager.RegisterStartupScript(Page, GetType(), "AlertDisplay", "showControlsAfterPostBackCheckedNoInstallment();", true);
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
                    //individual
                    int indcheck = mem.EditIndividual(donortier, expiry_date, individualID.Value, DateTime.Now);
                    if (indcheck > 0)
                    {
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



