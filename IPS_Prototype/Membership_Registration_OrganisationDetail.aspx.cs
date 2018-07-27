using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IPS_Prototype.DAL;
using System.Data;
using IPS_Prototype.Class;
using System.Text.RegularExpressions;
using IPS_Prototype.RetrieveClass;

namespace IPS_Prototype
{
    public partial class Membership_Registration_OrganizationDetail : System.Web.UI.Page
    {
        private ArrayList orgList;

        MembershipDAO db = new MembershipDAO();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Person"] != null)
            {
                orgList = (ArrayList)Session["Person"];
            }
            else {
                Session["Person"] = null;




            }

            if (Session["EDIT_ORG_ID"] != null)
            {
                Session["Person"] = null;
                OrgInfo o1 = new OrgInfo();
                MembershipDAO dalMem = new MembershipDAO();
                string org_id = Session["EDIT_ORG_ID"].ToString();
                o1 = dalMem.getAllOrgInfo(org_id);

                txtOrgNameField.Value = o1.orgName.ToString();
                txtMailAddrLine1.Value = o1.mailLine1.ToString();
                txtMailAddrLine2.Value = o1.mailLine2.ToString();
                txtCity.Value = o1.city.ToString();
                txtPostalCode.Value = o1.postalCode.ToString();
                txtTelephone.Value = o1.telNo.ToString();
                txtOffice.Value = o1.officeNo.ToString();
                txtWebsiteURL.Value = o1.websiteURL.ToString();
                txtbDesc.Value = o1.busDesc.ToString();
                pointOfContact.Value = o1.PoC.ToString();
                txtnotes.Value = o1.notes.ToString();
                txtUEN.Value = o1.uen.ToString();

            }



        }


        protected void button_next(object sender, EventArgs e)
        {
            bool flag = false;
            //STORE RESPONSE INTO A SESSION


            if (string.IsNullOrEmpty(txtOrgNameField.Value.ToString()) || txtOrgNameField.Value.Trim().ToString() == "")
            {
                flag = false;
                ScriptManager.RegisterStartupScript(Page, GetType(), "AlertFailureDisplay", "displayFailureMsg('Please Organisaton Name Field.')", true);

            }
            else
            {
                orgList.Add(txtOrgNameField.Value.ToString());
                flag = true;

            }
            if (string.IsNullOrEmpty(txtMailAddrLine1.Value.ToString()) || txtMailAddrLine1.Value.Trim().ToString() == "")
            {
                flag = false;
                ScriptManager.RegisterStartupScript(Page, GetType(), "AlertFailureDisplay", "displayFailureMsg('Please Mailing Address Line 1 Field.')", true);

            }
            else
            {
                orgList.Add(txtMailAddrLine1.Value.ToString());
                orgList.Add(txtMailAddrLine2.Value.ToString());
                flag = true;

            }
            if (string.IsNullOrEmpty(txtCity.Value.ToString()) || txtCity.Value.Trim().ToString() == "")
            {
                flag = false;
                ScriptManager.RegisterStartupScript(Page, GetType(), "AlertFailureDisplay", "displayFailureMsg('Please City Field.')", true);
            }
            else
            {
                orgList.Add(txtCity.Value.ToString());
                flag = true;

            }
            if (string.IsNullOrEmpty(txtPostalCode.Value.ToString()) || txtPostalCode.Value.Trim().ToString() == "")
            {
                flag = false;
                ScriptManager.RegisterStartupScript(Page, GetType(), "AlertFailureDisplay", "displayFailureMsg('Please Postal Code Field.')", true);
            }
            else
            {
                orgList.Add(txtPostalCode.Value.ToString());
                flag = true;

            }
            if (string.IsNullOrEmpty(txtTelephone.Value.ToString()) || txtTelephone.Value.Trim().ToString() == "")
            {
                flag = false;
                ScriptManager.RegisterStartupScript(Page, GetType(), "AlertFailureDisplay", "displayFailureMsg('Please Telephone Number Field.')", true);
            }
            else
            {
                orgList.Add(txtTelephone.Value.ToString());
                flag = true;

            }
            if (string.IsNullOrEmpty(txtOffice.Value.ToString()) || txtOffice.Value.Trim().ToString() == "")
            {
                flag = false;
                ScriptManager.RegisterStartupScript(Page, GetType(), "AlertFailureDisplay", "displayFailureMsg('Please Office Number Field.')", true);

            }
            else
            {
                orgList.Add(txtOffice.Value.ToString());
                flag = true;

            }
            if (string.IsNullOrEmpty(txtWebsiteURL.Value.ToString()) || txtWebsiteURL.Value.Trim().ToString() == "")
            {
                flag = false;
                ScriptManager.RegisterStartupScript(Page, GetType(), "AlertFailureDisplay", "displayFailureMsg('Please Website URL Field.')", true);
            }
            else
            {
                if (IsUrlValid(txtWebsiteURL.Value.ToString()).Equals(true))
                {
                    orgList.Add(txtWebsiteURL.Value.ToString());
                    flag = true;
                }
                else
                {
                    flag = false;
                    ScriptManager.RegisterStartupScript(Page, GetType(), "AlertFailureDisplay", "displayFailureMsg('Please Website URL Field. (URL not valid format)')", true);

                }


            }
            if (string.IsNullOrEmpty(pointOfContact.Value.ToString()) || pointOfContact.Value.Trim().ToString() == "")
            {
                flag = false;
                ScriptManager.RegisterStartupScript(Page, GetType(), "AlertFailureDisplay", "displayFailureMsg('Please Point of Contact Field.')", true);
            }
            else
            {
                orgList.Add(pointOfContact.Value.ToString());
                orgList.Add(txtbDesc.Value.ToString());
                flag = true;

            }
            if (string.IsNullOrEmpty(txtnotes.Value.ToString()) || txtnotes.Value.Trim().ToString() == "")
            {
                flag = false;
                ScriptManager.RegisterStartupScript(Page, GetType(), "AlertFailureDisplay", "displayFailureMsg('Please Notes Field.')", true);
            }
            else
            {
                orgList.Add(txtnotes.Value.ToString());
                flag = true;

            }
            if (string.IsNullOrEmpty(txtUEN.Value.ToString()) || txtUEN.Value.Trim().ToString() == "")
            {
                flag = false;
                ScriptManager.RegisterStartupScript(Page, GetType(), "AlertFailureDisplay", "displayFailureMsg('Please UEN Field.')", true);
            }
            else
            {
                if (IsUENValid(txtUEN.Value.ToString()).Equals(true))
                {
                    orgList.Add(txtUEN.Value.ToString());
                    flag = true;
                }
                else
                {
                    flag = false;
                    ScriptManager.RegisterStartupScript(Page, GetType(), "AlertFailureDisplay", "displayFailureMsg('UEN is not valid.')", true);

                }
                

            }

            if (flag != false)
            {
                int check = 0;
                try
                {
                    check = db.addOrg(orgList);
                    if (check == 1 || check == 2)
                    {
                        Response.Redirect("Membership_Registration_CorperateAssociateRepresentative.aspx");

                        ScriptManager.RegisterStartupScript(Page, GetType(), "AlertDisplay", "displaySuccess('Successfully Created New Org: " + txtOrgNameField.Value + "');", true);


                    }
                    else if (check == 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "AlertFailureDisplay", "displayFailure();", true);
                    }


                }
                catch (Exception ex)
                {
                    ErrorLog.WriteErrorLog(ex.ToString());
                    ScriptManager.RegisterStartupScript(Page, GetType(), "AlertFailureDisplay", "displayFailure();", true);


                }

            }
            else
            {
                //error message
            }

        }
        private bool IsUrlValid(string url)
        {


            string pattern = @"(http[s]?:\/\/|[a-z]*\.[a-z]{3}\.[a-z]{2})([a-z]*\.[a-z]{3})|([a-z]*\.[a-z]*\.[a-z]{3}\.[a-z]{2})|([a-z]+\.[a-z]{3})";
            Regex reg = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            return reg.IsMatch(url);
        }
        private bool IsUENValid(string uen) {
            string pattern = @"([Uu])+\d{9,10}";
            Regex reg = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            return reg.IsMatch(uen);

        }
    }

}
